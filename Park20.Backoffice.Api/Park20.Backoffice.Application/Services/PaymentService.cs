using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Domain.Payment;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;
using PaymentClient;
using Protos;

namespace Park20.Backoffice.Application.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IParkRepository _parkRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IParkLogRepository _parkLogRepository;
        private readonly IParkyWalletRepository _parkWalletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IParkyCoinsConfigurationRepository _parkyCoinsConfigurationRepository;
        private readonly IConfiguration _configuration;
        public PaymentService(IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository, IConfiguration configuration, IParkRepository parkRepository,
            IVehicleRepository vehicleRepository, IParkLogRepository parkLogRepository, IParkyWalletRepository parkyWalletRepository,
            IUserRepository userRepository, IParkyCoinsConfigurationRepository parkyCoinsConfigurationRepository)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
            _parkRepository = parkRepository;
            _vehicleRepository = vehicleRepository;
            _parkLogRepository = parkLogRepository;
            _parkWalletRepository = parkyWalletRepository;
            _userRepository = userRepository;
            _parkyCoinsConfigurationRepository = parkyCoinsConfigurationRepository;
        }

        #region Make Payment

        private async Task<int> GetCurrentBalance(string userName)
        {

            var parkyWallet = await _parkWalletRepository.GetParkyWalletByUsername(userName);

            return (parkyWallet != null) ? parkyWallet.CurrentBalance : 0;
        }

        private async Task<decimal> PayWithParkyCoins(string licensePlate, decimal totalCost)
        {
            var currencyValue = await _parkyCoinsConfigurationRepository.GetCurrencyValue();

            var currentUser = await _userRepository.GetCustomerByLicensePlate(licensePlate);

            var currentBalance = await GetCurrentBalance(currentUser.Username);

            // totalCost = 411
            // currentBalance = 3100


            var newBalance = (currentBalance * currencyValue - totalCost) < 0 ? 0 : currentBalance - totalCost;

            await _parkWalletRepository.UpdateCurrentBalance(currentUser.ParkyWalletId, newBalance);

            return currentBalance - newBalance;
        }

        public async Task<HibridPayment> MakePayment(string licensePlate, decimal totalCost)
        {
            var token = await GetTokenFromPaymentMethodByLicensePlate(licensePlate);

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    return default;
            //}

            var parkyCoinsSpent = await PayWithParkyCoins(licensePlate, totalCost);

            var missingPayment = totalCost - parkyCoinsSpent;

            bool isSucessfull = missingPayment > 0 ? false : true;

            if (!isSucessfull)
            {
                isSucessfull = await SimulatePayment(token, missingPayment);
            }

            await _invoiceRepository.CreateInvoice(new Invoice(totalCost, isSucessfull), licensePlate);

            return new HibridPayment(parkyCoinsSpent, missingPayment, totalCost, isSucessfull);
        }

        private async Task<string?> GetTokenFromPaymentMethodByLicensePlate(string licensePlate) => await _paymentRepository.GetTokenFromLicensePlate(licensePlate);

        #endregion

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodListFromUser(string username)
        {
            return await _paymentRepository.GetAllFromUser(username);
        }


        public async Task<PaymentMethod?> AddPaymentMethodToUser(PaymentMethod paymentMethod, string username)
        {
            var result = await _paymentRepository.AddPaymentMethod(paymentMethod, username);
            if (result != null)
            {
                return result;
            }
            return default;
        }

        private async Task<double> CalculateTime(ParkLog parkLog)
        {

            if (parkLog == null)
            {
                return 0;
            }

            return (parkLog.EndTime - parkLog.StartTime).TotalMinutes;
        }

        public async Task<decimal> CalculateCost(string parkName, string licensePlate)
        {
            var parkLog = await _parkLogRepository.GetParkLog(licensePlate);

            double totalMinutes = await CalculateTime(parkLog) * 60000;

            if (totalMinutes <= 0)
            {
                return 0;
            }

            // pay with ParkyCoins

            var customer = await _userRepository.GetCustomerByLicensePlate(licensePlate);

            if (customer == null)
            {
                return -1;
            }

            await CreateParkyWalletMovementsForEachHour(customer.ParkyWalletId, totalMinutes, customer.Username);

            Vehicle? vehicle = await _vehicleRepository.GetVehicle(licensePlate);

            if (vehicle == null)
            {
                return 0;
            }


            Park park = await _parkRepository.GetParkByName(parkName);

            Period period = GetLinePriceTable(parkLog.StartTime, parkLog.EndTime, park).Period;

            List<Fraction> orderedFractions = period.Fractions.Where(fraction =>
                            fraction.VehicleType == vehicle.Type).OrderBy(f => f.Order).ToList();

            double sumFractionsTime = 0;
            int index = 0;
            decimal total = 0;

            while (sumFractionsTime < totalMinutes)
            {

                Fraction fractionToAdd = orderedFractions[index];
                total += fractionToAdd.Price;

                sumFractionsTime += fractionToAdd.Minutes.TotalMinutes;
                if (index + 1 < orderedFractions.Count)
                {
                    index++;

                }
            }
            return total;


        }

        private async Task CreateParkyWalletMovementsForEachHour(int parkyWalletId, double totalMinutes, string username)
        {

            var amountForEachHour = await _parkyCoinsConfigurationRepository.GetParkingValue();

            int amount = (int)(totalMinutes / 60) * amountForEachHour;

            if (amount < 0)
            {
                return;
            }

            var currentBalance = await GetCurrentBalance(username);

            await _parkWalletRepository.UpdateCurrentBalance(parkyWalletId, currentBalance + amount);
        }

        private LinePriceTable GetLinePriceTable(DateTime initialDate, DateTime finalDate, Park park)
        {
            foreach (LinePriceTable linePriceTable in park.PriceTable.LinePrices)
            {
                Period period = linePriceTable.Period;

                if (IsInPeriod(initialDate, finalDate, period))
                {
                    return linePriceTable;
                }
            }

            return null;
        }

        private bool IsInPeriod(DateTime initialDate, DateTime finalDate, Period period)
        {
            TimeSpan initialTimeOfDay = initialDate.TimeOfDay;
            TimeSpan finalTimeOfDay = finalDate.TimeOfDay;

            if (period.InitialTime < period.FinalTime)
            {
                // Caso simples: o período vai de um horário para outro no mesmo dia
                return initialTimeOfDay >= period.InitialTime && finalTimeOfDay <= period.FinalTime;
            }
            else
            {
                // Caso mais complexo: o período atravessa a meia-noite para o dia seguinte
                return (initialTimeOfDay >= period.InitialTime || finalTimeOfDay <= period.FinalTime);
            }
        }


        #region Api Calls

        private async Task<bool> SimulatePayment(string token, decimal totalCost)
        {
            var config = _configuration.GetSection("PaymentEndpoints");

            var baseUrl = config["PaymentSimulatorBaseUrl"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return default;

            using var channel = GrpcChannel.ForAddress(baseUrl);
            var client = new PaymentGrpc.PaymentGrpcClient(channel);
            var fieldMask = FieldMask.FromFieldNumbers<PaymentResponse>(1);
            return (await client.ProcessPaymentAsync(new Protos.PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask })).Result;
        }


        #endregion

    }

    internal class MakePaymentRequestDto
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
    }
}

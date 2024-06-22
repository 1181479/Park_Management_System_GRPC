using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
using static PaymentClient.PaymentGrpc;

namespace Park20.Backoffice.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly List<double> durations = [];
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IParkRepository _parkRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IParkLogRepository _parkLogRepository;
        private readonly IParkyWalletRepository _parkWalletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IParkyCoinsConfigurationRepository _parkyCoinsConfigurationRepository;
        private readonly IConfiguration _configuration;
        private readonly GrpcChannel channel;
        private readonly PaymentGrpcClient client;
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
            var config = _configuration.GetSection("PaymentEndpoints");

            var httpHandler = new HttpClientHandler();

            // Load the certificate
            httpHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, X509Chain, SslPolicyErrors) => true;

            //httpHandler.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate2(certPath));

            var baseUrl = config["PaymentSimulatorBaseUrl"];
            channel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });
            client = new PaymentGrpc.PaymentGrpcClient(channel);
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
            missingPayment = 10;
            bool isSucessfull = missingPayment > 0 ? false : true;

            if (!isSucessfull)
            {
                isSucessfull = await SimulatePayment(token, missingPayment);
            }

            await _invoiceRepository.CreateInvoice(new Invoice(totalCost, isSucessfull), licensePlate);

            return new HibridPayment(parkyCoinsSpent, missingPayment, totalCost, isSucessfull);
        }

        public async Task<HibridPayment> MakePaymentClientStream(string licensePlate, decimal totalCost)
        {
            var token = await GetTokenFromPaymentMethodByLicensePlate(licensePlate);

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    return default;
            //}

            var parkyCoinsSpent = await PayWithParkyCoins(licensePlate, totalCost);

            var missingPayment = totalCost - parkyCoinsSpent;
            missingPayment = 10;
            bool isSucessfull = missingPayment > 0 ? false : true;

            if (!isSucessfull)
            {
                isSucessfull = await SimulatePaymentClientStream(token, missingPayment);
            }

            await _invoiceRepository.CreateInvoice(new Invoice(totalCost, isSucessfull), licensePlate);

            return new HibridPayment(parkyCoinsSpent, missingPayment, totalCost, isSucessfull);
        }

        public async Task<HibridPayment> MakePaymentServerStream(string licensePlate, decimal totalCost)
        {
            var token = await GetTokenFromPaymentMethodByLicensePlate(licensePlate);

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    return default;
            //}

            var parkyCoinsSpent = await PayWithParkyCoins(licensePlate, totalCost);

            var missingPayment = totalCost - parkyCoinsSpent;
            missingPayment = 10;
            bool isSucessfull = missingPayment > 0 ? false : true;

            if (!isSucessfull)
            {
                isSucessfull = await SimulatePaymentServerStream(token, missingPayment);
            }

            await _invoiceRepository.CreateInvoice(new Invoice(totalCost, isSucessfull), licensePlate);

            return new HibridPayment(parkyCoinsSpent, missingPayment, totalCost, isSucessfull);
        }

        public async Task<HibridPayment> MakePaymentTwoSidedStream(string licensePlate, decimal totalCost)
        {
            var token = await GetTokenFromPaymentMethodByLicensePlate(licensePlate);

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    return default;
            //}

            var parkyCoinsSpent = await PayWithParkyCoins(licensePlate, totalCost);

            var missingPayment = totalCost - parkyCoinsSpent;
            missingPayment = 10;
            bool isSucessfull = missingPayment > 0 ? false : true;

            if (!isSucessfull)
            {
                isSucessfull = await SimulatePaymentTwoSidedStream(token, missingPayment);
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

        //res = (await client.ProcessPaymentAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask })).Result; //Unary communication
        /*var stream = client.ProcessPaymentClientStream();
        await stream.RequestStream.WriteAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask }); //Client sided stream
        await stream.RequestStream.CompleteAsync();
        res = (await stream.ResponseAsync).Result;*/
        /*using (var call = client.ProcessPaymentServerStream(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask }))
        {
            var responseStream = call.ResponseStream;
            while (await responseStream.MoveNext())         //Server sided stream
            {
                res = responseStream.Current.Result;
            }
        }*/
        /*var requestStream = client.ProcessPaymentTwoSideStream();         //Two sided stream
            DateTime startTime = DateTime.Now;
            var responseTask = Task.Run(async () =>
            {
                await foreach (var response in requestStream.ResponseStream.ReadAllAsync())
                {
                    res = response.Result;
                    DateTime endTime = DateTime.Now;
                    TimeSpan duration = endTime - startTime;
                    durations.Add(duration.TotalMilliseconds - 3000);
                }
            });

            for (int i = 0; i < 3; i++)
            {
                startTime = DateTime.Now;
                await requestStream.RequestStream.WriteAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask });
            }

            await requestStream.RequestStream.CompleteAsync();
            await responseTask;*/
        private async Task<bool> SimulatePayment(string token, decimal totalCost)
        {
            PaymentResponse res;
            var fieldMask = FieldMask.FromFieldNumbers<PaymentResponse>(1,2);
            res = (await client.ProcessPaymentAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask })); //Unary communication
            return res.Result;
        }

        private async Task<bool> SimulatePaymentClientStream(string token, decimal totalCost)
        {
            // PaymentResponse res;
            // var fieldMask = FieldMask.FromFieldNumbers<PaymentResponse>(1);
            // var stream = client.ProcessPaymentClientStream();
            // DateTime startTime = DateTime.Now;
            // await stream.RequestStream.WriteAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask }); //Client sided stream
            // await stream.RequestStream.CompleteAsync();
            // res = (await stream.ResponseAsync);
            // DateTime endTime = DateTime.Now;
            // TimeSpan duration = endTime - startTime;
            // durations.Add(duration.TotalMilliseconds - 500);
            // return res.Result;
            return true;
        }

        private async Task<bool> SimulatePaymentServerStream(string token, decimal totalCost)
        {
            // PaymentResponse res = new();
            // var fieldMask = FieldMask.FromFieldNumbers<PaymentResponse>(1);
            // using (var call = client.ProcessPaymentServerStream(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask }))
            // {
            //     DateTime startTime = DateTime.Now;
            //     var responseStream = call.ResponseStream;
            //     while (await responseStream.MoveNext())         //Server sided stream
            //     {
            //         res = responseStream.Current;
            //         DateTime endTime = DateTime.Now;
            //         TimeSpan duration = endTime - startTime;
            //         durations.Add(duration.TotalMilliseconds - 500);
            //     }
            // }
            // return res.Result;
            return true;
        }

        private async Task<bool> SimulatePaymentTwoSidedStream(string token, decimal totalCost)
        {
            // PaymentResponse res = new();
            // var fieldMask = FieldMask.FromFieldNumbers<PaymentResponse>(1);
            // var requestStream = client.ProcessPaymentTwoSideStream();         //Two sided stream
            // DateTime startTime = DateTime.Now;
            // var responseTask = Task.Run(async () =>
            // {
            //     await foreach (var response in requestStream.ResponseStream.ReadAllAsync())
            //     {
            //         res = response;
            //         DateTime endTime = DateTime.Now;
            //         TimeSpan duration = endTime - startTime;
            //         durations.Add(duration.TotalMilliseconds - 500);
            //     }
            // });

            // startTime = DateTime.Now;
            // await requestStream.RequestStream.WriteAsync(new PaymentRequest { Amount = (double)totalCost, Token = token, FieldMask = fieldMask });

            // await requestStream.RequestStream.CompleteAsync();
            // await responseTask;
            // return res.Result;
            return true;
        }


        #endregion

        public void PrintMetrics()
        {
            double sum = 0;
            durations.Sort();
            string outputPath = "/App/output"; // Path inside the Docker container

            // Create directory if it doesn't exist
            Directory.CreateDirectory(outputPath);

            string filePath = Path.Combine(outputPath, DateTime.Now.ToString("HHmmss") + "payment.csv");
            string content = "req;time";
            int i = 0;
            foreach (var duration in durations)
            {
                i++;
                sum += duration;
                content += "\n" + i + ";" + duration;
            }
            File.WriteAllText(filePath, content);
            if (durations.Count > 0)
            {
                double min = durations.First();
                double max = durations.Last();
                double avg = sum / durations.Count;
                double median = durations.Count % 2 == 0 ?
                    (durations[durations.Count / 2 - 1] + durations[durations.Count / 2]) / 2.0 :
                    durations[durations.Count / 2];

                double p90 = durations[(int)Math.Ceiling(0.9 * durations.Count) - 1];
                double p95 = durations[(int)Math.Ceiling(0.95 * durations.Count) - 1];

                Console.WriteLine($"Average: {avg} milliseconds");
                Console.WriteLine($"Minimum: {min} milliseconds");
                Console.WriteLine($"Median: {median} milliseconds");
                Console.WriteLine($"Maximum: {max} milliseconds");
                Console.WriteLine($"p(90): {p90} milliseconds");
                Console.WriteLine($"p(95): {p95} milliseconds");
            }
            durations.Clear();
        }
    }

    internal class MakePaymentRequestDto
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
    }
}

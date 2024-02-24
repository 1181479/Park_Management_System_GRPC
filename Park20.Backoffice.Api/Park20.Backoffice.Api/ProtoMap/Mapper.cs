using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.User;
using Proto;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Park20.Backoffice.Api.ProtoMap
{
    public static class Mapper
    {
        public static ListPark Map(List<Core.Domain.Park.Park> lp)
        {
            List<Proto.Park> parks = [];
            lp.ForEach(p =>
            {
                parks.Add(Map(p));
            });
            return new ListPark
            {
                Parks = { parks }
            };
        }
        public static Proto.Park Map(Core.Domain.Park.Park p)
        {
            return new Proto.Park
            {
                Id = p.Id,
                IsActive = p.IsActive,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                OpeningTime = Timestamp.FromDateTime(p.OpeningTime.ToUniversalTime()),
                ClosingTime = Timestamp.FromDateTime(p.ClosingTime.ToUniversalTime()),
                Location = p.Location + "",
                NightFee = p.NightFee,
                NumberFloors = p.NumberFloors,
                ParkName = p.ParkName,
                ParkingSpots = { Map(p.ParkingSpots) },
                PriceTable = MapPriceTable(p)
            };
        }

        public static List<Proto.ParkingSpot> Map(List<Core.Domain.Park.ParkingSpot> pss)
        {
            List<Proto.ParkingSpot> parkingSpots = [];
            pss.ForEach(ps =>
            {
                parkingSpots.Add(Map(ps));
            });
            return parkingSpots;
        }

        public static Proto.ParkingSpot Map(Core.Domain.Park.ParkingSpot ps)
        {
            return new Proto.ParkingSpot
            {
                ParkingSpotId = ps.ParkingSpotId,
                FloorNumber = ps.FloorNumber,
                Status = ps.Status,
                VehicleType = (Proto.VehicleType)ps.VehicleType
            };
        }

        public static Proto.PriceTable MapPriceTable(Core.Domain.Park.Park p)
        {
            return new Proto.PriceTable
            {
                InitialDate = Timestamp.FromDateTime(p.PriceTable.InitialDate.ToUniversalTime()),
                NightFee = p.NightFee,
                ParkName = p.ParkName,
                PriceLines = { Map(p.PriceTable.LinePrices) }
            };
        }
        public static List<Proto.LinePriceTable> Map(List<Core.Domain.Park.LinePriceTable> lpt)
        {
            List<Proto.LinePriceTable> linePriceTables = [];
            lpt.ForEach(lp =>
            {
                linePriceTables.Add(Map(lp));
            });
            return linePriceTables;
        }
        public static Proto.LinePriceTable Map(Core.Domain.Park.LinePriceTable lpt)
        {
            return new Proto.LinePriceTable
            {
                Period = Map(lpt.Period)
            };
        }

        public static Proto.Period Map(Core.Domain.Park.Period p)
        {
            return new Proto.Period
            {
                InitialTime = Timestamp.FromDateTime(DateTime.UtcNow + p.InitialTime),
                FinalTime = Timestamp.FromDateTime(DateTime.UtcNow + p.FinalTime),
                FractionList = { Map(p.Fractions) }
            };
        }

        public static List<Fractions> Map(List<Fraction> lf)
        {
            List<Fractions> fractions = [];
            lf.ForEach(f =>
            {
                fractions.Add(Map(f));
            });
            return fractions;
        }
        public static Fractions Map(Fraction f)
        {
            return new Fractions
            {
                Minutes = f.Minutes.Minutes,
                Order = f.Order,
                Price = ((double)f.Price),
                VehicleType = (Proto.VehicleType)f.VehicleType
            };
        }

        public static Core.Domain.Park.Park Map(Proto.PriceTable pt)
        {
            return new Core.Domain.Park.Park
            {
                ParkName = pt.ParkName,
                NightFee = pt.NightFee,
                PriceTable = Map(pt.InitialDate.ToDateTime(), pt.PriceLines.ToList())
            };
        }

        public static Core.Domain.Park.PriceTable Map(DateTime initialDate, List<Proto.LinePriceTable> lpt)
        {
            return new Core.Domain.Park.PriceTable { InitialDate = initialDate, LinePrices = Map(lpt) };
        }

        public static List<Core.Domain.Park.LinePriceTable> Map(List<Proto.LinePriceTable> lpt)
        {
            List<Core.Domain.Park.LinePriceTable> linePriceTables = [];
            lpt.ForEach(lp =>
            {
                linePriceTables.Add(Map(lp));
            });
            return linePriceTables;
        }
        public static Core.Domain.Park.LinePriceTable Map(Proto.LinePriceTable lpt)
        {
            return new Core.Domain.Park.LinePriceTable
            {
                Period = Map(lpt.Period)
            };
        }

        public static Core.Domain.Park.Period Map(Proto.Period p)
        {
            return new Core.Domain.Park.Period
            {
                InitialTime = p.InitialTime.ToDateTime().TimeOfDay,
                FinalTime = p.FinalTime.ToDateTime().TimeOfDay,
                Fractions = Map(p.FractionList.ToList())
            };
        }

        public static List<Core.Domain.Park.Fraction> Map(List<Fractions> lf)
        {
            List<Core.Domain.Park.Fraction> fractions = [];
            lf.ForEach(f =>
            {
                fractions.Add(Map(f));
            });
            return fractions;
        }
        public static Core.Domain.Park.Fraction Map(Fractions f)
        {
            return new Core.Domain.Park.Fraction
            {
                Order = f.Order,
                Minutes = TimeSpan.FromMinutes(f.Minutes),
                VehicleType = (Core.Domain.VehicleType)f.VehicleType,
                Price = ((decimal)f.Price)
            };
        }

        public static List<CustomerParkyCoinsSpentResult> Map(List<Core.Domain.DashboardElements> dashboardElements)
        {
            List<CustomerParkyCoinsSpentResult> elements = [];
            dashboardElements.ForEach(f =>
            {
                elements.Add(Map(f));
            });
            return elements;
        }

        public static CustomerParkyCoinsSpentResult Map(Core.Domain.DashboardElements dashboardElement)
        {
            return new CustomerParkyCoinsSpentResult
            {
                Username = dashboardElement.Username,
                ParkyCoinsSpent = dashboardElement.ParkyCoinsSpent
            };
        }

        public static Proto.ParkyWallet Map(Core.Domain.ParkyWallets.ParkyWallet pwd)
        {
            return new Proto.ParkyWallet { CurrentBalance = pwd.CurrentBalance, Movements = { Map(pwd.Movements) } };
        }

        public static List<ParkyWalletMovement> Map(List<ParkyWalletMovements> lpwmd)
        {
            List<ParkyWalletMovement> movements = [];
            lpwmd.ForEach(f =>
            {
                movements.Add(Map(f));
            });
            return movements;
        }

        public static ParkyWalletMovement Map(ParkyWalletMovements pwmd)
        {
            return new ParkyWalletMovement
            {
                Date = pwmd.Date.ToString(),
                MovementType = pwmd.MovementId == 0 ? MovementType.Inbound.ToString() : MovementType.Outbound.ToString(),
                Amount = pwmd.Amount
            };
        }

        public static ListPaymentMethodResult Map(IEnumerable<PaymentMethod> lpaymentMethodResultDto)
        {
            return new ListPaymentMethodResult
            {
                ListPaymentMethod = { Map(lpaymentMethodResultDto.ToList()) }
            };
        }

        public static List<PaymentMethodResult> Map(List<PaymentMethod> lpaymentMethodResultDto)
        {
            List<PaymentMethodResult> methods = [];
            lpaymentMethodResultDto.ForEach(f =>
            {
                methods.Add(Map(f));
            });
            return methods;
        }

        public static PaymentMethodResult Map(PaymentMethod paymentMethodResultDto)
        {
            return new PaymentMethodResult
            {
                FullName = paymentMethodResultDto.FullName,
                LastFourDigits = paymentMethodResultDto.CardLastFourDigits
            };
        }

        public static PaymentMethod Map(CreatePaymentMethodRequest request)
        {
            return new PaymentMethod
            {
                CardLastFourDigits = request.LastFourDigits,
                ExpirationDate = request.ExpirationDate.ToDateTime(),
                FullName = request.FullName,
                PaymentToken = request.Token
            };
        }

        public static ListCreateCustomerResult Map(List<Customer> result)
        {
            return new ListCreateCustomerResult
            {
                Customers = { MapCustomers(result.ToList()) }
            };
        }

        public static List<CreateCustomerResult> MapCustomers(List<Customer> result)
        {
            List<CreateCustomerResult> customers = [];
            result.ForEach(f =>
            {
                customers.Add(Map(f));
            });
            return customers;
        }

        public static CreateCustomerResult Map(Customer result)
        {
            return new CreateCustomerResult
            {
                Email = result.Email,
                Name = result.Name,
                Username = result.Username,
            };
        }

        public static Vehicle Map(CreateVehicleRequest request)
        {
            return new Vehicle
            {
                LicensePlate = request.LicensePlate,
                Brand = request.Brand,
                Model = request.Model,
                Type = (Core.Domain.VehicleType)System.Enum.Parse(typeof(Core.Domain.VehicleType), request.Type)
            };
        }

        public static VehicleResult Map(Vehicle result)
        {
            return new VehicleResult
            {
                Brand = result.Brand,
                LicensePlate = result.LicensePlate,
                Model = result.Model,
                Type = result.Type.ToString()
            };
        }

        public static List<VehicleResult> Map(List<Vehicle> result)
        {
            List<VehicleResult> vehicles = [];
            result.ForEach(f =>
            {
                vehicles.Add(Map(f));
            });
            return vehicles;
        }

        public static VehicleResults MapResults(List<Vehicle> result)
        {
            return new VehicleResults
            {
                Vehicles = { Map(result) }
            };
        }

        public static ParkLog Map(string licensePlate, string parkName)
        {
            return new ParkLog(licensePlate, parkName);
        }

        public static Customer Map(CreateCustomerRequest request)
        {
            return new Customer(request.Username, request.Email, request.Password, request.Name, false, null);
        }
    }
}

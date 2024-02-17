using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Proto;
using System;

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

        public static ListParkDistanceResult Map(List<ParkDistanceResultDto> dto)
        {
            List<ParkDistanceResult> distance = [];
            dto.ForEach(d =>
            {
                distance.Add(Map(d));
            });
            return new ListParkDistanceResult
            {
                ParkDistance = { distance }
            };
        }

        public static ParkDistanceResult Map(ParkDistanceResultDto dto)
        {
            return new ParkDistanceResult
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                DistanceToTarget = dto.DistanceToTarget,
                Location = dto.Location,
                ParkName = dto.ParkName
            };
        }
        public static GetPriceTable Map(GetPriceTableDto dto)
        {
            return new GetPriceTable
            {
                InitialDate = Timestamp.FromDateTime(dto.InitialDate.ToUniversalTime()),
                NightFee = dto.NightFee,
                ParkName = dto.ParkName,
                PriceLines = { Map(dto.PriceLines) }
            };
        }

        public static List<GetLinePriceTable> Map(List<GetPriceTableDto.GetLinePriceTableDto> lpt)
        {
            List<GetLinePriceTable> linePriceTables = [];
            lpt.ForEach(lp =>
            {
                linePriceTables.Add(Map(lp));
            });
            return linePriceTables;
        }

        public static GetLinePriceTable Map(GetPriceTableDto.GetLinePriceTableDto dto)
        {
            return new GetLinePriceTable
            {
                Period = Map(dto.Period)
            };
        }

        public static GetPeriod Map(GetPriceTableDto.GetPeriodDto dto)
        {
            return new GetPeriod
            {
                InitialTime = dto.InitialTime,
                FinalTime = dto.FinalTime,
                Fractions = { Map(dto.FractionList) }
            };
        }

        public static List<GetFractions> Map(List<GetPriceTableDto.GetFractionsDto> lf)
        {
            List<GetFractions> fractions = [];
            lf.ForEach(f =>
            {
                fractions.Add(Map(f));
            });
            return fractions;
        }
        public static GetFractions Map(GetPriceTableDto.GetFractionsDto dto)
        {
            return new GetFractions
            {
                AutomobilePrice = ((double)dto.AutomobilePrice),
                ElectricPrice = ((double)dto.ElectricPrice),
                GplPrice = ((double)dto.GplPrice),
                Minutes = ((double)dto.Minutes),
                MotorcyclePrice = ((double)dto.MotorcyclePrice),
                Order = dto.Order
            };
        }

        public static PriceTableDto Map(Proto.PriceTable pt)
        {
            return new PriceTableDto(pt.ParkName, pt.NightFee, pt.InitialDate.ToDateTime(), Map(pt.PriceLines.ToList()));
        }
        public static List<LinePriceTableDto> Map(List<Proto.LinePriceTable> lpt)
        {
            List<LinePriceTableDto> linePriceTables = [];
            lpt.ForEach(lp =>
            {
                linePriceTables.Add(Map(lp));
            });
            return linePriceTables;
        }
        public static LinePriceTableDto Map(Proto.LinePriceTable lpt)
        {
            return new LinePriceTableDto(Map(lpt.Period));
        }

        public static PeriodDto Map(Proto.Period p)
        {
            return new PeriodDto(p.InitialTime.ToString(), p.FinalTime.ToString(), Map(p.FractionList.ToList()));
        }

        public static List<FractionsDto> Map(List<Fractions> lf)
        {
            List<FractionsDto> fractions = [];
            lf.ForEach(f =>
            {
                fractions.Add(Map(f));
            });
            return fractions;
        }
        public static FractionsDto Map(Fractions f)
        {
            return new FractionsDto(f.Order, f.Minutes, (Core.Domain.VehicleType)f.VehicleType, ((decimal)f.Price));
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

        public static ParkyWallet Map(ParkyWalletDto pwd)
        {
            return new ParkyWallet { CurrentBalance = pwd.CurrentBalance, Movements = { Map(pwd.Movements) } };
        }

        public static List<ParkyWalletMovement> Map(List<ParkyWalletMovementDto> lpwmd)
        {
            List<ParkyWalletMovement> movements = [];
            lpwmd.ForEach(f =>
            {
                movements.Add(Map(f));
            });
            return movements;
        }

        public static ParkyWalletMovement Map(ParkyWalletMovementDto pwmd)
        {
            return new ParkyWalletMovement
            {
                Date = pwmd.Date.ToString(),
                MovementType = pwmd.MovementType,
                Amount = pwmd.Amount
            };
        }

        public static ListPaymentMethodResult Map(IEnumerable<PaymentMethodResultDto> lpaymentMethodResultDto)
        {
            return new ListPaymentMethodResult
            {
                ListPaymentMethod = { Map(lpaymentMethodResultDto.ToList()) }
            };
        }

        public static List<PaymentMethodResult> Map(List<PaymentMethodResultDto> lpaymentMethodResultDto)
        {
            List<PaymentMethodResult> methods = [];
            lpaymentMethodResultDto.ForEach(f =>
            {
                methods.Add(Map(f));
            });
            return methods;
        }

        public static PaymentMethodResult Map(PaymentMethodResultDto paymentMethodResultDto)
        {
            return new PaymentMethodResult
            {
                FullName = paymentMethodResultDto.FullName,
                LastFourDigits = paymentMethodResultDto.LastFourDigits
            };
        }

        public static CreatePaymentMethodRequestDto Map(CreatePaymentMethodRequest request)
        {
            return new CreatePaymentMethodRequestDto(request.LastFourDigits, request.ExpirationDate.ToDateTime(), request.FullName, request.Token, request.Username);
        }

        internal static ListCreateCustomerResult Map(List<CreateCustomerResultDto> result)
        {
            return new ListCreateCustomerResult
            {
                Customers = { MapCustomers(result.ToList()) }
            };
        }

        internal static List<CreateCustomerResult> MapCustomers(List<CreateCustomerResultDto> result)
        {
            List<CreateCustomerResult> customers = [];
            result.ForEach(f =>
            {
                customers.Add(Map(f));
            });
            return customers;
        }

        internal static CreateCustomerResult Map(CreateCustomerResultDto result)
        {
            return new CreateCustomerResult
            {
                Email = result.Email,
                Name = result.Name,
                Username = result.Username,
            };
        }

        internal static CreateVehicleRequestDto Map(CreateVehicleRequest request)
        {
            return new CreateVehicleRequestDto(request.LicensePlate, request.Brand, request.Model, request.Type, request.Username);
        }

        internal static VehicleResult Map(VehicleResultDto result)
        {
            return new VehicleResult
            {
                Brand = result.Brand,
                LicensePlate = result.LicensePlate,
                Model = result.Model,
                Type = result.Type
            };
        }

        internal static List<VehicleResult> Map(List<VehicleResultDto> result)
        {
            List<VehicleResult> vehicles = [];
            result.ForEach(f =>
            {
                vehicles.Add(Map(f));
            });
            return vehicles;
        }

        internal static VehicleResults MapResults(List<VehicleResultDto> result)
        {
            return new VehicleResults
            {
                Vehicles = { Map(result) }
            };
        }
    }
}

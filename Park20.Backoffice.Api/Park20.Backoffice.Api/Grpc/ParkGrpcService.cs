using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.IServices;
using Proto;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Park20.Backoffice.Api.Grpc
{
    public class ParkGrpcService : ParkGrpc.ParkGrpcBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkService _parkService;

        public ParkGrpcService(IVehicleService vehicleService, IParkService parkService)
        {
            _vehicleService = vehicleService;
            _parkService = parkService;
        }

        public override Task<ListPark> GetAllParks(EmptyRequest request, ServerCallContext context)
        {
            ListPark filteredParks = new();
            ListPark parks = Mapper.Map(_parkService.GetAllParks().ToList());
            if (!request.FieldMask.ToString().Contains("parks"))
            {
                foreach (var park in parks.Parks)
                {
                    Proto.Park filteredPark = new Proto.Park();
                    request.FieldMask.Merge(park, filteredPark);
                    filteredParks.Parks.Add(filteredPark);
                }
            }
            else
            {
                request.FieldMask.Merge(parks, filteredParks);
            }
            return Task.FromResult(filteredParks);
        }

        public override Task<TaskResult> UpdatePriceTable(Proto.PriceTable request, ServerCallContext context)
        {
            return Task.FromResult(new TaskResult { IsSuccessful = _parkService.UpdatePriceTable(Mapper.Map(request)).Result.Value });
        }

        public override Task<TaskResult> UpdateParkingSpotStatus(UpdateParkingSpotStatusRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TaskResult { IsSuccessful = _parkService.UpdateParkingSpotStatus(request.Status, request.ParkingSpotId).Result });
        }

        public override Task<TaskResult> GetAvailableSpace(ParkCheckRequest request, ServerCallContext context)
        {
            if (request == null)
            {
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            }
            var vehicle = _vehicleService.GetVehicle(request.LicencePlate).Result;
            if (vehicle == null)
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            var availableSpace = _parkService.GetAvailableSpace(vehicle.Type.ToString(), request.ParkName).Result;

            if (availableSpace == null || !availableSpace.HasValue)
            {
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            }
            return Task.FromResult(new TaskResult { IsSuccessful = availableSpace.Value });
        }

        public override Task<TaskResult> GetVehicleTypeAvailable(ParkCheckRequest request, ServerCallContext context)
        {
            if (request == null)
            {
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            }
            var vehicle = _vehicleService.GetVehicle(request.LicencePlate).Result;
            if (vehicle == null)
                return Task.FromResult(new TaskResult { IsSuccessful = false });

            var vehicleTypeAvailable = _parkService.GetVehicleTypeAvailable(vehicle.Type.ToString(), request.ParkName).Result;

            if (vehicleTypeAvailable == null || !vehicleTypeAvailable.HasValue)
            {
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            }
            return Task.FromResult(new TaskResult { IsSuccessful = vehicleTypeAvailable.Value });
        }

        public override Task<ParkingSpotCount> GetParkingSpotsCount(GetInfoByParkName request, ServerCallContext context)
        {
            List<Core.Domain.Park.ParkingSpot> parkingSpots = _parkService.GetNumberParkingSpots(request.ParkName).Result;
            ParkingSpotCount counts = new ParkingSpotCount();

            foreach (var parkingSpot in parkingSpots)
            {
                switch (parkingSpot.VehicleType)
                {
                    case Core.Domain.VehicleType.Motocycle:
                        counts.MotocycleCount++;
                        break;
                    case Core.Domain.VehicleType.GPL:
                        counts.GplCount++;
                        break;
                    case Core.Domain.VehicleType.Electric:
                        counts.ElectricCount++;
                        break;
                    case Core.Domain.VehicleType.Automobile:
                        counts.AutomobileCount++;
                        break;
                }
            }

            ParkingSpotCount filteredCount = new ParkingSpotCount();
            request.FieldMask.Merge(new ParkingSpotCount
            {
                AutomobileCount = counts.AutomobileCount,
                ElectricCount = counts.ElectricCount,
                GplCount = counts.GplCount,
                MotocycleCount = counts.MotocycleCount
            }, filteredCount);
            return Task.FromResult(filteredCount);
        }

        public override Task<ListParkDistanceResult> GetAllParksByDistance(GetAllParksByDistanceRequest request, ServerCallContext context)
        {
            List<Core.Domain.Park.Park> parks = _parkService.GetAllParksWithDistance().Result.ToList();
            // Calcula a distância de cada parque à localização alvo
            ListParkDistanceResult parksWithDistance = new ListParkDistanceResult
            {
                ParkDistance ={
                    parks.Select(park =>
                    {
                        double distance = _parkService.CalculateDistanceBetweenCoordinates(park.Latitude, park.Longitude, request.Latitude, request.Longitude);
                        return new ParkDistanceResult
                        {
                            ParkName = park.ParkName,
                            DistanceToTarget = distance,
                            Location = park.Location,
                            Latitude = park.Latitude,
                            Longitude = park.Longitude,

                        };
                    }).ToList()
                }
            };

            ListParkDistanceResult filteredDistances = new();
            if (!request.FieldMask.ToString().Contains("parkDistance"))
            {
                foreach (var distance in parksWithDistance.ParkDistance)
                {
                    ParkDistanceResult filteredDistance = new();
                    request.FieldMask.Merge(distance, filteredDistance);
                    filteredDistances.ParkDistance.Add(filteredDistance);
                }
            }
            else
            {
                request.FieldMask.Merge(parksWithDistance, filteredDistances);
            }
            return Task.FromResult(filteredDistances);
        }

        public override Task<ListParkNames> GetParkNames(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ListParkNames { ParkNames = { _parkService.GetParkNames().Result } });
        }

        public override Task<ListParkingSpot> GetParkingSpots(GetInfoByParkName request, ServerCallContext context)
        {
            ListParkingSpot lPS = new ListParkingSpot { ParkingSpots = { Mapper.Map(_parkService.GetParkingSpots(request.ParkName).Result) } };
            ListParkingSpot lfilteredPS = new ListParkingSpot();
            if (!request.FieldMask.ToString().Contains("parkingSpots"))
            {
                foreach (var ps in lPS.ParkingSpots)
                {
                    Proto.ParkingSpot filteredPS = new Proto.ParkingSpot();
                    request.FieldMask.Merge(ps, filteredPS);
                    lfilteredPS.ParkingSpots.Add(filteredPS);
                }
            }
            else
            {
                request.FieldMask.Merge(lPS, lfilteredPS);
            }
            return Task.FromResult(lfilteredPS);
        }

        public override Task<Proto.PriceTable> GetPriceTableByParkName(GetInfoByParkName request, ServerCallContext context)
        {
            Proto.PriceTable pt = Mapper.MapPriceTable(_parkService.GetPriceTableByParkName(request.ParkName).Result);
            Proto.PriceTable filteredPT = new();
            request.FieldMask.Merge(pt, filteredPT);
            return Task.FromResult(filteredPT);
        }

        public override async Task<ListPark> GetAllParksClientStream(IAsyncStreamReader<EmptyRequest> requestStream, ServerCallContext context)
        {
            ListPark filteredParks = new();
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;

                if (request == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty request received."));
                }

                ListPark parks = Mapper.Map(_parkService.GetAllParks().ToList());
                var fieldMask = requestStream.Current.FieldMask;

                if (fieldMask == null || !fieldMask.ToString().Contains("parks"))
                {
                    foreach (var park in parks.Parks)
                    {
                        Proto.Park filteredPark = new Proto.Park();
                        fieldMask.Merge(park, filteredPark);
                        filteredParks.Parks.Add(filteredPark);
                    }
                }
                else
                {
                    fieldMask.Merge(parks, filteredParks);
                }
            }
            return filteredParks;
        }

        public override async Task GetAllParksTwoSidedStream(IAsyncStreamReader<EmptyRequest> requestStream, IServerStreamWriter<ListPark> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                ListPark filteredParks = new();
                // Apply filtering based on FieldMask
                if (request == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty request received."));
                }

                ListPark parks = Mapper.Map(_parkService.GetAllParks().ToList());
                var fieldMask = requestStream.Current.FieldMask;

                if (fieldMask == null || !fieldMask.ToString().Contains("parks"))
                {
                    foreach (var park in parks.Parks)
                    {
                        Proto.Park filteredPark = new Proto.Park();
                        fieldMask.Merge(park, filteredPark);
                        filteredParks.Parks.Add(filteredPark);
                    }
                }
                else
                {
                    fieldMask.Merge(parks, filteredParks);
                }

                await responseStream.WriteAsync(filteredParks);
            }
        }

        public override async Task GetAllParksServerStream(EmptyRequest request, IServerStreamWriter<ListPark> responseStream, ServerCallContext context)
        {
            ListPark filteredParks = new();
            ListPark parks = Mapper.Map(_parkService.GetAllParks().ToList());

            // Apply filtering based on FieldMask
            var fieldMask = request.FieldMask;

            if (fieldMask == null || !fieldMask.ToString().Contains("parks"))
            {
                foreach (var park in parks.Parks)
                {
                    Proto.Park filteredPark = new Proto.Park();
                    fieldMask.Merge(park, filteredPark);
                    filteredParks.Parks.Add(filteredPark);
                }
            }
            else
            {
                fieldMask.Merge(parks, filteredParks);
            }
            await responseStream.WriteAsync(filteredParks);
        }

    }
}

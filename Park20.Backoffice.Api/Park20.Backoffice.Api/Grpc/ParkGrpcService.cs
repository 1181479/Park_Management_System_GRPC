using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;
using Proto;

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
            request.FieldMask.Merge(parks, filteredParks);
            return Task.FromResult(parks);
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
            var availableSpace = _parkService.GetAvailableSpace(vehicle.Type, request.ParkName).Result;

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

            var vehicleTypeAvailable = _parkService.GetVehicleTypeAvailable(vehicle.Type, request.ParkName).Result;

            if (vehicleTypeAvailable == null || !vehicleTypeAvailable.HasValue)
            {
                return Task.FromResult(new TaskResult { IsSuccessful = false });
            }
            return Task.FromResult(new TaskResult { IsSuccessful = vehicleTypeAvailable.Value });
        }

        public override Task<ParkingSpotCount> GetParkingSpotsCount(GetInfoByParkName request, ServerCallContext context)
        {
            ParkingSpotCountDto dto = _parkService.GetNumberParkingSpots(request.ParkName).Result;
            return Task.FromResult(new ParkingSpotCount
            {
                AutomobileCount = dto.AutomobileCount,
                ElectricCount = dto.ElectricCount,
                GplCount = dto.GPLCount,
                MotocycleCount = dto.MotocycleCount
            });
        }

        public override Task<ListParkDistanceResult> GetAllParksByDistance(GetAllParksByDistanceRequest request, ServerCallContext context)
        {
            return Mapper.Map(_parkService.GetAllParksWithDistance(request.Latitude, request.Longitude).Result.ToList());
        }

        public override Task<ListParkNames> GetParkNames(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ListParkNames { ParkNames = { _parkService.GetParkNames().Result } });
        }

        public override Task<ListParkingSpot> GetParkingSpots(GetInfoByParkName request, ServerCallContext context)
        {
            return Task.FromResult(new ListParkingSpot { Parks = { Mapper.Map(_parkService.GetParkingSpots(request.ParkName).Result) } });
        }

        public override Task<GetPriceTable> GetPriceTableByParkName(GetInfoByParkName request, ServerCallContext context)
        {
            return Mapper.Map(_parkService.GetPriceTableByParkName(request.ParkName).Result);
        }
    }
}

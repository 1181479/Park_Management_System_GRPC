using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;
using Proto;
using System.ComponentModel;

namespace Park20.Backoffice.Api.Grpc
{
    public class VehicleGrpcService : VehicleGrpc.VehicleGrpcBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IParkLogService _parkLogService;
        private readonly IPaymentService _paymentService;

        public VehicleGrpcService(IVehicleService vehicleService, IParkLogService parkLogService, IPaymentService paymentService)
        {
            _vehicleService = vehicleService;
            _parkLogService = parkLogService;
            _paymentService = paymentService;
        }

        public override Task<VehicleResult> AddVehicle(CreateVehicleRequest request, ServerCallContext context)
        {
            VehicleResult vr = Mapper.Map(_vehicleService.AddVehicleToUser(Mapper.Map(request)).Result);
            VehicleResult filteredVehicle = new();
            request.FieldMask.Merge(vr, filteredVehicle);
            return Task.FromResult(filteredVehicle);
        }

        public override Task<ParkingSpotsUpdateResult> ParkCar(ParkingSpotsUpdateRequest request, ServerCallContext context)
        {
            _parkLogService.StartingCountingTimeOperation(request.LicensePlate, request.ParkName);
            return Task.FromResult(new ParkingSpotsUpdateResult { IsSuccessful = _parkLogService.UpdateAvailableParkingSpots(request.ParkName, request.LicensePlate, request.IsEntrance).Result });
        }

        public override Task<Proto.HibridPayment> LeavePark(ParkingSpotsUpdateRequest request, ServerCallContext context)
        {
            _parkLogService.StopCountingTimeOperation(request.LicensePlate, request.ParkName);

            var totalCost = _paymentService.CalculateCost(request.ParkName, request.LicensePlate).Result;

            var payment = _paymentService.MakePayment(request.LicensePlate, totalCost).Result;

            _parkLogService.UpdateAvailableParkingSpots(request.ParkName, request.LicensePlate, request.IsEntrance);
            Proto.HibridPayment hp = new();
            request.FieldMask.Merge(new Proto.HibridPayment
            {
                IsSuccessfull = payment.isSuccessfull,
                OtherPaymentMethodAmount = ((double)payment.otherPaymentMethodAmount),
                ParkyCoinsAmount = ((double)payment.parkyCoinsAmount),
                TotalCost = ((double)payment.totalCost)
            }, hp);
            return Task.FromResult(hp);
        }

        public override Task<VehicleResult> GetVehicleByLicencePlate(GetVehicleByLicencePlateRequest request, ServerCallContext context)
        {
            VehicleResult vr = Mapper.Map(_vehicleService.GetVehicle(request.LicensePlate).Result);
            VehicleResult filteredVehicle = new();
            request.FieldMask.Merge(vr, filteredVehicle);
            return Task.FromResult(filteredVehicle);
        }

        public override Task<VehicleResults> GetVehicleListFromUser(GetVehicleListFromUserRequest request, ServerCallContext context)
        {
            VehicleResults lvr = Mapper.MapResults(_vehicleService.GetVehicleListFromUser(request.Name).Result.ToList());
            VehicleResults filteredVR = new();
            if (!request.FieldMask.ToString().Contains("vehicles"))
            {
                foreach (var vehicle in lvr.Vehicles)
                {
                    VehicleResult filteredVehicle = new();
                    request.FieldMask.Merge(vehicle, filteredVehicle);
                    filteredVR.Vehicles.Add(filteredVehicle);
                }
            }
            else
            {
                request.FieldMask.Merge(lvr, filteredVR);
            }
            return Task.FromResult(filteredVR);
        }
    }
}

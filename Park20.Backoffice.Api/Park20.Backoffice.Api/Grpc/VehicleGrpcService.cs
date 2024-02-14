using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
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
            return Task.FromResult(Mapper.Map(_vehicleService.AddVehicleToUser(Mapper.Map(request)).Result));
        }

        public override Task<ParkingSpotsUpdateResult> ParkCar(ParkingSpotsUpdateRequest request, ServerCallContext context)
        {
            _parkLogService.StartingCountingTimeOperation(request.LicensePlate, request.ParkName);
            return Task.FromResult(new ParkingSpotsUpdateResult { IsSuccessful = _parkLogService.UpdateAvailableParkingSpots(request.ParkName, request.LicensePlate, request.IsEntrance).Result });
        }

        public override Task<HibridPayment> LeavePark(ParkingSpotsUpdateRequest request, ServerCallContext context)
        {
            _parkLogService.StopCountingTimeOperation(request.LicensePlate, request.ParkName);

            var totalCost = _paymentService.CalculateCost(request.ParkName, request.LicensePlate).Result;

            var payment = _paymentService.MakePayment(request.LicensePlate, totalCost).Result;

            _parkLogService.UpdateAvailableParkingSpots(request.ParkName, request.LicensePlate, request.IsEntrance);
            return Task.FromResult(new HibridPayment
            {
                IsSuccessfull = payment.isSuccessfull,
                OtherPaymentMethodAmount = ((double)payment.otherPaymentMethodAmount),
                ParkyCoinsAmount = ((double)payment.parkyCoinsAmount),
                TotalCost = ((double)payment.totalCost)
            });
        }

        public override Task<VehicleResult> GetVehicleByLicencePlate(GetVehicleByLicencePlateRequest request, ServerCallContext context)
        {
            return Task.FromResult(Mapper.Map(_vehicleService.GetVehicle(request.LicensePlate).Result));
        }

        public override Task<VehicleResults> GetVehicleListFromUser(GetVehicleListFromUserRequest request, ServerCallContext context)
        {
            return Task.FromResult(Mapper.MapResults(_vehicleService.GetVehicleListFromUser(request.Name).Result.ToList()));
        }
    }
}

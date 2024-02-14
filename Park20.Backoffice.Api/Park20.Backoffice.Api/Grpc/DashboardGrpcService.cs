using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;
using Proto;

namespace Park20.Backoffice.Api.Grpc
{
    public class DashboardGrpcService : DashboardGrpc.DashboardGrpcBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardGrpcService(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public override Task<Proto.DashboardElements> CreateDashBoardMostSpenders(CreateDashboardUsageParkyCoinsRequest request, ServerCallContext context)
        {
            List<Core.Domain.DashboardElements> dashboardElements = _dashboardService.GetUsersWithMostParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result;
            return Task.FromResult(new Proto.DashboardElements { Elements = { Mapper.Map(dashboardElements) } });
        }

        public override Task<Proto.DashboardElements> CreateDashBoardWorstSpenders(CreateDashboardUsageParkyCoinsRequest request, ServerCallContext context)
        {
            List<Core.Domain.DashboardElements> dashboardElements = _dashboardService.GetUsersWithLessParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result;
            return Task.FromResult(new Proto.DashboardElements { Elements = { Mapper.Map(dashboardElements) } });
        }

        public override Task<Proto.DashboardElements> CreateDashBoardMidSpenders(CreateDashboardUsageParkyCoinsRequest request, ServerCallContext context)
        {
            List<Core.Domain.DashboardElements> dashboardElements = _dashboardService.GetUsersWithMidParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result;
            return Task.FromResult(new Proto.DashboardElements { Elements = { Mapper.Map(dashboardElements) } });
        }
    }
}

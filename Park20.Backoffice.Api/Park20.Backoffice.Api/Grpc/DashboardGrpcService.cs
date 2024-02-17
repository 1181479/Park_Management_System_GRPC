using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;
using Proto;
using static Google.Rpc.Context.AttributeContext.Types;

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
            Proto.DashboardElements de = new() { Elements = { Mapper.Map(_dashboardService.GetUsersWithMostParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result) } };
            return Task.FromResult(FilterData(request, de));
        }

        public override Task<Proto.DashboardElements> CreateDashBoardWorstSpenders(CreateDashboardUsageParkyCoinsRequest request, ServerCallContext context)
        {
            Proto.DashboardElements de = new() { Elements = { Mapper.Map(_dashboardService.GetUsersWithLessParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result) } };
            return Task.FromResult(FilterData(request, de));
        }

        public override Task<Proto.DashboardElements> CreateDashBoardMidSpenders(CreateDashboardUsageParkyCoinsRequest request, ServerCallContext context)
        {
            Proto.DashboardElements de = new() { Elements = { Mapper.Map(_dashboardService.GetUsersWithMidParkyCoinsSpent(request.ParkName, request.InitialDate.ToDateTime(), request.EndDate.ToDateTime(), request.VehicleType.ToString(), request.TotalMinutes).Result) } };
            return Task.FromResult(FilterData(request, de));
        }

        private static Proto.DashboardElements FilterData(CreateDashboardUsageParkyCoinsRequest request, Proto.DashboardElements de)
        {
            Proto.DashboardElements filteredDe = new();
            if (!request.FieldMask.ToString().Contains("elements"))
            {
                foreach (var element in de.Elements)
                {
                    CustomerParkyCoinsSpentResult filteredElement = new CustomerParkyCoinsSpentResult();
                    request.FieldMask.Merge(element, filteredElement);
                    filteredDe.Elements.Add(filteredElement);
                }
            }
            else
            {
                request.FieldMask.Merge(de, filteredDe);
            }
            return filteredDe;
        }
    }
}

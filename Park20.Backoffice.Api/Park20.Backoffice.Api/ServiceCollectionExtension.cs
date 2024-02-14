using Microsoft.Extensions.DependencyInjection;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;
using Park20.Backoffice.Infrastructure.Repositories;

namespace Park20.Backoffice.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IVehicleRepository, VehicleRepository>()
                .AddTransient<IParkRepository, ParkRepository>()
                .AddTransient<IPaymentRepository, PaymentRepository>()
                .AddTransient<IParkLogRepository, ParkLogRepository>()
                .AddTransient<IInvoiceRepository, InvoiceRepository>()
                .AddTransient<IParkyWalletRepository, ParkyWalletRepository>()
                .AddTransient<IParkyCoinsConfigurationRepository, ParkyCoinsConfigurationRepository>()
                .AddTransient<IDashboardRepository, DashboardRepository>();

        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>()
                .AddTransient<IVehicleService, VehicleService>()
                .AddTransient<IParkService, ParkService>()
                .AddTransient<IPaymentService, PaymentService>()
                .AddTransient<IParkLogService, ParkLogService>()
                .AddTransient<IParkyWalletService, ParkyWalletService>()
                .AddTransient<IDashboardService, DashboardService>()
                .AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200", "http://localhost:4202", "http://localhost:4300", "http://localhost:4204")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

    }
}

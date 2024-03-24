using Dapper;
using Park20.Backoffice.Api.Grpc;
using Park20.Backoffice.Infrastructure;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var conn = new SqlConnection(builder.Configuration.GetConnectionString("DBConnection"));
try
{
    conn.Open();
    conn.Execute(File.ReadAllText("./sql/db.sql"));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "Park20 APP", Version = "v1" });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Park20");
    });
}*/

app.UseHttpsRedirection();

app.MapGrpcService<VehicleGrpcService>();
app.MapGrpcService<UserGrpcService>();
app.MapGrpcService<PaymentGrpcService>();
app.MapGrpcService<ParkyWalletGrpcService>();
app.MapGrpcService<ParkGrpcService>();
app.MapGrpcService<DashboardGrpcService>();
app.MapGrpcReflectionService();
app.UseCors();

app.Run();
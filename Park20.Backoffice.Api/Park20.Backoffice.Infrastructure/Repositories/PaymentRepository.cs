using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Park20.Backoffice.Core.Domain.User;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class PaymentRepository(IConfiguration configuration) : IPaymentRepository
    {
        

       

        public async Task<string?> GetTokenFromLicensePlate(string licensePlate)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<string>(PaymentQueries.PaymentTokenByLicensePlate, new { licensePlate });

            if (result == null)
                return default;

            return result;
        }

        public async Task<PaymentMethod?> AddPaymentMethod(PaymentMethod payment, string username)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var result = await connection.ExecuteAsync(PaymentQueries.AddPaymentMethod, new { cardLastFourDigits = payment.CardLastFourDigits, fullName = payment.FullName, expirationDate = payment.ExpirationDate, paymentToken = payment.PaymentToken, username = username });
            if (result > 0)
            {
                return payment;
            }
            return default;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllFromUser(string username)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QueryAsync<PaymentMethod>(PaymentQueries.GetAllFromUser, new { username = username });
        }
    }
}

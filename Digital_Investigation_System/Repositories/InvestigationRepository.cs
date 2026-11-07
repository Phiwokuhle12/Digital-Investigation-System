
using Dapper;
using Npgsql;
using DigitalInvestigationAPI.Models;

namespace DigitalInvestigationAPI.Repositories
{
    public class InvestigationRepository
    {
        private readonly IConfiguration _configuration;

        public InvestigationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<InvestigationResult>> GetInvestigationResults()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Database connection string is missing."
                );

            using (var connection = new NpgsqlConnection(connectionString))
            {
                string sql = @"
                    SELECT
                        i.""InvestigationID"",
                        s.""SuspectName"" AS ""SuspectName"",
                        i.""Conclusion"",
                        i.""DateStarted""
                    FROM ""Investigations"" i
                    INNER JOIN ""Suspects"" s
                        ON i.""SuspectID"" = s.""SuspectID""";

                return await connection.QueryAsync<InvestigationResult>(sql);
            }
        }
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
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
                _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT 
                        i.InvestigationID,
                        s.SuspectName AS SuspectName,
                        i.Conclusion,
                        i.DateStarted
                    FROM Investigations i
                    INNER JOIN Suspects s
                        ON i.SuspectID = s.SuspectID";

                return await connection.QueryAsync<InvestigationResult>(sql);
            }
        }
    }
}
using Microsoft.AspNetCore.Hosting.Server;

namespace Ploomes.API
{
    public static class ConnectionStringBuilder
    {
        private const string CONNECTION_STRING =
            @"Server=localhost,1433;Database=Ploomes;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";
        public static string BuildConnectionString()
        {
            return CONNECTION_STRING;
        }
    }
}

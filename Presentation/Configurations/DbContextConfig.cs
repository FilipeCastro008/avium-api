using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Avium.API.Configurations {
    public static class DbContextConfig {

        public static IServiceCollection AddDbContextConfig(this IServiceCollection service, IConfiguration configuration) { 
        
            service.AddDbContext<AviumContext>(options => 
                options.UseSqlServer(GetConnectionString(configuration),
                   sqlServerOptionsAction: sqlOptions =>
                   {
                       sqlOptions.CommandTimeout(90);
                       sqlOptions.EnableRetryOnFailure(
                           maxRetryCount: 10,
                           maxRetryDelay: TimeSpan.FromSeconds(30),
                           errorNumbersToAdd: null
                       );
                   }
                ));

            return service; 
        }

        private static string GetConnectionString(IConfiguration configuration) {

            return configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' não encontrada.");
        }

    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Order.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, 
                                                    Action<TContext, IServiceProvider> seeder, 
                                                    int? retry = 0)
                                                    where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating to SQL Server database has been started...");

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrating to SQL Server database has been completed.");
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating to SQL Server database!");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);

                        logger.LogInformation("Trying to migrate again after 2 secs!");

                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }

                return host;
            }
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                   TContext context,
                                                   IServiceProvider services)
                                                   where TContext : DbContext
        {
            context.Database.Migrate();

            seeder(context, services);
        }
    }
}

using Microsoft.Extensions.Logging;
using Order.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiuredOrders());

                await context.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContextSeed).Name);
            }
        }

        private static IEnumerable<OrderModel> GetPreconfiuredOrders()
        {
            return new List<OrderModel>
            {
                new OrderModel()
                {
                    UserName = "swn",
                    FirstName = "Osman Ali",
                    LastName = "Yardım",
                    EmailAddress = "osmanaliyardim@gmail.com",
                    AddressLine = "Bayraklı",
                    Country = "Türkiye",
                    TotalPrice = 350
                }
            };
        }
    }
}

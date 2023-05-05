using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Domain.Entities;
using Order.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<OrderModel>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName)
        {
            var orderList = await _context.Orders
                .Where(o => o.UserName == userName).ToListAsync();

            return orderList;
        }
    }
}

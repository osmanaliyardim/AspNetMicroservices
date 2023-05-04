using AutoMapper;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using Order.Domain.Entities;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<OrderModel, OrdersVm>().ReverseMap();
        }
    }
}

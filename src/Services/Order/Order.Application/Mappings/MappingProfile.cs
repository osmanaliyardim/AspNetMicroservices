using AutoMapper;
using Order.Application.Features.Orders.Commands.CheckoutOrder;
using Order.Application.Features.Orders.Commands.DeleteOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using Order.Domain.Entities;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<OrderModel, OrdersVm>().ReverseMap();
            CreateMap<OrderModel, CheckoutOrderCommand>().ReverseMap();
            CreateMap<OrderModel, UpdateOrderCommand>().ReverseMap();
            CreateMap<OrderModel, DeleteOrderCommand>().ReverseMap();
        }
    }
}

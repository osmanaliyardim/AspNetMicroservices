using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;
using Order.Application.Exceptions;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, 
            ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                _logger.LogError("Order does not exist in database.");
                throw new NotFoundException(nameof(OrderModel), request.Id);
            }

            await _orderRepository.DeleteAsync(orderToDelete);

            _logger.LogInformation($"Order {orderToDelete.Id} is successfully removed.");

            return Unit.Value;
        }
    }
}

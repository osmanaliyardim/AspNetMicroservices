using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Infrastructure;
using Order.Application.Contracts.Persistence;
using Order.Application.Models;
using Order.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, 
            IMapper mapper, 
            IEmailService emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderModel = _mapper.Map<OrderModel>(request);
            var newOrder = await _orderRepository.AddAsync(orderModel);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created!");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        private async Task SendMail(OrderModel order)
        {
            var email = new Email()
            {
                To = "osmanaliyardim@gmail.com",
                Body = "Your order",
                Subject = "Order was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}

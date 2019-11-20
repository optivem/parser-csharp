﻿using System.Threading.Tasks;
using Optivem.Framework.Core.Application;
using Optivem.Framework.Core.Common;
using Optivem.Framework.Core.Common.Mapping;
using Optivem.Framework.Core.Domain;
using Optivem.Template.Core.Application.Orders.Requests;
using Optivem.Template.Core.Application.Orders.Responses;
using Optivem.Template.Core.Domain.Orders;

namespace Optivem.Template.Core.Application.Orders.UseCases
{
    public class ArchiveOrderUseCase : RequestHandler<ArchiveOrderRequest, ArchiveOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public ArchiveOrderUseCase(IMapper mapper, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
            : base(mapper)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public override async Task<ArchiveOrderResponse> HandleAsync(ArchiveOrderRequest request)
        {
            var orderId = new OrderIdentity(request.Id);

            var order = await _orderRepository.FindAsync(orderId);

            if (order == null)
            {
                throw new NotFoundRequestException();
            }

            order.Archive();

            order = await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return Mapper.Map<Order, ArchiveOrderResponse>(order);
        }
    }
}
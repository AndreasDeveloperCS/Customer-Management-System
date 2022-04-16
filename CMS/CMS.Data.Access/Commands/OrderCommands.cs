using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Helpers;
using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Repositories.Interfaces;
using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Commands
{
    public class OrderCommands : IOrderCommands
    {
        private IOrderCommandsRepository _repository;

        public OrderCommands(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<IOrderCommandsRepository>();
        }

        public int CreateOrder(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
           return _repository.CreateOrder(order.ToOrder());
        }

        public async Task<int> CreateOrderAsync(OrderDto order, CancellationToken token)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return await  _repository.CreateOrderAsync(order.ToOrder(), token);
        }

        public int DeleteOrder(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return _repository.DeleteOrder(order.ToOrder());
        }

        public async Task<int> DeleteOrderAsync(OrderDto order, CancellationToken token)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return await _repository.DeleteOrderAsync(order.ToOrder(), token);
        }

        public IEnumerable<int> DeleteRangeOrders(IEnumerable<OrderDto> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            return _repository.DeleteRangeOrders(orders.Select(s=> s.ToOrder()));
        }

        public async Task<IEnumerable<int>> DeleteRangeOrdersAsync(IEnumerable<OrderDto> orders, CancellationToken token)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            return await _repository.DeleteRangeOrdersAsync(orders.Select(s => s.ToOrder()), token);
        }

        public int  UpdateOrder(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return _repository.UpdateOrder(order.ToOrder());
        }

        public async Task<int> UpdateOrderAsync(OrderDto order, CancellationToken token)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            return await _repository.UpdateOrderAsync(order.ToOrder(), token);
        }
    }
}

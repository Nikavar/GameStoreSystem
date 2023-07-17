using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface IOrderService
	{
		Task<Order> AddOrderAsync(int? accountId, OrderModel entity);

		//Task<IEnumerable<Order>> GetAllOrdersAsync();
		//Task<IEnumerable<Order>> GetManyOrdersAsync(Expression<Func<Order, bool>> filter);
		//Task UpdateOrderAsync(int orderId, OrderModel entity);
		//Task<OrderModel> GetOrderById(int? orderId);

	}
}

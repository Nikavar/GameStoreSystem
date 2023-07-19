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
		Task<Order> AddToCardAsync(int? gameId, int? orderId, int accountId);
		Task<Order> GetCurrentOrderAsync(int? accountId);
		Task<Order> CreateOrderAsync(Order model);
		Task<Order> UpdateOrderAsync(int? gameId, int? orderId, int accountId, SignEnum sign);
		Task<Order> RemoveGameFromCardAsync(int? gameId, int? accountId);
		Task RemoveAllGamesFromCardAsync(int? gameId, int? accountId);
	}
}

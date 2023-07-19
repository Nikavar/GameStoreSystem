using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
	}

	public interface IOrderRepository : IBaseRepository<Order>
	{

	}
}

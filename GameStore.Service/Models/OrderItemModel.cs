using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
	public class OrderItemModel
	{
		[Key]
		public int Id { get; set; }
		public int? GameId { get; set; }
		public int? OrderId { get; set; }
		public int ItemCount { get; set; } = 0;
		public decimal GamePrice { get; set; }
		public decimal itemTotalAmount => ItemCount * GamePrice;

    }
}

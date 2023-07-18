using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
	public class OrderModel
	{
		[Key]
        public int Id { get; set; }
		public int? AccountId { get; set; }
		public List<OrderItemModel>? OrderItems { get; set; }
		public DateTime? DateCompleted { get; set; }
		public decimal TotalAmount => OrderItems?.Sum(x => x.itemTotalAmount) ?? 0;
	}
}

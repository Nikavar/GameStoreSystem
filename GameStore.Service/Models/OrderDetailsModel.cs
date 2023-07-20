using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
	public class OrderDetailsModel
	{
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }

		[Required]
		public string? FirstName { get; set; }

		[Required]
		public string? LastName { get; set; }

		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Phone { get; set; }

		[Required]
		public PaymentType PaymentType { get; set; }

		[MaxLength(length:600)]
		public string? Comments { get; set; }
	}
}

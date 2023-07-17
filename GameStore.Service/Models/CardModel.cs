using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
	public class CardModel
	{
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int GameId { get; set; }
		public int OrderCount { get; set; } = 0;
	}
}

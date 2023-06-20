using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class OrderGame
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public Order Order { get; set; }
        public Game Game { get; set; }

    }
}

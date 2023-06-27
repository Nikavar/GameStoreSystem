using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "US dollar";
        public string Description { get; set; }
        public string ProfileImage { get; set; }
    }
}

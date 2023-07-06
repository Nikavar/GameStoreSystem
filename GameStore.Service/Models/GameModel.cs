using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
    public class GameModel
    {
        // comment #3
        public int Id { get; set; }

        [Required]
        public string? GameName { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "US dollar";
        [Required]
        public string? Description { get; set; }
        public string? ProfileImage { get; set; }
    }
}

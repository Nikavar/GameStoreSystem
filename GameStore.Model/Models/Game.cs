using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? GameName { get; set; }

        [NotNull]
        public decimal Price { get; set; }
        public string Currency { get; set; } = "US dollar";

        [Required]
        public string? Description { get; set; }
        public string? ProfileImage { get; set; }

        // relations

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<GameGenre>? GameGenres { get; set; }
        public ICollection<OrderItem>? GameOrderItems { get; set; }
    }
}

using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
        [NotNull]
        public decimal Price { get; set; }
        public string Currency { get; set; } = "US dollar";

        [Required]
        public string? Description { get; set; }
        public string? ProfileImage { get; set; }
        public ICollection<Genre>? Genres { get; set; }

    }
}

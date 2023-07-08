﻿using System;
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
        //public ICollection<Genre>? Genres { get; set; }

        // relations

        // Comment #2.1
        public ICollection<Comment>? Comments { get; set; }

        // Comment N2.2
        public ICollection<GameGenre>? GameGenres { get; set; }
    }
}

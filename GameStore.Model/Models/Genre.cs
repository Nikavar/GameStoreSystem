using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string? GenreName { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
        public ICollection<Genre>? Genres { get; set; }

        // relations
        [ForeignKey("ParentId")]
        public Genre? ParentGenre { get; set; }
        public ICollection<GameGenre>? GenreGames { get; set; }

    }
}
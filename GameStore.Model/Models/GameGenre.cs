using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class GameGenre
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int GenreId { get; set; }

        // relations
        public Game Game { get; set; }
        public Genre Genre { get; set; }
    }
}

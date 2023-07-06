using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
    public class GenreModel
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? GenreName { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ParentId")]
        public List<GenreModel>? ChildGenres { get; set; }
    }
}

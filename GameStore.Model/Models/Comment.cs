using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int GameId { get; set; }
        public int ParentId { get; set; }
        public Game Game { get; set; }
    }
}

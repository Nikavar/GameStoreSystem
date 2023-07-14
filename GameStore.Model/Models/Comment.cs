using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? CommentContent { get; set; }
        public string? User { get; set; }
        public DateTime PostedTime { get; set; }
        public int GameId { get; set; }
        public int? ReplyId { get; set; }

        // relations
        public Game? Game { get; set; }

        [ForeignKey("ReplyId")]
        public Comment? Reply { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment>? Replies { get; set;}

    }
}

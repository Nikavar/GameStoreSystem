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

        [MaxLength(length:600)]
        public string? CommentContent { get; set; }
        public string? User { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public int? GameId { get; set; }
        public int? ReplyId { get; set; }

        // relations
        public Game? Game { get; set; }

        [ForeignKey("ReplyId")]
        public Comment? Reply { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Comment>? Replies { get; set;}

    }
}

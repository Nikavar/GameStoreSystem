using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public int? ReplyId { get; set; }

        [MaxLength(length:600)]
        public string? CommentContent { get; set; }
        public string? User { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public ICollection<CommentModel>? Replies { get; set; }
    }
}

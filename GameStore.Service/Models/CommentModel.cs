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
        public string? CommentContent { get; set; }
        public string? User { get; set; }
        public DateTime PostedTime { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CommentModel>? Replies { get; set; }
    }
}

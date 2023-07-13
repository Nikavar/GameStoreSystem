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
        public string CommentText { get; set; }
        public int GameId { get; set; }
        public int ParentId { get; set; }
        public Game Game { get; set; }


        // To-Do
        //public class Comment : BaseEntity
        //{
        //    public string CommentDetails { get; set; }
        //    public string User { get; set; }
        //    public DateTime PostedTime { get; set; }
        //    public Product Product { get; set; }
        //    public int ProductId { get; set; }
        //    public int? ReplyId { get; set; }

        //    [ForeignKey("ReplyId")]
        //    public Comment Reply { get; set; }
        //    public bool IsDeleted { get; set; }
        //    public List<Comment> Replies { get; set; }
        //}
    }
}

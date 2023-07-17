using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetCommentsByGameIdAsync(int? id);
        Task<CommentModel> AddCommentAsync(CommentModel model);
    }
}

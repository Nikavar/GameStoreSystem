using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWor, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.unitOfWork = unitOfWor;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByGameIdAsync(int? id)
        {
            var commentList = await commentRepository.GetAllAsync();

            var entity = commentList
                .Where(comment => comment.ReplyId == null && comment.GameId == id)
                .Select(x => new Comment
                {
                    Id = x.Id,
                    ReplyId = x.ReplyId,
                    CommentContent = x.CommentContent,
                    IsDeleted = x.IsDeleted,
                    PostedTime = x.PostedTime,
                    User = x.User,
                    Replies = x.Replies,
                    GameId = x.GameId
                }).ToList();

            var model = mapper.Map<IEnumerable<CommentModel>>(entity);


            return model;
        }

        // task 3.2
        public async Task<CommentModel> AddCommentAsync(CommentModel model)
        {
            var entity = await commentRepository.AddAsync(mapper.Map<Comment>(model));
            return mapper.Map<CommentModel>(entity);
        }


        // task 3.3
        public async Task UpdateCommentAsync(CommentModel model)
        {
            var entity = mapper.Map<Comment>(model);
            await commentRepository.UpdateAsync(entity);
        }

        // task 3.4
        public async Task DeleteComment(int? id)
        {
            var entity = await commentRepository.GetByIdAsync(id);
            entity.IsDeleted = true;

            await commentRepository.UpdateAsync(entity);
        }
    }
}

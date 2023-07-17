using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentController(ICommentService commentService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        // task 3.1
        [HttpGet("Game/{gameId}/Comment")]
        public async Task<ActionResult> GetComments([FromRoute] int gameId)
        {
            var model = await commentService.GetCommentsByGameIdAsync(gameId);
            return Ok(model);
        }

        // task 3.2
        [HttpPost("Game/{gameId}/AddComment")]
        public async Task<ActionResult> AddCommentToGameAsync([FromRoute] int gameId, CommentModel model)
        {
            model.Id = gameId;
            var result = await commentService.AddCommentAsync(model);
            return Ok(result);
        }

		// task 3.3
		[HttpPut("Game/{gameId}/UpdateComment")]
		public async Task<ActionResult> UpdateCommentAsync([FromRoute] CommentModel model)
		{
			await commentService.UpdateCommentAsync(model);
			return Ok();
		}
	}
}

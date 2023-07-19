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

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
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
            model.GameId = gameId;
            var result = await commentService.AddCommentAsync(model);
            return Ok(result);
        }

        // task 3.3
        [HttpPut("Game/{gameId}/Update/{commentId}")]
        public async Task<ActionResult> UpdateCommentAsync([FromRoute] int gameId, [FromRoute] int commentId, [FromBody] CommentModel model)
        {
                model.GameId = gameId;
                model.Id = commentId;
          await commentService.UpdateCommentAsync(model);
          return Ok();
        }

        // task 3.4
        [HttpDelete("Game/{gameId}/Delete/{commentId}")]
        public async Task<ActionResult> DeleteComment([FromRoute] int? gameId, [FromRoute] int? commentId)
        {
          await commentService.DeleteCommentAsync(gameId, commentId);
          return Ok();
        }

        // task 3.5
        [HttpPut("Game/{gameId}/Restore/{commentId}")]
        public async Task<ActionResult> RestoreComment([FromRoute] int? gameId, [FromRoute] int? commentId)
        {
          await commentService.RestoreCommentAsync(gameId, commentId);
          return Ok();
        }

		// task 3.6
		[HttpPut("Game/{gameId}/ReplyOn/{commentId}")]
		public async Task<ActionResult> ReplyOnComment([FromRoute] int? gameId, [FromRoute] int? commentId, [FromBody] CommentModel model)
		{
			await commentService.ReplyOnCommentAsync(gameId, commentId, model);
			return Ok();
		}
	}
}

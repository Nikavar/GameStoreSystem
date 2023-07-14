using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service;
using GameStore.Service.Interfaces;
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


        [HttpGet("Game/{gameId}/Comment")]
        public async Task<ActionResult> GetComments([FromRoute] int gameId)
        {
            var model = await commentService.GetCommentsByGameIdAsync(gameId);
            return Ok(model);
        }
    }
}

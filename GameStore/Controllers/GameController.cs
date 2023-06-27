using AutoMapper;
using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var list = await gameService.GetAllGamesAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var game = await gameService.GetGameByIdAsync(id);
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] GameModel model)
        {
            await gameService.AddGameAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] GameModel model)
        {
            await gameService.UpdateGameAsync(model); 
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] GameModel model)
        {
            await gameService.DeleteGameAsync(model);
            return Ok();
        }
    }
}

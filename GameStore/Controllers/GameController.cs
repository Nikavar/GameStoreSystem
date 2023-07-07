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
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GameController(IGameService gameService, IGenreService genreService, IMapper mapper)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var gameList = await gameService.GetAllGamesAsync();

            return Ok(gameList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var game = await gameService.GetGameByIdAsync(id);
            return Ok(game);
        }

        // task 1.5

        [HttpGet("Find")]
        public async Task<ActionResult> GetByGerneAndName([FromQuery]int? genreId, string name)
        {
            if (genreId != null)
                var genres = genreService.GetGenreById(genreId);

            var game = await gameService.GetGameByIdAsync(id);
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] GameModel model)
        {
            if (await gameService.AddGameAsync(model))
            {
                return Ok("The Game created!");
            }

            return BadRequest("The Game could not be created!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] GameModel model)
        {
            if (gameService.IsGameModelValidate(model))
            {
                await gameService.UpdateGameAsync(model);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] GameModel model)
        {
            if (gameService.IsGameModelValidate(model))
            {
                await gameService.DeleteGameAsync(model);
                return Ok();
            }

            return BadRequest();
        }
    }
}

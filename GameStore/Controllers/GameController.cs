    using AutoMapper;
    using GameStore.Model.Models;
    using GameStore.Service;
    using GameStore.Service.Interfaces;
    using GameStore.Service.Models;
    using Mapster;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
        public async Task<ActionResult> GetByGenreAndName([FromQuery] int? genreId, string? name)
        {
            var result = await gameService.GetByGenreAndName(genreId, name);

            if(result == null)
            {
                return BadRequest("Something was wrong!");
            }

            return Ok(result.Adapt<GameModel>());
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

        // task 1.6
        [HttpPut("AddImageToGame")]
        public async Task<ActionResult> AddImageToGameAsync([FromBody] GameModel model)
        {
            if (gameService.IsGameModelValidate(model))
            {
                await gameService.AddImageToGame(model);
                return Ok();
            }

            return BadRequest();
        }

        // task 1.7
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, GameModel model)
        {
            if (gameService.IsGameModelValidate(model))
            {
                model.Id = id;
                await gameService.UpdateGameAsync(model);
                return StatusCode(201);
            }

            return BadRequest();
        }

        // task 1.8

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await gameService.DeleteGameAsync(id);
            }
            catch (NullReferenceException ex)
            {
                return StatusCode(404);
            }

            catch (Exception ex)
            {
                return StatusCode(400);
            }
            return BadRequest();
        }

    }
}

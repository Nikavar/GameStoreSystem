﻿using AutoMapper;
using GameStore.Model.Models;
using GameStore.Service;
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
        private readonly IGenreService genreService;
        private readonly IGameGenreService gameGenreService;
        private readonly IMapper mapper;

        public GameController(IGameService gameService, IGenreService genreService, IGameGenreService gameGenreService, IMapper mapper)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.gameGenreService = gameGenreService;
            this.mapper = mapper;
        }

        [HttpGet("AllGame")]
        public async Task<ActionResult> GetAllAsync()
        {
            var gameList = await gameService.GetAllGamesAsync();
            //var model = mapper.Map<IEnumerable<GameModel>>(gameList);
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
        public async Task<ActionResult> GetByGenreAndName([FromQuery]int? genreId, string? name)
        {
            var genreList = await genreService.GetAllGenresAsync();
            var gameList = await gameService.GetAllGamesAsync();
            var gameGenreList = await gameGenreService.GetAllGameGenreAsync();

            IEnumerable<Game> result;

            if (gameList != null && genreList != null && gameGenreList != null)
            {
                if (genreId != null && !string.IsNullOrEmpty(name))
                {
                    var getByNameAndGenreId = (from game in gameList
                                               join gameGenre in gameGenreList
                                               on game.Id equals gameGenre.GameId
                                               join genre in genreList
                                               on gameGenre.GenreId equals genre.Id
                                               where genre.Id == genreId && game.GameName?.ToLower() == name.ToLower()
                                               select game
                          );

                    result = getByNameAndGenreId;
                }


                else if (genreId == null && !string.IsNullOrEmpty(name))
                {
                    result = (from game in gameList
                              where game.GameName?.ToLower() == name.ToLower()
                              select game);
                }

                else if (genreId != null && string.IsNullOrEmpty(name))
                {
                    var getByGenreId = (from game in gameList
                                        join gameGenre in gameGenreList
                                        on game.Id equals gameGenre.GameId
                                        join genre in genreList
                                        on gameGenre.GenreId equals genre.Id
                                        where genre.Id == genreId
                                        select game
                                       );

                    result = getByGenreId;
                }

                else
                {
                    return BadRequest("You must enter GenreId or GameName");
                }
            }

            else
            {
                return BadRequest("Something was wrong!");
            }

            //return Ok(mapper.Map<IEnumerable<GameModel>>(gameList));
            return Ok(result.ToList().Adapt<GameModel>());
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

        [HttpPut("Update")]
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
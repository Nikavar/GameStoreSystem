using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameStore.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGenreService genreService;
        private readonly IGameGenreService gameGenreService;

        public GameService(IGameRepository gameRepo, IUnitOfWork unitOfWork, IMapper mapper,
            IGenreService genreService, IGameGenreService gameGenreService)         
        {
            this.gameRepository = gameRepo;
            this.unitOfWork = unitOfWork;

            this.genreService = genreService;
            this.gameGenreService = gameGenreService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await gameRepository.GetAllAsync();
        }

        public async Task<IEnumerable<GameModel>> GetManyGamesAsync(Expression<Func<Game, bool>> filter)
        {
            var entity = await gameRepository.GetManyAsync(filter);
            return mapper.Map<IEnumerable<GameModel>>(entity);
        }

        public async Task<GameModel> GetGameByIdAsync(params object[] key)
        {
            var entity = await gameRepository.GetByIdAsync(key);
            return mapper.Map<GameModel>(entity);
        }

        public async Task<bool> AddGameAsync(GameModel model)
        {
            var isValidate = IsGameModelValidate(model);

            if (isValidate)
            {
                await gameRepository.AddAsync(mapper.Map<Game>(model));
                return true;
            }

            return false;
        }

        public async Task DeleteGameAsync(GameModel model)
        {
            var isValidate = IsGameModelValidate(model);

            if (isValidate)
            {   
                await gameRepository.DeleteAsync(mapper.Map<Game>(model));
            }            
        }

        public async Task DeleteManyGamesAsync(Expression<Func<Game, bool>> filter)
        {
            await gameRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateGameAsync(GameModel model)
        {
            var isValidate = IsGameModelValidate(model);

            if (isValidate)
            {
                await gameRepository.UpdateAsync(mapper.Map<Game>(model));
            }
        }

        public bool IsGameModelValidate(GameModel model)
        {
            if (string.IsNullOrWhiteSpace(model.GameName) || string.IsNullOrWhiteSpace(model.Description))
                return false;

            return true;
        }

        public async Task AddImageToGame(GameModel model)
        {
            await gameRepository.AddImageToGame(mapper.Map<Game>(model));
        }

        public async Task<IEnumerable<GameModel>> GetByGenreAndName(int? genreId, string? gameName)
        {
            var genreList = await genreService.GetAllGenresAsync();
            var gameList = await GetAllGamesAsync();
            var gameGenreList = await gameGenreService.GetAllGameGenreAsync();

            IEnumerable<Game> result = new List<Game>();

            // To_Do put in GameService!!!
            if (gameList != null && genreList != null && gameGenreList != null)
            {
                if (genreId != null && !string.IsNullOrEmpty(gameName))
                {
                    var getByNameAndGenreId = (from game in gameList
                                               join gameGenre in gameGenreList
                                               on game.Id equals gameGenre.GameId
                                               join genre in genreList
                                               on gameGenre.GenreId equals genre.Id
                                               where genre.Id == genreId && game.GameName?.ToLower() == gameName.ToLower()
                                               select game
                          );

                    result = getByNameAndGenreId;
                }


                else if (genreId == null && !string.IsNullOrEmpty(gameName))
                {
                    result = (from game in gameList
                              where game.GameName?.ToLower() == gameName.ToLower()
                              select game);
                }

                else if (genreId != null && string.IsNullOrEmpty(gameName))
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

            }
            
            return mapper.Map<IEnumerable<GameModel>>(result);
        }
    }
}

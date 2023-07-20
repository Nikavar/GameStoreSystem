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
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IGameRepository gameRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGenreService genreService;
        private readonly IGameGenreService gameGenreService;

        public GameService(IGameRepository gameRepo, IGameGenreRepository gameGenreRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepo;
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

        // task 1.8 gameService
        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await GetGameByIdAsync(id);

            if (game != null)
            {
                var gameGenres = await gameGenreRepository.GetManyAsync(x => x.GameId == id);

                if (gameGenres != null)
                {
                    await gameGenreRepository.DeleteManyAsync(x => x.GameId == id);
                }
                await gameRepository.DeleteAsync(mapper.Map<GameModel, Game>(game));

                return true;
            }
            throw new NullReferenceException();
        }

        public async Task DeleteManyGamesAsync(Expression<Func<Game, bool>> filter)
        {
            await gameRepository.DeleteManyAsync(filter);
        }

        // task 1.4 update the game
        public async Task UpdateGameAsync(GameModel model)
        {
            var isValidate = IsGameModelValidate(model);

            if (isValidate)
            {
                await gameGenreRepository.DeleteManyAsync(x => x.GameId == model.Id);

                foreach(var genre in model.Genres)
                {
                    await gameGenreRepository.AddAsync(new GameGenre { GameId = model.Id, GenreId = genre.Id });
                }

                await gameRepository.UpdateAsync(mapper.Map<Game>(model));
            }
        }

        public bool IsGameModelValidate(GameModel model)
        {
            if (string.IsNullOrWhiteSpace(model.GameName) || string.IsNullOrWhiteSpace(model.Description))
                return false;

            return true;
        }

        // Fixed _ task 1.6
        public async Task AddImageToGame(GameModel model)
        {
            await gameRepository.UpdateAsync(mapper.Map<Game>(model));
        }

        public async Task<IEnumerable<GameModel>> GetByGenreAndName(int? genreId, string? gameName)
        {
            var genreList = await genreService.GetAllGenresAsync();
            var gameList = await GetAllGamesAsync();
            var gameGenreList = await gameGenreService.GetAllGameGenresAsync();

            IEnumerable<Game> result = new List<Game>();

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

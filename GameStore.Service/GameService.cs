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

namespace GameStore.Service
{
    public class GameService : IGameService
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IGameRepository gameRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GameService(IGameRepository gameRepo, IGameGenreRepository gameGenreRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepo;
            this.gameRepository = gameRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await gameRepository.GetAllAsync();
            //return mapper.Map<IEnumerable<GameModel>>(entity);
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
    }
}

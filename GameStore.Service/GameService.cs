using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GameService(IGameRepository gameRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.gameRepository = gameRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<GameModel>> GetAllGamesAsync()
        {
            var entity = await gameRepository.GetAllAsync();
            return mapper.Map<List<GameModel>>(entity);
        }
        public async Task<IReadOnlyList<GameModel>> GetManyGamesAsync(Expression<Func<Game, bool>> filter)
        {
            var entity = await gameRepository.GetManyAsync(filter);
            return mapper.Map<List<GameModel>>(entity);
        }

        public async Task<GameModel> GetGameByIdAsync(params object[] key)
        {
            var entity = await gameRepository.GetByIdAsync(key);
            return mapper.Map<GameModel>(entity);
        }

        public async Task<GameModel> AddGameAsync(GameModel model)
        {
            var entity = await gameRepository.AddAsync(mapper.Map<Game>(model));
            return mapper.Map<GameModel>(entity);
        }

        public async Task DeleteGameAsync(GameModel model)
        {
            var entity = mapper.Map<Game>(model);
            await gameRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyGamesAsync(Expression<Func<Game, bool>> filter)
        {
            await gameRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateGameAsync(GameModel model)
        {
            var entity = mapper.Map<Game>(model);
            await gameRepository.UpdateAsync(entity);
        }       
    }

    public interface IGameService
    {
        Task<IReadOnlyList<GameModel>> GetAllGamesAsync();
        Task<IReadOnlyList<GameModel>> GetManyGamesAsync(Expression<Func<Game, bool>> filter);
        Task<GameModel> GetGameByIdAsync(params object[] key);
        Task<GameModel> AddGameAsync(GameModel entity);
        Task UpdateGameAsync(GameModel entity);
        Task DeleteGameAsync(GameModel entity);
        Task DeleteManyGamesAsync(Expression<Func<Game, bool>> filter);
    }
}

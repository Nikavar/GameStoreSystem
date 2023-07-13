using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class GameGenreService : IGameGenreService
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GameGenreService(IGameGenreRepository gameRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.gameGenreRepository = gameRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<GameGenre>> GetAllGameGenresAsync()
        {
            return await gameGenreRepository.GetAllAsync();
        }
        public async Task<IEnumerable<GameGenre>> GetManyGameGenresAsync(Expression<Func<GameGenre, bool>> filter)
        {
            return await gameGenreRepository.GetManyAsync(filter);
        }

        public Task<GameGenre> GetGameGenreByIdAsync(params object[] key)
        {
            return gameGenreRepository.GetByIdAsync(key);
        }

        public async Task<GameGenre> AddGameGenreAsync(GameGenre entity)
        {
            return await gameGenreRepository.AddAsync(entity);
        }

        public async Task DeleteGameGenreAsync(GameGenre entity)
        {
            await gameGenreRepository.DeleteAsync(entity);
        }

        public async Task DeleteManyGameGenresAsync(Expression<Func<GameGenre, bool>> filter)
        {
            await gameGenreRepository.DeleteManyAsync(filter);
        }

        public async Task UpdateGameGenreAsync(GameGenre entity)
        {
            await gameGenreRepository.UpdateAsync(entity);
        }
    }
       
}

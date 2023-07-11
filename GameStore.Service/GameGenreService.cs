using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class GameGenreService : IGameGenreService
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IUnitOfWork unitOfWork;
        private IMapper mapper;

        public GameGenreService(IGameGenreRepository gameGenreRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameGenre>> GetAllGameGenreAsync()
        {
            return await gameGenreRepository.GetAllAsync();
        }
    }

}

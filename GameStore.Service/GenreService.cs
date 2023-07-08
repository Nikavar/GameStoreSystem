using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await genreRepository.GetAllAsync();
            //return mapper.Map<IEnumerable<GenreModel>>(entity);
        }

        public async Task<GenreModel> GetGenreByIdAsync(int id)
        {
            var entity = await genreRepository.GetByIdAsync(id);
            return mapper.Map<GenreModel>(entity);  
        }
    }

    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<GenreModel> GetGenreByIdAsync(int id);
    }
}

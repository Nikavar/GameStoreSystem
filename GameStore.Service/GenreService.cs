using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
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
        private readonly IUnitOfWork unitOfWork;

        public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            this.genreRepository = genreRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
           return await genreRepository.GetAllAsync();
        }
    }

    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
    }
}

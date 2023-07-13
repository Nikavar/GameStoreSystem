using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IDbFactory dbFactory) : base(dbFactory) 
        { 
        
        }
    }

    public interface IGenreRepository : IBaseRepository<Genre>
    {

    }
}

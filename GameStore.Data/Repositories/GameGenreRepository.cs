using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public class GameGenreRepository : BaseRepository<GameGenre>, IGameGenreRepository
    {
        public GameGenreRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IGameGenreRepository : IBaseRepository<GameGenre>
    {

    }
}

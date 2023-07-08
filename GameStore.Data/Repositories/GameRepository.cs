using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task AddImageToGame(Game entity)
        {
            await UpdateAsync(entity);
        }
    }

    public interface IGameRepository : IBaseRepository<Game>
    {
        Task AddImageToGame(Game entity);
    }
}

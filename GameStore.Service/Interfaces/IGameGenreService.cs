using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface IGameGenreService
    {
        Task<IEnumerable<GameGenre>> GetAllGameGenresAsync();
        Task<IEnumerable<GameGenre>> GetManyGameGenresAsync(Expression<Func<GameGenre, bool>> filter);
        Task<GameGenre> GetGameGenreByIdAsync(params object[] key);
        Task<GameGenre> AddGameGenreAsync(GameGenre entity);
        Task UpdateGameGenreAsync(GameGenre entity);
        Task DeleteGameGenreAsync(GameGenre entity);
        Task DeleteManyGameGenresAsync(Expression<Func<GameGenre, bool>> filter);
    }
}

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
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<IEnumerable<GameModel>> GetManyGamesAsync(Expression<Func<Game, bool>> filter);
        Task<GameModel> GetGameByIdAsync(params object[] key);
        Task<bool> AddGameAsync(GameModel entity);
        Task UpdateGameAsync(GameModel entity);
        Task DeleteGameAsync(int id);
        Task DeleteManyGamesAsync(Expression<Func<Game, bool>> filter);
        bool IsGameModelValidate(GameModel model);
        Task AddImageToGame(GameModel model);
    }
}

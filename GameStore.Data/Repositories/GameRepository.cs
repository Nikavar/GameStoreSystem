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

        public override async Task<bool> IsModelValidate<TModel>(TModel model)
        {
            if(model != null)
            {
                var gameName = model.GetType().GetProperty("GameName").ToString();
                var description = model.GetType().GetProperty("Description").ToString();

                if (String.IsNullOrEmpty(gameName) || String.IsNullOrEmpty(description))
                    return true;
            }

            return false;
        }
    }

    public interface IGameRepository : IBaseRepository<Game>
    {
        
    }
}

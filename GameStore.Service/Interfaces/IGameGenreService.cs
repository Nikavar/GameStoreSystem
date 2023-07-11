using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface IGameGenreService
    {
        Task<IEnumerable<GameGenre>> GetAllGameGenreAsync();
    }
}

using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface IAccountService
    {
        Task<Account> RegisterAccountAsync(AccountModel model);
        Task<Account> LoginAccountAsync(string username, string password);
    }
}

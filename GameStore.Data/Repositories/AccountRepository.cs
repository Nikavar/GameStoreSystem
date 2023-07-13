using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public async Task<Account> RegisterAccount(Account account)
        {
            await dbSet.AddAsync(account);
            await dataContext.SaveChangesAsync();
            return account;
        }
    }

    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> RegisterAccount(Account account);
    }
}

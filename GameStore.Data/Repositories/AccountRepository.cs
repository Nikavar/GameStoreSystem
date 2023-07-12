using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public async Task<Account> LoginAccountAsync(string username, string password)
        {
            return await dbSet.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<Account> RegisterAccountAsync(Account account)
        {
            await base.AddAsync(account);
            return account;
        }
    }

    public interface IAccountRepository : IBaseRepository<Account>
    { 
        Task<Account> RegisterAccountAsync(Account Account);
        Task<Account> LoginAccountAsync(string username, string password);

    }
}

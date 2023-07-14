using GameStore.Data.Infrastructure;
using GameStore.Model.Models;


namespace GameStore.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public async Task<Account> RegisterAccountAsync(Account account)
        {
            await base.AddAsync(account);
            return account;
        }
    }

    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> RegisterAccountAsync(Account account);
    }
}

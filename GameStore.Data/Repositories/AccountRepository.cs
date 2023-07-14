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
            return await dbSet.Where(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
        }

    }

    public interface IAccountRepository : IBaseRepository<Account>
    {

        Task<Account> LoginAccountAsync(string username, string password);
    }
}

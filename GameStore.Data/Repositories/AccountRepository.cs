using GameStore.Data.Infrastructure;
using GameStore.Model.Models;


namespace GameStore.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }
    }

    public interface IAccountRepository : IBaseRepository<Account>
    {

    }
}

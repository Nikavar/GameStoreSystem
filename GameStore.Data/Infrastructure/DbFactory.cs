using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        GameStoreContext? dbContext;

        private readonly DbContextOptions<GameStoreContext> options;

        public DbFactory(DbContextOptions<GameStoreContext> options)
        {
            this.options = options;
        }
        public GameStoreContext Init()
        {
            return dbContext ?? (dbContext = new GameStoreContext(options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}

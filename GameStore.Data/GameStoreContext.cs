using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data
{
	public class GameStoreContext : DbContext
	{
        public GameStoreContext() : base()
        {

        }

        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {

        }

        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Game>? Games { get; set; }
        public DbSet<GameGenre>? GameGenres { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderGame>? OrderGames { get; set; }
        public DbSet<PaymentType>? PaymentTypes { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<RoleUser>? UserRoles { get; set; }
        public DbSet<User>? Users { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}

using GameStore.Data.Configuration;
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
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Game>? Games { get; set; }
        public DbSet<GameGenre>? GameGenres { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<RoleAccount>? AccountRoles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData
                (
                new Genre
                {
                    Id = 1,
                    GenreName = "Strategy",
                    ParentId = null,                   
                },
                new Genre
                {
                    Id = 2,
                    GenreName = "Rally",
                    ParentId = 1,
                },
                new Genre
                {
                    Id = 3,
                    GenreName = "Arcade",
                    ParentId = 1,
                }, new Genre
                {
                    Id = 4,
                    GenreName = "Formula",
                    ParentId = 1,
                }, new Genre
                {
                    Id = 5,
                    GenreName = "Off - road",
                    ParentId = 1,
                },
                new Genre
                {
                    Id = 6,
                    GenreName = "Rpg",
                    ParentId = null,
                }, 

                new Genre
                {
                    Id = 7,
                    GenreName = "Sports",
                    ParentId = null,
                }, 

                new Genre
                {
                    Id = 8,
                    GenreName = "Races",
                    ParentId = null,
                },

                new Genre
                {
                    Id = 9,
                    GenreName = "Action",
                    ParentId = null,
                },
                new Genre
                {
                    Id = 10,
                    GenreName = "Fps",
                    ParentId = 9,
                }, new Genre
                {
                    Id = 11,
                    GenreName = "Tps",
                    ParentId = 9,
                }, new Genre
                {
                    Id = 12,
                    GenreName = "Misc",
                    ParentId = 9,
                },
                new Genre
                {
                    Id = 13,
                    GenreName = "Adventure",
                    ParentId = null,
                },
                new Genre
                {
                    Id = 14,
                    GenreName = "Puzzle & skill",
                    ParentId = null,
                },
                new Genre
                {
                    Id = 15,
                    GenreName = "Other",
                    ParentId = null,
                }
            );

            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());

		}
    }
}

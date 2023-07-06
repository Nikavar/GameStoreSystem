using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Configuration
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.ToTable("GameGenres");

            builder.HasKey(x => x.Id);

            // relations

            builder.HasOne(x => x.Game)
                .WithMany(y => y.GameGenres);

            builder.HasOne(x => x.Genre)
                .WithMany(y => y.GenreGames);            
        }
    }
}

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
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            builder.Property(g => g.GameName)
                .IsUnicode(true);

            builder.Property(g => g.Price)
                .HasColumnType("decimal");

            builder.Property(g => g.Currency)
                .IsUnicode (false);
        }
    }
}

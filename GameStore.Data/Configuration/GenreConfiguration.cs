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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {

        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.GenreName)
                .IsRequired()
                .IsUnicode(true);

            // relations

            builder.HasOne(x => x.ParentGenre)
                .WithMany(x => x.Genres)
                .IsRequired(false)
                .HasForeignKey(k => k.ParentId);
        } 
    }
}

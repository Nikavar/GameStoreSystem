using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentContent)
                .HasMaxLength(600)
                .IsUnicode(true);

            builder.Property(c => c.GameId)
                .HasColumnName("GameId");

            builder.Property(c => c.ReplyId)
                .HasColumnName("ReplyId");

            // relations
            builder.HasOne(g => g.Game)
                .WithMany(c => c.Comments);

            builder.HasMany(x => x.Replies)
                .WithOne(x => x.Reply)
                .IsRequired(false)
                .HasForeignKey(c => c.ReplyId);
        }
    }
}

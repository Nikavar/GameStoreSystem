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
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.ToTable("RoleUsers");

            builder.HasKey(ru => ru.Id);

            builder.Property(ru => ru.UserId)
                .IsUnicode(false);

            builder.Property(ru => ru.RoleId)
                .IsUnicode(false);

            // relations
            builder.HasOne(u => u.User)
                .WithMany(ru => ru.RoleUsers);

            builder.HasOne(r => r.Role)
                .WithMany(ru => ru.RoleUsers);
        }
    }
}

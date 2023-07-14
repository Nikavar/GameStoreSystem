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
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleAccount>
    {
        public void Configure(EntityTypeBuilder<RoleAccount> builder)
        {
            builder.ToTable("RoleAccounts");

            builder.HasKey(ru => ru.Id);

            builder.Property(ru => ru.AccountId)
                .IsUnicode(false);

            builder.Property(ru => ru.RoleId)
                .IsUnicode(false);

            // relations
            builder.HasOne(u => u.Account)
                .WithMany(ru => ru.AccountRoles);

            builder.HasOne(r => r.Role)
                .WithMany(ru => ru.RoleAccounts);
        }
    }
}

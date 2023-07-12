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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder.ToTable("Accounts");

            builder.HasKey(acc => acc.Id);

            builder.Property(acc => acc.FirstName)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(acc => acc.LastName)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(acc => acc.Email)
                .IsUnicode (false);

            // relations

            builder.HasMany(ar => ar.AccountRoles)
                .WithOne(acc => acc.Account);

            builder.HasMany(o => o.AccountRoles)
                .WithOne(acc => acc.Account);

        }       
    }
}

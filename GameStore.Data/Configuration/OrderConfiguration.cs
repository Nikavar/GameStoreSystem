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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.AccountId)
                .HasColumnName("AccountId");

            builder.Property(o => o.Comments)
                .HasMaxLength(600)
                .IsUnicode(true);

            // relations
            builder.HasOne(o => o.Account)
                .WithMany(o => o.Orders);

            builder.HasOne(o => o.PaymentType)
                .WithMany(o => o.Orders);

        }
    }
}

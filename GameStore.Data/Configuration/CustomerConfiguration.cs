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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.ToTable("Customers");

            builder.HasKey(cust => cust.Id);

            builder.Property(cust => cust.FirstName)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(cust => cust.LastName)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(cust => cust.PhoneNumber)
                .HasMaxLength(30);

            builder.Property(cust => cust.Email)
                .IsUnicode (false);

            builder.Property(cust => cust.UserId)
                .HasColumnName("UserId");

            // relations
            builder.HasOne(u => u.User)
                .WithMany(cust => cust.Customers);

        }       
    }
}

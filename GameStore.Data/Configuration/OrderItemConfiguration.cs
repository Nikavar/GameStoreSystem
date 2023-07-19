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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(x => x.Id);

            // relations

            builder.HasOne(x => x.Order)
                .WithMany(y => y.OrderItems);

            builder.HasOne(x => x.Game)
                .WithMany(y => y.GameOrderItems);            
        }
    }
}

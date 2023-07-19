using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Configuration
{
	public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
	{
		public void Configure(EntityTypeBuilder<OrderDetails> builder)
		{
			builder.ToTable("OrderDetails");

			builder.Property(od => od.FirstName)
				.IsRequired();
			builder.Property(od => od.LastName)
				.IsRequired();
			builder.Property(od => od.Email)
				.IsRequired();
			builder.Property(od => od.Phone)
				.IsRequired();
			builder.Property(od => od.PaymentType)
				.IsRequired();

			builder.HasOne(o => o.Order)
				.WithMany(od => od.OrderDetails);
		}
	}
}

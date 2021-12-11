using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace palugada_api.Entities {
    public class OrderDetail {
        public int OrderDetailId { get; set; }
        public int Amount { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }

    public class OrderDetailModelBuilder : IEntityTypeConfiguration<OrderDetail> {
        public void Configure(EntityTypeBuilder<OrderDetail> builder) {

            builder.HasKey(e => e.OrderDetailId);
            builder.Property(e => e.OrderDetailId).UseIdentityByDefaultColumn();

        }
    }
}

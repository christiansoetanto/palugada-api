using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace palugada_api.Entities {
    public class OrderHeader {
        public int OrderHeaderId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }

    public class OrderHeaderModelBuilder : IEntityTypeConfiguration<OrderHeader> {
        public void Configure(EntityTypeBuilder<OrderHeader> builder) {

            builder.HasKey(e => e.OrderHeaderId);
            builder.Property(e => e.OrderHeaderId).UseIdentityByDefaultColumn();

        }
    }
}

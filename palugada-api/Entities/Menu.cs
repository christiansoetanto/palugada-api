using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace palugada_api.Entities {
    public class Menu {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class MenuModelBuilder : IEntityTypeConfiguration<Menu> {
        public void Configure(EntityTypeBuilder<Menu> builder) {

            builder.HasKey(e => e.MenuId);
            builder.Property(e => e.MenuId).UseIdentityByDefaultColumn();

        }
    }
}

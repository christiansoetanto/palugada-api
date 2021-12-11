using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace palugada_api.Entities {
    public class User {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Menu> Menu { get; set; }
    }

    public class UserModelBuilder : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.HasKey(e => e.UserId);
            builder.Property(e => e.UserId).UseIdentityByDefaultColumn();

        }
    }
}

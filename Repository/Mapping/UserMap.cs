using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(m => m.Id);
            this.Property(m => m.UserName).IsRequired().HasMaxLength(10);
            this.Property(m => m.Account).IsRequired().HasMaxLength(10);
            this.Property(m => m.Password).HasMaxLength(16);

        }
    }
}

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
            this.Property(m => m.Account).HasMaxLength(16);
            this.Property(m => m.Password).HasMaxLength(16);
            this.Property(m => m.UserName).HasMaxLength(20);
            this.Property(m => m.Email).HasMaxLength(30);
            this.Property(m => m.HeadImage).HasMaxLength(50);
            this.Property(m => m.CreateTime).IsRequired();
        }
    }
}

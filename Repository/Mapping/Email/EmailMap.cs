using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapping
{
    internal class EmailMap: EntityTypeConfiguration<Email>
    {
        public EmailMap()
        {
            this.ToTable("Email");
            this.HasKey(m => m.Id);
            this.Property(m => m.From).HasMaxLength(30);
            this.Property(m => m.EmailStatus).HasMaxLength(10);
        }
    }
}

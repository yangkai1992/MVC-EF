using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public Guid GroupId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

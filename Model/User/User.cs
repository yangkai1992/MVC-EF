using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage="请输入密码")]
        public string Password { get; set; }

        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

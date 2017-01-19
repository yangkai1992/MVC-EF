using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LoginHistory
    {
        public LoginHistory()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage="请输入账号")]
        public string Account { get; set; }

        [NotMapped]
        [Required(ErrorMessage="请输入密码")]
        public string Password { get; set; }

        [NotMapped]
        public string Code { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int ErrorTimes { get; set; }
    }
}

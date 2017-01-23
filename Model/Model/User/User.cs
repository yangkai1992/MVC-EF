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
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage="请输入账号")]
        [DisplayName("账号")]
        public string Account { get; set; }

        [Required(ErrorMessage="请输入密码")]
        [DisplayName("密码")]
        public string Password { get; set; }

        [Required(ErrorMessage="请输入昵称")]
        [DisplayName("昵称")]
        public string UserName { get; set; }

        [DisplayName("邮箱")]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$",ErrorMessage="请输入正确的邮箱")]
        public string Email { get; set; }

        [DisplayName("头像")]
        public string HeadImage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Email
    {
        public Email()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [DisplayName("发件人")]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请输入正确的邮箱")]
        [Required(ErrorMessage = "请填写发件人")]
        public string From { get; set; }

        [DisplayName("收件人")]
        [Required(ErrorMessage = "请填写收件人")]
        public string To { get; set; }

        [DisplayName("主题")]
        public string Subject { get; set; }

        /// <summary>
        /// 发件时间 收件时间 删除时间等
        /// </summary>
        public DateTime EmailTime { get; set; }

        public int ReadStatus { get; set; }
        public string EmailStatus { get; set; }

        [DisplayName("正文")]
        public string Content { get; set; }

        [DisplayName("附件")]
        public string Attachment { get; set; }
    }
}

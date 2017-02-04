using Common;
using Hangfire;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService:IEmailService
    {
        private readonly IRepository<Email> _emailRepository;
        private readonly IDbContext _dbContext;

        public EmailService(IRepository<Email> emailRepository,IDbContext dbContext)
        {
            this._emailRepository = emailRepository;
            this._dbContext = dbContext;
        }

        public void SentEmail(Email model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Email");
            }
            _emailRepository.Insert(model);

            BackgroundJob.Enqueue(() => SendEmail(model));
        }


        public List<Email> PageSearch(int pageIndex, int pageSize, out int totalCount, IEnumerable<string> wheres = null, string orderBy = "", params System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            string sql = SQLUtls.GeneratePageSearchSQL(pageIndex, pageSize, "[email]", wheres, orderBy);
            return _dbContext.PageSearch<Email>(sql, out totalCount, sqlParameters);
        }


        public Email GetEmail(Guid id)
        {
            return this._emailRepository.GetById(id);
        }


        public static void SendEmail(Email model)
        {
            MailMessage message = new MailMessage();
            MailAddress fromAddr = new MailAddress("2126517797@qq.com");
            message.From = fromAddr;
            message.To.Add(model.To);
            message.Subject = model.Subject;
            message.Body = model.Content;
            SmtpClient client = new SmtpClient("smtp.qq.com", 25);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("2126517797@qq.com", "gykjrqfjpzcubjfj");
            client.Send(message);
        }
    }
}

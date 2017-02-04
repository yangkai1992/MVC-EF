using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EmailController : AuthorizationController
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailList(string emailType,int pageIndex)
        {
            if(pageIndex<1)
                pageIndex=1;

            List<string> wheres = new List<string>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            wheres.Add(" EmailStatus=@EmailStatus");
            sqlParameters.Add(new SqlParameter("@EmailStatus",emailType));

            int totalCount;
            List<Email> emails= this._emailService.PageSearch(pageIndex, 15, out totalCount, wheres, "order by emailTime desc", sqlParameters.ToArray());
            return PartialView("_EmailBox", emails);
        }

        public ActionResult WriteEmail()
        {
            return PartialView("_Compose");
        }

        public ActionResult ReadEmail(Guid id)
        {
            Email email = this._emailService.GetEmail(id);
            return PartialView("_ReadEmail",email);
        }

        [ValidateInput(false)]
        public ActionResult SentEmail(Email model)
        {
            model.UserId = CurrentUser.Id;
            model.EmailTime = DateTime.Now;
            model.From = CurrentUser.Email;
            model.EmailStatus = EmailStatusEnum.sent.ToString();

            this._emailService.SentEmail(model);

            return RedirectToAction("Index");
        }
    }
}
using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UserController : AuthorizationController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            int pageIndex = WebHelper.GetIntQuery("PageIndex");
            if (pageIndex < 1)
                pageIndex = 1;

            List<string> wheres = new List<string>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            string account = WebHelper.GetStringQuery("account");
            if (!string.IsNullOrEmpty(account))
            {
                wheres.Add(" Account=@Account");
                sqlParameters.Add(new SqlParameter("@Account", account));
                queryParams.Add("account", account);
            }

            string email = WebHelper.GetStringQuery("email");
            if (!string.IsNullOrEmpty(email))
            {
                wheres.Add(" Email=@Email");
                sqlParameters.Add(new SqlParameter("@Email", email));
                queryParams.Add("email", email);
            }

            string userName = WebHelper.GetStringQuery("userName");
            if (!string.IsNullOrEmpty(userName))
            {
                wheres.Add(" UserName = @UserName");
                sqlParameters.Add(new SqlParameter("@UserName", userName));
                queryParams.Add("userName", userName);
            }

            DateTime startTime = WebHelper.GetDateTimeQuery("startTime");
            DateTime endTime = WebHelper.GetDateTimeQuery("endTime");
            if (startTime > DateTime.MinValue && endTime > DateTime.MinValue)
            {
                wheres.Add(" CreateTime between @startTime and @endTime");
                sqlParameters.Add(new SqlParameter("@startTime", startTime));
                sqlParameters.Add(new SqlParameter("@endTime", endTime));
                queryParams.Add("startTime", startTime.ToString("MM/dd/yyyy"));
                queryParams.Add("endTime", endTime.ToString("MM/dd/yyyy"));
            }

            int totalCount;
            List<Model.User> users = _userService.PageSearch(pageIndex, 10, out totalCount,wheres,"",sqlParameters.ToArray());

            users.ForEach(m => m.HeadImage = GetHeadImageUrl(m.HeadImage));

            ViewBag.PageInfo = new PagingModel { PageIndex = pageIndex, PageSize = 10, TotalCount = totalCount, Url = Url.Content("~/User/Index"),QueryParams=queryParams };
            return View(users);
        }

        public ActionResult Detail(Guid id)
        {
            Model.User user = _userService.GetUser(id);
            user.HeadImage = GetHeadImageUrl(user.HeadImage);
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Model.User user = _userService.GetUser(id);
            user.HeadImage = GetHeadImageUrl(user.HeadImage);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.User user, HttpPostedFileBase newHeadImage)
        {
            if (newHeadImage != null)
            {
                user.HeadImage = newHeadImage.FileName;
                newHeadImage.SaveAs(Server.MapPath(Path.Combine( FilePathConfig.HEADIMAGE_PATH,newHeadImage.FileName)));
            }

            Model.User oldUser = _userService.GetUser(user.Id);
            EFHelper.UpdateModel<Model.User>(user, oldUser);
            _userService.UpdateUser(oldUser);

            if (user.Id == CurrentUser.Id)
            {
                CurrentUser = oldUser;
            }
            return Redirect(Url.Content("~/User/Detail/" + user.Id));
        }

        private string GetHeadImageUrl(string headImage)
        {
            if (!string.IsNullOrEmpty(headImage))
                return string.Format("{0}/{1}", FilePathConfig.HEADIMAGE_URL, headImage);
            else
                return string.Format("{0}/{1}", FilePathConfig.HEADIMAGE_URL, "default.jpg");
        }
    }
}
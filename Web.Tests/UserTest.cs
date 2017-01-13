using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Repository;
using Model;


namespace Web.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUser()
        {
            IDbContext context = new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1") as IDbContext;
            IRepository<User> repository = new EfRepository<User>(context);
            IRepository<LoginHistory> loginHistory = new EfRepository<LoginHistory>(context);
            UserService userService = new UserService(repository,loginHistory);

            User user = new User()
            {
                Account = "123",
                Password = null,
                UserName = "杨凯",
                CreateTime = DateTime.Now
            };

            userService.CreateUser(user);
        }
    }
}

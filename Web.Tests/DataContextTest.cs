using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Tests
{
    [TestClass]
    public class DataContextTest
    {
        [TestMethod]
        public void ExecuteCommandTest()
        {
            IDbContext context = new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1") as IDbContext;

            var result = context.ExecuteSQl("select count(*) from [user]");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void SqlQueryTest()
        {
            IDbContext context = new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1") as IDbContext;

            var result = context.PageSearch("select * from [user]");
            
        }
    }
}

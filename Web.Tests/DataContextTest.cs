using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
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
        public void PageSearchTest()
        {
            IDbContext context = new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1") as IDbContext;
            int count = 0;
            var result = context.PageSearch<User>(@"select top 5 * from 
                                            (select row_number()over(order by id)rownumber,* from [user])a
                                            where rownumber>5
                                            select count(*) from [user]", out count);
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void SqlQueryTest()
        {
            IDbContext context = new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1") as IDbContext;

            var result = context.ExecuteSQL<User>("select * from [user]");
            Assert.IsTrue(result.Count > 0);
        }
    }
}

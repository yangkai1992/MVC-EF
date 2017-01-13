using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Web.Tests
{
    [TestClass()]
    public class CommonTest
    {
        [TestMethod()]
        public void LogTest()
        {
            LogHelper.Error("sdffff");
        }
    }
}

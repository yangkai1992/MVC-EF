using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;

namespace Web.Tests
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void CacheSet()
        {
            RedisCache cache = new RedisCache();
            var test = new { Name = "yangkai", Age = 45 };

            cache.Set("yang1", test, 10);
        }

        [TestMethod]
        public void CacheGet()
        {
            RedisCache cache = new RedisCache();
            var result = cache.Get<string>("yang");

            Assert.AreEqual(result, "456");
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityServiceClient.Common;

namespace CityList.Test
{
    [TestClass]
    public class UnixTimeTest
    {
        [TestMethod]
        public void ConvertTimeTest()
        {
            var dt = DateTime.Parse("2010-06-05T10:30:50");
            Assert.AreEqual(dt, ConvertTimeTest(dt));

            dt = DateTime.Parse("2019-03-08T11:25:15");
            Assert.AreEqual(dt, ConvertTimeTest(dt));

        }

        #region Implementation

        public DateTime ConvertTimeTest(DateTime dt)
        {
            var ut = dt.ToUnixTime();

            return ut.FromUnixTime();

        }

        #endregion
    }
}

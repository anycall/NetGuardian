using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGuardian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGuardian.Tests
{
    [TestClass()]
    public class IPHelperTests
    {
        [TestMethod()]
        public void ConvertIPAreaTest()
        {
            var testString1 = "192.168.1.10-20";
            
            var result1 = IPHelper.ConvertIPArea(testString1);

            Assert.Fail();
        }
    }
}
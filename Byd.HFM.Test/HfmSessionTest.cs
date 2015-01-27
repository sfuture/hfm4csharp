using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Byd.HFM.Test
{
    [TestFixture()]
    public class HfmSessionTest
    {
        [SetUp()]
        public void SetUp()
        {
            
        }

        [TearDown()]
        public void TearDown()
        {
        }

        [Test()]
        public void TestConnect()
        {
            bool connected = HfmSession.TestConnect("", "admin", "weilai#00", "192.168.192.188", "Financial Management", "GLPCON");
            Assert.AreEqual(connected,true);

            connected = HfmSession.TestConnect("", "admin", "error password", "192.168.192.188", "Financial Management", "GLPCON");
            Assert.AreEqual(connected, false);
         }


    }
}

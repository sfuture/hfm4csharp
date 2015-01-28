using Byd.HFM.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byd.HFM.Test
{
    [TestFixture()]
    public class HfmDimensionTest
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
        public void TestEnumMembers2()
        {
            HfmSession hfmSession = HfmSession.CreateSession("", "admin", "weilai#00", "192.168.192.188", "Financial Management","GLPCON");
            HfmDimension hfmDimension = hfmSession.CreateDimension();
            var ret = hfmDimension.EnumMembers2(HfmDimensionType.Account);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Scenario);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Custom1);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Custom2);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Custom3);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Custom4);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Entity);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Icp);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Period);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.View);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Year);
            Assert.Less(0, ret.Count());
            ret = hfmDimension.EnumMembers2(HfmDimensionType.Value);
            Assert.Less(0, ret.Count());

        }

        [Test()]
        public void TestGetMember()
        {
            HfmSession hfmSession = HfmSession.CreateSession("", "admin", "weilai#00", "192.168.192.188", "Financial Management", "GLPCON");
            HfmDimension hfmDimension = hfmSession.CreateDimension();
            var ret = hfmDimension.EnumMembers2(HfmDimensionType.Account);
            for (int index = 0; index < ret.Count && index < 20; index++)
            {
                DimensionMember member = ret[index];
                int id = hfmDimension.GetMemberID(HfmDimensionType.Account, member.MemberLabel);
                Assert.AreEqual(id, member.MemberID);
                string label = hfmDimension.GetMemberLabel(HfmDimensionType.Account, member.MemberID);
                Assert.AreEqual(label, member.MemberLabel);

            }

        }
    }
}

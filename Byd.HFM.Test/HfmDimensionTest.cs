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
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmDimension hfmDimension = hfmSession.GetDimension();
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
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmDimension hfmDimension = hfmSession.GetDimension();
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

        [Test()]
        public void TestEnumEntityMembers()
        {
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmDimension hfmDimension = hfmSession.GetDimension();
            var ret = hfmDimension.EnumEntityMembers();
            var retv = ret.FirstOrDefault(c => c.Description == "GLP Wuxi Logistics Development Co.Ltd.");

        }

        [Test()]
        public void TestEnumAccountMembers()
        {
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmDimension hfmDimension = hfmSession.GetDimension();
            var ret = hfmDimension.EnumAccountMembers();
            var retv = ret.FirstOrDefault(c => c.Description == "HisRate_Acquisition");

        }

        [Test()]
        public void TestEnumCustomMembers()
        {
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmDimension hfmDimension = hfmSession.GetDimension();
            var ret = hfmDimension.EnumCustomMembers(HfmDimensionType.Custom2);
            var retv = ret.FirstOrDefault(c => c.Description == "TOPC2");

        }
    }
}

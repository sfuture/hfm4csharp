using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byd.HFM.Model
{
    public class DimensionMember
    {
        public int MemberID { get; set; }
        public int ParentID { get; set; }
        public string MemberLabel { get; set; }
        public string Description { get; set; }
        public int NumChildren { get; set; }
    }
}

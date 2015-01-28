using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byd.HFM.Model
{
    public class DimensionTreeModel
    {
        public string Type { get; set; }
        public string ParentCode { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public int Order { get; set; }
    }
}

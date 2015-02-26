using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class REPORT
    {
        [XmlAttribute(AttributeName = "NAME")]
        public string NAME;

        [XmlAttribute(AttributeName = "TYPE")]
        public string TYPE;
    }
}

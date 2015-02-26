using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class NODE
    {
        [XmlElement(ElementName = "PARENT")]
        public string PARENT;

        [XmlElement(ElementName = "CHILD")]
        public string CHILD;

        [XmlElement(ElementName = "AT")]
        public AT[] AT;
    }
}

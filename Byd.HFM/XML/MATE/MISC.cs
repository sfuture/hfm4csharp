using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class MISC
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name;

        [XmlElement(ElementName = "MISCENTRY")]
        public MISCENTRY[] MISCENTRY;

        [XmlElement(ElementName = "DESCRIPTION")]
        public DESCRIPTION[] DESCRIPTION;

    }
}

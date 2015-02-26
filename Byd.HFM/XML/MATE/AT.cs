using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class AT
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name;

        [XmlText]
        public string Text;
    }
}

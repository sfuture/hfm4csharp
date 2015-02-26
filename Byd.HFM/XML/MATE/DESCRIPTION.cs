using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class DESCRIPTION
    {
        [XmlAttribute(AttributeName = "Language")]
        public string Language;

        [XmlText]
        public string Text;
    }
}

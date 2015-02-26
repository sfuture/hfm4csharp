using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class MEMBER
    {
        [XmlElement(ElementName = "LABEL")] 
        public string LABEL;

        [XmlElement(ElementName = "AT")] 
        public AT[] AT;

        [XmlElement(ElementName = "DEFAULTPARENT")] 
        public string DEFAULTPARENT;

        [XmlElement(ElementName = "DESCRIPTION")]
        public DESCRIPTION[] DESCRIPTION;
    }
}

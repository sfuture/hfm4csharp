using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class MEMBERS
    {
        [XmlElement(ElementName = "MEMBER")] 
        public MEMBER[] MEMBER;
    }
}

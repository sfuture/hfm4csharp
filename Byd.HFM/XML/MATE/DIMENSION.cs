using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    public class DIMENSION
    {
        [XmlAttribute(AttributeName = "Name")] 
        public  string Name;

        [XmlElement(ElementName = "MEMBERS")] 
        public MEMBERS MEMBERS;

        [XmlElement(ElementName = "HIERARCHY")] 
        public HIERARCHY HIERARCHY;
    }
}

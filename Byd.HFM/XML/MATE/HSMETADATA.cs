using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Byd.HFM.XML.MATE
{
    [XmlRoot(ElementName = "HSMETADATA")]
    public class HSMETADATA
    {
        [XmlElement(ElementName = "SCHEMA_VERSION")] 
        public string SCHEMA_VERSION;

        [XmlElement(ElementName = "REPORT")]
        public REPORT REPORT;

        [XmlElement(ElementName = "PRODUCT_ID")] 
        public string PRODUCT_ID;

        [XmlElement(ElementName = "LANGUAGES")] 
        public LANGUAGES LANGUAGES;

        [XmlElement(ElementName = "MISC")]
        public MISC[] MISC;

        [XmlElement(ElementName = "DIMENSION")]
        public DIMENSION[] DIMENSION;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byd.HFM.Model
{
    public class DimensionModel
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }

        public string CodeName1 { get; set; }

        public string CodeName2 { get; set; }
        //#S
        public int DefaultFreq { get; set; }
        public int DefaultView { get; set; }
        public int ZeroViewForNonadj { get; set; }
        public int ZeroViewForAdj { get; set; }
        public bool ConsolidateYTD { get; set; }
        public string UserDefined1 { get; set; }
        public string UserDefined2 { get; set; }
        public string UserDefined3 { get; set; }
        public bool SupportsProcessManagement { get; set; }
        public int SecurityClass { get; set; }
        public int MaximumReviewLevel { get; set; }
        public bool UsesLineItems { get; set; }
        //public string EnableDataAudit { get; set; }
        //public string DefFreqForICTrans { get; set; }
        public int PhasedSubStartYear { get; set; }

        //#E
        public string DefCurrency { get; set; }
        public bool AllowAdjs { get; set; }
        public bool IsICP { get; set; }
        public bool AllowAdjFromChildren { get; set; }
        public string HoldingCompany { get; set; }
        public int SecurityAsPartner { get; set; }

        //#A
        public short AccountType { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsConsolidated { get; set; }
        //public string IsICP { get; set; }
        public string PlugAcct { get; set; }
        public string Custom1TopMember { get; set; }
        public string Custom2TopMember { get; set; }
        public string Custom3TopMember { get; set; }
        public string Custom4TopMember { get; set; }
        public short NumDecimalPlaces { get; set; }
        //public string UsesLineItems { get; set; }
        public bool EnableCustom1Aggr { get; set; }
        public bool EnableCustom2Aggr { get; set; }
        public bool EnableCustom3Aggr { get; set; }
        public bool EnableCustom4Aggr { get; set; }
        public string XBRLTags { get; set; }
        public int ICPTopMember { get; set; }
        //public string EnableDataAudit { get; set; }
        public string CalcAttribute { get; set; }
        public int SubmissionGroup { get; set; }

        //#C1,C2,C3,C4
        public bool SwitchSignForFlow { get; set; }
        public bool SwitchTypeForFlow { get; set; }

        public string DefaultParent { get; set; }

        public bool MergerNodeFlag { get; set; }
    }
}

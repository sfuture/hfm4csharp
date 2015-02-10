

namespace Byd.HFM.Model
{
    public class DimensionMember
    {
        public int MemberID { get; set; }
        public int ParentID { get; set; }
        public string MemberLabel { get; set; }
        public string Description { get; set; }
        public int NumChildren { get; set; }


        public string DefCurrency { get; set; }
        public bool AllowAdjs { get; set; }
        public bool AllowAdjFromChildren { get; set; }
        public bool IsICP { get; set; }
        public string HoldingCompany { get; set; }
        public int SecurityAsPartner { get; set; }
        public int SecurityClass { get; set; }
        public string UserDefined1 { get; set; }
        public string UserDefined2 { get; set; }
        public string UserDefined3 { get; set; }
        public short AccountType { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsConsolidated { get; set; }
        public string PlugAcct { get; set; }
        public string Custom1TopMember { get; set; }
        public string Custom2TopMember { get; set; }
        public string Custom3TopMember { get; set; }
        public string Custom4TopMember { get; set; }
        public short NumDecimalPlaces { get; set; }
        public bool UsesLineItems { get; set; }
        public string XBRLTags { get; set; }
        public string ICPTopMember { get; set; }
        public string CalcAttribute { get; set; }
        public int SubmissionGroup { get; set; }
        public bool SwitchSignForFlow { get; set; }
        public bool SwitchTypeForFlow { get; set; }
    }
}

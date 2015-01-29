using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HFMCONSTANTSLib;
using HFMWMETADATALib;
using HFMWSESSIONLib;

namespace Byd.HFM
{
    public enum HfmDimensionType
    {
        Scenario = tagHFMDIMENSIONS.DIMENSIONSCENARIO,
        Year = tagHFMDIMENSIONS.DIMENSIONYEAR,
        Period = tagHFMDIMENSIONS.DIMENSIONPERIOD,
        View = tagHFMDIMENSIONS.DIMENSIONVIEW,
        Entity = tagHFMDIMENSIONS.DIMENSIONENTITY,
        Value = tagHFMDIMENSIONS.DIMENSIONVALUE,
        Account = tagHFMDIMENSIONS.DIMENSIONACCOUNT,
        Icp = tagHFMDIMENSIONS.DIMENSIONICP,
        Custom1 = tagHFMDIMENSIONS.DIMENSIONCUSTOM1,
        Custom2 = tagHFMDIMENSIONS.DIMENSIONCUSTOM2,
        Custom3 = tagHFMDIMENSIONS.DIMENSIONCUSTOM3,
        Custom4 = tagHFMDIMENSIONS.DIMENSIONCUSTOM4,
        Parent = tagHFMDIMENSIONS.DIMENSIONPARENT,
    }

    public class HfmDimensionTypeHelper
    {
        private static List<HfmDimensionType> _HfmDimensionFieldList = null;
        public static List<HfmDimensionType> HfmDimensionFieldList
        {
            get
            {
                if (_HfmDimensionFieldList == null)
                {
                    _HfmDimensionFieldList = new List<HfmDimensionType>();
                    _HfmDimensionFieldList.Add(HfmDimensionType.Scenario);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Year);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Period);
                    _HfmDimensionFieldList.Add(HfmDimensionType.View);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Entity);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Value);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Account);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Icp);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Custom1);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Custom2);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Custom3);
                    _HfmDimensionFieldList.Add(HfmDimensionType.Custom4);
                }
                return _HfmDimensionFieldList;
            }
        }
        public static string GetDimensionTypeName(HfmDimensionType argDimensionType)
        {
            return Enum.GetName(typeof(HfmDimensionType), argDimensionType);
        }

        public static string GetDimensionFieldName(HfmDimensionType argDimensionType)
        {
            string fieldName = string.Empty;
            switch (argDimensionType)
            {
                case HfmDimensionType.Scenario:
                    fieldName = "HFM_SCENARIO";
                    break;
                case HfmDimensionType.Year:
                    fieldName = "HFM_YEAR";
                    break;
                case HfmDimensionType.Period:
                    fieldName = "HFM_PERIOD";
                    break;
                case HfmDimensionType.View:
                    fieldName = "HFM_VIEW";
                    break;
                case HfmDimensionType.Entity:
                    fieldName = "HFM_ENTITY";
                    break;
                case HfmDimensionType.Value:
                    fieldName = "HFM_VALUE";
                    break;
                case HfmDimensionType.Account:
                    fieldName = "HFM_ACCOUNT";
                    break;
                case HfmDimensionType.Icp:
                    fieldName = "HFM_ICP";
                    break;
                case HfmDimensionType.Custom1:
                    fieldName = "HFM_CUSTOM1";
                    break;
                case HfmDimensionType.Custom2:
                    fieldName = "HFM_CUSTOM2";
                    break;
                case HfmDimensionType.Custom3:
                    fieldName = "HFM_CUSTOM3";
                    break;
                case HfmDimensionType.Custom4:
                    fieldName = "HFM_CUSTOM4";
                    break;
                case HfmDimensionType.Parent:
                    fieldName = "HFM_PARENT";
                    break;
            }
            return fieldName;
        }

        public static HFMwDimension GetHFMwDimension(HFMwSession argHFMwSession,HfmDimensionType argDimensionType)
        {
            HFMwDimension hfmwDimension = null;
            switch (argDimensionType)
            {
                case HfmDimensionType.Scenario:
                    hfmwDimension = argHFMwSession.metadata.scenarios;
                    break;
                case HfmDimensionType.Year:
                    hfmwDimension = argHFMwSession.metadata.years;
                    break;
                case HfmDimensionType.Period:

                    hfmwDimension = argHFMwSession.metadata.periods;
                    break;
                case HfmDimensionType.View:
                    hfmwDimension = argHFMwSession.metadata.views;
                    break;
                case HfmDimensionType.Entity:
                    hfmwDimension = argHFMwSession.metadata.entities;
                    break;
                case HfmDimensionType.Value:
                    hfmwDimension = argHFMwSession.metadata.values;
                    break;
                case HfmDimensionType.Account:
                    hfmwDimension = argHFMwSession.metadata.accounts;
                    break;
                case HfmDimensionType.Icp:
                    hfmwDimension = argHFMwSession.metadata.ICPs;
                    break;
                case HfmDimensionType.Custom1:
                    hfmwDimension = argHFMwSession.metadata.custom1;
                    break;
                case HfmDimensionType.Custom2:
                    hfmwDimension = argHFMwSession.metadata.custom2;
                    break;
                case HfmDimensionType.Custom3:
                    hfmwDimension = argHFMwSession.metadata.custom3;
                    break;
                case HfmDimensionType.Custom4:
                    hfmwDimension = argHFMwSession.metadata.custom4;
                    break;
            }
            return hfmwDimension;
        }
    }
}

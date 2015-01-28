using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HFMWMETADATALib;
using HFMWSESSIONLib;

namespace Byd.HFM
{
    public enum HfmDimensionType
    {
        Scenario = 0,
        Year = 1,
        Period = 2,
        View = 3,
        Entity = 4,
        Value = 5,
        Account = 6,
        Icp = 7,
        Custom1 = 8,
        Custom2 = 9,
        Custom3 = 10,
        Custom4 = 11,
        Parent = 12,
        IncludingParent = 13,
    }

    public class HfmDimensionTypeHelper
    {

        public static string GetDimensionTypeName(HfmDimensionType argDimensionType)
        {
            return Enum.GetName(typeof(HfmDimensionType), argDimensionType);
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

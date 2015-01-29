using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using HFMCONSTANTSLib;
using HFMWSESSIONLib;
using HSVSESSIONLib;

namespace Byd.HFM
{
    public class HfmData
    {
        private HsvSession _HsvSession = null;
        private HFMwSession _HFMwSession = null;

        public HfmData(HsvSession argHsvSession, HFMwSession argHFMwSession)
        {
            _HsvSession = argHsvSession;
            _HFMwSession = argHFMwSession;
        }

        public HfmSession HfmSession
        {
            get;
            set;
        }

        public int[] GetMembersThatHaveData(Dictionary<HfmDimensionType, int> argMemberIDDictionary,
            HfmDimensionType argDimensionType, string argValue)
        {
            int[] retVal = new int[0];
            try
            {
                object data = null;
                if (string.IsNullOrEmpty(argValue))
                {
                    _HsvSession.Data.GetMembersThatHaveData(argMemberIDDictionary[HfmDimensionType.Scenario]
                        , argMemberIDDictionary[HfmDimensionType.Year]
                        , argMemberIDDictionary[HfmDimensionType.Period]
                        , argMemberIDDictionary[HfmDimensionType.Entity]
                        , argMemberIDDictionary[HfmDimensionType.Parent]
                        , argMemberIDDictionary[HfmDimensionType.Value]
                        , argMemberIDDictionary[HfmDimensionType.Account]
                        , argMemberIDDictionary[HfmDimensionType.Icp]
                        , argMemberIDDictionary[HfmDimensionType.Custom1]
                        , argMemberIDDictionary[HfmDimensionType.Custom2]
                        , argMemberIDDictionary[HfmDimensionType.Custom3]
                        , argMemberIDDictionary[HfmDimensionType.Custom4]
                        , argDimensionType, true, null, out data);
                    if (data != null)
                    {
                        retVal = (int[]) data;
                    }
                }
                else
                {
                    retVal = new int[] {MemberLabel2Id(argDimensionType, argValue)};
                }
            }
            catch (Exception ex)
            {
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
            return retVal;
        }

        public void SetCells(DataTable argDataTable)
        {
            try
            {
                int Count = argDataTable.Rows.Count;
                int[] varalScenario = new int[Count];
                int[] varalYear = new int[Count];
                int[] varalPeriod = new int[Count];
                int[] varalView = new int[Count];
                int[] varalEntity = new int[Count];
                int[] varalParent = new int[Count];
                int[] varalValue = new int[Count];
                int[] varalAccount = new int[Count];
                int[] varalICP = new int[Count];
                int[] varalCustom1 = new int[Count];
                int[] varalCustom2 = new int[Count];
                int[] varalCustom3 = new int[Count];
                int[] varalCustom4 = new int[Count];
                double[] varalData = new double[Count];
                bool[] varaIsNoData = new bool[Count];
                for (int i = 0; i < Count; i++)
                {
                    DataRow dataRow = argDataTable.Rows[i];
                    varalScenario[i] = MemberLabel2Id(HfmDimensionType.Scenario, dataRow);
                    varalYear[i] = MemberLabel2Id(HfmDimensionType.Year, dataRow);
                    varalPeriod[i] = MemberLabel2Id(HfmDimensionType.Period, dataRow);
                    varalView[i] = MemberLabel2Id(HfmDimensionType.View, dataRow); ;
                    varalEntity[i] = MemberLabel2Id(HfmDimensionType.Entity, dataRow); ;
                    //varalParent[i] = MemberLabel2Id(HfmDimensionType.Parent, dataRow); ;
                    varalValue[i] = MemberLabel2Id(HfmDimensionType.Value, dataRow);
                    varalAccount[i] = MemberLabel2Id(HfmDimensionType.Account, dataRow);
                    varalICP[i] = MemberLabel2Id(HfmDimensionType.Icp, dataRow);
                    varalCustom1[i] = MemberLabel2Id(HfmDimensionType.Custom1, dataRow);
                    varalCustom2[i] = MemberLabel2Id(HfmDimensionType.Custom2, dataRow);
                    varalCustom3[i] = MemberLabel2Id(HfmDimensionType.Custom3, dataRow);
                    varalCustom4[i] = MemberLabel2Id(HfmDimensionType.Custom4, dataRow);
                    if ("1".Equals(dataRow["DELETED"]))
                    {
                        varalData[i] = 0;
                        varaIsNoData[i] = true;
                    }
                    else
                    {
                        varalData[i] = double.Parse(dataRow["VALUE"].ToString());
                        varaIsNoData[i] = false;
                    }
                }
                object pvaralStatus;
                _HsvSession.Data.SetCells2(varalScenario, varalYear, varalPeriod, varalView, varalEntity, varalParent, varalValue, varalAccount, varalICP, varalCustom1, varalCustom2, varalCustom3, varalCustom4, varalData, varaIsNoData, out pvaralStatus);

                for (int i = 0; i < Count; i++)
                {
                    DataRow dataRow = argDataTable.Rows[i];
                    if ((((int[])pvaralStatus)[i] & (int)tagCALCSTATUSLOWBITS.CELLSTATUS_NODATA) ==
                        (int)tagCALCSTATUSLOWBITS.CELLSTATUS_NODATA)
                    {
                        dataRow["STATE"] = -1;
                    }
                    else
                    {
                        dataRow["STATE"] = ((Int32[])pvaralStatus)[i];
                    }
                }
            }
            catch (Exception ex)
            {   //-2147220984   Error occurred while writing cube data.
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
        }

        public void GetCells(DataTable argDataTable)
        {
            try
            {
                if (argDataTable == null || argDataTable.Rows.Count == 0)
                {
                    return;
                }
                int Count = argDataTable.Rows.Count;
                int[] varalScenario = new int[Count];
                int[] varalYear = new int[Count];
                int[] varalPeriod = new int[Count];
                int[] varalView = new int[Count];
                int[] varalEntity = new int[Count];
                int[] varalParent = new int[Count];
                int[] varalValue = new int[Count];
                int[] varalAccount = new int[Count];
                int[] varalICP = new int[Count];
                int[] varalCustom1 = new int[Count];
                int[] varalCustom2 = new int[Count];
                int[] varalCustom3 = new int[Count];
                int[] varalCustom4 = new int[Count];
                var hfmDimension = this.HfmSession.GetDimension();
                string entityFieldName = HfmDimensionTypeHelper.GetDimensionFieldName(HfmDimensionType.Entity);
                string entityParentFieldName = HfmDimensionTypeHelper.GetDimensionFieldName(HfmDimensionType.Parent);
                for (int i = 0; i < Count; i++)
                {
                    DataRow dataRow = argDataTable.Rows[i];
                    varalScenario[i] = MemberLabel2Id(HfmDimensionType.Scenario, dataRow);
                    varalYear[i] = MemberLabel2Id(HfmDimensionType.Year, dataRow);
                    varalPeriod[i] = MemberLabel2Id(HfmDimensionType.Period, dataRow);
                    varalView[i] = MemberLabel2Id(HfmDimensionType.View, dataRow);
                    varalEntity[i] = MemberLabel2Id(HfmDimensionType.Entity, dataRow);
                    string entityLabel = dataRow[entityFieldName].ToString();
                    string parentLabel = dataRow[entityParentFieldName].ToString();
                    if (!string.IsNullOrEmpty(parentLabel))
                    {
                        varalParent[i] = MemberLabel2Id(HfmDimensionType.Entity, parentLabel);
                    }
                    else
                    {
                        varalParent[i] = hfmDimension.GetDefaultParentID(entityLabel);
                    }

                    varalValue[i] = MemberLabel2Id(HfmDimensionType.Value, dataRow);
                    varalAccount[i] = MemberLabel2Id(HfmDimensionType.Account, dataRow);
                    varalICP[i] = MemberLabel2Id(HfmDimensionType.Icp, dataRow);
                    varalCustom1[i] = MemberLabel2Id(HfmDimensionType.Custom1, dataRow);
                    varalCustom2[i] = MemberLabel2Id(HfmDimensionType.Custom2, dataRow);
                    varalCustom3[i] = MemberLabel2Id(HfmDimensionType.Custom3, dataRow);
                    varalCustom4[i] = MemberLabel2Id(HfmDimensionType.Custom4, dataRow);
                }
                object pvaradData = null;
                object pvaralStatus = null;

                _HsvSession.Data.GetCells(varalScenario, varalYear, varalPeriod, varalView, varalEntity, varalParent,
                    varalValue, varalAccount, varalICP, varalCustom1, varalCustom2, varalCustom3, varalCustom4,
                    out pvaradData, out pvaralStatus);

                for (int i = 0; i < Count; i++)
                {
                    DataRow dataRow = argDataTable.Rows[i];
                    dataRow["VALUE"] = ((double[]) pvaradData)[i];
                    if ((((int[]) pvaralStatus)[i] & (int) tagCALCSTATUSLOWBITS.CELLSTATUS_NODATA) ==
                        (int) tagCALCSTATUSLOWBITS.CELLSTATUS_NODATA)
                    {
                        dataRow["STATE"] = -1;
                    }
                    else
                    {
                        dataRow["STATE"] = ((Int32[]) pvaralStatus)[i];
                    }
                }
            }
            catch (Exception ex)
            {
                //-2147220975   Error occurred while accessing the safe array.
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
        }


        public DataTable ExpandData(DataTable argTable)
        {
            DataTable retTable = argTable.Clone();
            foreach (DataRow dataRow in argTable.Rows)
            {
                DataTable tempTable = argTable.Clone();
                DataTable expandedTable = ExpandData(dataRow, tempTable);
                retTable.Merge(expandedTable);
            }
            return retTable;
        }

        public DataTable ExpandData(DataRow argDataRow, DataTable argTempTable)
        {
            DataTable retDataTable = argTempTable.Clone();
            List<HfmDimensionType> emptyMemberList = GetEmptyMembers(argDataRow);
            if (emptyMemberList.Count == 0)
            {
                DataRow newRow = retDataTable.NewRow();
                Array.Copy(newRow.ItemArray, argDataRow.ItemArray, argDataRow.ItemArray.Length);
                retDataTable.Rows.Add(newRow);
                return retDataTable;
            }

            Dictionary<HfmDimensionType, int> memberIdDictionary = GetMemberIds(argDataRow);
            HfmDimensionType emptyMember = emptyMemberList[0];
            string emptyMemberFieldName = HfmDimensionTypeHelper.GetDimensionFieldName(emptyMember);
            int[] memberIds = GetMembersThatHaveData(memberIdDictionary, emptyMember, argDataRow[emptyMemberFieldName].ToString());
            foreach (int memberId in memberIds)
            {
                string label = HfmSession.GetDimension().GetMemberLabel(emptyMember, memberId);
                if (label == "[ICP None]" || label == "[None]")
                {
                    continue;
                }
                DataRow newRow = argTempTable.NewRow();
                Array.Copy(newRow.ItemArray, argDataRow.ItemArray, argDataRow.ItemArray.Length);
                newRow[emptyMemberFieldName] = label;
                argTempTable.Rows.Add(newRow);
            }
            if (emptyMemberList.Count == 1)
            {
                retDataTable.Merge(argTempTable);
            }
            else
            {
                DataTable expandTable = ExpandData(argTempTable);
                retDataTable.Merge(expandTable);
            }
            return retDataTable;
        }

        private List<HfmDimensionType> GetEmptyMembers(DataRow argDataRow)
        {
            List<HfmDimensionType> emptyMemberList = new List<HfmDimensionType>();
            foreach (var dimensionType in HfmDimensionTypeHelper.HfmDimensionFieldList)
            {
                string fieldName = HfmDimensionTypeHelper.GetDimensionFieldName(dimensionType);
                string memberLabel = argDataRow[fieldName].ToString();
                if (string.IsNullOrEmpty(memberLabel))
                {
                    emptyMemberList.Add(dimensionType);
                }
            }

            return emptyMemberList;
        }

        private Dictionary<HfmDimensionType, int> GetMemberIds(DataRow argDataRow)
        {
            Dictionary<HfmDimensionType, int> memberIdDictionary = new Dictionary<HfmDimensionType, int>();
            foreach (var dimensionType in HfmDimensionTypeHelper.HfmDimensionFieldList)
            {
                string member = HfmDimensionTypeHelper.GetDimensionFieldName(dimensionType);
                string memberLabel = argDataRow[member].ToString();
                if (string.IsNullOrEmpty(memberLabel))
                {
                    memberIdDictionary.Add(dimensionType, (short)tagPOVDEFAULTS.MEMBERNOTUSED);
                }
                else
                {
                    memberIdDictionary.Add(dimensionType, MemberLabel2Id(dimensionType, memberLabel));
                }
            }
            string entityLabel = argDataRow[HfmDimensionTypeHelper.GetDimensionFieldName(HfmDimensionType.Entity)].ToString();
            this.HfmSession.GetDimension().GetDefaultParentID(entityLabel);
            memberIdDictionary.Add(HfmDimensionType.Parent, HfmSession.GetDimension().GetDefaultParentID(entityLabel));
            return memberIdDictionary;
        }


        private int MemberLabel2Id(HfmDimensionType argDimensionType,DataRow argDataRow)
        {
            string fieldName = HfmDimensionTypeHelper.GetDimensionFieldName(argDimensionType);
            string memberLabel = argDataRow[fieldName].ToString();
            return MemberLabel2Id(argDimensionType, memberLabel);
        }

        private int MemberLabel2Id(HfmDimensionType argDimensionType, string argLabel)
        {
            return this.HfmSession.GetDimension().GetMemberID(argDimensionType, argLabel);
        }
    }
}

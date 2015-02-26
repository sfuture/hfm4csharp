using System;
using System.Collections.Generic;
using System.IO;
using Byd.HFM.Model;
using HFMCONSTANTSLib;
using HFMWMETADATALib;
using HFMWSESSIONLib;
using HSVMETADATALib;
using HSVMETADATALOADACVLib;
using HSVSESSIONLib;

namespace Byd.HFM
{
    public class HfmDimension
    {
        private static Dictionary<HfmDimensionType,Dictionary<string, int>> memberIdDictionary;
        private static Dictionary<HfmDimensionType,Dictionary<int, string>> memberLabelDictionary;

        private HsvSession _HsvSession = null;
        private HFMwSession _HFMwSession = null;

        public HfmDimension(HsvSession argHsvSession, HFMwSession argHFMwSession)
        {
            _HsvSession = argHsvSession;
            _HFMwSession = argHFMwSession;
            init();
        }
        public HfmSession HfmSession
        {
            get;
            set;
        }

        private void init()
        {
            if (HfmDimension.memberIdDictionary == null)
            {
                HfmDimension.memberIdDictionary = new Dictionary<HfmDimensionType,Dictionary<string, int>>() ;
                foreach (HfmDimensionType hfmDimensionType in Enum.GetValues(typeof(HfmDimensionType)))
                {
                    memberIdDictionary[hfmDimensionType] = new Dictionary<string, int>();
                }
            }
            if (HfmDimension.memberLabelDictionary == null)
            {
                HfmDimension.memberLabelDictionary = new Dictionary<HfmDimensionType,Dictionary<int, string>>();
                foreach (HfmDimensionType hfmDimensionType in Enum.GetValues(typeof(HfmDimensionType)))
                {
                    memberLabelDictionary[hfmDimensionType] = new Dictionary<int, string>();
                }
            }
        }


        public int GetMemberID(HfmDimensionType argDimensionType, String argLabel)
        {
            try
            {
                if (!HfmDimension.memberIdDictionary[argDimensionType].ContainsKey(argLabel))
                {
                    HsvMetadata hsvMetadata = _HsvSession.Metadata as HsvMetadata;
                    IHsvTreeInfo treeInfo = (IHsvTreeInfo)hsvMetadata.Dimension[(short)argDimensionType];
                    HfmDimension.memberIdDictionary[argDimensionType].Add(argLabel, treeInfo.GetItemID(argLabel));
                }
                return HfmDimension.memberIdDictionary[argDimensionType][argLabel];
            }
            catch (Exception ex)
            {
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
        }

        public String GetMemberLabel(HfmDimensionType argDimensionType, int argId)
        {
            try
            {
                if (!HfmDimension.memberLabelDictionary[argDimensionType].ContainsKey(argId))
                {
                    HsvMetadata hsvMetadata = _HsvSession.Metadata as HsvMetadata;
                    IHsvTreeInfo treeInfo = (IHsvTreeInfo)hsvMetadata.Dimension[(short)argDimensionType];
                    String sLabel = String.Empty;
                    treeInfo.GetLabel(argId, out sLabel);
                    HfmDimension.memberLabelDictionary[argDimensionType].Add(argId, sLabel);
                }
                return HfmDimension.memberLabelDictionary[argDimensionType][argId];
            }
            catch (Exception ex)
            {
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
        }

        public int GetDefaultParentID(String argEntity)
        {
            try
            {
                if (!HfmDimension.memberIdDictionary[HfmDimensionType.Parent].ContainsKey(argEntity))
                {
                    int entityId = GetMemberID(HfmDimensionType.Entity, argEntity);
                    HFMwMetadata hfmwMetadata = _HFMwSession.metadata as HFMwMetadata;
                    HFMwEntities hfmwEntities = hfmwMetadata.entities as HFMwEntities;
                    HFMwDimension hfmwDimension = hfmwEntities.dimension as HFMwDimension;
                    int parentEntityId = hfmwDimension.GetDefaultParent(entityId);
                    memberIdDictionary[HfmDimensionType.Parent].Add(argEntity, parentEntityId);
                }
                return memberIdDictionary[HfmDimensionType.Parent][argEntity];
            }
            catch (Exception ex)
            {
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
        }
        public List<DimensionMember> EnumMembers2(HfmDimensionType argDimensionType)
        {
            return EnumMembers2(argDimensionType, "[Hierarchy]");
        }

        public List<DimensionMember> EnumMembers2(HfmDimensionType argDimensionType,string argMemberListName)
        {
            object count = null,
                varMemberIDs = null,
                varParentIDs = null,
                valMemberLabels = null,
                varDescriptions = null,
                varNumChildren = null,
                varTotalMembersInEnum = null;

            HFMwDimension hfmwDimension = HfmDimensionTypeHelper.GetHFMwDimension(_HFMwSession,argDimensionType);
            count = hfmwDimension.EnumMembers2(tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED,
                tagPOVDEFAULTS.MEMBERNOTUSED,
                tagPOVDEFAULTS.MEMBERNOTUSED, argMemberListName, "", 0, 0,
                (int)tagWEBOM_METADATA_INFO_FLAGS.WEBOM_METADATA_INFO_ALL,
                ref varMemberIDs, ref varParentIDs, ref valMemberLabels, ref varDescriptions, ref varNumChildren,
                ref varTotalMembersInEnum);
            int total = (int)varTotalMembersInEnum;

            List<DimensionMember> retList = new List<DimensionMember>();

            for (int i = 0; i < total; i++)
            {
                DimensionMember dimensionMember = new DimensionMember();
                dimensionMember.MemberID = (int)((object[])varMemberIDs)[i];
                if (varParentIDs != null)
                {
                    dimensionMember.ParentID = (int) ((object[]) varParentIDs)[i];
                }
                dimensionMember.NumChildren = (int)((object[])varNumChildren)[i];
                dimensionMember.MemberLabel = ((object[])valMemberLabels)[i] as string;
                dimensionMember.Description = ((object[])varDescriptions)[i] as string;
                retList.Add(dimensionMember);
            }
            return retList;
        }

        public List<DimensionMember> EnumEntityMembers()
        {
            List<DimensionMember> memberList = EnumMembers2(HfmDimensionType.Entity);
            var hsvMetadata = _HsvSession.Metadata as IHsvMetadata;
            var hsvEntities = hsvMetadata.Entities as IHsvEntities;
            bool boolOutValue;
            int intOutValue;
            string strOutValue;
            foreach (var member in memberList)
            {

                hsvEntities.GetDefaultValueID(member.MemberID, out intOutValue);
                var hsvTreeInfo = hsvMetadata.Values as IHsvTreeInfo;
                hsvTreeInfo.GetLabel(intOutValue, out strOutValue);
                member.DefCurrency = strOutValue;


                hsvEntities.GetAllowAdjustments(member.MemberID, out boolOutValue);
                member.AllowAdjs = boolOutValue;

                hsvEntities.GetAllowAdjustmentsFromChildren(member.MemberID, out boolOutValue);
                member.AllowAdjFromChildren = boolOutValue;

                hsvEntities.IsICP(member.MemberID, out boolOutValue);
                member.IsICP = boolOutValue;

                hsvEntities.GetHoldingCompany(member.MemberID, out intOutValue);
                //hsvTreeInfo.GetLabel(intOutValue, out strOutValue);
                member.HoldingCompany = GetMemberLabel(HfmDimensionType.Entity, intOutValue); 

                hsvEntities.GetSecurityAsPartnerID(member.MemberID, out intOutValue);
                member.SecurityAsPartner = intOutValue;

                hsvEntities.GetUserDefined1(member.MemberID, out strOutValue);
                member.UserDefined1 = strOutValue;

                hsvEntities.GetUserDefined2(member.MemberID, out strOutValue);
                member.UserDefined2 = strOutValue;

                hsvEntities.GetUserDefined3(member.MemberID, out strOutValue);
                member.UserDefined3 = strOutValue;

                hsvEntities.GetSecurityClassID(member.MemberID, out intOutValue);
                member.SecurityClass = intOutValue;
            }
            return memberList;
        }

        public List<DimensionMember> EnumAccountMembers()
        {
            List<DimensionMember> memberList = EnumMembers2(HfmDimensionType.Account);
            try
            {
                var hsvMetadata = _HsvSession.Metadata as IHsvMetadata;
                var hsvAccounts = hsvMetadata.Accounts as IHsvAccounts;
                bool boolOutValue;
                int intOutValue;
                short shortOutValue;
                string strOutValue;
                foreach (var member in memberList)
                {

                    hsvAccounts.GetAccountType(member.MemberID, out shortOutValue);
                    member.AccountType = shortOutValue;

                    hsvAccounts.IsCalculated(member.MemberID, out boolOutValue);
                    member.IsCalculated = boolOutValue;

                    hsvAccounts.IsConsolidated(member.MemberID, out boolOutValue);
                    member.IsConsolidated = boolOutValue;

                    hsvAccounts.IsICP(member.MemberID, out boolOutValue);
                    member.IsICP = boolOutValue;

                    hsvAccounts.GetPlugAccount(member.MemberID, out intOutValue);
                    member.PlugAcct = GetMemberLabel(HfmDimensionType.Account, intOutValue);

                    hsvAccounts.GetTopMemberOfValidCustom1Hierarchy(member.MemberID, out intOutValue);
                    member.Custom1TopMember = GetMemberLabel(HfmDimensionType.Custom1, intOutValue);

                    hsvAccounts.GetTopMemberOfValidCustom2Hierarchy(member.MemberID, out intOutValue);
                    member.Custom2TopMember = GetMemberLabel(HfmDimensionType.Custom2, intOutValue); ;

                    hsvAccounts.GetTopMemberOfValidCustom3Hierarchy(member.MemberID, out intOutValue);
                    member.Custom3TopMember = GetMemberLabel(HfmDimensionType.Custom3, intOutValue); ;

                    hsvAccounts.GetTopMemberOfValidCustom4Hierarchy(member.MemberID, out intOutValue);
                    member.Custom4TopMember = GetMemberLabel(HfmDimensionType.Custom4, intOutValue); ;

                    hsvAccounts.GetNumDecimalPlaces(member.MemberID, out shortOutValue);
                    member.NumDecimalPlaces = shortOutValue;

                    hsvAccounts.UsesLineItems(member.MemberID, out boolOutValue);
                    member.UsesLineItems = boolOutValue;

                    hsvAccounts.GetXBRLTags(member.MemberID, out strOutValue);
                    member.XBRLTags = strOutValue;

                    hsvAccounts.GetICPTopMember(member.MemberID, out intOutValue);
                    member.ICPTopMember = GetMemberLabel(HfmDimensionType.Account, intOutValue);

                    hsvAccounts.GetCalcAttribute(member.MemberID, out strOutValue);
                    member.CalcAttribute = strOutValue;

                    hsvAccounts.GetSubmissionGroup(member.MemberID, ref intOutValue);
                    member.SubmissionGroup = intOutValue;

                    hsvAccounts.GetUserDefined1(member.MemberID, out strOutValue);
                    member.UserDefined1 = strOutValue;

                    hsvAccounts.GetUserDefined2(member.MemberID, out strOutValue);
                    member.UserDefined2 = strOutValue;

                    hsvAccounts.GetUserDefined3(member.MemberID, out strOutValue);
                    member.UserDefined3 = strOutValue;

                    hsvAccounts.GetSecurityClassID(member.MemberID, out intOutValue);
                    member.SecurityClass = intOutValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(HfmCommon.GetHfmErrorMessage(ex.Message));
            }
            return memberList;
        }
        public List<DimensionMember> EnumCustomMembers(HfmDimensionType argDimensionType)
        {
            if (argDimensionType != HfmDimensionType.Custom1 && argDimensionType != HfmDimensionType.Custom2 &&
                argDimensionType != HfmDimensionType.Custom3 && argDimensionType != HfmDimensionType.Custom4)
            {
                return null;
            }

            List<DimensionMember> memberList = EnumMembers2(argDimensionType);
            var hsvMetadata = _HsvSession.Metadata as IHsvMetadata;
            IHsvCustom hsvCustom = null;
            switch (argDimensionType)
            {
                case HfmDimensionType.Custom1:
                    hsvCustom = hsvMetadata.Custom1 as IHsvCustom;
                    break;
                case HfmDimensionType.Custom2:
                    hsvCustom = hsvMetadata.Custom2 as IHsvCustom;
                    break;
                case HfmDimensionType.Custom3:
                    hsvCustom = hsvMetadata.Custom3 as IHsvCustom;
                    break;
                case HfmDimensionType.Custom4:
                    hsvCustom = hsvMetadata.Custom4 as IHsvCustom;
                    break;
            }

            bool boolOutValue;
            int intOutValue;
            short shortOutValue;
            string strOutValue;

            foreach (var member in memberList)
            {
                hsvCustom.IsSwitchSignEnabledForFlow(member.MemberID, out boolOutValue);
                member.SwitchSignForFlow = boolOutValue;
                hsvCustom.IsSwitchTypeEnabledForFlow(member.MemberID, out boolOutValue);
                member.SwitchTypeForFlow = boolOutValue;
                hsvCustom.GetUserDefined1(member.MemberID, out strOutValue);
                member.UserDefined1 = strOutValue;
                hsvCustom.GetUserDefined2(member.MemberID, out strOutValue);
                member.UserDefined2 = strOutValue;
                hsvCustom.GetUserDefined3(member.MemberID, out strOutValue);
                member.UserDefined3 = strOutValue;
                hsvCustom.GetSecurityClassID(member.MemberID, out intOutValue);
                member.SecurityClass = intOutValue;
            }

            return memberList;

        }


        public List<DimensionTreeModel> CreateTreeModel(HfmDimensionType argDimensionType)
        {
            List<DimensionTreeModel> treeModelList = new List<DimensionTreeModel>();
            var dimensionList = EnumMembers2(argDimensionType);
            HsvMetadata hsvMetadata = _HsvSession.Metadata as HsvMetadata;
            IHsvTreeInfo treeInfo = (IHsvTreeInfo)hsvMetadata.Dimension[(short)argDimensionType];
            int order = 0;
            foreach (var member in dimensionList)
            {
                object parentIds;
                treeInfo.EnumParents(member.MemberID, out parentIds);
                foreach (int parentId in (int[])parentIds)
                {
                    DimensionTreeModel treeModel = new DimensionTreeModel();
                    treeModel.Type = HfmDimensionTypeHelper.GetDimensionTypeName(argDimensionType);
                    if (parentId == -1)
                    {
                        treeModel.ParentCode = "ROOT";
                    }
                    else
                    {
                        string parentLabel;
                        treeInfo.GetLabel(parentId, out parentLabel);
                        treeModel.ParentCode = parentLabel;
                    }
                    treeModel.Code = member.MemberLabel;
                    treeModel.CodeName = member.Description;
                    treeModel.Order = ++order;
                    treeModelList.Add(treeModel);
                }
            }
            return treeModelList;
        }


        public void Extract(string argExtractFilePath, string argExtractLogPath)
        {
            HsvMetadataLoadACV hsvMetadataLoadACV = new HsvMetadataLoadACV();
            hsvMetadataLoadACV.SetSession(_HsvSession);
            IHsvLoadExtractOptions options = hsvMetadataLoadACV.ExtractOptions;
            IHsvLoadExtractOption option = options.Item[HSV_METADATAEXTRACT_OPTION.HSV_METAEXTRACT_OPT_ACCOUNTS_SYSTEM];
            option.CurrentValue = false;

            option = options.Item[HSV_METADATAEXTRACT_OPTION.HSV_METAEXTRACT_OPT_FILE_FORMAT];
            option.CurrentValue = HSV_METALOADEX_FILE_FORMAT.HSV_METALOADEX_FORMAT_XML;

            hsvMetadataLoadACV.Extract(argExtractFilePath, argExtractLogPath);
        }

    }
}

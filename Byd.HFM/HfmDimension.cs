using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Byd.HFM.Model;
using HFMCONSTANTSLib;
using HFMWMETADATALib;
using HFMWSESSIONLib;
using HSVMETADATALib;
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


        public List<DimensionMember> EnumMembers2(HfmDimensionType argDimensionType)
        {
            return EnumMembers2(argDimensionType, "[Hierarchy]");
        }

        public List<DimensionMember> EnumMembers2(HfmDimensionType argDimensionType,string argMemberListName)
        {
            //_HFMwSession.descriptionLanguage = 0;
            //_HFMwSession.ApplyUserSettings(true);
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

        //private void GetHFMEntityByMemberList(string pMemberListName)
        //{
        //    try
        //    {
        //        _HFMwSession.descriptionLanguage = 0;
        //        _HFMwSession.ApplyUserSettings(true);
        //        object lCount = null,
        //            valMemberIDs = null,
        //            valParentIDs = null,
        //            vaMemberLabels = null,
        //            vaDescriptions = null,
        //            valNumChildren = null,
        //            lTotalMembersInEnum = null;
        //        lCount =
        //            ((HFMwDimension) ((HFMwEntities) _HFMwSession.metadata.entities).dimension).EnumMembers2(
        //                tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED,
        //                tagPOVDEFAULTS.MEMBERNOTUSED, pMemberListName, "", 0, 0,
        //                Convert.ToInt32(tagWEBOM_METADATA_INFO_FLAGS.WEBOM_METADATA_INFO_ALL),
        //                ref valMemberIDs, ref valParentIDs, ref vaMemberLabels, ref vaDescriptions, ref valNumChildren,
        //                ref lTotalMembersInEnum);

        //        int total = (int) lTotalMembersInEnum;
        //        object[] oMemberIDs = (object[]) valMemberIDs,
        //            oParentIDs = (object[]) valParentIDs,
        //            oMemberLabels = (object[]) vaMemberLabels,
        //            oDescriptions = (object[]) vaDescriptions;

        //        if (!oDimensionLabels.ContainsKey((int) tagHFMDIMENSIONS.DIMENSIONENTITY))
        //        {
        //            oDimensionLabels.Add((int) tagHFMDIMENSIONS.DIMENSIONENTITY, new Dictionary<int, ITEM_INFO>());
        //            oDimensionItemIds.Add((int) tagHFMDIMENSIONS.DIMENSIONENTITY, new Dictionary<string, ITEM_INFO>());
        //        }
        //        string[] labels;
        //        Dictionary<int, ITEM_INFO> temp = oDimensionLabels[(int) tagHFMDIMENSIONS.DIMENSIONENTITY];
        //        Dictionary<string, ITEM_INFO> temp2 = oDimensionItemIds[(int) tagHFMDIMENSIONS.DIMENSIONENTITY];
        //        for (int i = 0; i < total; i++)
        //        {
        //            if (!temp.ContainsKey((int) oMemberIDs[i]))
        //            {
        //                labels = ((string) oMemberLabels[i]).Split('.');
        //                ITEM_INFO item = new ITEM_INFO();
        //                item.ItemId = (int) oMemberIDs[i];
        //                item.Label = labels[labels.Length - 1];
        //                item.Description = (string) oDescriptions[i];

        //                temp.Add((int) oMemberIDs[i], item);
        //                temp2.Add(item.Label, item);
        //            }
        //        }

        //        _HFMwSession.descriptionLanguage = 1;
        //        _HFMwSession.ApplyUserSettings(true);
        //        oDescriptions = (object[]) vaDescriptions;
        //        lCount =
        //            ((HFMwDimension) ((HFMwEntities) iHFMwMetadata.entities).dimension).EnumMembers2(
        //                tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED,
        //                tagPOVDEFAULTS.MEMBERNOTUSED, pMemberListName, "", 0, 0,
        //                Convert.ToInt32(tagWEBOM_METADATA_INFO_FLAGS.WEBOM_METADATA_INFO_ALL),
        //                ref valMemberIDs, ref valParentIDs, ref vaMemberLabels, ref vaDescriptions, ref valNumChildren,
        //                ref lTotalMembersInEnum);
        //        for (int i = 0; i < total; i++)
        //        {
        //            temp[(int) oMemberIDs[i]].Description1 = (string) oDescriptions[i];
        //        }

        //        iHFMwSession.descriptionLanguage = 2;
        //        iHFMwSession.ApplyUserSettings(true);
        //        oDescriptions = (object[]) vaDescriptions;
        //        lCount =
        //            ((HFMwDimension) ((HFMwEntities) iHFMwMetadata.entities).dimension).EnumMembers2(
        //                tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED, tagPOVDEFAULTS.MEMBERNOTUSED,
        //                tagPOVDEFAULTS.MEMBERNOTUSED, pMemberListName, "", 0, 0,
        //                Convert.ToInt32(tagWEBOM_METADATA_INFO_FLAGS.WEBOM_METADATA_INFO_ALL),
        //                ref valMemberIDs, ref valParentIDs, ref vaMemberLabels, ref vaDescriptions, ref valNumChildren,
        //                ref lTotalMembersInEnum);
        //        for (int i = 0; i < total; i++)
        //        {
        //            temp[(int) oMemberIDs[i]].Description2 = (string) oDescriptions[i];
        //        }

        //        iHFMwSession.descriptionLanguage = 0;
        //        iHFMwSession.ApplyUserSettings(true);

        //        int plValueID, plHoldingCompanyEntityID, plSecurityAsPartnerID, plSecurityClassID;
        //        bool pbOK, pbIsICP;
        //        string pbstrLabel, pbstrUserDefined;
        //        HsvEntities iHsvEntities = (HsvEntities) iHsvMetadata.Entities;
        //        foreach (int key in temp.Keys)
        //        {
        //            DIMENSION_PROPERY_ITEMS dimension_propery_items = new DIMENSION_PROPERY_ITEMS();
        //            ITEM_INFO item_info = temp[key];
        //            dimension_propery_items.Type = "Entity";
        //            dimension_propery_items.Code = item_info.Label;
        //            dimension_propery_items.DefaultParent = GetDefaultParentLabel(dimension_propery_items.Code);
        //            dimension_propery_items.CodeName = item_info.Description;
        //            dimension_propery_items.CodeName1 = item_info.Description1;
        //            dimension_propery_items.CodeName2 = item_info.Description2;
        //            iHsvEntities.GetDefaultValueID(key, out plValueID);
        //            iTreeInfo = iHsvMetadata.Values;
        //            iTreeInfo.GetLabel(plValueID, out pbstrLabel);
        //            dimension_propery_items.DefCurrency = pbstrLabel;
        //            iHsvEntities.GetAllowAdjustments(key, out pbOK);
        //            dimension_propery_items.AllowAdjs = pbOK;
        //            iHsvEntities.GetAllowAdjustmentsFromChildren(key, out pbOK);
        //            dimension_propery_items.AllowAdjFromChildren = pbOK;
        //            iHsvEntities.IsICP(key, out pbIsICP);
        //            dimension_propery_items.IsICP = pbIsICP;
        //            iHsvEntities.GetHoldingCompany(key, out plHoldingCompanyEntityID);
        //            iTreeInfo = iHsvMetadata.Entities;
        //            iTreeInfo.GetLabel(plHoldingCompanyEntityID, out pbstrLabel);
        //            dimension_propery_items.HoldingCompany = pbstrLabel;
        //            iHsvEntities.GetSecurityAsPartnerID(key, out plSecurityAsPartnerID);
        //            dimension_propery_items.SecurityAsPartner = plSecurityAsPartnerID;
        //            iHsvEntities.GetUserDefined1(key, out pbstrUserDefined);
        //            dimension_propery_items.UserDefined1 = pbstrUserDefined;
        //            iHsvEntities.GetUserDefined2(key, out pbstrUserDefined);
        //            dimension_propery_items.UserDefined2 = pbstrUserDefined;
        //            iHsvEntities.GetUserDefined3(key, out pbstrUserDefined);
        //            dimension_propery_items.UserDefined3 = pbstrUserDefined;
        //            iHsvEntities.GetSecurityClassID(key, out plSecurityClassID);
        //            dimension_propery_items.SecurityClass = plSecurityClassID;
        //            oDimensionProperyItems.Add(dimension_propery_items);
        //        }

        //        //Tree Data
        //        int order = 0;
        //        for (int i = 0; i < total; i++)
        //        {
        //            DIMENSION_TREE_DATA dimension_tree_data = new DIMENSION_TREE_DATA();
        //            labels = ((string) oMemberLabels[i]).Split('.');
        //            dimension_tree_data.Type = "Entity";
        //            if ((int) oParentIDs[i] == -1)
        //            {
        //                dimension_tree_data.ParentCode = "ROOT";
        //                dimension_tree_data.Code = labels[0];
        //            }
        //            else
        //            {
        //                dimension_tree_data.ParentCode = labels[0];
        //                dimension_tree_data.Code = labels[1];
        //            }
        //            dimension_tree_data.Order = ++order;
        //            oDimensionTreeData.Add(dimension_tree_data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

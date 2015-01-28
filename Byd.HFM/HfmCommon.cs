using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HFMCONSTANTSLib;
using HSVRESOURCEMANAGERLib;

namespace Byd.HFM
{
    public class HfmCommon
    {
        public static string GetHfmErrorMessage(string xmlMessage)
        {
            HsvResourceManager iHsvResourceManager = new HsvResourceManager();
            iHsvResourceManager.Initialize((short)tagHFM_TIERS.HFM_WEB_TIER);
            int errNum = 0;
            int iLanguageID = 12;
            object pvarbstrFormattedError;
            object pvarbstrTechnicalError;
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlMessage);
                errNum = int.Parse(xDoc.SelectSingleNode("//Num").InnerText);
                string specialErrorMsg = GetMessageByErrNum(errNum);
                if (specialErrorMsg != string.Empty)
                {
                    return specialErrorMsg;
                }
                string sErrorInfo = iHsvResourceManager.GetResourceStringFromHR(iLanguageID, errNum);
                iHsvResourceManager.GetFormattedError(iLanguageID, errNum, xmlMessage, "未知错误", out pvarbstrFormattedError, out pvarbstrTechnicalError);
                return errNum + ":\n" + pvarbstrFormattedError.ToString().Replace("<BR>", "\n");
            }
            catch
            {
                return xmlMessage;
            }
        }

        public static string GetHfmErrorMessage(int pErrNum)
        {
            string specialErrorMsg = GetMessageByErrNum(pErrNum);
            if (specialErrorMsg != string.Empty)
            {
                return specialErrorMsg;
            }
            HsvResourceManager iHsvResourceManager = new HsvResourceManager();
            iHsvResourceManager.Initialize((short)tagHFM_TIERS.HFM_WEB_TIER);
            int iLanguageID = 12;
            object pvarbstrFormattedError;
            object pvarbstrTechnicalError;
            try
            {
                string ErrMessage = string.Empty;
                iHsvResourceManager.GetFormattedError(iLanguageID, pErrNum, ErrMessage, "未知错误", out pvarbstrFormattedError, out pvarbstrTechnicalError);
                return pvarbstrFormattedError.ToString().Replace("<BR>", "\n");
            }
            catch
            {
                return "未知错误。";
            }
        }

        //-2147214570
        private static string GetMessageByErrNum(int pErrNum)
        {
            string retVal = string.Empty;
            switch (pErrNum)
            {
                case -2147214570:
                    retVal = "有下级单位尚未提交，合并单位无法提交。";
                    break;
                default:
                    break;
            }
            return retVal;
        }
    }
}

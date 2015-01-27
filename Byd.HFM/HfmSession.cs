﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HFMWAPPLICATIONSLib;
using HFMWSESSIONLib;
using HFMWSYSTEMINFOLib;
using HSVSESSIONLib;
using HSVSYSTEMINFOLib;
using HSXCLIENTLib;

namespace Byd.HFM
{
    public class HfmSession
    {
        private HsvSession _HsvSession = null;
        private HFMwSession _HFMwSession = null;
        private HfmSession()
        {
            
        }

        public static HfmSession CreateSession(string argDomain, string argUser, string argPassword, string argCluster,
            string argProduct, string argApplication)
        {
            HfmSession hfmSession = new HfmSession();
            hfmSession._HsvSession = CreateHsvSession(argDomain, argUser, argPassword, argCluster, argProduct,
                argApplication);
            hfmSession._HFMwSession = CreateHFMwSession(argDomain, argUser, argPassword, argCluster, argProduct,
                argApplication);
            return hfmSession;
        }

        public static bool TestConnect(string argDomain, string argUser, string argPassword, string argCluster,
            string argProduct, string argApplication)
        {
            bool retValue = false;
            try
            {
                retValue = CreateHsvSession(argDomain, argUser, argPassword, argCluster, argProduct, argApplication) != null;
            }
            catch(Exception ex)
            {
            }
            return retValue;

        }

        private static HsvSession CreateHsvSession(string argDomain, string argUser, string argPassword, string argCluster, string argProduct, string argApplication)
        {
            HsxClient hsxClient = null;
            HsvSession hsvSession = null;
            object server;
            object session;
            try
            {
                hsxClient = new HsxClient();
                hsxClient.SetLogonInfoSSO(argDomain, argUser, "", argPassword);
                hsxClient.OpenApplication(argCluster, argProduct, argApplication, out server, out session);
                hsvSession = (HsvSession) session;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DComHelper.ReleaseHelper(hsxClient);
            }
            return hsvSession;
        }

        private static HFMwSession CreateHFMwSession(string argDomain, string argUser, string argPassword, string argCluster, string argProduct, string argApplication)
        {
            HFMwManageApplications hfmwManageApplications = null; ;
            HFMwSession hfmwSession = null;
            try
            {
                hfmwManageApplications = new HFMwManageApplications();
                hfmwManageApplications.SetLogonInfo(argDomain, argUser, argPassword);
                hfmwSession = (HFMwSession)hfmwManageApplications.OpenApplication(argCluster, argApplication);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hfmwSession;
        }
    }
}

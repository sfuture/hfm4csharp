using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Byd.HFM
{
    public class DComHelper
    {
        public static void ReleaseHelper(object argObject)
        {
            try
            {
                if (argObject != null)
                {
                    Marshal.ReleaseComObject(argObject);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

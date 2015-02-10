using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byd.HFM.Test
{
   public  class HfmHelper
    {

       public static HfmSession CreateHfmSession()
       {
           HfmSession hfmSession = HfmSession.CreateSession("", "admin", "weilai#00", "192.168.192.188", "Financial Management", "GLPCON");
           return hfmSession;
       }
    }
}

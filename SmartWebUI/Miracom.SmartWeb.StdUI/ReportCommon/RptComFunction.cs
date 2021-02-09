using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public sealed class RptComFunction
    {
        // ____/__/__ __:__:__ Data Format 변경
        // ex) 19991231 -> 1999/12/31 or 19991231235959 -> 1999/12/31 23:59:59 변경
        public static string AttachDateFormat(string AttachDate)
        {
            string EditDate=null;

            if (AttachDate.Length == 8 &&  IsNumeric(AttachDate) == true)
            {
                EditDate = AttachDate.Substring(0, 4) + "/" + AttachDate.Substring(4, 2) + "/" + AttachDate.Substring(6, 2);
            }
            else if (AttachDate.Length == 14 && IsNumeric(AttachDate) == true)
            {
                EditDate = AttachDate.Substring(0, 4) + "/" + AttachDate.Substring(4, 2) + "/" + AttachDate.Substring(6, 2) + " ";
                EditDate += AttachDate.Substring(8, 2) + ":" + AttachDate.Substring(10, 2) + ":" + AttachDate.Substring(12, 2);
            }
            return EditDate;
        }

        public static bool IsNumeric(string data)
        {
            bool result = false;

            try
            {
                double a = Convert.ToDouble(data);
                result = true;
            }

            catch
            {
                result = false;
            }
            return result;
        }

       
    }
}

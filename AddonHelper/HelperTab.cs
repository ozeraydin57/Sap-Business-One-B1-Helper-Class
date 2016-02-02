using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddOns.Library.Helper
{
    class HelperTab:HelperControl
    {

        public void ChangeCursor(SAPbouiCOM.Form oForm, string uniqId)
        {
            oForm.Items.Item(uniqId).Click();
        }

    }
}

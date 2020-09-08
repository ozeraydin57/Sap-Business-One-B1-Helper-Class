using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddOns.Library.Helper
{
    public class HelperTextBox : HelperControl
    {
        public string GetTextValue(SAPbouiCOM.Form oForm, string uniqId) //textbox ın değerini alır
        {
            string val = "";
            try
            {
                val = ((SAPbouiCOM.EditText)oForm.Items.Item(uniqId).Specific).Value;

                
            }
            catch 
            {
                
                
            }
            return val;
        }

        public void SetTextValue(SAPbouiCOM.Form oForm, string uniqId, string value) //textbox a değer set et
        {
            ((SAPbouiCOM.EditText)oForm.Items.Item(uniqId).Specific).Value = value;
        }

    }
}

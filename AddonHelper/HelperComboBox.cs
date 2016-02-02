using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddOns.Library.Helper
{
    public class HelperComboBox : HelperControl
    {
        //private SAPbouiCOM.ComboBox GetComboBoxObjet(string itemId) //combo oluştur
        //{
        //    return (SAPbouiCOM.ComboBox)((SAPbouiCOM.ComboBox)(GetItem(itemId).Specific));
        //}
        public void ClearComboBox(SAPbouiCOM.ComboBox cmbbox) //combo siler
        {
            //sil gitsin
        }
        public void FillComboBox(SAPbouiCOM.ComboBox cmbbox, string sql) //combo doldur
        {
            HelperRecordset hrs = new HelperRecordset();

            var oRecordSet = hrs.GetRecordSet();
            oRecordSet.DoQuery(sql);
            cmbbox.ValidValues.Add("", "");
            if (oRecordSet.RecordCount != 0)
            {
                oRecordSet.MoveFirst();
                for (int i = 0; i < oRecordSet.RecordCount; i++)
                {
                    cmbbox.ValidValues.Add(hrs.ReadRecordSetData(oRecordSet, "Description"), hrs.ReadRecordSetData(oRecordSet, "Value"));
                    oRecordSet.MoveNext();
                }
            }
        }
        public void SelectComboBoxValue(SAPbouiCOM.Form oForm, SAPbobsCOM.Recordset oRecordSet, string uniqId, string field) //combobox a değer set et
        {
            try
            {
                string val = oRecordSet.Fields.Item(field).Value.ToString();
                ((SAPbouiCOM.ComboBox)oForm.Items.Item(uniqId).Specific).Select(val);
            }
            catch
            {

            }
        }

        public void SetComboBoxValue(SAPbouiCOM.ComboBox cb, string value) //combobox a değer set et
        {
            try
            {
                cb.Select(value);
            }
            catch
            {

            }
        }

        public void FillComboBox(SAPbouiCOM.ComboBox cb, List<ComboEntity> datasource)
        {
            foreach (var item in datasource)
            {
                cb.ValidValues.Add(item.Value, item.Description);
            }

        }
    }

    public class ComboEntity
    {
        public string Description { get; set; }
        public string Value { get; set; }
    }
}

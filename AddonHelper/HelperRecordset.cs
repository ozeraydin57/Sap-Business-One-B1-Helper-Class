using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddOns.Library.Helper
{
    public class HelperRecordset : HelperControl
    {
        public SAPbobsCOM.Recordset GetRecordSet() // recordset oluştur
        {
            return (SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        }

        public string ReadRecordSetData(SAPbobsCOM.Recordset oRecordSet, string field) //reordset data oku
        {
            return oRecordSet.Fields.Item(field).Value.ToString();
        }

        public void SearchRecordset(SAPbouiCOM.ComboBox cmbbox, string sql) //combo doldur
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

        public bool SearchRecordset(SAPbobsCOM.Recordset oRecordSet, string field, string value)
        {
            HelperRecordset hrs = new HelperRecordset();

            if (oRecordSet.RecordCount != 0)
            {
                oRecordSet.MoveFirst();
                for (int i = 0; i < oRecordSet.RecordCount; i++)
                {
                    var val = oRecordSet.Fields.Item(field).Value.ToString();
                    if (val.Trim() == value.Trim())
                    {
                        return true;
                    }

                    oRecordSet.MoveNext();
                }

                return false;
            }
            else
                return false;
        }

        //tablo adı ve kolon adını verilirse değeri getirir, whereValue ve whereColumn doldurulursa ilgili kolona where cümleciğini ekler
        public string ReadSingelData(string table, string returnColumn, string whereColumn = "", string whereValue = "")
        {

            string sql = " select top 1 " + returnColumn + " from " + table;

            if (!string.IsNullOrEmpty(whereValue) && !string.IsNullOrEmpty(whereColumn))
            {
                sql += " where " + whereColumn + "='" + whereValue + "' ";
            }

            sql += " order by DocEntry Desc ";

            SAPbobsCOM.Recordset oRecordSet = GetRecordSet();
            oRecordSet.DoQuery(sql);
            var _return = oRecordSet.Fields.Item(returnColumn).Value.ToString();

            return _return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace AddOns.Library.Helper
{
    public class HelperControl
    {
        public SAPbouiCOM.Form GetForm(string type, int count = 0) //form oluştur hazırlar
        {
            return (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetForm(type, count);
            
        }

        public string GetCurrentUserId() //login olan danışmanın userid si
        {
            return ((SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany()).UserSignature.ToString();
        }

        public string GetCurrenLanguageId() //login olan danışmanın dil id si
        {
            return ((SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany()).language.ToString();
        }

        public string GetCurrentSlpcode() //login olan danışmanın slpcode u
        {
            var userId = GetCurrentUserId();
            return GetSlpcodeFromUserId(userId);
        }

        public string GetSlpcodeFromUserId(string userId) //user id si verilince slpcodu nu geri veriyor
        {
            HelperRecordset hrs = new HelperRecordset();
            string sql = string.Format("select ohem.SalesPrson as SlpCode from ousr inner join ohem on ousr.USERID=ohem.userId where OUSR.USERID='{0}'", userId);
            var oRecordSet = hrs.GetRecordSet();
            oRecordSet.DoQuery(sql);

            var slpCode = hrs.ReadRecordSetData(oRecordSet, "SlpCode");

            return slpCode;
        }

        public string GetCurrentBranchId() //Seçilmiş şube idsi geri veriyor
        {
            SAPbouiCOM.Form _MenuForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.GetForm("169", 0);
            SAPbouiCOM.StaticText _Text = (SAPbouiCOM.StaticText)_MenuForm.Items.Item("7").Specific;
            string _Caption = _Text.Caption;
            string[] strBranch;
            string _CurrentBranch;
            strBranch = _Caption.Split(':');
            _CurrentBranch = strBranch[1].Trim();

            HelperRecordset hrs = new HelperRecordset();
            string sql = string.Format("select BPLId from OBPL where BPLName like N'%{0}%'", _CurrentBranch);
            var oRecordSet = hrs.GetRecordSet();
            oRecordSet.DoQuery(sql);

            var BPLId = hrs.ReadRecordSetData(oRecordSet, "BPLId");

            return BPLId;
        }

    }
}

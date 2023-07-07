using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RDNIDWRAPPER;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using System.Globalization;
using Oracle.DataAccess.Client;
using System.Text.RegularExpressions;


namespace SVMember
{
    public partial class Frm_ApproveData : Form
    {
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;
        string str;


        public Frm_ApproveData()
        {
            InitializeComponent();
        }

        private void Frm_ApproveData_Load(object sender, EventArgs e)
        {
            ClsMST.GetDb();
            ShowData();
            
        }

        void ShowData() //string id_card
        {

            str = "select " +
                             " appl_docno,   " +
                             " app_date,   " +
                             " member_no,   " +
                             " mbucfprename.prename_desc," +
                             " memb_namen," +
                             " memb_surnamen," +
                             " card_personc, " +
                             " card_personn, " +
                             " memb_addrc, " +
                             " addr_mooc, " +
                             " addr_soic, " +
                             " addr_villagec, " +
                             " addr_roadc, " +
                             " mbucftambol.tambol_desc,  " +
                             " mbucfdistrict.district_desc, " +
                             " mbucfprovince.province_desc, " +
                             " mbucfdistrict.postcode, " + 
                             " memb_addrn, " +
                             " addr_moon, " +
                             " addr_soin, " +
                             " addr_villagen, " +
                             " addr_roadn, " +
                              " mbucfprovince_new.province_desc ||'' new_province, " +
                             " mbucfdistrict_new.district_desc ||'' new_district," +
                             " mbucftambol_new.tambol_desc ||'' new_tambol," +
                             " mbucfdistrict_new.postcode new_postcode," +
                             " mbucfprename.prename_desc || mba.memb_namec || '    ' || mba.memb_surnamec as MbnameC,   " +
                             " mbucfprename.prename_desc || mba.memb_namen || '    ' || mba.memb_surnamen as MbnameN,   " +
                             " memb_addrc ||' หมู่ที่.' ||addr_mooc || ' ซอย.' || addr_soic || ' หมู่บ้าน.' ||addr_villagec || ' ถนน' ||addr_roadc ||' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' ||mbucfprovince.province_desc || ' ' ||mbucfdistrict.postcode Cur_Add," +
                             " memb_addrn ||' หมู่ที่.' ||addr_moon || ' ซอย.' || addr_soin || ' หมู่บ้าน.' ||addr_villagen || ' ถนน' ||addr_roadn ||' ตำบล' || mbucftambol_new.tambol_desc || ' อำเภอ' || mbucfdistrict_new.district_desc || ' จังหวัด' || mbucfprovince_new.province_desc || ' ' ||mbucfdistrict.postcode New_Add, " +
                             " birth_datec, " +
                             " birth_daten, " +
                             " mba.addr_mobilephonec, " +
                             " mba.addr_mobilephonen, " +
                             " mbucfprovince_new.province_code Newprovince_code, " +
                             " mbucfdistrict_new.district_code Newdistrict_code, " +
                             " mbucftambol_new.tambol_code Newtambol_code, " +
                             " mbucfprename.prename_code, " +
                             " app_status " + 
                            
                             " From mbreqchgaddress mba " +
                             " inner join mbucfprename on mba.prename_codec = mbucfprename.prename_code  " +
                             " inner join mbucfprename on mba.prename_coden = mbucfprename.prename_code " +
                             " inner join mbucfprovince on mba.provice_codec = mbucfprovince.province_code " +
                             " inner join mbucfdistrict on mba.amphur_codec = mbucfdistrict.district_code " +
                             " inner join mbucftambol on mba.tambon_codec = mbucftambol.tambol_code " +
                             " inner join mbucfprovince  mbucfprovince_new on mba.province_coden = mbucfprovince_new.province_code" +
                             " inner join mbucfdistrict  mbucfdistrict_new on mba.amphur_coden = mbucfdistrict_new.district_code" +
                             " inner join mbucftambol  mbucftambol_new on mba.tambon_coden = mbucftambol_new.tambol_code" +
                             " where appl_docno ='" + this.Text + "'";

            DataTable dt = ClsMST.SelectQuery(str);
        
            string txt_Crradd = "";

            if (dt.Rows[0]["memb_addrc"].ToString() != "") { txt_Crradd += dt.Rows[0]["memb_addrc"].ToString(); }
            if (dt.Rows[0]["addr_mooc"].ToString() != "") { txt_Crradd += " หมู่ที่ " + dt.Rows[0]["addr_mooc"].ToString(); }
            if (dt.Rows[0]["addr_soic"].ToString() != "") { txt_Crradd += " ซอย " + dt.Rows[0]["addr_soic"].ToString(); }
            if (dt.Rows[0]["addr_villagec"].ToString() != "") { txt_Crradd += " หมู่บ้าน " + dt.Rows[0]["addr_villagec"].ToString(); }
            if (dt.Rows[0]["addr_roadc"].ToString() != "") { txt_Crradd += " ถนน " + dt.Rows[0]["addr_roadc"].ToString(); }

            txt_Crradd += " ตำบล" + dt.Rows[0]["tambol_desc"].ToString();
            txt_Crradd += " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            txt_Crradd += " จังหวัด" + dt.Rows[0]["province_desc"].ToString();
            txt_Crradd += " จังหวัด" + dt.Rows[0]["postcode"].ToString();
            
            string txt_Newadd = "";

            if (dt.Rows[0]["memb_addrn"].ToString() != "") { txt_Newadd += dt.Rows[0]["memb_addrn"].ToString(); }
            if (dt.Rows[0]["addr_moon"].ToString() != "") { txt_Newadd += " หมู่ที่ " + dt.Rows[0]["addr_moon"].ToString(); }
            if (dt.Rows[0]["addr_soin"].ToString() != "") { txt_Newadd += " ซอย " + dt.Rows[0]["addr_soin"].ToString(); }
            if (dt.Rows[0]["addr_villagen"].ToString() != "") { txt_Newadd += "หมู่บ้าน " + dt.Rows[0]["addr_villagen"].ToString(); }
            if (dt.Rows[0]["addr_roadn"].ToString() != "") { txt_Newadd += " ถนน " + dt.Rows[0]["addr_roadn"].ToString(); }



            txt_Newadd += " " + dt.Rows[0]["New_Add"].ToString();

            lb_Member_noC.Text = dt.Rows[0]["member_no"].ToString();
            lb_Member_noN.Text = dt.Rows[0]["member_no"].ToString();
            lb_Card_personC.Text = dt.Rows[0]["card_personc"].ToString();
            lb_Card_personN.Text = dt.Rows[0]["card_personn"].ToString();
            lb_NameC.Text = dt.Rows[0]["MbnameC"].ToString();
            lb_PrenameN.Text = dt.Rows[0]["prename_desc"].ToString();
            lb_NameN.Text = dt.Rows[0]["memb_namen"].ToString();
            lb_SernameN.Text = dt.Rows[0]["memb_surnamen"].ToString();
            lb_Birth_DateC.Text = dt.Rows[0]["birth_datec"].ToString();
            lb_Birth_DateN.Text = dt.Rows[0]["birth_daten"].ToString();
            lb_MobilephoneC.Text = dt.Rows[0]["addr_mobilephonec"].ToString();
            lb_MobilephoneN.Text = dt.Rows[0]["addr_mobilephonen"].ToString();
            lb_AddressC.Text = txt_Crradd.ToString();
           // lb_HomenoN.Text = txt_Newadd.ToString();
            lb_HomenoN.Text = dt.Rows[0]["memb_addrn"].ToString();
            lb_MooN.Text = dt.Rows[0]["addr_moon"].ToString();
            lb_SoiN.Text = dt.Rows[0]["addr_soin"].ToString();
            lb_VillageN.Text = dt.Rows[0]["addr_villagen"].ToString();
            lb_RoadN.Text = dt.Rows[0]["addr_roadn"].ToString();
            lb_TambolN.Text = dt.Rows[0]["new_tambol"].ToString();
            lb_AumphurN.Text = dt.Rows[0]["new_district"].ToString();
            lb_ProvinceN.Text = dt.Rows[0]["new_province"].ToString();
            lb_PostcodeN.Text = dt.Rows[0]["new_postcode"].ToString();
            lb_Province_Code.Text = dt.Rows[0]["Newprovince_code"].ToString();
            lb_Aumphur_Code.Text = dt.Rows[0]["Newdistrict_code"].ToString();
            lb_Tambol_Code.Text = dt.Rows[0]["Newtambol_code"].ToString();
            lb_Prename_Code.Text = dt.Rows[0]["prename_code"].ToString();
            lb_App_Status.Text = dt.Rows[0]["app_status"].ToString();

            this.Text = lb_Apv_Id.Text;
            


           // CheckData();
        }

        void UpdateData() 
        {
            sql = "UPDATE mbmembmaster" +
                " SET member_no = '" + lb_Member_noN.Text + "'" +
                ", card_person = '" + lb_Card_personN.Text.Replace("-", "") + "'" +
                ", prename_code = '" + lb_Prename_Code.Text + "'" +
                ", memb_name = '" + lb_NameN.Text + "'" +
                ", memb_surname = '" + lb_SernameN.Text.Trim() + "'" +
                ", birth_date = TO_DATE('" + Fc.GetshotDate(lb_Birth_DateN.Text ,0) + "'," + "'MM/DD/YYYY')" +
                ", addr_mobilephone = '" + lb_MobilephoneN.Text + "'" +
                ", addr_no = '" + lb_HomenoN.Text + "'" +
                ", addr_moo = '" + lb_MooN.Text + "'" +
                ", addr_soi = '" + lb_SoiN.Text + "'" +
                ", addr_village = '" + lb_VillageN.Text + "'" +
                ", addr_road = '" + lb_RoadN.Text + "'" +
                ", tambol_code = '" + lb_Tambol_Code.Text + "'" +
                ", amphur_code = '" + lb_Aumphur_Code.Text + "'" +
                ", province_code = '" + lb_Province_Code.Text + "'" +
                ", addr_postcode = '" + lb_PostcodeN.Text + "'" +
                " WHERE card_person = '" + lb_Card_personN.Text.Replace("-", "") + "'";
                
            ClsMST.Save_ORACLE(sql);

            sql = "UPDATE mbreqchgaddress" +
                " SET app_status = '1'" +
                 ", apv_id = '" + lb_Apv_Id.Text + "'" +
                 ", apv_date = TO_DATE('" + Fc.GetshotDate(lb_Apv_Date.Text, 0) + "'," + "'MM/DD/YYYY')" +
                 " WHERE card_personn = '" + lb_Card_personN.Text.Replace("-", "") + "'";

            ClsMST.Save_ORACLE(sql);

        }


        private void btn_Show_Data_Click(object sender, EventArgs e)
        {
            lb_Apv_Date.Text = DateTime.Now.ToString("MM/dd/yyyy");

            UpdateData();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Frm_Approve2 Frm = new Frm_Approve2();
            Frm.Show();
            this.Close();
  
        }

      

   




        //private void ClearData()
        //{
        //    // Clear the text boxes
        //    m_txtID.Text = "";
        //    m_txtFullNameT.Text = "";
        //    m_txtFullNameE.Text = "";
        //    m_txtBrithDate.Text = "";
        //    m_txtAddress.Text = "";
        //    // Clear the picture box
        //    m_picPhoto.Image = null;
        //    // Clear any other relevant fields
        //    lb_nameInBut.Text = "";
        //    lb_surnameInBut.Text = "";
        //    //label2.Text = "";
        //    lb_mbno.Text = "";
        //    lbMb_name.Text = "";
        //    lbMb_Add.Text = "";
        //    lbMb_Add2.Text = "";
        //    lb_BirthdateC.Text = "";
        //    lb_Tel.Text = "";
        //    lb_Errortext.Text = "";
        //    label2.Text = "";
        //    add_check.Visible = false;
        //    ID_check.Visible = false;
        //    Tname_check.Visible = false;
        //    Ename_check.Visible = false;
        //    birth_check.Visible = false;
        //    birth_check2.Visible = false;
        //    tel_chexk.Visible = false;
        //    add_cross2.Visible = false;
        //    Tname_cross2.Visible = false;
        //    mbno_check.Visible = false;
        //    add_check2.Visible = false;
        //    crrAdd_cross.Visible = false;
        //    Tname_check2.Visible = false;
        //    crrAdd_check.Visible = false;
        //    crrBirth_cross.Visible = false;



        //    CardStateChange = false;

        //}
    }
}

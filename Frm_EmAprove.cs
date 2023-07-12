using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SVMember
{
    public partial class Frm_EmAprove : Form
    {
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;
        string str;

        public Frm_EmAprove()
        {
            InitializeComponent();
        }

        private void Frm_EmAprove_Load(object sender, EventArgs e)
        {
            ClsMST.GetDb();
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
                             " where appl_docno ='" + lb_Member_noN.Text + "'";

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

        private void btn_Show_Click(object sender, EventArgs e)
        {
            ShowData();
        }

    }
}

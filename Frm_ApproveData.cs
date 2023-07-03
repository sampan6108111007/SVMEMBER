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

            //str = "select " +
            //                 " member_no,   " +
            //                 " card_personc,   " +
            //                 " card_personn,    " +
            //                 " mbucfprename.prename_code || '' prename_code ," +
            //                 " mbucfprename.prename_desc," +
            //                 //" memb_namec," +
            //                 " memb_surnamec," +
            //                 " mbucfprename.prename_desc || mba.memb_namec || '    ' || mba.memb_surnamec as MbnameC,   " +
            //                 " memb_namen,   " +
            //                 " memb_surnamen,  " +
            //                 " mbucfprename.prename_desc || mba.memb_namen || '    ' || mba.memb_surnamen as MbnameN,   " +
            //                 " mba.memb_addrc,   " +
            //                 " mba.addr_mooc,   " +
            //                 " mba.addr_soic,   " +
            //                 " mba.addr_villagec,   " +
            //                 " mba.addr_roadc,   " +
            //                 " mba.tambon_codec,   " +
            //                 " mba.amphur_codec,     " +
            //                 " mba.provice_codec,  " +
            //                 " mbucftambol.tambol_desc, " +
            //                 " mbucfdistrict.district_desc,  " +
            //                 " mbucfprovince.province_desc, " +
            //                 " mbucfdistrict.postcode, " +
            //                // " memb_addrc ||' หมู่ที่.' ||addr_mooc || ' ซอย.' || addr_soic || ' หมู่บ้าน.' ||addr_villagec || ' ถนน' ||addr_roadc ||' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' ||mbucfprovince.province_desc || ' ' ||mbucfdistrict.postcode Cur_Add, " +
            //                 " mba.memb_addrn,   " +
            //                 " mba.addr_moon,   " +
            //                 " mba.addr_soin,   " +
            //                 " mba.addr_villagen,   " +
            //                 " mba.addr_roadn, " +
            //                 " mba.tambon_coden, " +
            //                 " mba.amphur_coden, " +
            //                 " mba.province_coden, " +
            //                 " mbucftambol.tambol_desc, " +
            //                 " mbucfdistrict.district_desc, " +
            //                 " mbucfprovince.province_desc, " +
            //                 " mbucfdistrict.postcode, " +
            //                 //" memb_addrn ||' หมู่ที่.' ||addr_moon || ' ซอย.' || addr_soin || ' หมู่บ้าน.' ||addr_villagen || ' ถนน' ||addr_roadn ||' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' ||mbucfprovince.province_desc || ' ' ||mbucfdistrict.postcode New_Add, " +
            //                 " memb_addrn ||' หมู่ที่.' ||addr_moon || ' ซอย.' || addr_soin || ' หมู่บ้าน.' ||addr_villagen || ' ถนน' ||addr_roadn ||' ตำบล' || mbucftambol_new.tambol_desc || ' อำเภอ' || mbucfdistrict_new.district_desc || ' จังหวัด' || mbucfprovince_new.province_desc || ' ' ||mbucfdistrict.postcode New_Add," +
            //                 " birth_datec, " +
            //                 " birth_daten, " +
            //                 " mba.addr_mobilephonec, " +
            //                 " mba.addr_mobilephonen " +
            //                 " mbucfprovince_new.province_desc ||'' new_province," +
            //                 " mbucfdistrict_new.district_desc ||'' new_district," +
            //                 " mbucftambol_new.tambol_desc ||'' new_tambol" +
            //                 " From mbreqchgaddress mba " +
            //                 " inner join mbucfprename on mba.prename_codec = mbucfprename.prename_code  " +
            //                 " inner join mbucfprename on mba.prename_coden = mbucfprename.prename_code " +
            //                 " inner join mbucfprovince on mba.provice_codec = mbucfprovince.province_code " +
            //                 " inner join mbucfdistrict on mba.amphur_codec = mbucfdistrict.district_code " +
            //                 " inner join mbucftambol on mba.tambon_codec = mbucftambol.tambol_code " +
            //                 " inner join mbucfprovince  mbucfprovince_new on mba.province_coden = mbucfprovince_new.province_code" +
            //                 " inner join mbucfdistrict  mbucfdistrict_new on mba.amphur_coden = mbucfdistrict_new.district_code" +
            //                 " inner join mbucftambol  mbucftambol_new on mba.tambon_coden = mbucftambol_new.tambol_code" +
            //                 " where appl_docno ='" + this.Text + "'";


            str = "select " +
                             " appl_docno,   " +
                             " app_date,   " +
                             " member_no,   " +
                             " mbucfprename.prename_desc," +
                             " memb_namen," +
                             " memb_surnamen," +
                             " card_personc,   " +
                             " card_personn,    " +
                             " memb_addrc,    " +
                             " addr_mooc,    " +
                             " addr_soic,    " +
                             " addr_villagec,    " +
                             " addr_roadc,    " +
                             " mbucftambol.tambol_desc,  " +
                             " mbucfdistrict.district_desc, " +
                             " mbucfprovince.province_desc, " +
                             " mbucfdistrict.postcode, " + 
                             " memb_addrn,    " +
                             " addr_moon,    " +
                             " addr_soin,    " +
                             " addr_villagen,    " +
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
                             " mba.addr_mobilephonen " +
                            
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





            //if (lb_Errortext.Text != "")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
            //{
            //    return;
            //}



            DataTable dt = ClsMST.SelectQuery(str);
            //if (dt.Rows.Count <= 0)
            //{

            //    string message = "ข้อมูลบัตรประชาชน ไม่มีอยู่ในข้อมูลสมาชิกสหกรณ์ กรุณาติดต่อเจ้าหน้าที่";
            //    //lb_Errortext.Text = message;

            //    if (MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
            //    {
            //        return;
            //    };

            //}


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
            lb_HomenoN.Text = txt_Newadd.ToString();
            lb_MooN.Text = dt.Rows[0]["addr_moon"].ToString();
            lb_SoiN.Text = dt.Rows[0]["addr_soin"].ToString();
            lb_VillageN.Text = dt.Rows[0]["addr_villagen"].ToString();
            lb_RoadN.Text = dt.Rows[0]["addr_roadn"].ToString();
            lb_TambolN.Text = dt.Rows[0]["new_tambol"].ToString();
            lb_AumphurN.Text = dt.Rows[0]["new_district"].ToString();
            lb_ProvinceN.Text = dt.Rows[0]["new_province"].ToString();
            lb_PostcodeN.Text = dt.Rows[0]["new_postcode"].ToString();

            


            //lbMb_Add.Text = txt_add.ToString();
            //lbMb_name.Text = dt.Rows[0]["Mbname"].ToString();
            //lbMb_Add2.Text = txt_Crradd.ToString();
            //lb_BirthdateC.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 9);
            //lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
            //lb_Tel.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            //lb_telC.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            //lb_PostcodeC.Text = dt.Rows[0]["curraddr_postcode"].ToString();
            //lb_PrenameC.Text = dt.Rows[0]["prename_code"].ToString();
            //lb_PrenameCode.Text = dt.Rows[0]["prename_code"].ToString();
            //lb_nameC.Text = dt.Rows[0]["memb_name"].ToString();
            //lb_surnameC.Text = dt.Rows[0]["memb_surname"].ToString();
            //lb_HomeNoC.Text = dt.Rows[0]["curraddr_no"].ToString();
            //lb_MooC.Text = dt.Rows[0]["curraddr_moo"].ToString();
            //lb_TrokC.Text = dt.Rows[0]["curraddr_village"].ToString();
            //lb_SoiC.Text = dt.Rows[0]["curraddr_soi"].ToString();
            //lb_RoadC.Text = dt.Rows[0]["curraddr_road"].ToString();
            //lb_TumbolC.Text = dt.Rows[0]["Currtambol_code"].ToString();
            //lb_AmphoeC.Text = dt.Rows[0]["Curramphur_CODE"].ToString();
            //lb_ProvinceC.Text = dt.Rows[0]["CurrPROVINCE_CODE"].ToString();
            //lb_PostcodeC.Text = dt.Rows[0]["Curraddr_POSTCODE"].ToString();
            //lb_IDcardC.Text = dt.Rows[0]["card_person"].ToString();
            //lb_BirthdateC2.Text = dt.Rows[0]["birth_date"].ToString();

           // CheckData();


        }

        private void btn_Show_Data_Click(object sender, EventArgs e)
        {
            //ShowData();
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

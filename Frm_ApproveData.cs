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
        }

        void ShowData() //string id_card
        {

            str = "select " +
                             " mb.MEMBER_NO,   " +
                             " mb.CARD_PERSON,    " +
                              "mbucfprename.prename_code || '' prename_code ," +
                             "mbucfprename.prename_desc," +
                             "mb.MEMB_NAME," +
                             "mb.MEMB_SURNAME," +
                             " mbucfprename.prename_desc || mb.MEMB_NAME || '    ' || mb.MEMB_SURNAME as Mbname,   " +
                             " mb.MEMBGROUP_CODE,   " +
                             " mbucfmembgroup.membgroup_desc,  " +
                             " mb.resign_status,   " +
                             " mb.MEMBER_DATE,   " +
                             " mb.BIRTH_DATE,   " +
                             " mb.MEMBER_STATUS,   " +
                             " mb.RETRY_DATE,   " +
                             " mb.RESIGN_DATE,   " +
                             " mb.MATE_NAME,   " +
                             " mb.addr_phone,     " +
                             " mb.addr_mobilephone,  " +
                             " mb.addr_no, " +
                             " mb.addr_moo,  " +
                             " mb.addr_SOI, " +
                             " mb.addr_village, " +
                             " mb.addr_ROAD, " +
                //" mb.addr_no ||' หมู่ที่.' ||mb.addr_moo || ' ซอย.' || mb.addr_SOI || ' หมู่บ้าน.' ||mb.addr_village || ' ถนน' ||mb.addr_ROAD || ' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' || mbucfprovince.province_desc || ' ' ||mb.addr_postcode Add1," +
                             " mb.tambol_code,   " +
                             " mb.amphur_CODE,   " +
                             " mb.PROVINCE_CODE,   " +
                             " mb.addr_POSTCODE,   " +
                             " mbucftambol.tambol_desc, " +
                             " mbucfdistrict.district_desc, " +
                             " mbucfprovince.province_desc, " +
                //" mb.Curraddr_no ||' หมู่ที่.' || mb.Curraddr_moo || ' ซอย.' || mb.Curraddr_SOI || ' หมู่บ้าน.' || mb.Curraddr_village || ' ถนน' || mb.Curraddr_ROAD || ' ตำบล' || mbucftambol_curr.tambol_desc || ' อำเภอ' || mbucfdistrict_curr.district_desc || ' จังหวัด' ||mbucfprovince_curr.province_desc || ' ' ||mb.Curraddr_POSTCODE Cur_Add1, " +
                             " ' ตำบล' || mbucftambol_curr.tambol_desc || ' อำเภอ' || mbucfdistrict_curr.district_desc || ' จังหวัด' ||mbucfprovince_curr.province_desc || ' ' ||mb.Curraddr_POSTCODE Cur_Add1, " +
                             " mb.Curraddr_no, " +
                             " mb.Curraddr_moo, " +
                             " mb.Curraddr_SOI, " +
                             " mb.Curraddr_village, " +
                             " mb.Curraddr_ROAD, " +
                             " mb.Currtambol_code,     " +
                             " mb.Curramphur_CODE,     " +
                             " mb.CurrPROVINCE_CODE,     " +
                             " mb.Curraddr_POSTCODE,  " +
                             " mbucfprovince_curr.province_desc ||'' crr_province," +
                             " mbucfdistrict_curr.district_desc ||'' crr_district, " +
                             " mbucftambol_curr.tambol_desc ||'' crr_tambol, " +
                             " mb.Curraddr_POSTCODE " +
                             " From MBMEMBMASTER mb  " +
                             " inner join mbucfprename on mb.prename_code = mbucfprename.prename_code  " +
                             " inner join mbucfmembgroup on mb.MEMBGROUP_CODE = mbucfmembgroup.membgroup_code " +
                             " inner join mbucfprovince on mb.province_code = mbucfprovince.province_code " +
                             " inner join mbucfdistrict on mb.amphur_code = mbucfdistrict.district_code " +
                             " inner join mbucftambol on mb.tambol_code = mbucftambol.tambol_code " +
                             " inner join mbucfprovince  mbucfprovince_curr on mb.currprovince_code = mbucfprovince_curr.province_code" +
                             " inner join mbucfdistrict  mbucfdistrict_curr on mb.curramphur_code = mbucfdistrict_curr.district_code" +
                             " inner join mbucftambol  mbucftambol_curr on mb.currtambol_code = mbucftambol_curr.tambol_code" +
                             " where mb.Card_person ='" + m_txtID.Text.Replace("-", "") + "' AND  mb.resign_status ='0'";






            if (lb_Errortext.Text != "")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
            {
                return;
            }



            DataTable dt = ClsMST.SelectQuery(str);
            if (dt.Rows.Count <= 0)
            {

                string message = "ข้อมูลบัตรประชาชน ไม่มีอยู่ในข้อมูลสมาชิกสหกรณ์ กรุณาติดต่อเจ้าหน้าที่";
                lb_Errortext.Text = message;

                if (MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    return;
                };

            }



            string txt_add = "";

            if (dt.Rows[0]["addr_no"].ToString() != "") { txt_add += dt.Rows[0]["addr_no"].ToString(); }
            if (dt.Rows[0]["addr_moo"].ToString() != "") { txt_add += " หมู่ที่ " + dt.Rows[0]["addr_moo"].ToString(); }
            if (dt.Rows[0]["addr_SOI"].ToString() != "") { txt_add += " ซอย " + dt.Rows[0]["addr_moo"].ToString(); }
            if (dt.Rows[0]["addr_village"].ToString() != "") { txt_add += " หมู่บ้าน " + dt.Rows[0]["addr_village"].ToString(); }
            if (dt.Rows[0]["addr_ROAD"].ToString() != "") { txt_add += " ถนน " + dt.Rows[0]["addr_ROAD"].ToString(); }

            txt_add += " ตำบล" + dt.Rows[0]["tambol_desc"].ToString();
            txt_add += " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            txt_add += " จังหวัด" + dt.Rows[0]["province_desc"].ToString();
            lb_PostcodeInBut.Text = dt.Rows[0]["addr_postcode"].ToString();

            string txt_Crradd = "";

            if (dt.Rows[0]["Curraddr_no"].ToString() != "") { txt_Crradd += dt.Rows[0]["Curraddr_no"].ToString(); }
            if (dt.Rows[0]["Curraddr_moo"].ToString() != "") { txt_Crradd += " หมู่ที่ " + dt.Rows[0]["Curraddr_moo"].ToString(); }
            if (dt.Rows[0]["Curraddr_SOI"].ToString() != "") { txt_Crradd += " ซอย " + dt.Rows[0]["Curraddr_SOI"].ToString(); }
            if (dt.Rows[0]["Curraddr_village"].ToString() != "") { txt_Crradd += "หมู่บ้าน " + dt.Rows[0]["Curraddr_village"].ToString(); }
            if (dt.Rows[0]["Curraddr_ROAD"].ToString() != "") { txt_Crradd += " ถนน " + dt.Rows[0]["Curraddr_ROAD"].ToString(); }



            txt_Crradd += " " + dt.Rows[0]["Cur_Add1"].ToString();



            lbMb_Add.Text = txt_add.ToString();
            lbMb_name.Text = dt.Rows[0]["Mbname"].ToString();
            lbMb_Add2.Text = txt_Crradd.ToString();
            lb_BirthdateC.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 9);
            lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
            lb_Tel.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            lb_telC.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            lb_PostcodeC.Text = dt.Rows[0]["curraddr_postcode"].ToString();
            lb_PrenameC.Text = dt.Rows[0]["prename_code"].ToString();
            lb_PrenameCode.Text = dt.Rows[0]["prename_code"].ToString();
            lb_nameC.Text = dt.Rows[0]["memb_name"].ToString();
            lb_surnameC.Text = dt.Rows[0]["memb_surname"].ToString();
            lb_HomeNoC.Text = dt.Rows[0]["curraddr_no"].ToString();
            lb_MooC.Text = dt.Rows[0]["curraddr_moo"].ToString();
            lb_TrokC.Text = dt.Rows[0]["curraddr_village"].ToString();
            lb_SoiC.Text = dt.Rows[0]["curraddr_soi"].ToString();
            lb_RoadC.Text = dt.Rows[0]["curraddr_road"].ToString();
            lb_TumbolC.Text = dt.Rows[0]["Currtambol_code"].ToString();
            lb_AmphoeC.Text = dt.Rows[0]["Curramphur_CODE"].ToString();
            lb_ProvinceC.Text = dt.Rows[0]["CurrPROVINCE_CODE"].ToString();
            lb_PostcodeC.Text = dt.Rows[0]["Curraddr_POSTCODE"].ToString();
            lb_IDcardC.Text = dt.Rows[0]["card_person"].ToString();
            lb_BirthdateC2.Text = dt.Rows[0]["birth_date"].ToString();

            CheckData();


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

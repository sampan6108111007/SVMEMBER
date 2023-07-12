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
    public partial class Frm_Approve2 : Form
    {
        public Frm_Approve2()
        {
            InitializeComponent();
        }

        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        DataTable dt_LNNow = new DataTable();
        DataTable dt_LNHun = new DataTable();
        DataTable dt_WF = new DataTable();
        Int32 _LstID = 0;
        Int32 _LstYY = 0;

        private void Frm_Approve2_Load(object sender, EventArgs e)
        {
                ClsMST.GetDb();
             Get_Request();
             ShowData();
            
            
        }

        void Get_Request()
        {


            string str = "select " +
                             " appl_docno,   " +
                             " app_date,   " +
                             " member_no,   " +
                             " card_personc,   " +
                             " card_personn,    " +
                             " app_status," +
                             " mbucfprename.prename_desc || mba.memb_namec || '    ' || mba.memb_surnamec as MbnameC,   " +
                             " mbucfprename.prename_desc || mba.memb_namen || '    ' || mba.memb_surnamen as MbnameN,   " +
                             " memb_addrc ||' หมู่ที่.' ||addr_mooc || ' ซอย.' || addr_soic || ' หมู่บ้าน.' ||addr_villagec || ' ถนน' ||addr_roadc ||' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' ||mbucfprovince.province_desc || ' ' ||mbucfdistrict.postcode Cur_Add," +
                             " memb_addrn ||' หมู่ที่.' ||addr_moon || ' ซอย.' || addr_soin || ' หมู่บ้าน.' ||addr_villagen || ' ถนน' ||addr_roadn ||' ตำบล' || mbucftambol_new.tambol_desc || ' อำเภอ' || mbucfdistrict_new.district_desc || ' จังหวัด' || mbucfprovince_new.province_desc || ' ' ||mbucfdistrict.postcode New_Add, " +
                             " birth_datec, " +
                             " birth_daten, " +
                             " mba.addr_mobilephonec, " +
                             " mba.addr_mobilephonen, " +
                             " 0 as sYES,0 as sNO " +
                //" mbucfprovince_new.province_desc ||'' new_province, " +
                //" mbucfdistrict_new.district_desc ||'' new_district," +
                //" mbucftambol_new.tambol_desc ||'' new_tambol" +
                             " From mbreqchgaddress mba " +
                             " inner join mbucfprename on mba.prename_codec = mbucfprename.prename_code  " +
                             " inner join mbucfprename on mba.prename_coden = mbucfprename.prename_code " +
                             " inner join mbucfprovince on mba.provice_codec = mbucfprovince.province_code " +
                             " inner join mbucfdistrict on mba.amphur_codec = mbucfdistrict.district_code " +
                             " inner join mbucftambol on mba.tambon_codec = mbucftambol.tambol_code " +
                             " inner join mbucfprovince  mbucfprovince_new on mba.province_coden = mbucfprovince_new.province_code" +
                             " inner join mbucfdistrict  mbucfdistrict_new on mba.amphur_coden = mbucfdistrict_new.district_code" +
                             " inner join mbucftambol  mbucftambol_new on mba.tambon_coden = mbucftambol_new.tambol_code" +
                             " where app_status ='0'";

            DataTable dt = ClsMST.SelectQuery(str);
            dataGridView1.DataSource = dt;


        }

        void ShowData() //string id_card
        {

           string str = "select " +
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
                             " where app_status ='0'";

            DataTable dt = ClsMST.SelectQuery(str);

            //string txt_Crradd = "";

            //if (dt.Rows[0]["memb_addrc"].ToString() != "") { txt_Crradd += dt.Rows[0]["memb_addrc"].ToString(); }
            //if (dt.Rows[0]["addr_mooc"].ToString() != "") { txt_Crradd += " หมู่ที่ " + dt.Rows[0]["addr_mooc"].ToString(); }
            //if (dt.Rows[0]["addr_soic"].ToString() != "") { txt_Crradd += " ซอย " + dt.Rows[0]["addr_soic"].ToString(); }
            //if (dt.Rows[0]["addr_villagec"].ToString() != "") { txt_Crradd += " หมู่บ้าน " + dt.Rows[0]["addr_villagec"].ToString(); }
            //if (dt.Rows[0]["addr_roadc"].ToString() != "") { txt_Crradd += " ถนน " + dt.Rows[0]["addr_roadc"].ToString(); }

            //txt_Crradd += " ตำบล" + dt.Rows[0]["tambol_desc"].ToString();
            //txt_Crradd += " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            //txt_Crradd += " จังหวัด" + dt.Rows[0]["province_desc"].ToString();
            //txt_Crradd += " จังหวัด" + dt.Rows[0]["postcode"].ToString();

            //string txt_Newadd = "";

            //if (dt.Rows[0]["memb_addrn"].ToString() != "") { txt_Newadd += dt.Rows[0]["memb_addrn"].ToString(); }
            //if (dt.Rows[0]["addr_moon"].ToString() != "") { txt_Newadd += " หมู่ที่ " + dt.Rows[0]["addr_moon"].ToString(); }
            //if (dt.Rows[0]["addr_soin"].ToString() != "") { txt_Newadd += " ซอย " + dt.Rows[0]["addr_soin"].ToString(); }
            //if (dt.Rows[0]["addr_villagen"].ToString() != "") { txt_Newadd += "หมู่บ้าน " + dt.Rows[0]["addr_villagen"].ToString(); }
            //if (dt.Rows[0]["addr_roadn"].ToString() != "") { txt_Newadd += " ถนน " + dt.Rows[0]["addr_roadn"].ToString(); }




            //txt_Newadd += " " + dt.Rows[0]["New_Add"].ToString();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลที่อนุมัติ  ", "แจ้งเดือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lb_Member_noN.Text = dt.Rows[0]["member_no"].ToString();
            lb_Card_personN.Text = dt.Rows[0]["card_personn"].ToString();
            lb_NameN.Text = dt.Rows[0]["memb_namen"].ToString();
            lb_SernameN.Text = dt.Rows[0]["memb_surnamen"].ToString();
            lb_Birth_DateN.Text = dt.Rows[0]["birth_daten"].ToString();
            lb_MobilephoneN.Text = dt.Rows[0]["addr_mobilephonen"].ToString();
            lb_HomenoN.Text = dt.Rows[0]["memb_addrn"].ToString();
            lb_MooN.Text = dt.Rows[0]["addr_moon"].ToString();
            lb_SoiN.Text = dt.Rows[0]["addr_soin"].ToString();
            lb_VillageN.Text = dt.Rows[0]["addr_villagen"].ToString();
            lb_RoadN.Text = dt.Rows[0]["addr_roadn"].ToString();
            lb_PostcodeN.Text = dt.Rows[0]["new_postcode"].ToString();
            lb_Appl_Docno.Text = dt.Rows[0]["appl_docno"].ToString();
            lb_Province_Code.Text = dt.Rows[0]["Newprovince_code"].ToString();
            lb_Aumphur_Code.Text = dt.Rows[0]["Newdistrict_code"].ToString();
            lb_Tambol_Code.Text = dt.Rows[0]["Newtambol_code"].ToString();
            lb_Prename_Code.Text = dt.Rows[0]["prename_code"].ToString();

            lb_Apv_Id.Text = this.Text;
            
            
            
        }

        void UpdateData()
        {
            sql = "UPDATE mbmembmaster" +
                " SET member_no = '" + lb_Member_noN.Text + "'" +
                ", card_person = '" + lb_Card_personN.Text.Replace("-", "") + "'" +
                ", prename_code = '" + lb_Prename_Code.Text + "'" +
                ", memb_name = '" + lb_NameN.Text + "'" +
                ", memb_surname = '" + lb_SernameN.Text.Trim() + "'" +
                ", birth_date = TO_DATE('" + Fc.GetshotDate(lb_Birth_DateN.Text, 0) + "'," + "'MM/DD/YYYY')" +
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

      

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string _Req_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
          
            if (_Req_no == "") { return; }

            Frm_ApproveData Frm = new Frm_ApproveData();
           

            
            Frm.Text = _Req_no;
            this.Text = Frm.Text;

            
            
            Frm.lb_Apv_Id.Text = lb_Apv_Id.Text;


            Frm.ShowDialog();
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                checkBox2.Checked = false;
                Get_ItemChk(1);
            }
            catch (Exception)
            {

                return;
            }
        }

     
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //     checkBox2.Checked = true;
                checkBox1.Checked = false;

                Get_ItemChk(2);

            }
            catch (Exception)
            {

                return;
            }
        }


        private void Get_ItemChk(int getChk)
        {
            Int32 xReturn = 0;
            string xFieldCls = "", xFieldSet = "";

            if (getChk == 1)
            {
                xFieldSet = "sYES";
                xFieldCls = "sNO";
            }
            else
            {
                xFieldSet = "sNO";
                xFieldCls = "sYES";
            }


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Rows[i].Cells[xFieldSet].Value = true;
                dataGridView1.Rows[i].Cells[xFieldCls].Value = false;

            }
        }

        private Int32 GetChkTik()
        {
            Int32 xRe = 0;
            string ChkY = "", ChkN = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                ChkY = dataGridView1.Rows[i].Cells["sYES"].Value.ToString();
                ChkN = dataGridView1.Rows[i].Cells["sNO"].Value.ToString();

                if (ChkY != "0" && ChkN != "0")
                {
                    xRe++;
                   
                }
                if (ChkY == "1" && ChkN == "0")
                {
                    xRe++;
                }
                if (ChkY == "0" && ChkN == "1")
                {
                    string message = "ท่านยืนยันที่จะไม่อนุมัติใช่หรือไม่";

                    if (MessageBox.Show(message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        this.Close(); //MessageBox.Show("อัปเดตข้อมูลของท่านเรียบร้อย");
                    }
                   
                   // xRe++;


                }

            }
            return xRe;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string xFieldSet = "";
            string xFieldCls = "";

            try
            {

                if (dataGridView1.Columns[e.ColumnIndex].Name == "sYES")
                {
                    xFieldSet = "sYES";
                    xFieldCls = "sNO";
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldSet].Value = true;
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldCls].Value = false;
 
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "sNO")
                {
                    xFieldSet = "sNO";
                    xFieldCls = "sYES";
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldSet].Value = true;
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldCls].Value = false;
                   
                }

            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lb_Apv_Date.Text = DateTime.Now.ToString("MM/dd/yyyy");

            if (GetChkTik() == 0)
            {
                MessageBox.Show("ท่านไม่ได้เลือกรายการอนุมัติ กรุณาเลือกรายการอนุมัติ  ", "แจ้งเดือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateData();
            MessageBox.Show("อนุมัตเรียบร้อย  ", "แจ้งเดือน",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }



    }
}

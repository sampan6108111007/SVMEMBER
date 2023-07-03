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

        }

        void Get_Request()
        {

            
            string str = "select " +
                             " appl_docno,   " +
                             " app_date,   " +
                             " member_no,   " +
                             " card_personc,   " +
                             " card_personn,    " +                       
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

      

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string _Req_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
          
            if (_Req_no == "") { return; }

            Frm_ApproveData Frm = new Frm_ApproveData();

            
            Frm.Text = _Req_no;
            Frm.Show();
            this.Hide();
           // Frm.ShowDialog();
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


                //if (xFieldSet == "sYES")
                //{
                //    dataGridView1.Rows[i].Cells[6].Value = Fc.GetshotDate(dateTimePicker1.Value.ToString(), 111);
                //}
                //else
                //{
                //    dataGridView1.Rows[i].Cells[6].Value = "";
                //}

                //bool ChkCell = (bool)dataGridView1.Rows[i].Cells[8].Value;
                //if (ChkCell == true)
                //{
                //    dataGridView1.Rows[i].Cells[6].Value = Fc.GetshotDate(dateTimePicker1.Value.ToString(), 111);
                //}
                //else { dataGridView1.Rows[i].Cells[6].Value = ""; }



                //             if ((bool)dta.Cells[0].Value == true))
                //{
                //    
                //}
                //            else
                //            {
                //                dataGridView1.CurrentRow.Cells[2].Value = "";            
                //            }




            }
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
                    //dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.GreenYellow;      
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "")
                    {
                      //  dataGridView1.CurrentRow.Cells[5].Value = Fc.GetshotDate(dateTimePicker1.Value.ToString(), 111);
                    }


                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "sNO")
                {
                    xFieldSet = "sNO";
                    xFieldCls = "sYES";
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldSet].Value = true;
                    dataGridView1.Rows[e.RowIndex].Cells[xFieldCls].Value = false;
                    //dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.OrangeRed;
                   // dataGridView1.CurrentRow.Cells[5].Value = "";
                }

            }
            catch (Exception)
            {


            }
        }



    }
}

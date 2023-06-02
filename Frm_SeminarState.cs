using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SVMember
{
    public partial class Frm_SeminarState : Form
    {
        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter MSsql_Da = new OleDbDataAdapter();
        OleDbCommand MSsql_Save = new OleDbCommand();
        string sql;

        public Frm_SeminarState()
        {
            InitializeComponent();
        }

        private void Frm_SeminarState_Load(object sender, EventArgs e)
        {
            ConnectDB();
            Get_MBinfo();
        }
        private void ConnectDB()
        {

            string ConnectStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\SEMINAR.mdb;Persist Security Info=False;";

            conn.ConnectionString = ConnectStr;
            conn.Open();

        }
        public DataTable SelectQuery(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                MSsql_Da = new OleDbDataAdapter(str, conn);
                MSsql_Da.Fill(dt);


            }
            catch (Exception)
            {


            }
            return dt;
        }
        private void Get_MBinfo()
        {
            DataTable dt = new DataTable();

   //         //  จำนวนรายทั้งหมด ---------------
   //         sql = "select count(member_no ) FROM MemberInfoMST  " +
   //                   " WHERE Amper='" + amper + "' AND GroupName2='" + gp + "'";
   //         dt = SelectQuery(sql);
   ////         label3.Text = "จำนวนทั้งหมด  " + dt.Rows[0][0].ToString() + " ราย ";


            // มาแล้ว --------------------------------------
            sql = "SELECT MemberInfoMST.Seq_no,Member_Regist.Member_no,MemberInfoMST.Membername, Member_Regist.Regis_Date,MemberInfoMST.GroupName,MemberInfoMST.GroupName2 " +
                       " FROM Member_Regist INNER JOIN MemberInfoMST ON Member_Regist.ID_card = MemberInfoMST.ID_card" +
                        " order by Member_Regist.Regis_Date desc";


            dt = SelectQuery(sql);
            dataGridView1.DataSource = dt;
            label4.Text = "ระบบรับข้อมูลแล้ว จำนวน   " + dt.Rows.Count.ToString() + "  ราย";

            // --  ยังไม่มา    ------------------------------------
             sql="SELECT Seq_no, member_no,Membername, GroupName, GroupName2"+
                     " FROM MemberInfoMST "+
                     " where not (member_no in(select member_no from  Member_Regist))" +
                     "order by Seq_no";




            //sql = "SELECT seq_no,Member_no,Membername,groupname,groupname2 FROM MemberInfoMST  " +
            //          " WHERE Amper='" + amper + "' AND GroupName2='" + gp + "'" +
            //         " and  not(Member_no  in (select member_no from  MemberInfochg  WHERE Amper='" + amper + "' AND GroupName2='" + gp + "' )) " +
            //         " order by seq_no";


            dt = SelectQuery(sql);
            dataGridView2.DataSource = dt;
            label5.Text = "ระบบยังไม่ได้รับ จำนวน   " + dt.Rows.Count.ToString() + "  ราย";








            // txtMemberID.Text = dt.Rows[0]["member_no"].ToString();
            //   txt_MBgroup.Text = dt.Rows[0]["membgroup_desc"].ToString();

            //    lb_nameInBase.Text = dt.Rows[0]["memb_name"].ToString();
            //     lb_surnameInBase.Text = dt.Rows[0]["memb_surname"].ToString();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_MBinfo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Get_MBinfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  ExportExcel(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // ExportExcel(dataGridView2);
        }
        //void ExportExcel(DataGridView GV)
        //{

        //    // creating Excel Application  
        //    try
        //    {

            

        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    // creating new WorkBook within Excel application  
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    // creating new Excelsheet in workbook  
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    // see the excel sheet behind the program  
        //    app.Visible = true;
        //    // get the reference of first sheet. By default its name is Sheet1.  
        //    // store its reference to worksheet  
        //    worksheet = workbook.Sheets["Sheet1"];
        //    worksheet = workbook.ActiveSheet;
        //    // changing the name of active sheet  
        //    worksheet.Name = "Exported from gridview";
        //    // storing header part in Excel  
        //    for (int i = 1; i < GV.Columns.Count + 1; i++)
        //    {
        //        worksheet.Cells[1, i] = GV.Columns[i - 1].HeaderText;
        //    }
        //    // storing Each row and column value to excel sheet  
        //    for (int i = 0; i < GV.Rows.Count - 1; i++)
        //    {
        //        for (int j = 0; j < GV.Columns.Count; j++)
        //        {
        //            worksheet.Cells[i + 2, j + 1] = GV.Rows[i].Cells[j].Value.ToString();
        //        }
        //    }
        //    string fr = DateTime.Now.ToString();
        //    // save the application  
        //    workbook.SaveAs("c:\\output" + fr + ".xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    // Exit from the application  
        //    app.Quit();
        //      app.GetOpenFilename("c:\\output"+ fr +".xls");

        //    }
        //    catch (Exception)
        //    {

        //        return;
        //    }
        //}

        private void button4_Click(object sender, EventArgs e)
        {
     
        }


    }
}

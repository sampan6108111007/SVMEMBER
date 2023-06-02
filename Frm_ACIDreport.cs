using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;


namespace SVMember
{
    public partial class Frm_ACIDreport : Form
    {
        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter MSsql_Da = new OleDbDataAdapter();
        OleDbCommand MSsql_Save = new OleDbCommand();
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        DirectoryInfo Di, Di_ref;
        string sql;


        public Frm_ACIDreport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_MBinfo(comboBox1.Text, comboBox2.Text);
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
        private void Get_MBinfo(string amper,string gp)
        {
            DataTable dt = new DataTable();

            //  จำนวนรายทั้งหมด ---------------
            sql = "select count(member_no ) FROM MemberInfoMST  " +
                      " WHERE Amper='" + amper + "' AND GroupName2='" + gp + "'";            
            dt = SelectQuery(sql);
            label3.Text = "จำนวนทั้งหมด  "  +  dt.Rows[0][0].ToString() + " ราย ";


            // มาแล้ว --------------------------------------
            sql = "SELECT MemberInfoMST.seq_no,MemberInfoCHG.edit_date,MemberInfoCHG.Member_no, MemberInfoMST.Membername " +
                    " FROM MemberInfoMST INNER JOIN MemberInfoCHG ON MemberInfoMST.Member_no = MemberInfoCHG.Member_no "+
                    " WHERE (((MemberInfoMST.Amper)='"+ amper +"') AND ((MemberInfoMST.GroupName2)='"+ gp +"')) "+
                    " order by MemberInfoCHG.edit_date desc";

            dt = SelectQuery(sql);
            dataGridView1.DataSource = dt;
            label4.Text = "ระบบรับข้อมูลแล้ว จำนวน   " + dt.Rows.Count.ToString() + "  ราย";

            // --  ยังไม่มา    ------------------------------------

            sql = "SELECT seq_no,Member_no,Membername,groupname,groupname2 FROM MemberInfoMST  "+
                      " WHERE Amper='"+ amper +"' AND GroupName2='"+ gp +"'" +
                     " and  not(Member_no  in (select member_no from  MemberInfochg  WHERE Amper='" + amper + "' AND GroupName2='" + gp + "' )) " +
                     " order by seq_no";


            dt = SelectQuery(sql);
            dataGridView2.DataSource = dt;
            label5.Text = "ระบบยังไม่ได้รับ จำนวน   " + dt.Rows.Count.ToString() + "  ราย";



            




            // txtMemberID.Text = dt.Rows[0]["member_no"].ToString();
            //   txt_MBgroup.Text = dt.Rows[0]["membgroup_desc"].ToString();

            //    lb_nameInBase.Text = dt.Rows[0]["memb_name"].ToString();
            //     lb_surnameInBase.Text = dt.Rows[0]["memb_surname"].ToString();

        }

        private void Frm_ACIDreport_Load(object sender, EventArgs e)
        {
            ConnectDB();
        }
        private void ConnectDB()
        {

            //string ConnectStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\POPA2563.mdb;Persist Security Info=False;";

            string ConnectStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ClsMST.Read_INFConfig("ipdatabase") + "\\POPA2563.mdb;Persist Security Info=False;";


            conn.ConnectionString = ConnectStr;
            conn.Open();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked ==false)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Get_MBinfo(comboBox1.Text, comboBox2.Text);
        }

    }
}

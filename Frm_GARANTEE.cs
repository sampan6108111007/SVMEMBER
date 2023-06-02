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
    public partial class Frm_GARANTEE : Form
    {
        public Frm_GARANTEE()
        {
            InitializeComponent();
        }

        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;

        Bitmap memoryImage;
        private void Frm_GARANTEE_Load(object sender, EventArgs e)
        {
            MBno = this.Text.Split(sp);

            txt_Print.Text = "ข้อมูล ณ วัน/เวลา : " + Fc.GetshotDate(DateTime.Now.ToString(), 111);


            ClsMST.GetDB_Oracle(); // Oracle
            ShowDATA();
        }
        void ShowDATA()
        {
            sql = "select lnmst.loancontract_no,lnc.description,lnmst.principal_balance from lncontmaster lnmst "+
                        " inner join lncontcoll lnc "+
                        " on lnmst.loancontract_no=lnc.loancontract_no " +
                        " where lnmst.principal_balance>0 and lnmst.member_no='" + MBno[0] + "'"+
                        " order by lnmst.loanapprove_date,lnmst.loancontract_no";
            
            dataGridView1.DataSource = ClsMST.SelectQuery_ORA(sql);

            sql="select lnmst.loancontract_no,'คุณ'||mb.memb_name || '     ' || mb.memb_surname as description,lnmst.principal_balance from lncontmaster lnmst "+
                    " inner join mbmembmaster mb "+
                    " on lnmst.member_no=mb.member_no "+
                    " inner join lncontcoll lnc "+
                    " on lnmst.loancontract_no=lnc.loancontract_no "+
                    " where lnmst.principal_balance>0 and lnc.ref_collno='" + MBno[0] + "'"+
                    " order by lnmst.loanapprove_date,lnmst.loancontract_no";
            dataGridView2.DataSource = ClsMST.SelectQuery_ORA(sql);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            CaptureScreen();
            printDocument1.Print();
            button1.Visible = true;
            button2.Visible = true;
        }
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender,
               System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

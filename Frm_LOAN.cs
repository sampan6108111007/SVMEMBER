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
    public partial class Frm_LOAN : Form
    {
        public Frm_LOAN()
        {
            InitializeComponent();
        }

        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;

        Bitmap memoryImage;

        private void Frm_LOAN_Load(object sender, EventArgs e)
        {
            MBno = this.Text.Split(sp);

            txt_Print.Text = "ข้อมูล ณ วัน/เวลา : " + Fc.GetshotDate(DateTime.Now.ToString(), 111);
            txt_Mbname.Text = MBno[0] + "    " + MBno[1];

            ClsMST.GetDB_Oracle(); // Oracle
            ShowDATA();

            //dataGridView1.Columns[0].Width = 20;
        }

        private void ShowDATA()
        {
            sql = " select loancontract_no,loanapprove_amt,loanapprove_date,period_payment " +
                      " ,last_stm_no || '/' || period_payamt as period " +
                      " ,lastpayment_date, " +
                      " case when payment_status = 1  then 'ปกติ' " +
                      " when payment_status = -11  then 'งดต้น' " +
                      " when payment_status = -13  then 'งดเก็บ' " +
                      " else '-ไม่ระบุ-' " +
                      " end as payment_status " +
                      " ,principal_balance from lncontmaster " +
                      " where principal_balance>0 and " +
                      " member_no='" + MBno[0] + "'" +
                      " order by loanapprove_date ";
            dataGridView1.DataSource = ClsMST.SelectQuery_ORA(sql);

            double gt = 0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                gt += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString());
            }

            lb_total.Text = gt.ToString("###,###.00");
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

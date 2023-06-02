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
    public partial class Frm_Account : Form
    {
        public Frm_Account()
        {
            InitializeComponent();
        }

        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;

        Bitmap memoryImage;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Account_Load(object sender, EventArgs e)
        {
            
            //Frm_Main frm = new Frm_Main();
            //string MB =  frm.txtMemberID.Text + "|" + frm.txt_MBgroup.Text;


            MBno = this.Text.Split(sp);
           // MBno = MB.Split(sp);

            txt_Print.Text = "ข้อมูล ณ วัน/เวลา : " + Fc.GetshotDate(DateTime.Now.ToString(), 111);
            txt_Mbname.Text = MBno[0] + "    " + MBno[1];

            ClsMST.GetDB_Oracle(); // Oracle
            ShowDATA();
        }

        private void ShowDATA()
        {
            
            sql="select deptaccount_no,prncbal from dpdeptmaster "+
                        " where deptclose_status=0  "+
                        " and member_no='"+ MBno[0] +"'"+
                        " order by depttype_code,deptopen_date";
            dataGridView1.DataSource = ClsMST.SelectQuery_ORA(sql);


            Get_AccountDET(dataGridView1.CurrentRow.Cells[0].Value.ToString());


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Get_AccountDET(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }

        private void Get_AccountDET(string Acc_no)
        {
            sql = "select * from dpdeptmaster where deptaccount_no='"+ Acc_no +"'";
            DataTable dt = ClsMST.SelectQuery_ORA(sql);
            if (dt.Rows.Count<=0){return;}
            ClearDATA();
            Double prncbal = 0,withdrawable_amt = 0,deptmonth_amt=0;

            prncbal = Convert.ToDouble(dt.Rows[0]["prncbal"].ToString());
            withdrawable_amt =Convert.ToDouble( dt.Rows[0]["withdrawable_amt"].ToString());
            deptmonth_amt =Convert.ToDouble(dt.Rows[0]["deptmonth_amt"].ToString());

            lb_Acc.Text=dt.Rows[0]["deptaccount_no"].ToString();
            lb_Acc2.Text=dt.Rows[0]["deptaccount_no"].ToString();
            lb_AccName.Text=dt.Rows[0]["deptaccount_name"].ToString();
            lb_AccName2.Text = dt.Rows[0]["deptaccount_name"].ToString();

            lb_Cond.Text=dt.Rows[0]["condforwithdraw"].ToString();
            lb_LstCont.Text= Fc.GetshotDate( dt.Rows[0]["lastaccess_date"].ToString(),155);
            lb_MMamt.Text = deptmonth_amt.ToString("###,###.00");
            lb_OpenDate.Text=Fc.GetshotDate( dt.Rows[0]["deptopen_date"].ToString(),155);
            lb_Prnbal.Text = prncbal.ToString("###,###.00");
            lb_ton.Text = withdrawable_amt.ToString("###,###.00");


            // Statement -------------------------

            sql="select * from ( "+
                        //" select dst.operate_date,dst.deptitemtype_code,dtype.sign_Flag,dst.deptitem_amt,dst.prncbal, "+
                        " select dst.operate_date,dst.deptitemtype_code,dst.prncbal, " +
                        " case when dtype.sign_Flag=1 then dst.deptitem_amt else NULL end Item_1, "+
                        " case when dtype.sign_Flag=-1 then dst.deptitem_amt else NULL end Item_2 "+
                        " from dpdeptstatement dst "+
                        " inner join dpucfdeptitemtype dtype "+
                        " on dst.deptitemtype_code= dtype.deptitemtype_code "+
                        " where dst.deptaccount_no='" + Acc_no  + "' " +
                        " order by dst.operate_date desc "+
                        " ) "+
                        " where rownum<=12 ";

            dataGridView2.DataSource = ClsMST.SelectQuery_ORA(sql);

            double _Gt = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                  _Gt+=  Convert.ToDouble( dataGridView2.Rows[i].Cells[2].Value);                
            }

            lb_gt.Text = _Gt.ToString("###,###.00");
            lb_Count.Text = dataGridView1.Rows.Count.ToString();
            //-------------------------------------



        }

        private void ClearDATA()
        {
            lb_Acc.Text = "";
            lb_Acc2.Text = "";
            lb_AccName.Text = "";
            lb_AccName2.Text = "";
            lb_Cond.Text = "";
            lb_LstCont.Text = "";
            lb_MMamt.Text = "";
            lb_OpenDate.Text = "";
            lb_Prnbal.Text = "";
            lb_ton.Text = "";
            lb_Count.Text = "";
            lb_gt.Text = "";
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
    }
}

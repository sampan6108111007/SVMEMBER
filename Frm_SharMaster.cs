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


namespace SVMember
{
    public partial class Frm_SharMaster : Form
    {
        public Frm_SharMaster()
        {
            InitializeComponent();
        }

        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;

        Bitmap memoryImage;
        

        private void Frm_SharMaster_Load(object sender, EventArgs e)
        {
          

            MBno = this.Text.Split(sp);

            txt_Print.Text = "ข้อมูล ณ วัน/เวลา : " +  Fc.GetshotDate(DateTime.Now.ToString(), 111);

            
            ClsMST.GetDB_Oracle(); // Oracle
            ShowDATA();
        }
        void ShowDATA()
        {
            sql = "select sty.sharetype_desc,sh.sharebegin_amt* sty.share_value as ym,sh.last_period,sh.periodshare_amt * sty.share_value  as ypm,sh.sharestk_amt*sty.unitshare_value as ynow  " +
                        ",case sh.payment_status  when 1 then 'ปกติ'else 'งดเก็บ' end  as status" +
                        " from shsharemaster sh " +
                        "inner join shsharetype sty " +
                        " on  sh.sharetype_code = sty.sharetype_code " +
                        "where sh.member_no='" + MBno[0] + "'";

            DataTable dt =ClsMST.SelectQuery_ORA(sql);

            decimal ym=0;ym=Convert.ToDecimal(dt.Rows[0]["ym"].ToString());
            decimal ynow=0;ynow=Convert.ToDecimal(dt.Rows[0]["ynow"].ToString());
            decimal ypm=0;ypm=Convert.ToDecimal(dt.Rows[0]["ypm"].ToString());



            txt_Mbname.Text = MBno[0] +"     " + MBno[1] ;
            txt_mbGroup.Text = MBno[2];
            txt_HunType.Text = dt.Rows[0]["sharetype_desc"].ToString();
            txt_HunYM.Text = string.Format("{0:#,0.00}", ym);
            txt_HunNW.Text = string.Format("{0:#,0.00}", ynow);
            txt_HunPM.Text = string.Format("{0:#,0.00}", ypm);
            txt_HunST.Text = dt.Rows[0]["status"].ToString();
            
            // statement
            // เอาอันนี้มาใช้เลย
            //SELECT * FROM (SELECT * FROM TableName ORDER BY ColumnName) WHERE ROWNUM <= 3;

            sql = "select * from ( " +
                                                    " select operate_date,slip_date,ref_docno,shritemtype_code,period,share_amount ,sharestk_amt *10 as sharestk_amt  " +
                                                    " from shsharestatement "+
                                                    " where member_no='"+ MBno[0] +"' "+
                                                    " order by seq_no desc "+
                                                " ) "+
                                                " where  rownum<=13";

            dataGridView1.DataSource = ClsMST.SelectQuery_ORA(sql);

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

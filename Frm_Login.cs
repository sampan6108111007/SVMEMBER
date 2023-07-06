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
    public partial class Frm_Login : Form
    {

          string str;
        // Class -------------------------------------------------------------------------------------------------
          ClassMST ClsMST = new ClassMST();
        EncrytionBase64 Encry = new EncrytionBase64();

        


        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            ClsMST.GetDb();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btn_Login.Focus();
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string User = textBox1.Text;
             string Pwd = Encry.EncryptAscii(textBox2.Text);
             str = "select * from amsecusers where user_name='" + User + "' and password='" + Pwd + "'";
             DataTable dt = ClsMST.SelectQuery(str);

            if (dt.Rows.Count <= 0)
            {
               lb_error.Text = "* กรุณาตรวจสอบ [USER] และ [PASSWORD]";
                textBox1.Focus();
                return;
            }

            Frm_Approve2 Frm = new Frm_Approve2(); 
            Frm.Show();
            this.Hide();
            //this.Text = textBox1.Text + "|" + dt.Rows[0]["Fullname"].ToString();


            //this.Close();
        }

       

       
    }
}

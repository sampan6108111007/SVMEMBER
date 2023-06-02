using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace SVMember
{
    public partial class Frm_ACrenamfile : Form
    {
        public Frm_ACrenamfile()
        {
            InitializeComponent();
        }
        ClassMST Cls = new ClassMST();
        private void button1_Click(object sender, EventArgs e)
        {
            Int32 k = new Int32();
           foreach(var files in Directory.GetFiles(@"G:\8-9\"))
{
    FileInfo info = new FileInfo(files);
    var fileName = Path.GetFileName(info.FullName);
    k++;
    //textBox1.Text += textBox1.Text + "|" + info.FullName.ToString();

    dataGridView1.Rows.Add(k.ToString(),info.FullName.ToString());

}

           MessageBox.Show(dataGridView1.Rows.Count.ToString());

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Source file to be renamed  
            string Fn = "",Fnn="";
            progressBar1.Maximum = dataGridView1.Rows.Count;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                string sourceFile = dataGridView1.Rows[i].Cells[1].Value.ToString();

                // Create a FileInfo  
                System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                // Check if file is there  
                if (fi.Exists)
                {

                    string a = fi.Name.ToString();
                    Fn = a.Substring( 8, 6);
                    Fnn = "profile_" + Cls.Get_Member_Format(Fn);



                    // Move file with a new name. Hence renamed.  
                    
                    fi.MoveTo(@"C:\Temp\" + Fnn + ".jpg");
                    progressBar1.Value = i;
                 //   Console.WriteLine("File Renamed.");
                }


            }

        }
    }
}

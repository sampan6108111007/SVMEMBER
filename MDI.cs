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
    public partial class MDI : Form
    {
        // Class -------------------------------------------------------------------------------------------------
        ClassMST ClsMST = new ClassMST();

        // ตัวแปล--------------------------------------------------------------------------------------------------
        public String RPT_user, RPT_Pwd, By_UserID, SQL, XXXXXXXXXXXXXXXXX;
        public string _UserLOGIN { get; set; }

        // Control      ------------------------------------------------------------------------------ ------------
        ToolStripMenuItem MnuStripItem = new ToolStripMenuItem();
        //----------------------------------------------------------------------------------------------------------

        private int childFormNumber = 0;

        public MDI()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void เสยบบตรToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_UpdateData Frm = new Frm_UpdateData();
            Frm.MdiParent = this;
            Frm.Show();
        }

        private void MDI_Load(object sender, EventArgs e)
        {
            Frm_Login Frm = new Frm_Login();
           
            //Frm.ShowDialog();
            Frm.MdiParent = this;
            Frm.Show();

            toolStripLabelUserID.Text = Frm.Text;

            ClsMST.GetDb();

            try
            {
                //toolStripStatusLabel1.Text = Frm.lbFullname.Text;
                //toolStripStatusLabel2.Text = " | " + 
                //toolStripStatusLabel3.Text = " | " + Frm.lbUserType.Text;
                //toolStripStatusLabel4.Text = " | " + Frm.lbApprove.Text;

                this.Enabled = true;

                //toolStripLabel3.Text = Frm.lbUserName.Text;
                //toolStripLabel4.Text = Frm.lbFullname.Text;
                //toolStripLabel5.Text = Frm.lbUserType.Text;
                //toolStripLabel6.Text = Frm.lbApprove.Text;
                //toolStripLabelUserID.Text = Frm.lbUserCode.Text;

                //By_UserID = toolStripLabelUserID.Text;

                By_UserID = Environment.MachineName.ToString();
                //By_UserID = "TANG";

                _UserLOGIN = By_UserID;
                //  ---         สร้างเมนูหลัก โดย เลขที่พนักงาน  ---------
               // Create_MenuItem(By_UserID);
                // -------------------------------------------------------

                //   Cndb.User_LOGIN = By_UserID;



                // 
            }
            catch (Exception ex)
            {
                Application.Exit();
            }
        }
    }
}

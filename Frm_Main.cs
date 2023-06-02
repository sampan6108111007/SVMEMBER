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
    public partial class Frm_Main : Form
    {
        public string mB_Info;
        string sql;
        ClassMST ClsMST = new ClassMST();

        RDNIDWRAPPER.RDNID mRDNIDWRAPPER = new RDNIDWRAPPER.RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


        Bitmap memoryImage;
        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        public Frm_Main()
        {
            InitializeComponent();
            string fileName = StartupPath + "\\RDNIDLib.DLX";
            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLX not found");
            }

            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
           // this.Text = String.Format("R&D NID Card VC# {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);


            byte[] _lic = String2Byte(fileName);

            int nres = 0;
            nres = RDNID.openNIDLibRD(_lic);
            if (nres != 0)
            {
                String m;
                m = String.Format(" error no {0} ", nres);
                MessageBox.Show(m);
            }

            byte[] Licinfo = new byte[1024];

            RDNID.getLicenseInfoRD(Licinfo);

            //m_lblDLXInfo.Text = aByteToString(Licinfo);

            byte[] Softinfo = new byte[1024];
            RDNID.getSoftwareInfoRD(Softinfo);
         //   m_lblSoftwareInfo.Text = aByteToString(Softinfo);

            ListCardReader();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            ClsMST.GetDB_Oracle(); // Oracle
           ReadCard();
        }

        protected int ReadCard()
        {
            String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);

            IntPtr obj = selectReader(strTerminal);


            Int32 nInsertCard = 0;
            nInsertCard = RDNID.connectCardRD(obj);
            if (nInsertCard != 0)
            {
                String m;
                m = String.Format(" error no {0} ", nInsertCard);
                MessageBox.Show(m);

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
                return nInsertCard;
            }

            byte[] id = new byte[30];
            int res = RDNID.getNIDNumberRD(obj, id);
            if (res != DefineConstants.NID_SUCCESS)
                return res;
            String NIDNum = aByteToString(id);



            byte[] data = new byte[1024];
            res = RDNID.getNIDTextRD(obj, data, data.Length);
            if (res != DefineConstants.NID_SUCCESS)
                return res;

            String NIDData = aByteToString(data);
            if (NIDData == "")
            {
                MessageBox.Show("Read Text error");
            }
            else
            {
                string[] fields = NIDData.Split('#');

                m_txtID.Text = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                m_txtFullNameT.Text = fullname;

                // เอส   ----------------------------------------------------------------------------------------------------------------

                lb_nameInBut.Text = fields[(int)NID_FIELD.NAME_T];
                lb_surnameInBut.Text = fields[(int)NID_FIELD.SURNAME_T];

                //----------------------------------------------------------------------------------------------------------------



                fullname = fields[(int)NID_FIELD.TITLE_E] + " " +
                                    fields[(int)NID_FIELD.NAME_E] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_E] + " " +
                                    fields[(int)NID_FIELD.SURNAME_E];
                m_txtFullNameE.Text = fullname;

          //     m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);

                m_txtAddress.Text = fields[(int)NID_FIELD.HOME_NO] + "   " +
                                        fields[(int)NID_FIELD.MOO] + "   " +
                                        fields[(int)NID_FIELD.TROK] + "   " +
                                        fields[(int)NID_FIELD.SOI] + "   " +
                                        fields[(int)NID_FIELD.ROAD] + "   " +
                                        fields[(int)NID_FIELD.TUMBON] + "   " +
                                        fields[(int)NID_FIELD.AMPHOE] + "   " +
                                        fields[(int)NID_FIELD.PROVINCE] + "   "
                                        ;
                //if (fields[(int)NID_FIELD.GENDER] == "1")
                //{
                //    m_txtGender.Text = "ชาย";
                //}
                //else
                //{
                //    m_txtGender.Text = "หญิง";
                //}
                //m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
                //m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
                //if ("99999999" == m_txtExpiryDate.Text)
                //    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
                //m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];
            }

            byte[] NIDPicture = new byte[1024 * 5];
            int imgsize = NIDPicture.Length;
            res = RDNID.getNIDPhotoRD(obj, NIDPicture, out imgsize);
            if (res != DefineConstants.NID_SUCCESS)
                return res;

            byte[] byteImage = NIDPicture;
            if (byteImage == null)
            {
                MessageBox.Show("Read Photo error");
            }
            else
            {
                //m_picPhoto
                Image img = Image.FromStream(new MemoryStream(byteImage));
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                m_picPhoto.Image = (Image)MyImage;
                m_picPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            RDNID.disconnectCardRD(obj);
            RDNID.deselectReaderRD(obj);

            //   ตรวจเลขที่ ใน DATABASE

            Get_MBinfo(NIDNum);
             

            return 0;



        }

        private void Get_MBinfo(string mbcd)
        {
            sql = "select mb.member_no,mb.memb_name,mb.memb_surname,mb.membgroup_code || '|' ||  mg.membgroup_desc as membgroup_desc from mbmembmaster mb " +
                    " inner join mbucfmembgroup mg " +
                    " on mb.membgroup_code=mg.membgroup_code " +
                    " where mb.Card_person='" + mbcd + "'";

            DataTable dt = ClsMST.SelectQuery_ORA(sql);

            if (dt.Rows.Count<=0)
            {
                return;
            }

            txtMemberID.Text = dt.Rows[0]["member_no"].ToString();
            txt_MBgroup.Text = dt.Rows[0]["membgroup_desc"].ToString();

            lb_nameInBase.Text = dt.Rows[0]["memb_name"].ToString();
            lb_surnameInBase.Text = dt.Rows[0]["memb_surname"].ToString();
            
        }
        public IntPtr selectReader(String reader)
        {
            IntPtr mCard = (IntPtr)0;
            byte[] _reader = String2Byte(reader);
            IntPtr res = (IntPtr)RDNID.selectReaderRD(_reader);
            if ((Int64)res > 0)
                mCard = (IntPtr)res;
            return mCard;
        }

        private void ListCardReader()
        {
            byte[] szReaders = new byte[1024 * 2];
            int size = szReaders.Length;
            int numreader = RDNID.getReaderListRD(szReaders, size);
            if (numreader <= 0)
                return;
            String s = aByteToString(szReaders);
            String[] readlist = s.Split(';');
            if (readlist != null)
            {
                for (int i = 0; i < readlist.Length; i++)
                    m_ListReaderCard.Items.Add(readlist[i]);
                m_ListReaderCard.SelectedIndex = 0;
            }
        }

        static string aByteToString(byte[] b)
        {
            Encoding ut = Encoding.GetEncoding(874); // 874 for Thai langauge
            int i;
            for (i = 0; b[i] != 0; i++) ;

            string s = ut.GetString(b);
            s = s.Substring(0, i);
            return s;
        }

        static byte[] String2Byte(string s)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(874);
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(s);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            return asciiBytes;
        }
        enum NID_FIELD
        {
            NID_Number,   //1234567890123#

            TITLE_T,    //Thai title#
            NAME_T,     //Thai name#
            MIDNAME_T,  //Thai mid name#
            SURNAME_T,  //Thai surname#

            TITLE_E,    //Eng title#
            NAME_E,     //Eng name#
            MIDNAME_E,  //Eng mid name#
            SURNAME_E,  //Eng surname#

            HOME_NO,    //12/34#
            MOO,        //10#
            TROK,       //ตรอกxxx#
            SOI,        //ซอยxxx#
            ROAD,       //ถนนxxx#
            TUMBON,     //ตำบลxxx#
            AMPHOE,     //อำเภอxxx#
            PROVINCE,   //จังหวัดxxx#

            GENDER,     //1#			//1=male,2=female

            BIRTH_DATE, //25200131#	    //YYYYMMDD 
            ISSUE_PLACE,//xxxxxxx#      //
            ISSUE_DATE, //25580131#     //YYYYMMDD 
            EXPIRY_DATE,//25680130      //YYYYMMDD 
            ISSUE_NUM,  //12345678901234 //14-Char
            END
        };

        private void button6_Click(object sender, EventArgs e)
        {
              if (txtMemberID.Text == "")
                {
                    MessageBox.Show("กรุณาระบุ เลขทะเบียนสมาชิก", "อะจุ๋ย", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMemberID.Focus();
                    return;
                }

              ShowBill("");
        }

        private void ShowBill(string ac)
        {
            //  ac  คือ การสั่งพิมพ์ แล้วเก็บประวัตไว้ ใน DB
            if (txtMemberID.Text == "")
            {

                return;
            }


            string xVal = "";
          

      //      string Prt = ""; if (radioButton4.Checked == true) { Prt = "COPY"; }

        //    string ID = Get_MMYY();



      //      string URL = "www.lptcoop.com:82/member/FrmSqlExcute.aspx?Keeping=" + xVal + "&id=" + HttpUtility.UrlEncode(ID) + "&ac=" + ac + "&Prt=" + Prt + "&com=" + Environment.MachineName.ToString();
        //    OpenURLInBrowser(URL, 2);


        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = SystemColors.ButtonHighlight; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CaptureScreen();
            //printDocument1.Print();

            
            Frm_SharMaster frm = new Frm_SharMaster();
            frm.Text = txtMemberID.Text + "|" + m_txtFullNameT.Text + "|" + txt_MBgroup.Text;
            frm.ShowDialog();

            
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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            Frm_GARANTEE frm = new Frm_GARANTEE();
            frm.Text = txtMemberID.Text + "|" + m_txtFullNameT.Text + "|" + txt_MBgroup.Text;
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frm_LOAN frm = new Frm_LOAN();
            frm.Text = txtMemberID.Text + "|" + m_txtFullNameT.Text + "|" + txt_MBgroup.Text;
           frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Frm_Account frm = new Frm_Account();
            frm.Text = txtMemberID.Text + "|" + m_txtFullNameT.Text + "|" + txt_MBgroup.Text;
           frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void m_ListReaderCard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

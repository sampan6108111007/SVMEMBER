using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


using RDNIDWRAPPER;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

//using CrystalDecisions.CrystalReports;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.ReportSource;


namespace SVMember
{
    public partial class Frm_Seminar : Form
    {

        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter MSsql_Da = new OleDbDataAdapter();
        OleDbCommand MSsql_Save = new OleDbCommand();
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        DirectoryInfo Di, Di_ref;


        public string mB_Info;
        
        string Temp_Path = @Application.StartupPath + "\\Picture\\";
        string sql, RPTCur_Path=Application.StartupPath+"\\RPT\\";

        RDNIDWRAPPER.RDNID mRDNIDWRAPPER = new RDNIDWRAPPER.RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


        Bitmap memoryImage;
        IntPtr obj;
        byte[] _lic;
        bool CardStateChange = false;

      //  ReportDocument MyReport = new ReportDocument();




        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        public Frm_Seminar()
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
        private void timer_AutoRead_Tick(object sender, EventArgs e)
        {
            
            Int32 nInsertCard = 0;
            nInsertCard = RDNID.connectCardRD(obj);

            //textBox1.Text  +=  nInsertCard.ToString() + " on  " ;

            //nInsertCard = RDNID.disconnectCardRD(obj);

            //textBox1.Text += nInsertCard.ToString() + " off | ";

            if (nInsertCard != 0)
            {
                ReadCard();
            }
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
                //m = String.Format(" ไม่พบเครื่องเสียบบัตร ประชาชน  ", nInsertCard);
                //MessageBox.Show(m);

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
                CardStateChange = false;
                return nInsertCard;


            }

            CardStateChange = true;


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

                lbID_Card.Text = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                if (lbID_Card.Text == lb_IDCard_Exits.Text)
                {
                    return nInsertCard;
                }

                panel1.Visible = true; // กำลังรับข้อมูล  animation


                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                //m_txtFullNameT.Text = fullname;

                // เอส   ----------------------------------------------------------------------------------------------------------------


                lb_Tname2.Text = fields[(int)NID_FIELD.TITLE_T] +fields[(int)NID_FIELD.NAME_T] + "   " + fields[(int)NID_FIELD.SURNAME_T];
                lb_TFN.Text = fields[(int)NID_FIELD.TITLE_T];
                lb_Tname.Text = fields[(int)NID_FIELD.NAME_T];
                lb_Tsurname.Text = fields[(int)NID_FIELD.SURNAME_T];
                



                //lb_EFN.Text = fields[(int)NID_FIELD.TITLE_E];
                //lb_Ename.Text = fields[(int)NID_FIELD.NAME_E];
                //lb_Esurname.Text = fields[(int)NID_FIELD.SURNAME_E];

                //lb_BD.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);

                //if (fields[(int)NID_FIELD.GENDER] == "1")
                //{ cbo_Sex.SelectedIndex = 0; }// "ชาย";
                //else { cbo_Sex.SelectedIndex = 1; }// "หญิง";






                lbID_Card.Text = fields[(int)NID_FIELD.NID_Number];


                //lb_TFN.Text = fields[(int)NID_FIELD.TITLE_T];

                //lb_Add1.Text = fields[(int)NID_FIELD.HOME_NO];
                //lb_AddMoo.Text = fields[(int)NID_FIELD.MOO].ToString().Replace("หมู่ที่ ", "");
                //lb_AddSoi.Text = fields[(int)NID_FIELD.TROK].Replace("ตรอก", "") + "   " + fields[(int)NID_FIELD.SOI].Replace("ซอย", "");
                //lb_AddRoad.Text = fields[(int)NID_FIELD.ROAD].Replace("ถนน", "");
                lb_ISSUE_NUM.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                //cbo_TUMBON.Text = fields[(int)NID_FIELD.TUMBON].Replace("ตำบล", "");
                //cbo_AMPHOE.Text = fields[(int)NID_FIELD.AMPHOE].Replace("อำเภอ", "");
                //cbo_PROVINCE.Text = fields[(int)NID_FIELD.PROVINCE].Replace("จังหวัด", "");


                //----------------------------------------------------------------------------------------------------------------

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

            RDNID.disconnectCardRD(obj);  //ปิด connect เครื่องอ่าน
            //   RDNID.deselectReaderRD(obj);

            panel1.Visible = false; // กำลังรับข้อมูล Animation


            //   ตรวจเลขที่ ใน DATABASE

            Get_MBinfo(NIDNum);

            lb_IDCard_Exits.Text = lbID_Card.Text;    // 

            

            if (Chk_AutoSave.Checked == true)
            {
                SaveData();
                PrintSlip(lbID_Card.Text.Replace("-",""));
            }

          
            return 0;



        }



        private void PrintSlip(string ID_card)
        {
           // ReportDocument rpt = new ReportDocument();




            string vWhere = "{Member_Regist.ID_card} = '" + ID_card + "'";
            if (txt_MbNo.Text != "")
            {
                string sql = "select * from Member_Regist  where id_card= '" + ID_card + "'";
                DataTable dt = SelectQuery(sql);
                //rpt.Load(@RPTCur_Path + "\\SlipQ.rpt");

                //rpt.SetDataSource(dt);
                //rpt.PrintToPrinter(1, true, 0, 0);
                

           //     MyReport.Load(@RPTCur_Path + "\\SlipQ.rpt");
           //     //MyReport.SetDataSource = Application.StartupPath.ToString()  + "\\SEMINAR.mdb";

           //     MyReport.SetDatabaseLogon("", "", "Microsoft.ACE.OLEDB.12.0", Application.StartupPath + "\\SEMINAR.mdb", true);   

           //     //rpt.SetDatabaseLogon("bsps", "123", "Microsoft.Jet.OLEDB.4.0", Application.StartupPath + "\\Data\\schoolfeemanagementDB.mdb", true);
           ////     MyReport.SetDataSource(dt);
           //     MyReport.VerifyDatabase();
           //     MyReport.Refresh();
           //     MyReport.RecordSelectionFormula = vWhere;

           //     //ReportDocument.PrintToPrinter(1, true, 0, 0);

           //     MyReport.PrintToPrinter(1, true, 0, 0);
           //     MyReport.Close();
            }
            else
            {

                Frm_SeminarDialog frm = new Frm_SeminarDialog();
                frm.lb_Condi1.Text = lb_Tname2.Text;
                frm.ShowDialog();


                //    //Left_rpt.DataDefinition.FormulaFields["txt_yyyyMM"].Text

                //MyReport.Load(@RPTCur_Path + "\\SlipQ_no.rpt");
                //MyReport.DataDefinition.FormulaFields["txt_mbname"].Text = "'" + lb_Tname2.Text + "'";
                //MyReport.DataDefinition.FormulaFields["txt_ID_Card"].Text =  "'" + lbID_Card.Text + "'";

                //MyReport.PrintToPrinter(1, false, 0, 0);
                ////MyReport.PrintToPrinter(1, true, 0, 0);
            }

             
            
            //MyReport.DataSourceConnections[0].SetConnection(RptConfig[2], RptConfig[3], RptConfig[0], RptConfig[1]);

            

            
         

        }

        private void  Get_MBinfo(string ID_card)
        {
            sql = "select * from memberinfoMST where id_card='" + ID_card + "'";

            DataTable dt = SelectQuery(sql);

            if (dt.Rows.Count <= 0)
            {
           //     MessageBox.Show("ระบบค้นหาไม่พบเลขที่บัตรประชาชนเลขที่    " + lbID_Card.Text  + 
      //          Environment.NewLine + " ไม่ได้เป็นตัวแทนสมาชิก หรือ ลาออก หรือ พ้นสมาชิกภาพ แล้วหรือไม่","ค้นหา",MessageBoxButtons.OK,MessageBoxIcon.Error);
         //       ClearMe();

               
                txt_MbNo.Text = "";
                return;
                
            }


            lb_group.Text = dt.Rows[0]["groupname"].ToString();
            lb_group2.Text=dt.Rows[0]["groupname2"].ToString();
            lb_Regis.Text = dt.Rows[0]["groupname3"].ToString();
            lb_SeqNo.Text = dt.Rows[0]["seq_no"].ToString();
            txt_MbNo.Text =dt.Rows[0]["member_no"].ToString();

           



            //txt_MbNo.Text = dt.Rows[0]["member_no"].ToString();
            //    lb_group.Text = dt.Rows[0]["groupname"].ToString() + " | " + dt.Rows[0]["groupname2"].ToString() +" | " + dt.Rows[0]["amper"].ToString();
         //   lb_group.Text = dt.Rows[0]["groupname2"].ToString() + " | " + dt.Rows[0]["amper"].ToString();
//lb_SeqNo.Text = dt.Rows[0]["seq_no"].ToString();




            // txtMemberID.Text = dt.Rows[0]["member_no"].ToString();
            //   txt_MBgroup.Text = dt.Rows[0]["membgroup_desc"].ToString();

            //    lb_nameInBase.Text = dt.Rows[0]["memb_name"].ToString();
            //     lb_surnameInBase.Text = dt.Rows[0]["memb_surname"].ToString();

        }

        private void ClearMe()
        {
            lbID_Card.Text = "";
            lb_SeqNo.Text = "";

         //   cbo_PROVINCE.Text = "";

            m_picPhoto.Load(Temp_Path + "\\no.jpg");
      
          //  txt_MbNo.Text = "";
            lb_group.Text = "";
            lb_group2.Text = "";
            lb_group.Text = "";
            lb_SeqNo.Text = "";
            lb_Regis.Text = "";

            CardStateChange = false;
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
        }
      

        private void Frm_Seminar_Load(object sender, EventArgs e)
        {
            IntPtr obj;
            byte[] _lic;
            bool CardStateChange = true;



            //  สร้าง  Folder  Temps เพื่อเตรียมการสร้าง Export ไฟล์จดหมาย
            Di = new DirectoryInfo(Temp_Path);
            if (Di.Exists == false) { Di.Create(); } // สร้าง Directory
            //---------------------------------------------------------------------------------------------------------

            ConnectDB();

            m_picPhoto.Load(Temp_Path + "\\no.jpg");
      
            //BindCbo_Sex();

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

        private void Chk_AutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRead.Checked == true)
            {
                timer_AutoRead.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                timer_AutoRead.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadCard();

        }
        void SaveData()
        {
            string Rdialog = "", txtID_Card;
            string Mbformat = ClsMST.Get_Member_Format(txt_MbNo.Text);

            if (lb_Tname.Text == "")
            {
                MessageBox.Show("ยังไม่มีข้อมูล ไม่สามารถบันทึกข้อมูลได้ : ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //--------- รุปภาพ --------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------
            Image newPics = ResizeImage(m_picPhoto.Image, new Size(800, 600));

            //string xFile = Temp_Path + "\\" + lbID_Card.Text.Replace("-", "") + ".jpg";

            string xFile = Temp_Path + "\\profile_" + Mbformat + ".jpg";


            if (File.Exists(xFile))
            {
                File.Delete(xFile);
            }

            newPics.Save(xFile);

            //-------------------------------------------------------------------------------------------------------------

            txtID_Card = lbID_Card.Text.Replace("-", "");


            try
            {

                sql = "insert into Member_Regist (" +
                    "xYear," +
                    "ID_Card," +
                    "Member_no," +
                    "Regis_date" +
                ")values(" +
                "'" + lb_xYear.Text + "'," +
                "'" + lbID_Card.Text.Replace("-", "") + "'," +
                "'" +txt_MbNo.Text + "'," +
                "'" + Fc.GetshotDate(DateTime.Now.ToString(), 111) + "'" +
                ")";

                


// update Current Member Info


                //sql = "insert into memberinfoCHG (" +
                //      "Edit_date," +
                //        "ID_card," +
                //        "Member_no," +
                //        "Tfn," +
                //        "Tname," +
                //        "Tsurname," +
                //        "Efn," +
                //        "Ename," +
                //        "Esurname," +
                //        "Birth_Date," +
                //        "Sex," +
                //        "Add1," +
                //        "AddMoo," +
                //       "Addsoi," +
                //       "AddVillage," +
                //       "AddRoad," +
                //       "TUMBON," +
                //       "AMPHOE," +
                //       "PROVINCE" +
                //        ")values(" +
                //        "'" + Fc.GetshotDate(DateTime.Now.ToString(), 11) + "'," +
                //        "'" + txtID_Card + "'," +
                //        "'" + txt_MbNo.Text + "'," +
                //        "'" + lb_TFN.Text + "'," +
                //        "'" + lb_Tname.Text + "'," +
                //        "'" + lb_Tsurname.Text + "'," +
                //        "'" + lb_EFN.Text + "'," +
                //        "'" + lb_Ename.Text + "'," +
                //        "'" + lb_Esurname.Text + "'," +
                //        "'" + Fc.GetshotDate(lb_BD.Text, 1) + "'," +
                //        "'" + cbo_Sex.Text + "'," +
                //        "'" + lb_Add1.Text + "'," +
                //        "'" + lb_AddMoo.Text + "'," +
                //        "'" + lb_AddSoi.Text + "'," +
                //        "'" + lb_AddVillage.Text + "'," +
                //        "'" + lb_AddRoad.Text + "'," +
                //        "'" + cbo_TUMBON.Text + "'," +
                //        "'" + cbo_AMPHOE.Text + "'," +
                //        "'" + cbo_PROVINCE.Text + "'" +
                //        ")";
                //Rdialog = " บันทึกข้อมูล  ";
                //}
                //else
                //{
                //    sql = "update memberinfoCHG set " +
                //                "ID_card='" + lbID_Card.Text.Replace("-", "") + "'" +
                //                ",Member_no='"+ txt_MbNo.Text +"'" +
                //                ",Tfn='" + lb_TFN.Text + "'" +
                //                ",Tname='" + lb_Tname.Text + "'" +
                //                ",Tsurname='" + lb_Tsurname.Text + "'" +
                //                ",Efn='" + lb_EFN.Text + "'" +
                //                ",Ename='" + lb_Ename.Text + "'" +
                //                ",Esurname='" + lb_Esurname.Text + "'" +
                //                ",Birth_Date='" + Fc.GetshotDate(lb_BD.Text, 1) + "'" +
                //                ",Sex='"+  cbo_Sex.Text +"'"+
                //                ",Add1='" + lb_Add1.Text + "'" +
                //                ",AddMoo='" + lb_AddMoo.Text + "'" +
                //               ",Addsoi='" + lb_AddSoi.Text + "'" +
                //               ",AddVillage='" + lb_AddVillage.Text + "'" +
                //               ",AddRoad='" + lb_AddRoad.Text + "'" +
                //               ",TUMBON='" + cbo_TUMBON.Text + "'" +
                //               ",AMPHOE='" + cbo_AMPHOE.Text + "'" +
                //               ",PROVINCE='" + cbo_PROVINCE.Text + "'" +
                //                " Where ID_card = '" + txtID_Card + "'";                         

                //    Rdialog = " Update  ";

                //}

                Save_AccesslDB(sql);

            }
            catch (Exception)
            { }

          


        }

        public static Image ResizeImage(Image image, Size size,
  bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public string Save_AccesslDB(string sql)
        {
            string xReturn = "";
            try
            {
                MSsql_Save = new OleDbCommand(sql, conn);
                MSsql_Save.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //      MessageBox.Show(sql + Environment.NewLine + ex.ToString());
                xReturn = ex.ToString();

            }
            return xReturn;


        }

        private void Chk_AutoSave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Chk_AutoPrn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_time.Text = DateTime.Now.ToLocalTime().ToString();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Seminar_SizeChanged(object sender, EventArgs e)
        {
            CenterControlInParent(panel2);
        }

        private void CenterControlInParent(Control ctrlToCenter)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txt_MbNo.Text = "020016";
            PrintSlip("3560300799641");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Frm_SeminarState frm = new Frm_SeminarState();
            frm.ShowDialog();
            this.Close(); 
        }

     

       

      

     

    
    }
}

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
    public partial class Frm_ACIDcard : Form
    {


        OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter MSsql_Da = new OleDbDataAdapter();
        OleDbCommand MSsql_Save = new OleDbCommand();
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        DirectoryInfo Di, Di_ref;


        public string mB_Info;
        string sql;
        string Temp_Path = @Application.StartupPath + "\\Picture\\";
       
        string  RPTCur_Path = Application.StartupPath + "\\RPT\\";


        RDNIDWRAPPER.RDNID mRDNIDWRAPPER = new RDNIDWRAPPER.RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


        Bitmap memoryImage;
        IntPtr obj;
        byte[] _lic;
        bool CardStateChange = false;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="assembly"></param>
       /// <returns></returns>
// ReportDocument MyReport = new ReportDocument();
        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
        public Frm_ACIDcard()
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

            if (nInsertCard != 0 )
            {
                ReadCard();
            }
            

            
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
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

                if (lbID_Card.Text==lb_IDCard_Exits.Text)
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
                lb_TFN.Text = fields[(int)NID_FIELD.NAME_T];
                lb_Tname.Text = fields[(int)NID_FIELD.NAME_T];
                lb_Tsurname.Text = fields[(int)NID_FIELD.SURNAME_T];

                lb_EFN.Text = fields[(int)NID_FIELD.TITLE_E];
                lb_Ename.Text = fields[(int)NID_FIELD.NAME_E];
                lb_Esurname.Text = fields[(int)NID_FIELD.SURNAME_E];

                lb_BD.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);

                if (fields[(int)NID_FIELD.GENDER] == "1")
                { cbo_Sex.SelectedIndex = 0; }// "ชาย";
                else { cbo_Sex.SelectedIndex = 1; }// "หญิง";




                    

                    lbID_Card.Text = fields[(int)NID_FIELD.NID_Number];
              

                    lb_TFN.Text = fields[(int)NID_FIELD.TITLE_T];

                    lb_Add1.Text = fields[(int)NID_FIELD.HOME_NO];
                    lb_AddMoo.Text = fields[(int)NID_FIELD.MOO].ToString().Replace("หมู่ที่ ","");
                    lb_AddSoi.Text = fields[(int)NID_FIELD.TROK].Replace("ตรอก", "") + "   " + fields[(int)NID_FIELD.SOI].Replace("ซอย", "");
                    lb_AddRoad.Text = fields[(int)NID_FIELD.ROAD].Replace("ถนน","");
                    lb_ISSUE_NUM.Text = fields[(int)NID_FIELD.ISSUE_NUM];
                
                    cbo_TUMBON.Text =  fields[(int)NID_FIELD.TUMBON].Replace("ตำบล","");
                    cbo_AMPHOE.Text = fields[(int)NID_FIELD.AMPHOE].Replace("อำเภอ", "");
                    cbo_PROVINCE.Text = fields[(int)NID_FIELD.PROVINCE].Replace("จังหวัด", "");

                 
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



                if (Chk_AutoSave.Checked==true)
                {
                    SaveData();    
                }

                if (Chk_AutoPRT.Checked==true)
                {
                       PrintSlip(lbID_Card.Text.Replace("-",""));
                }

                ClearMe();

                return 0;


            
        }

        private void Get_MBinfo(string ID_card)
        {
            sql = "select * from memberinfoMST where id_card='"+  ID_card+"'";

            DataTable dt = SelectQuery(sql);

            if (dt.Rows.Count <= 0)
            {
               //MessageBox.Show("ระบบค้นหาไม่พบเลขที่บัตรประชาชนเลขที่    " + lbID_Card.Text  + 
                                                         //Environment.NewLine + " ลาออก/พ้นสมาชิกภาพ แล้วหรือไม่","ค้นหา",MessageBoxButtons.OK,MessageBoxIcon.Error);
               //ClearMe();
                return;
            }

            txt_MbNo.Text =dt.Rows[0]["member_no"].ToString();
        //    lb_group.Text = dt.Rows[0]["groupname"].ToString() + " | " + dt.Rows[0]["groupname2"].ToString() +" | " + dt.Rows[0]["amper"].ToString();
            lb_group.Text =  dt.Rows[0]["groupname2"].ToString() + " | " + dt.Rows[0]["amper"].ToString();
            lb_SeqNo.Text = dt.Rows[0]["seq_no"].ToString();



            sql = "select * from LocationAT where groupname='" + dt.Rows[0]["groupname2"].ToString()  + "'";
            DataTable dt_at = SelectQuery(sql);

               if (dt_at.Rows.Count>0)
	            {
                    lb_LoAt.Text = dt_at.Rows[0]["LoAt"].ToString();
                    lb_LoDate.Text = dt_at.Rows[0]["LoDate"].ToString();
                    lb_LoTime.Text = dt_at.Rows[0]["LoTime"].ToString();
	            }


            // txtMemberID.Text = dt.Rows[0]["member_no"].ToString();
            //   txt_MBgroup.Text = dt.Rows[0]["membgroup_desc"].ToString();

        //    lb_nameInBase.Text = dt.Rows[0]["memb_name"].ToString();
       //     lb_surnameInBase.Text = dt.Rows[0]["memb_surname"].ToString();

        }

        private void ClearMe()
        {
            lbID_Card.Text = "";
            lb_TFN.Text = "";
            lb_Tname.Text = "";
            lb_Tsurname.Text = "";
            lb_EFN.Text = "";
            lb_Ename.Text = "";
            lb_Esurname.Text = "";
            lb_Add1.Text = "";
            lb_AddMoo.Text = "";
            lb_AddRoad.Text = "";
            lb_AddSoi.Text = "";
            lb_AddVillage.Text = "";
            lb_BD.Text = ""; ;
            lb_SeqNo.Text = "";
            


            cbo_TUMBON.Text = "";
            cbo_AMPHOE.Text = "";
            cbo_PROVINCE.Text = "";

            m_picPhoto.Load(Temp_Path + "\\no.jpg");
            lb_ISSUE_NUM.Text = "";
            txt_MbNo.Text = "";
            lb_group.Text = "";
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

        private void Frm_ACIDcard_Load(object sender, EventArgs e)
        {
            IntPtr obj;
            byte[] _lic;
            bool CardStateChange = true;



            //  สร้าง  Folder  Temps เพื่อเตรียมการสร้าง Export ไฟล์จดหมาย
            Di = new DirectoryInfo(Temp_Path);
            if (Di.Exists == false) { Di.Create(); } // สร้าง Directory
            //---------------------------------------------------------------------------------------------------------

            ConnectDB();


           //BindCbo_Sex();


        }

        private void ConnectDB()
        {

            string ConnectStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\POPA2565.mdb;Persist Security Info=False;";
          
            conn.ConnectionString = ConnectStr;
            conn.Open();
            
        }

        public DataTable SelectQuery (string str)
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

        private void BindCbo_Sex()
        {
            cbo_Sex.Items.Add(new { Text = "ชาย", Value = "M" });
            cbo_Sex.Items.Add(new { Text = "หญิง", Value = "F" });
            cbo_Sex.DisplayMember = "Text";
            cbo_Sex.ValueMember = "Value";
        }
        String _yyyymmdd_(String d)
        {
            string s = "";
            string _yyyy = d.Substring(0, 4);
            string _mm = d.Substring(4, 2);
            string _dd = d.Substring(6, 2);


            string[] mm = { "", "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            string _tm = "-";
            if (_mm == "00")
            {
                _tm = "-";
            }
            else
            {
                _tm = mm[int.Parse(_mm)];
            }
            if (_yyyy == "0000")
                _yyyy = "-";

            if (_dd == "00")
                _dd = "-";

            s = _dd + " " + _tm + " " + _yyyy;
            return s;
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        void SaveData()
        {
            string Rdialog="", txtID_Card;
            string Mbformat = ClsMST.Get_Member_Format(txt_MbNo.Text);

            if (lb_Tname.Text=="")
            {
                MessageBox.Show("ยังไม่มีข้อมูล ไม่สามารถบันทึกข้อมูลได้ : ", "แจ้งเตือน",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
           
            txtID_Card= lbID_Card.Text.Replace("-", "") ;
            

            try
            {

            
                sql = "insert into memberinfoCHG (" +
                      "Edit_date," + 
                        "ID_card," +
                        "Member_no,"+
                        "Tfn," +
                        "Tname," +
                        "Tsurname," +
                        "Efn," +
                        "Ename," +
                        "Esurname," +
                        "Birth_Date,"+
                        "Sex,"+
                        "Add1," +
                        "AddMoo," +
                       "Addsoi," +
                       "AddVillage," +
                       "AddRoad," +
                       "TUMBON," +
                       "AMPHOE," +
                       "PROVINCE" +
                        ")values(" +
                        "'" + Fc.GetshotDate(DateTime.Now.ToString()  ,11) + "'," +
                        "'" +  txtID_Card + "'," +
                        "'" +  txt_MbNo.Text + "'," +
                        "'" + lb_TFN.Text + "'," +
                        "'" + lb_Tname.Text + "'," +
                        "'" + lb_Tsurname.Text + "'," +
                        "'" + lb_EFN.Text + "'," +
                        "'" + lb_Ename.Text + "'," +
                        "'" + lb_Esurname.Text + "'," +
                        "'" + Fc.GetshotDate(lb_BD.Text,1) + "'," +
                        "'"+  cbo_Sex.Text +"',"+
                        "'" + lb_Add1.Text + "'," +
                        "'" + lb_AddMoo.Text + "'," +
                        "'" + lb_AddSoi.Text + "'," +
                        "'" + lb_AddVillage.Text + "'," +
                        "'" + lb_AddRoad.Text + "'," +
                        "'" + cbo_TUMBON.Text + "'," +
                        "'" + cbo_AMPHOE.Text + "'," +
                        "'" + cbo_PROVINCE.Text + "'" +
                        ")";
                Rdialog = " บันทึกข้อมูล  ";
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
            {}

            //MessageBox.Show("ระบบ ( " + Rdialog +  " ) เรียบร้อยแล้ว", "บันทึกข้อมูล",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);

            Frm_ACIDdialog frmdlg = new Frm_ACIDdialog();
            frmdlg.pictureBox1.Image=m_picPhoto.Image;
            frmdlg.lb_SeqNo.Text = lb_SeqNo.Text;
            frmdlg.lb_groupname.Text= lb_group.Text;
            frmdlg.lb_At.Text = lb_LoAt.Text;
            frmdlg.lb_date.Text = Fc.GetshotDate(lb_LoDate.Text,66);
            frmdlg.lb_time.Text = lb_LoTime.Text;
            frmdlg.ShowDialog();

            this.Text =  Convert.ToString(Convert.ToInt32(this.Text) + 1);
      














        


            //m_picPhoto.Image.Save(Temp_Path + "\\" + lbID_Card.Text + ".jpg");
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

        private void button2_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Chk_AutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRead.Checked == true)
            {
                timer_AutoRead.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
                //button4.Enabled = false;

            }
            else
            {
                timer_AutoRead.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
              //  button4.Enabled = true;
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void PrintSlip(string ID_card)
        {
            //ReportDocument rpt = new ReportDocument();




            string vWhere = "{MemberInfoCHG.ID_card} = '" + ID_card + "'";
            if (txt_MbNo.Text != "")
            {
                //string sql = "select * from MemberInfoCHG  where id_card= '" + ID_card + "'";
                //DataTable dt = SelectQuery(sql);
                //rpt.Load(@RPTCur_Path + "\\SlipQ.rpt");

                //rpt.SetDataSource(dt);
                //rpt.PrintToPrinter(1, true, 0, 0);


                //MyReport.Load(@RPTCur_Path + "\\SlipQPoppa.rpt");
                ////MyReport.SetDataSource = Application.StartupPath.ToString()  + "\\SEMINAR.mdb";

                //MyReport.SetDatabaseLogon("", "", "Microsoft.ACE.OLEDB.12.0", Application.StartupPath + "\\POPA2565.mdb", true);

                ////rpt.SetDatabaseLogon("bsps", "123", "Microsoft.Jet.OLEDB.4.0", Application.StartupPath + "\\Data\\schoolfeemanagementDB.mdb", true);
                ////     MyReport.SetDataSource(dt);
                //MyReport.VerifyDatabase();
                //MyReport.Refresh();
                //MyReport.RecordSelectionFormula = vWhere;

                ////ReportDocument.PrintToPrinter(1, true, 0, 0);

                //MyReport.PrintToPrinter(1, true, 0, 0);
                //MyReport.Close();
            }
            else
            {

                Frm_SeminarDialog frm = new Frm_SeminarDialog();
                frm.lb_Condi1.Text = lb_TFN.Text + lb_Tname.Text + "   " + lb_Tsurname.Text;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (Chk_AutoRead.Checked==true)
            {
                MessageBox.Show("ระบบไม่สามารถ ทำงานได้เนื่องจาก ท่านเปิดการทำงาน แบบ Auto Read", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Frm_ACIDreport frm = new Frm_ACIDreport();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //string xFile = Temp_Path + "\\profile_" + Mbformat + ".jpg";
            try
            {
                File.Move(@"C:\SAM.txt", @"C:\SAMUEL.txt"); // Try to move
                Console.WriteLine("Moved"); // Success
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex); // Write error
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       

    }
}

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

using System.Globalization;
using Oracle.DataAccess.Client;



namespace SVMember
{
    public partial class Frm_UpdateData : Form
    {
        
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;
        string str;

        
        IntPtr obj;
        bool CardStateChange = false;

        public string mB_Info;

        RDNIDWRAPPER.RDNID mRDNIDWRAPPER = new RDNIDWRAPPER.RDNID();
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


        Bitmap memoryImage;
        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        public Frm_UpdateData()
        {
           // InitializeComponent();

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

        
        private void Frm_UpdateData_Load(object sender, EventArgs e)
        {
            //ClsMST.GetDB_Oracle(); // Oracle
            //ShowData();
            

            ClsMST.GetDb();
            bool CardStateChange = true;
           
        }

        void ShowData() //string id_card
         {
            //string vWhere = "";

            str = "select " +
                             " mb.MEMBER_NO,   " +
                             " mb.CARD_PERSON,    " +
                             " mbucfprename.prename_desc || mb.MEMB_NAME || '    ' || mb.MEMB_SURNAME as Mbname,   " +
                             " mb.MEMBGROUP_CODE,   " +
                             " mbucfmembgroup.membgroup_desc,  " +
                             " mb.resign_status,   " +
                             " mb.MEMBER_DATE,   " +
                             " mb.BIRTH_DATE,   " +
                             " mb.MEMBER_STATUS,   " +
                             " mb.RETRY_DATE,   " +
                             " mb.RESIGN_DATE,   " +
                             " mb.MATE_NAME,   " +
                             " mb.addr_phone,     " +
                             " mb.addr_mobilephone,  " +
                             " mb.addr_no ||' ม.' ||mb.addr_moo || ' ซ.' || mb.addr_SOI || ' หมู่บ้าน.' ||mb.addr_village || ' ถ.' ||mb.addr_ROAD || ' ต.' || mbucftambol.tambol_desc || ' อ.' || mbucfdistrict.district_desc || ' จ. ' || mbucfprovince.province_desc || ' ' ||mb.addr_postcode Add1," +
                             " mb.tambol_code,   " +
                             " mb.amphur_CODE,   " +
                             " mb.PROVINCE_CODE,   " +
                             " mb.addr_POSTCODE,   " +
                             " mbucftambol.tambol_desc, " +
                             " mbucfdistrict.district_desc, " +
                             " mbucfprovince.province_desc, " +
                             " mb.Curraddr_no ||' ม.' || mb.Curraddr_moo || ' ซ.' || mb.Curraddr_SOI || ' หมู่บ้าน.' || mb.Curraddr_village || ' ถ.' || mb.Curraddr_ROAD || ' ต.' || mbucftambol_curr.tambol_desc || ' อ.' || mbucfdistrict_curr.district_desc || ' จ. ' ||mbucfprovince_curr.province_desc || ' ' ||mb.addr_postcode Cur_Add1, " +
                             " mb.Currtambol_code,     " +
                             " mb.Curramphur_CODE,     " +
                             " mb.CurrPROVINCE_CODE,     " +
                             " mb.Curraddr_POSTCODE,  " +
                             " mbucfprovince_curr.province_desc," +
                             "mbucfdistrict_curr.district_desc, " +
                             " mbucftambol_curr.tambol_desc, " +
                             " mb.Curraddr_POSTCODE " +
                             " From MBMEMBMASTER mb inner join mbucfprename on mb.prename_code = mbucfprename.prename_code  " +
                             " inner join mbucfmembgroup on mb.MEMBGROUP_CODE = mbucfmembgroup.membgroup_code " +
                             " inner join mbucfprovince on mb.province_code = mbucfprovince.province_code " +
                             " inner join mbucfdistrict on mb.amphur_code = mbucfdistrict.district_code " +
                             " inner join mbucftambol on mb.tambol_code = mbucftambol.tambol_code " +
                             " inner join mbucfprovince  mbucfprovince_curr on mb.currprovince_code = mbucfprovince_curr.province_code"+
                             " inner join mbucfdistrict  mbucfdistrict_curr on mb.curramphur_code = mbucfdistrict_curr.district_code" +
                             " inner join mbucftambol  mbucftambol_curr on mb.currtambol_code = mbucftambol_curr.tambol_code" +
                             " where mb.Card_person ='" + m_txtID.Text.Replace("-", "") + "'";
            
            
            

          

            //if (id_card != "")
            //{
            //    vWhere = "AND where mb.member_no ='" + id_card + "'";
            //}

            //str += vWhere;



            DataTable dt = ClsMST.SelectQuery(str);
            if (dt.Rows.Count<=0)
            {
                //string message = "ข้อมูลบัตรประชาชน ไม่มีอยู่ในข้อมูลสมาชิกสหกรณ์ กรุณาติดต่อเจ้าหน้าที่";
                //string title = "error";
                //MessageBoxButtons buttons = MessageBoxButtons.OK;
                //DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                //if (result == DialogResult.Abort)
                //{
                //    this.Close();
                //}  

                return;
            }


           // lbMb_name.Text = dt.Rows[0]["memb_name"].ToString();
            lbMb_name.Text = dt.Rows[0]["Mbname"].ToString();
            lbMb_Add.Text = dt.Rows[0]["Add1"].ToString();
            lbMb_Add2.Text = dt.Rows[0]["Cur_Add1"].ToString();
            label9.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString() , 15);
            lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
            label5.Text = dt.Rows[0]["addr_mobilephone"].ToString();
           // lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
           // lbMb_Add.Text = dt.Rows[0]["add1"].ToString();
            //lbMb_Add.Text = 
                

        
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
          
        //  //  ReadCard();
        //    ShowData();
        //}

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
            else 
            {
                ClearData();
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
      
                //String m;
                //m = String.Format(" error no {0} ", nInsertCard);
                //MessageBox.Show(m);

                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);

                CardStateChange = false;
                ClearData();
                
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
                //label2.Text = fullname;

               

                string birthday = fields[(int)NID_FIELD.BIRTH_DATE].ToString();
                DateTime date;
                CultureInfo thaiCulture = new CultureInfo("th-TH");
                thaiCulture.DateTimeFormat.Calendar = new ThaiBuddhistCalendar();

                if (DateTime.TryParseExact(birthday, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    int thaiYear = date.Year; // Convert to Thai year
                    string thaiDate = date.ToString("dd MMMM", thaiCulture) + " " + thaiYear.ToString();
                    m_txtBrithDate.Text = thaiDate;
                }
                else
                {
                    // Handle the case where the input is not in the expected format
                    // Display an error message or provide a default value
                }


                m_txtAddress.Text = fields[(int)NID_FIELD.HOME_NO] + " " +
                                        fields[(int)NID_FIELD.MOO] + " " +
                                        fields[(int)NID_FIELD.TROK] + " " +
                                        fields[(int)NID_FIELD.SOI] + " " +
                                        fields[(int)NID_FIELD.ROAD] + " " +
                                        fields[(int)NID_FIELD.TUMBON] + " " +
                                        fields[(int)NID_FIELD.AMPHOE] + " " +
                                        fields[(int)NID_FIELD.PROVINCE] + " "
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

                // Clear the values step by step
                

                ShowData();
              
                
               
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

          //  Get_MBinfo(NIDNum);

           

            return 0;

        }

        

        private void ClearData()
        {
            // Clear the text boxes
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtBrithDate.Text = "";
            m_txtAddress.Text = "";
            // Clear the picture box
            m_picPhoto.Image = null;
            // Clear any other relevant fields
            lb_nameInBut.Text = "";
            lb_surnameInBut.Text = "";
            //label2.Text = "";
            lb_mbno.Text = "";
            lbMb_name.Text = "";
            lbMb_Add.Text = "";
            lbMb_Add2.Text = "";
            label9.Text = "";
            label5.Text = "";


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
        };

     

        private void m_ListReaderCard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //ShowData();
            ReadCard();
           // MessageBox.Show("555");
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_time.Text = DateTime.Now.ToLocalTime().ToString();
        }

      

        //protected override void OnShown(EventArgs e)
        //{

        //    String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);
        //    IntPtr obj = selectReader(strTerminal);
        //     Int32 nInsertCard = 0;
        //    nInsertCard = RDNID.connectCardRD(obj);
        //    if (nInsertCard == 0)
        //    {
        //        base.OnShown(e);
        //        button1.PerformClick();
        //    }
        //    else {
        //        MessageBox.Show("666");

        //    }
           
        //}

       

        // Function to convert the date to Thai format
        

    }
}

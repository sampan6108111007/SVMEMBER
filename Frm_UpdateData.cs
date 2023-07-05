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
using System.Text.RegularExpressions;




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

        private bool ValidateThaiPhoneNumber(string phoneNumber)
        {
            // Define the regular expression pattern for Thai phone numbers
            string pattern = @"^(0[689]{1}[0-9]{8})$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Perform the validation
            bool isValid = regex.IsMatch(phoneNumber);

            return isValid;
        }


        void ShowData() //string id_card
         {
            //string vWhere = "";

            str = "select " +
                             " mb.MEMBER_NO,   " +
                             " mb.CARD_PERSON,    " +
                              "mbucfprename.prename_code || '' prename_code ," +
                             "mbucfprename.prename_desc," +
                             "mb.MEMB_NAME," +
                             "mb.MEMB_SURNAME," +
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
                             " mb.addr_no, " +
                             " mb.addr_moo,  "+
                             " mb.addr_SOI, "+
                             " mb.addr_village, "+
                             " mb.addr_ROAD, " +
                             //" mb.addr_no ||' หมู่ที่.' ||mb.addr_moo || ' ซอย.' || mb.addr_SOI || ' หมู่บ้าน.' ||mb.addr_village || ' ถนน' ||mb.addr_ROAD || ' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' || mbucfprovince.province_desc || ' ' ||mb.addr_postcode Add1," +
                             " mb.tambol_code,   " +
                             " mb.amphur_CODE,   " +
                             " mb.PROVINCE_CODE,   " +
                             " mb.addr_POSTCODE,   " +
                             " mbucftambol.tambol_desc, " +
                             " mbucfdistrict.district_desc, " +
                             " mbucfprovince.province_desc, " +
                             //" mb.Curraddr_no ||' หมู่ที่.' || mb.Curraddr_moo || ' ซอย.' || mb.Curraddr_SOI || ' หมู่บ้าน.' || mb.Curraddr_village || ' ถนน' || mb.Curraddr_ROAD || ' ตำบล' || mbucftambol_curr.tambol_desc || ' อำเภอ' || mbucfdistrict_curr.district_desc || ' จังหวัด' ||mbucfprovince_curr.province_desc || ' ' ||mb.Curraddr_POSTCODE Cur_Add1, " +
                             " ' ตำบล' || mbucftambol_curr.tambol_desc || ' อำเภอ' || mbucfdistrict_curr.district_desc || ' จังหวัด' ||mbucfprovince_curr.province_desc || ' ' ||mb.Curraddr_POSTCODE Cur_Add1, " +
                             " mb.Curraddr_no, " +
                             " mb.Curraddr_moo, " +
                             " mb.Curraddr_SOI, " +
                             " mb.Curraddr_village, " +
                             " mb.Curraddr_ROAD, " +
                             " mb.Currtambol_code,     " +
                             " mb.Curramphur_CODE,     " +
                             " mb.CurrPROVINCE_CODE,     " +
                             " mb.Curraddr_POSTCODE,  " +
                             " mbucfprovince_curr.province_desc ||'' crr_province," +
                             " mbucfdistrict_curr.district_desc ||'' crr_district, " +
                             " mbucftambol_curr.tambol_desc ||'' crr_tambol, " +
                             " mb.Curraddr_POSTCODE " +
                             " From MBMEMBMASTER mb  " +
                             " inner join mbucfprename on mb.prename_code = mbucfprename.prename_code  " +
                             " inner join mbucfmembgroup on mb.MEMBGROUP_CODE = mbucfmembgroup.membgroup_code " +
                             " inner join mbucfprovince on mb.province_code = mbucfprovince.province_code " +
                             " inner join mbucfdistrict on mb.amphur_code = mbucfdistrict.district_code " +
                             " inner join mbucftambol on mb.tambol_code = mbucftambol.tambol_code " +
                             " inner join mbucfprovince  mbucfprovince_curr on mb.currprovince_code = mbucfprovince_curr.province_code" +
                             " inner join mbucfdistrict  mbucfdistrict_curr on mb.curramphur_code = mbucfdistrict_curr.district_code" +
                             " inner join mbucftambol  mbucftambol_curr on mb.currtambol_code = mbucftambol_curr.tambol_code" +
                             " where mb.Card_person ='" + m_txtID.Text.Replace("-", "") + "' AND  mb.resign_status ='0'";



           
                                      //  " where mb.Card_person ='" + m_txtID.Text.Replace("-", "") + "' AND mb.resign_status ='0'";

            //if (id_card != "")
            //{
            //    vWhere = "AND where mb.member_no ='" + id_card + "'";
            //}

            //str += vWhere;

            if (lb_Errortext.Text!="")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
            {
                return;
            }



            DataTable dt = ClsMST.SelectQuery(str);
            if (dt.Rows.Count<=0)
            {
                
                string message = "ข้อมูลบัตรประชาชน ไม่มีอยู่ในข้อมูลสมาชิกสหกรณ์ กรุณาติดต่อเจ้าหน้าที่";
                lb_Errortext.Text = message;

                if (MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation)==DialogResult.OK )
                {
                    return;
                } ;
                
            }

           

            string txt_add = "";

            if (dt.Rows[0]["addr_no"].ToString() != "") { txt_add += dt.Rows[0]["addr_no"].ToString(); }
            if (dt.Rows[0]["addr_moo"].ToString() != "") { txt_add += " หมู่ที่ " + dt.Rows[0]["addr_moo"].ToString(); }
            if (dt.Rows[0]["addr_SOI"].ToString() != "") { txt_add += " ซอย " + dt.Rows[0]["addr_moo"].ToString(); }
            if (dt.Rows[0]["addr_village"].ToString() != "") { txt_add += " หมู่บ้าน " + dt.Rows[0]["addr_village"].ToString(); }
            if (dt.Rows[0]["addr_ROAD"].ToString() != "") { txt_add += " ถนน " + dt.Rows[0]["addr_ROAD"].ToString(); }

            txt_add += " ตำบล" + dt.Rows[0]["tambol_desc"].ToString();
            txt_add += " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            txt_add += " จังหวัด" + dt.Rows[0]["province_desc"].ToString();
            lb_PostcodeInBut.Text = dt.Rows[0]["addr_postcode"].ToString();
            //txt_add += " " + dt.Rows[0]["addr_postcode"].ToString();
           // txt_add = txt_add.Replace(" ", "");

            string txt_Crradd = "";

            if (dt.Rows[0]["Curraddr_no"].ToString() != "") { txt_Crradd += dt.Rows[0]["Curraddr_no"].ToString(); }
            if (dt.Rows[0]["Curraddr_moo"].ToString() != "") { txt_Crradd += " หมู่ที่ " + dt.Rows[0]["Curraddr_moo"].ToString(); }
            if (dt.Rows[0]["Curraddr_SOI"].ToString() != "") { txt_Crradd += " ซอย " + dt.Rows[0]["Curraddr_SOI"].ToString(); }
            if (dt.Rows[0]["Curraddr_village"].ToString() != "") { txt_Crradd += "หมู่บ้าน " + dt.Rows[0]["Curraddr_village"].ToString(); }
            if (dt.Rows[0]["Curraddr_ROAD"].ToString() != "") { txt_Crradd += " ถนน " + dt.Rows[0]["Curraddr_ROAD"].ToString(); }

            //txt_Crradd += " ตำบล" + dt.Rows[0]["tambol_desc"].ToString();
            //txt_Crradd += " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            //txt_Crradd += " จังหวัด" + dt.Rows[0]["province_desc"].ToString();
            //txt_Crradd += " " + dt.Rows[0]["Curraddr_POSTCODE"].ToString();


            txt_Crradd += " " + dt.Rows[0]["Cur_Add1"].ToString();

        

            lbMb_Add.Text = txt_add.ToString();
            lbMb_name.Text = dt.Rows[0]["Mbname"].ToString();
            lbMb_Add2.Text = txt_Crradd.ToString();
            lb_BirthdateC.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 9);
            //TextBox1.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 9);
            //lb_BirthdateC.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 12);
            lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
            lb_Tel.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            lb_telC.Text = dt.Rows[0]["addr_mobilephone"].ToString();
            lb_PostcodeC.Text = dt.Rows[0]["curraddr_postcode"].ToString();
            lb_PrenameC.Text = dt.Rows[0]["prename_code"].ToString();
            lb_PrenameCode.Text = dt.Rows[0]["prename_code"].ToString();
            lb_nameC.Text = dt.Rows[0]["memb_name"].ToString();
            lb_surnameC.Text = dt.Rows[0]["memb_surname"].ToString();
            lb_HomeNoC.Text = dt.Rows[0]["curraddr_no"].ToString();
            lb_MooC.Text = dt.Rows[0]["curraddr_moo"].ToString();
            lb_TrokC.Text = dt.Rows[0]["curraddr_village"].ToString();
            lb_SoiC.Text = dt.Rows[0]["curraddr_soi"].ToString();
            lb_RoadC.Text = dt.Rows[0]["curraddr_road"].ToString();
            lb_TumbolC.Text = dt.Rows[0]["Currtambol_code"].ToString();
            lb_AmphoeC.Text = dt.Rows[0]["Curramphur_CODE"].ToString();
            lb_ProvinceC.Text = dt.Rows[0]["CurrPROVINCE_CODE"].ToString();
            lb_PostcodeC.Text = dt.Rows[0]["Curraddr_POSTCODE"].ToString();
            lb_IDcardC.Text = dt.Rows[0]["card_person"].ToString();
            lb_BirthdateC2.Text = dt.Rows[0]["birth_date"].ToString();
            //lb_BirthdateC2.Text = Fc.GetshotDate(dt.Rows[0]["birth_date"].ToString(), 0);

           
            //string value = lb_BirthdateC.Text;
            //TextBox2.Text = Fc.GetshotDate(value, 1);
           
            //lb_PrenameC.Text = dt.Rows[0]["curraddr_postcode"].ToString();
           // lb_mbno.Text = dt.Rows[0]["member_no"].ToString();
           // lbMb_Add.Text = dt.Rows[0]["add1"].ToString();
            //lbMb_Add.Text = 

            
            CheckData();
            
            //|| mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' || mbucfprovince.province_desc || ' ' ||mb.addr_postcode Add1," +
                //" mb.addr_no ||' หมู่ที่.' ||mb.addr_moo || ' ซอย.' || mb.addr_SOI || ' หมู่บ้าน.' ||mb.addr_village || ' ถนน' ||mb.addr_ROAD || ' ตำบล' || mbucftambol.tambol_desc || ' อำเภอ' || mbucfdistrict.district_desc || ' จังหวัด' || mbucfprovince.province_desc || ' ' ||mb.addr_postcode Add1," +

          
          // MessageBox.Show(txt_add);
            }


        void SaveData()
        {
            string Rdialog = "";
           
            sql = "insert into mbreqchgaddress (" +
               "appl_docno," + //เลขที่คำขอ yyyymmdd HH:MM
               "branch_id," + // 027001
               "member_no," +
               "memb_addrc," +
               "addr_mooc," +
               "addr_soic," +
               "addr_villagec," +
               "addr_roadc," +
               "tambon_codec," +
               "amphur_codec," +
               "provice_codec," +
               "memb_addrn," +
               "addr_moon," +
               "addr_soin," +
               "addr_villagen," +
               "addr_roadn," +
               "tambon_coden," +
               "amphur_coden," +
               "province_coden," +
                "app_status," +
                "app_date," +
                "entry_id," + // ชื่อผู้บันถึก
                "entry_date," +
                "entry_type," + // 0 เอกสาร 1 บัตร
               "card_personc," +
               "card_personn," +
               "birth_datec," +
               "birth_daten," +
                 "addr_mobilephonec," +
                 "addr_mobilephonen," +
                 "bycard_number," +
                 "prename_codec," +
                 "prename_coden," +
                 "memb_namec," +
                 "memb_namen," +
                 "memb_surnamec," +
                 "memb_surnamen" +
                 ")values(" +
                // "'" + Fc.GetshotDate(DateTime.Now.ToString(), 11) + "'," +
                 "'" + lb_Appl_Docno.Text + "'" +
                 ",'" + lb_CoopId.Text + "'" +
                 ",'" + lb_mbno.Text + "'" +
                 ",'" + lb_HomeNoC.Text + "'" +
                 ",'" + lb_MooC.Text + "'" +
                 ",'" + lb_SoiC.Text + "'" +
                 ",'" + lb_TrokC.Text + "'" +
                 ",'" + lb_RoadC.Text + "'" +
                 ",'" + lb_TumbolC.Text + "'" +             
                 ",'" + lb_AmphoeC.Text + "'" +
                 ",'" + lb_ProvinceC.Text + "'" +
                 ",'" + lb_HomeNoInBut.Text + "'" +
                 ",'" + lb_MooInBut.Text + "'" +
                 ",'" + lb_SoiInBut.Text + "'" +
                 ",'" + lb_TrokInBut.Text + "'" +
                 ",'" + lb_RoadInBut.Text + "'" +
                 ",'" + lb_TumbolCode.Text + "'" +
                 ",'" + lb_AmphoeInBut.Text + "'" +
                 ",'" + lb_ProvinceInBut.Text + "'" +
                 ",'" + lb_App_Status.Text + "'" +
                 ",TO_DATE('" + Fc.GetshotDate(lb_App_Date.Text, 0) +"'," + "'MM/DD/YYYY')" +
                // ",'" + lb_App_Date.Text + "'" +
                 ",'" + lb_Entry_Id.Text + "'" +
                 ",TO_DATE('" + Fc.GetshotDate(lb_Entry_Date.Text, 0)  + "'," + "'MM/DD/YYYY')" +
                // ",'" + lb_Entry_Date.Text + "'" +
                 ",'" + lb_Entry_Type.Text + "'" +
                 ",'" + lb_IDcardC.Text + "'" +
                 ",'" + lb_IDcardInBut.Text + "'" +
                 ",TO_DATE('" + Fc.GetshotDate(lb_BirthdateC2.Text, 0) + "'," + "'MM/DD/YYYY')" +
                 //",'" + lb_BirthdateC2.Text + "'" +
                 //",'" + lb_BirthdateInBut.Text + "'" +
                 ",TO_DATE('" + Fc.GetshotDate(lb_BirthdateInBut.Text, 0) + "'," + "'MM/DD/YYYY')" +
                //  ",'" + Fc.GetshotDate(lb_BirthdateInBut.Text, 0) + "'" +
                 ",'" + lb_telC.Text + "'" +
                 ",'" + lb_Tel.Text + "'" +
                 ",'" + m_txtIssueNum.Text + "'" +
                 ",'" + lb_PrenameC.Text + "'" +
                 ",'" + lb_PrenameCode.Text + "'" +
                 ",'" + lb_nameC.Text + "'" +
                 ",'" + lb_nameInBut.Text + "'" +
                 ",'" + lb_surnameC.Text + "'" +
                 ",'" + lb_surnameInBut.Text +"'"+
                 ")";

            ClsMST.Save_ORACLE(sql);
            

                Rdialog = " บันทึกข้อมูล  ";
                MessageBox.Show("ระบบ ( " + Rdialog + " ) เรียบร้อยแล้ว", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            
           

            
        }


        void UpdateData()
        {
            sql = "UPDATE mbreqchgaddress" +
                " SET birth_daten = TO_DATE('" + lb_BirthdateC2.Text + "'," + "'MM/DD/YYYY')" +
                " , addr_mobilephonen = '" + lb_Tel.Text + "' WHERE card_personn = '" + m_txtID.Text.Replace("-","") + "'";
             
            ClsMST.Save_ORACLE(sql);

        }


        void ConvertInbut()
        {
            str = "SELECT mbucfdistrict.district_code , mbucfdistrict.district_desc, mbucfdistrict.postcode, mbucfdistrict.province_code, tambol_code, tambol_desc  FROM mbucftambol " +
                  "inner join mbucfdistrict on mbucftambol.district_code = mbucfdistrict.district_code WHERE tambol_desc = '" + lb_TumbolInBut.Text + "'"; 

            //if (label2.Text != "")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
            //{
            //    //MessageBox.Show("555");
            //    return;
            //}

            DataTable dt = ClsMST.SelectQuery(str);
            if (dt.Rows.Count >= 0)
            {
                
                
                    lb_ProvinceInBut.Text = dt.Rows[0]["province_code"].ToString();
                    lb_AmphoeInBut.Text = dt.Rows[0]["district_code"].ToString();
                    lb_TumbolCode.Text = dt.Rows[0]["tambol_code"].ToString();
                    lb_PostcodeInBut.Text = dt.Rows[0]["postcode"].ToString();
            }
        }


        //void ConvertPrename()
        //{
        //    str = "SELECT * FROM mbucfprename WHERE prename_desc = '" + lb_PrenameInbut.Text.Replace("น.ส","นางสาว") + "'";

        //    //if (label2.Text != "")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
        //    //{
        //    //    //MessageBox.Show("555");
        //    //    return;
        //    //}

        //    DataTable dt = ClsMST.SelectQuery(str);
        //    if (dt.Rows.Count >= 0)
        //    {


        //        lb_PrenameCode.Text = dt.Rows[0]["prename_code"].ToString();
               
        //    }
        //}


        

        private void timer_AutoRead_Tick(object sender, EventArgs e)
        {

            Int32 nInsertCard = 0;
            nInsertCard = RDNID.connectCardRD(obj);


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
                lb_IDcardInBut.Text = NIDNum;
               
               
                            
                String fullname = fields[(int)NID_FIELD.TITLE_T].Replace("น.ส.","นางสาว")  +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T];
                m_txtFullNameT.Text = fullname;

                // เอส   ----------------------------------------------------------------------------------------------------------------

                lb_PrenameInbut.Text = fields[(int)NID_FIELD.TITLE_T];
                lb_nameInBut.Text = fields[(int)NID_FIELD.NAME_T];
                lb_surnameInBut.Text = fields[(int)NID_FIELD.SURNAME_T];
                label4.Text = fullname;

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

                m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                //lb_BirthdateInBut.Text = fields[(int)NID_FIELD.BIRTH_DATE];

                string birthdate = fields[(int)NID_FIELD.BIRTH_DATE]; // Assuming birthdate is in a string format, e.g., "25391011"

                // Extract the individual components from the birthdate string
                int year = int.Parse(birthdate.Substring(0, 4)) - 543;
                int month = int.Parse(birthdate.Substring(4, 2));
                int day = int.Parse(birthdate.Substring(6, 2));

                // Create a DateTime object with the extracted components
                DateTime parsedDate = new DateTime(year, month, day);

                string formattedBirthdate = parsedDate.ToString("dd/MM/yyyy");
                lb_BirthdateInBut.Text = formattedBirthdate;

                





                m_txtAddress.Text = fields[(int)NID_FIELD.HOME_NO] + " " +
                                        fields[(int)NID_FIELD.MOO] + " " +
                                        fields[(int)NID_FIELD.TROK] + " " +
                                        fields[(int)NID_FIELD.SOI] + " " +
                                        fields[(int)NID_FIELD.ROAD] + " " +
                                        fields[(int)NID_FIELD.TUMBON] + " " +
                                        fields[(int)NID_FIELD.AMPHOE] + " " +
                                        fields[(int)NID_FIELD.PROVINCE] + " "
                                        ;

                lb_HomeNoInBut.Text = fields[(int)NID_FIELD.HOME_NO];
                lb_MooInBut.Text = fields[(int)NID_FIELD.MOO].Replace("หมู่ที่", "");
                lb_TrokInBut.Text = fields[(int)NID_FIELD.TROK];
                lb_SoiInBut.Text = fields[(int)NID_FIELD.SOI];
                lb_RoadInBut.Text = fields[(int)NID_FIELD.ROAD];
                lb_TumbolInBut.Text = fields[(int)NID_FIELD.TUMBON].Replace("ตำบล", "");
                lb_AmphoeInBut.Text = fields[(int)NID_FIELD.AMPHOE].Replace("อำเภอ", "");
                lb_ProvinceInBut.Text = fields[(int)NID_FIELD.PROVINCE].Replace("จังหวัด", "");


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
                ConvertInbut();
               // ConvertPrename();
               
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


        private void CheckInsert() 
        {
            str = "select * from mbreqchgaddress " +
                " where card_personn ='" + m_txtID.Text.Replace("-", "") + "' AND  app_status = '0'"; 
           

            DataTable dt = ClsMST.SelectQuery(str);

            if (dt.Rows.Count >= 0)
            {
                return;
            }

            lb_Capp_Status.Text = dt.Rows[0]["app_status"].ToString();

        }

        private void CheckData()

        {

           // bool match = true;

          

            if (label2.Text != "")  // ตรวจไม่พบ ยังมี dialog ค้างอยู่
            {
                //MessageBox.Show("555");
               return;
            }

            DataTable dt = ClsMST.SelectQuery(str);
            if (dt.Rows.Count >= 0) 
            {
                string Tname = m_txtFullNameT.Text.Replace(" ", "");
                //string Tname = lb_PrenameInbut.Text + lb_nameInBut.Text + lb_surnameInBut.Text;
                string lbName = lbMb_name.Text.Replace(" ", "");
                string address = m_txtAddress.Text.Replace(" ", "");
                string lbAddress = lbMb_Add.Text.Replace(" ", "");
                if (address != lbAddress && Tname == lbName)
                {
                    string message = "ที่อยู่ของสมาชิกไม่ตรงกับข้อมูลที่สหกรณ์มีอยู่ กรุณาติดต่อเจ้าหน้าที่";
                    label2.Text = message;
                   // add_cross.Visible = true;
                    add_check.Visible = true;
                    ID_check.Visible = true;
                    Tname_check.Visible = true;
                    Ename_check.Visible = true;
                    birth_check.Visible = true;
                    birth_check2.Visible = true;
                    tel_chexk.Visible = true;
                    add_cross2.Visible = true;
                    mbno_check.Visible = true;
                    crrAdd_cross.Visible = true;
                    Tname_check2.Visible = true;


                   

                    MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    //if (MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    //{
                    //    return;
                    //}


                }

               // add_check.Visible = true;
               // add_check2.Visible = true;
              //  MessageBox.Show("ข้อมูลที่อยู่ถูกต้อง","successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (Tname != lbName && address == lbAddress)
                {
                    //MessageBox.Show(Tname);
                    string message2 = "ชื่อ-นามสกุลไม่ตรงกับข้อมูลสหกรณ์ กรุณาติดต่อเจ้าหน้าที่";
                    //string message2 = Tname;
                    label2.Text = message2;
                    // add_cross.Visible = true;
                    Tname_cross2.Visible = true;
                    add_check.Visible = true;
                    ID_check.Visible = true;
                    Tname_check.Visible = true;
                    Ename_check.Visible = true;
                    birth_check.Visible = true;
                    birth_check2.Visible = true;
                    tel_chexk.Visible = true;
                    mbno_check.Visible = true;
                    crrAdd_check.Visible = true;
                    add_check2.Visible = true;

                  

                    MessageBox.Show(message2, "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    //if (MessageBox.Show(message2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    //{
                    //    return;
                    //}
                }

                 if (Tname == lbName && address == lbAddress)
                 {
                   
                     string message3 = "ข้อมูลของท่านถูกต้อง";
                     label2.Text = message3;                   
                     add_check.Visible = true;
                     add_check2.Visible = true;
                     crrAdd_check.Visible = true;
                     ID_check.Visible = true;
                     Tname_check.Visible = true;
                     Tname_check2.Visible = true;
                     Ename_check.Visible = true;
                     birth_check.Visible = true;
                     birth_check2.Visible = true;
                     tel_chexk.Visible = true;
                     mbno_check.Visible = true;

                   

                     MessageBox.Show(message3, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 }

                 if (Tname != lbName && address != lbAddress)
                 {
                     string message4 = "ชื่อ-นามสกุล และ ที่อยู่ของท่านไม่ตรงกับข้อมูลของสหกรณ์";
                     //string message4 = Tname;
                     label2.Text = message4;
                     // add_cross.Visible = true;
                     Tname_cross2.Visible = true;
                     add_check.Visible = true;
                     ID_check.Visible = true;
                     Tname_check.Visible = true;
                     Ename_check.Visible = true;
                     birth_check.Visible = true;
                     birth_check2.Visible = true;
                     tel_chexk.Visible = true;
                     add_cross2.Visible = true;
                     mbno_check.Visible = true;
                     crrAdd_cross.Visible = true;


                     MessageBox.Show(message4, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                 }

                

                 
            }

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
            lb_BirthdateC.Text = "";
            lb_Tel.Text = "";
            lb_Errortext.Text = "";
            label2.Text = "";
            add_check.Visible = false;
            ID_check.Visible = false;
            Tname_check.Visible = false;
            Ename_check.Visible = false;
            birth_check.Visible = false;
            birth_check2.Visible = false;
            tel_chexk.Visible = false;
            add_cross2.Visible = false;
            Tname_cross2.Visible = false;
            mbno_check.Visible = false;
            add_check2.Visible = false;
            crrAdd_cross.Visible = false;
            Tname_check2.Visible = false;
            crrAdd_check.Visible = false;
            crrBirth_cross.Visible = false;

                        

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
            ShowData();
            timer_AutoRead.Enabled = true;
            //ReadCard();
           // MessageBox.Show("555");
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

       

        private void Frm_UpdateData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("ท่านต้องการปิดโปรแกรมนี้ หรือไม่ ","ปิดโปรแกรม",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lbMb_Add_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            CheckInsert();

            if (lb_Capp_Status.Text == "0") 
            {
                string message = "ท่านเปลี่ยนแปลงข้อมูลเรียบร้อยแล้ว ท่านต้องการอัปเดตข้อมูลหรือไม่";
                lb_Errortext.Text = message;

                if (MessageBox.Show(message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string value = lb_BirthdateC.Text;
                    lb_BirthdateC2.Text = Fc.GetshotDate(value, 1);
                    UpdateData();
                    MessageBox.Show("อัปเดตข้อมูลของท่านเรียบร้อย");
                    return;
                };
                return;
            }

            lb_App_Date.Text = DateTime.Now.ToString("MM/dd/yyyy");
            lb_Entry_Date.Text = DateTime.Now.ToString("MM/dd/yyyy ");
            lb_Appl_Docno.Text = DateTime.Now.ToString("MdyyHm", CultureInfo.InvariantCulture);

            string phoneNumber = lb_Tel.Text; // Get the phone number from TextBox1

            bool isValid = ValidateThaiPhoneNumber(phoneNumber);

            if (isValid)
            {
                //MessageBox.Show("Valid Thai phone number");
                tel_chexk.Visible = true;
                crrTel_cross.Visible = false;
            }
            else
            {
                tel_chexk.Visible = false;
                crrTel_cross.Visible = true;
                MessageBox.Show("กรุณาระบุหมายเลขโทรศัพท์ให้ถูกต้อง");
            }

          

            if (lb_BirthdateC2.Text == "")
            {
                birth_check2.Visible = false;
                crrBirth_cross.Visible = true;
                MessageBox.Show("กรุณาระบุ วัน/เดือน/ปี เกิดให้ถูกต้อง");
            }
            else if (lb_BirthdateC2.Text != "")
            {
                birth_check2.Visible = true;
                crrBirth_cross.Visible = false;
            }

            SaveData();

        }

       

        private void lb_BirthdateC_Click(object sender, EventArgs e)
        {
            timer_AutoRead.Enabled = false;
        }

        private void lb_Tel_Click(object sender, EventArgs e)
        {
            timer_AutoRead.Enabled = false;
        }

        private void btn_chk_Click(object sender, EventArgs e)
        {
           // CheckInsert();
           // UpdateData();
            string value = lb_BirthdateC.Text;
            lb_BirthdateC2.Text = Fc.GetshotDate(value, 1);
            UpdateData();
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

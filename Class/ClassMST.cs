using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace SVMember
{
    class ClassMST
    {

        public string str, sql, Str_Connection;
        OracleCommand Ora_Save = new OracleCommand();
        OracleConnection Ora_Connection = new OracleConnection();
        OracleDataAdapter Ora_Da = new OracleDataAdapter();


        public SqlConnection MSsql_Connection = new SqlConnection();
        public SqlDataAdapter MSsql_Da = new SqlDataAdapter();
        public SqlCommand MSsql_Save = new SqlCommand();

        DataTable dt = new DataTable();
        allFunction Fc = new allFunction();

        static INIFile inif;
        string path = Application.StartupPath + @"\config.ini";
        char[] sp = { '|' };
        public string Get_DocControl(string Datatype,string Datatype2)
        {
            string xRe="";
             
            return xRe;
        
        }
        public string[] Get_MMname(Int32 MM)
        {
            string[] xRe = new string[2];
        switch (MM)
        {
            case 1: xRe[0] = "มกราคม"; xRe[1] = "1"; break;
            case 2: xRe[0] = "กุมภาพันธ์"; xRe[1] = "2"; break;
            case 3: xRe[0] = "มีนาคม"; xRe[1] = "3"; break;
            case 4: xRe[0] = "เมษายน"; xRe[1] = "4"; break;
            case 5: xRe[0] = "พฤษภาคม"; xRe[1] = "5"; break;
            case 6: xRe[0] = "มิถุนายน"; xRe[1] = "6"; break;
            case 7: xRe[0] = "กรกฎาคม"; xRe[1] = "7"; break;
            case 8: xRe[0] = "สิงหาคม"; xRe[1] = "8"; break;
            case 9: xRe[0] = "กันยายน"; xRe[1] = "9"; break;
            case 10: xRe[0] = "ตุลาคม"; xRe[1] = "10"; break;
            case 11: xRe[0] = "พฤศจิกายน"; xRe[1] = "11"; break;
            case 12: xRe[0] = "ธันวาคม"; xRe[1] = "12"; break;
                
                
            default: break;
        }
        return xRe;
        }
        public int IsValidEmail(string email)
        {
            int xRe = 0;
          
    try {
        var addr = new System.Net.Mail.MailAddress(email);        
           }
    catch {xRe = 1;}

    return xRe;
        }
        public string AutoCode_Format(string nb)
        {
            string xReturn = "";
            switch (nb.ToString().Length)
            {
                case 1: xReturn = "000" + nb.ToString(); break;
                case 2: xReturn = "00" + nb.ToString(); break;
                case 3: xReturn = "0" + nb.ToString(); break;
                default:
                    xReturn = nb; break;
            }
            return xReturn;
        }


        public void UPdate_LNmst_Payan(string ReqNo)
        {
            sql = " SELECT     *  FROM         LN_Structure_Walfare where Re_LN_status=1 and LNReq_no='"+ ReqNo +"'";
           // sql = " SELECT     *  FROM         LN_Structure_Walfare where  LNReq_no='" + ReqNo + "'";
            DataTable dt = SelectQuery_MsSql(sql);

            if (dt.Rows.Count<=0){return;}

            sql = "update LN_Structure_MST set payan='" + dt.Rows[0]["WT_name"].ToString() + "',PayanSync='" + dt.Rows[0]["WT_Descripts"].ToString() + "'" +
                      " where LNReq_no='"+ ReqNo +"'";

            Save_MsSqlDB(sql);



            

        
        }
        

        public void Update_LN_Structure_LNdet(string Member_no)
        {

            sql = "SELECT     member_no, Loancontract_no, principal_balance FROM         LNCONTMASTER" +
                     " WHERE     member_no = '"+ Member_no +"'";

            DataTable dt = SelectQuery_MsSql(sql);

            if (dt.Rows.Count<=0){return;}

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "Update  LN_Structure_LNdet set principal_balance=" + dt.Rows[i]["principal_balance"].ToString() + " where Loancontract_no='" + dt.Rows[i]["Loancontract_no"].ToString() + "' and member_no='" + Member_no + "'";
                Save_MsSqlDB(sql);
            }

        }

        public string UpdateLast_STid(Int32 LstID)
        {
          
            
            string[] _txt_LstID =  Get_LastID().Split(sp);
            Int32 _LastID =Convert.ToInt32( _txt_LstID[0].ToString());
            _LastID = _LastID + LstID;

            sql = "update AutoRun_LstID set LastID=" + _LastID + " where xYear='" + _txt_LstID[1].ToString() + "'";
            Save_MsSqlDB(sql);

            return sql;
            
            
    }
       public string   Get_LastID()
        {
            string xReturn = "";
            sql = "select * from AutoRun_LstID order by xYear desc";
            DataTable dt = SelectQuery_MsSql(sql);

            if (dt.Rows.Count >0) { xReturn = dt.Rows[0]["LastID"].ToString() + "|" + dt.Rows[0]["xYear"].ToString(); }
                       
           
           return xReturn;
        }

        public string Get_LastID_DocControl(string DataType, string subDataType)
        {

            string xReturn = "";

            sql = "select  * from sysDocControl_Method Where DataType='"+ DataType +"' and subDataType='"+ subDataType +"'";
            DataTable dt = SelectQuery_MsSql(sql);           
            if (dt.Rows.Count > 0)
            {
                Int32 LstCode = Convert.ToInt32(dt.Rows[0]["LastID"].ToString()) + 1;
                xReturn = dt.Rows[0]["xYear"].ToString().Substring(2, 2) + AutoCode_Format(LstCode.ToString("00#"));
            }
            return xReturn;



        }
        public void Update_LastID_DocControl(string DataType, string subDataType)
        { 

        sql = "update  sysDocControl_Method set LastID=LastID+1 where DataType='" + DataType + "' and SubDataType='"+ subDataType +"' ";
        Save_MsSqlDB(sql);
             
        }

        public string[] RPT_Config(string TypeClick)
        {
            string[] Rpt = new string[4];
            Rpt[0] = "sa";
            Rpt[1] = "lptcoop";
            Rpt[2] = @"192.168.7.101\WEB_LPTCOOP";
            // Rpt[2] = @"adminesso\WEB_LPTCOOP";
            //     Rpt[2] = @"ADMINESSO";
            //Rpt[3] = TypeClick.ToUpper();

            switch (TypeClick.ToUpper())
            {
                case "MENU": Rpt[3] = "ADMINESSO"; break;
                case "DATA": Rpt[3] = "ESSO"; break;
                case "LOCAL": Rpt[3] = "ESSO"; break;
                case "E-POST": Rpt[3] = "E-POST"; break;

                case "ORACLE":
                    Rpt[0] = "SCOTDAT";
                    Rpt[1] = "tch_lampang";
                    Rpt[2] = "tch_lampang";
                    break;
                default:
                    break;
            }

            return Rpt;
        }
        public void GetDB_str_Registry(string TypeClick)
        {
            // Str_Connection = @"server='Adminesso'; UID=sa;PWD=lptcoop; Database='ASSETMST';timeout=120;pooling=false";
            string[] Conn_Cfg = RPT_Config(TypeClick);
            Str_Connection = @"server='" + Conn_Cfg[2] + "'; UID=" + Conn_Cfg[0] + ";PWD=" + Conn_Cfg[1] + "; Database='" + Conn_Cfg[3] + "';timeout=120;pooling=false";
            
            try
            {
                MSsql_Connection = new SqlConnection(Str_Connection);
                MSsql_Connection.Open();
            }
            catch (Exception ex)
            {
                return;
            }

        }

       
        public string Get_IDcardByMBno(string MBno)
        {
            string xRe = "";
            str = "select CARD_PERSON,member_groupAMPER from member_info where member_no='" + MBno + "'";
            DataTable dt = SelectQuery_MsSql(str);
            if (dt.Rows.Count>0)
            {
                xRe = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();

            }


            return xRe;

        }
        public string Get_LNMBref_RefDate(string Member_No, string Member_NoRef, string LOAN_Code)
        {
            string xRe = "";

            sql = " SELECT * FROM " +
                      " (select  lnreqcollchg.Collchgreq_Date  from lnreqcollchg  inner join  lnreqcollchgdet on lnreqcollchg.collchgreq_docno= lnreqcollchgdet.collchgreq_docno where lnreqcollchg.member_no='" + Member_No + "' and lnreqcollchg.Loancontract_no='" + LOAN_Code + "' and lnreqcollchgdet.newcoll_refno='" + Member_NoRef + "' order by lnreqcollchg.Collchgreq_Date) " +
                      " WHERE rownum = 1";
            //sql = "select top 1 Collchgreq_date from lnreqcollchg where member_no='" + Member_Ref + "' and Loancontract_no='" + LOAN_Code + "' order by Collchgreq_date  ";
            DataTable dt = SelectQuery_ORA(sql);
            if (dt.Rows.Count > 0)
            {
                xRe = Fc.GetshotDate(dt.Rows[0][0].ToString(), 155);
            }


            return xRe;
        }

        public Int32 GET_UserPermission(string UserName)
        {
            //GetDB_str_RegistryADM();
            string sql = "select * from Method_USERApp where Username='" + UserName + "'";
            //string[] Conn_Cfg = RPT_Config();
            dt = new DataTable();
            dt = SelectQuery_MsSql(sql);

            return dt.Rows.Count;

        }
    

        //public void GetDB_str_RegistryADM()
        //{
        //    //string[] Conn_Cfg = RPT_ConfigUSER();
        //    Str_Connection = @"server='" + Conn_Cfg[2] + "'; UID=" + Conn_Cfg[0] + ";PWD=" + Conn_Cfg[1] + "; Database='" + Conn_Cfg[3] + "';timeout=120;pooling=false";

        //    try
        //    {
        //        MSsql_Connection = new SqlConnection(Str_Connection);
        //        MSsql_Connection.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        return;
        //    }

        //}

        public String GetDb()
    {
        string Strconn = "";

        try
        {
            //  string str = "Data Source=scotdat;User Id=tch_lampang;Password=tch_lampang;Pooling=False;";

            //string str = "Data Source=192.168.7.221/gcoop;Persist Security Info=True;User ID=iscolap;Password=iscolap;Unicode=True;";

            Strconn = "Data Source=192.168.7.221/gcoop;Persist Security Info=True;User ID=iscotest11;Password=iscotest11;";

            Ora_Connection = new OracleConnection(Strconn);
            
            Ora_Connection.Open();
        }
        catch (Exception ex)
        {

            //Form1.msg = "ผิดพลาด !! Connect ฐานข้อมูล Oracle  ไม่ได้";
         //   MessageBox.Show("เกิดปัญหาแล้วพี่น้อง " + ex.ToString());
           // return;
        }

        return Strconn;


        //if (Session["Username"].ToString() == "")
        //{
       // //    Response.Redirect("");
       //// if (db == null)
       // {
       //     db = "172.16.13.127";
       // }

        //switch (db.ToUpper())
        //{
        //    case "LOCAL":
        //        //  Strconn = "server='comcenter13'; UID='sa';PWD='lphpdc'; Database='OlderPerson';timeout=120;pooling=false";
        //        Strconn = "server='172.16.0.3'; UID='sa';PWD='lphpdc'; Database='OlderPerson';timeout=120;pooling=false";
        //        break;
        //    case "BLADE5":
        //        Strconn = "Server='172.16.13.127';Database=SSBHospital;UID=sa;PWD=lphpdc";
        //        break;
        //    case "FRONT-OFFICE":
        //        Strconn = "Server='172.16.13.84';Database=SSBHospital;UID=sa;PWD=lphpdc";
        //        break;
        //    case "PAYROLL":
        //        Strconn = "Server='172.16.13.127';Database=SSBPayroll;UID=sa;PWD=lphpdc";
        //        break;

        //    default:
        //        break;
        //}

    //string  Strconn = @"server='SCOSERVER_BACK\WEB_LPTCOOP'; UID='sa';PWD='lptcoop'; Database='Ecoop';timeout=120;pooling=false";

    //    string Strconn = @"server='adminesso'; UID='sa';PWD='lptcoop'; Database='Ecoop';timeout=120;pooling=false";

    //   // string Strconn = "server='192.168.2.200/lptcoop'; UID='sa';PWD='lptcoop'; Database='Ecoop';";
    //try
    //{
    //    _conn = new SqlConnection(Strconn);
    //    _conn.Open();
    //}
    //catch (Exception ex)
    //{
    //    _conn.Close();
    //    Msgbox.Show(ex.ToString());
    //}
        //return Strconn;
    }



        public void GetDB_Oracle()
        {
            try
            {
                //  string str = "Data Source=scotdat;User Id=tch_lampang;Password=tch_lampang;Pooling=False;";

                //string str = "Data Source=192.168.7.191/gcoop;Persist Security Info=True;User ID=iscolap;Password=iscolap;Unicode=True;";
                string str = "Data Source=192.168.7.191/gcoop;Persist Security Info=True;User ID=iscolaptest;Password=iscolaptest;Unicode=True;";

                Ora_Connection = new OracleConnection(str);
                Ora_Connection.Open();
            }
            catch (Exception ex)
            {

                //Form1.msg = "ผิดพลาด !! Connect ฐานข้อมูล Oracle  ไม่ได้";
                MessageBox.Show("เกิดปัญหาแล้วพี่น้อง " + ex.ToString());
                return;
            }

        }
        public DataTable SelectQuery_ORA(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                Ora_Da = new OracleDataAdapter(str, Ora_Connection);
                Ora_Da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Form1.msg = "ผิดพลาด !! Query Class ฐานข้อมูล Oracle  ไม่ได้";
                MessageBox.Show("เกิดปัญหาแล้วพี่น้อง Query " + ex.ToString());
            }

            return dt;

        }
        public string Get_HunAMT(string Member_no)
        {
            string xReturn = "";
            string sql = "SELECT SHSHAREMASTER.SHARESTK_AMT, SHSHAREMASTER.sharetype_code,SHSHARETYPE.SHARE_VALUE,SHSHAREMASTER.Rkeep_Sharevalue  FROM SHSHARETYPE,SHSHAREMASTER" +
                                  " WHERE (SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE) and SHSHAREMASTER.MEMBER_NO = '" + Member_no + "'";

            DataTable dt = SelectQuery_ORA(sql);
            if (dt.Rows.Count > 0)
            {
                xReturn = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString() + "|" + dt.Rows[0][2].ToString() + "|" + dt.Rows[0][3].ToString();
            }
            return xReturn;
        }

        public string Get_memberInfo(string MB)
        {
            string xRe = "";

            string sql = "select * from member_info where member_no='" + MB + "'";
            DataTable dt = SelectQuery_MsSql(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Member_name"].ToString();
            }
            return xRe;
        }


        public void Show_Bynary_Report(string Barcode_LPT)
        {
            str = "select xFile from Letter_det  where Barcode_lpt='" + Barcode_LPT + "'";

            //DataSet ds = Cndb.SelectQuery(str);

            //DataTable dt = new DataTable();
            //   MessageBox.Show(ds.Tables["show1"].Rows.Count.ToString());

            DataSet ds = SelectQuery_MsSqlDs(str);
              try
            {

                byte[] stream = (byte[])ds.Tables[0].Rows[0][0];
                //byte[] stream = (byte[])dt.Rows[4][0];
                 string xFile=@"C:\\Temps\"+Barcode_LPT+".pdf";
                File.WriteAllBytes( xFile , stream);
           
         

             //   System.Diagnostics.Process.Start(xFile);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void Save_Bynary_Report(string Barcode_LPT, string Path)
        {
            //Image IMG = Bitmap.FromFile(Path);
            //  Image newIMG = Resize_Image(IMG);
            try
            {
                string newFile = Path + "\\";
                if (!Directory.Exists(newFile))
                {
                    Directory.CreateDirectory(newFile);
                }
                //newFile += DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";
                //File.Create(tmp);
                //ResizeImage(Path, newFile, 300, 300, false);

                newFile += Barcode_LPT + ".pdf";

                string sql = "update LETTER_det set xFile =@img where Barcode_lpt='" + Barcode_LPT + "'";
                MSsql_Save = new SqlCommand(sql, MSsql_Connection);

                byte[] stream = File.ReadAllBytes(@"" + newFile + "");

                if (stream.Length > 0)
                {
                    MSsql_Save.Parameters.AddWithValue("@img", stream);
                    int result = MSsql_Save.ExecuteNonQuery();
                    //  if (result > 0) MessageBox.Show("insert done");
                }
            }
            catch (Exception)
            {

                return;
            }

        }

        
        public void Save_Bynary_Report2(string Member_no,string Subject, string Barcode_Post,  string PathFile)
        {
            //Image IMG = Bitmap.FromFile(Path);
            //  Image newIMG = Resize_Image(IMG);
            //try
            //{
                string newFile = PathFile;
                if (!File.Exists(newFile))
                {
                    return;
                }
                //if (!Directory.Exists(newFile))
               // {
                 //   Directory.CreateDirectory(newFile);
                //}
                //newFile += DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";
                //File.Create(tmp);
                //ResizeImage(Path, newFile, 300, 300, false);

             //   newFile += Barcode_LPT + ".pdf";

                

                 string sql = "update LETTER_det  set xFile =@img where member_no='"+ Member_no +"' and Barcode_lpt='" + Subject + "' and BarCode_POST='"+ Barcode_Post +"'";
                 SqlCommand         _Save = new SqlCommand(sql, MSsql_Connection);


                byte[] stream = File.ReadAllBytes("" + newFile + "");
                if (stream.Length > 0)
                {
                    _Save.Parameters.AddWithValue("@img", stream);
                    int result = _Save.ExecuteNonQuery();
                    //  if (result > 0) MessageBox.Show("insert done");
                }

          


                
            //}
            //catch (Exception)
            //{

            //    return;
            //}

        



            







        }


































        public void GetDB_MsSql()
        {
            try
            {
                //string str = @"server='SCOSERVER_BACK\WEB_LPTCOOP'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false"; ;
                string str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false"; ;
                // string str = @"server='adminesso'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false"; ;
                MSsql_Connection = new SqlConnection(str);
                MSsql_Connection.Open();
            }
            catch (Exception ex)
            {
                //   Form1.msg = "ผิดพลาด !! Connect ฐานข้อมูล Oracle  ไม่ได้";
                //   MessageBox.Show("เกิดปัญหาแล้วพี่น้อง " + ex.ToString() );
                return;
            }

        }

        public void GetDB_MsSql(string SV)
        {
            try
            {
                string str;
                //string str = @"server='192.168.7.101\WEB_LPTCOOP'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false"; ;
                // string str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false"; ;

                //    string str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false";
                switch (SV.ToUpper())
                {
                    case "LOCAL":
                        //      str= @"server='adminesso'; UID=sa;PWD=lptcoop; Database='E-POST';timeout=120;pooling=false";
                        str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='ESSOmeout=120;pooling=false";
                        break;
                    case "EPOST":
                        //      str= @"server='adminesso'; UID=sa;PWD=lptcoop; Database='E-POST';timeout=120;pooling=false";
                        str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='E-POST';timeout=120;pooling=false";
                        break;

                    default:
                        str = @"server='192.168.7.101\web_lptcoop'; UID=sa;PWD=lptcoop; Database='Esso';timeout=120;pooling=false";
                        break;
                }


                MSsql_Connection = new SqlConnection(str);
                MSsql_Connection.Open();
            }
            catch (Exception ex)
            {
                //   Form1.msg = "ผิดพลาด !! Connect ฐานข้อมูล Oracle  ไม่ได้";
                //   MessageBox.Show("เกิดปัญหาแล้วพี่น้อง " + ex.ToString() );
                return;
            }

        }
        public DataTable SelectQuery(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                Ora_Da = new OracleDataAdapter(str, Ora_Connection);
                Ora_Da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Form1.msg = "ผิดพลาด !! Query Class ฐานข้อมูล Oracle  ไม่ได้";
                MessageBox.Show("เกิดปัญหาแล้วพี่น้อง Query " + ex.ToString());
            }

            return dt;

        }

         public string Get_MemberINOF_Sprit(string Member_no)
         {
             string xRe="";
             sql="select * from member_info where member_no='"+ Member_no +"'";
             dt=SelectQuery_MsSql(sql);
             if (dt.Rows.Count>0)
	            {
                   double Hun =Convert.ToDouble(dt.Rows[0]["Hun"]) * Convert.ToDouble(dt.Rows[0]["Hun_value"]);
                    clsCalAge Age = new clsCalAge(Convert.ToDateTime(dt.Rows[0]["Birth_date"].ToString()));
                    xRe = dt.Rows[0]["member_no"].ToString() + "|" + dt.Rows[0]["member_Fullname"].ToString() + "|" + dt.Rows[0]["member_groupname"].ToString() + "|" + dt.Rows[0]["MATE_NAME"].ToString() + "|" + dt.Rows[0]["CARD_PERSON"].ToString() + "|" + dt.Rows[0]["member_add"].ToString() + "|" + Age + "|" +  Hun; 
	            }


             return xRe;
    }

         public string Get_Walfare_IN(string Member_NO)
         {
             string xRe;
             sql = "select  "+
                        " case when KSS=0 then 0 else (select amt from walfare_Method where Datatype='KSS') end as KSS,  "+
                        " case when SKS=0 then 0 else (select amt from walfare_Method where Datatype='SKS') end as SKS, "+
                        " case when SSK=0 then 0 else (select amt from walfare_Method where Datatype='SSK') end as SSK, "+
                        " case when SSAK=0 then 0 else (select amt from walfare_Method where Datatype='SSAK') end as SSAK, "+
                        " case when SSCSA=0 then 0 else (select amt from walfare_Method where Datatype='SSCSA') end as SSCSA "+
                         " from member_info  where member_no='"+ Member_NO +"' order by member_no";
             dt = SelectQuery_MsSql(sql);
             if (true)
             {
                 xRe = dt.Rows[0]["KSS"].ToString() + "|" + dt.Rows[0]["SKS"].ToString() + "|" + dt.Rows[0]["SSK"].ToString() + "|" + dt.Rows[0]["SSAK"].ToString() + "|" + dt.Rows[0]["SSCSA"].ToString();
             }
             return xRe;
         
         }
        public void Get_Local_member(string[] str, string xUpdate)
        {


            string sql = "select member_no from member_info where member_no='" + str[0].ToString() + "'";
            DataTable dt = SelectQuery_MsSql(sql);
            string sqlAddfield = "", sqlAddval = "";

            if (str[21].ToString() != "") { sqlAddfield += ",Work_date"; sqlAddval = ",'" + str[21].ToString() + "'"; }


            if (dt.Rows.Count <= 0)
            {

                sql = "insert into member_info (CARD_PERSON,member_no,fname,mate_name,member_Fullname,member_name,member_group,member_groupname,member_add,add1,add2,add3,add4,zipcode,tel,mobile,member_status,Position_Desc,Member_date,Birth_date,HUN,Hun_Type,Hun_Value,Hun_PerMM,member_Fname,member_Lname,xUpdate,Resign_date,Resign_status,salary";
                sql += sqlAddfield;
                sql += ") values (";
                sql += "'" + str[22].ToString() + "','" + str[0].ToString() + "','" + str[19].ToString() + "','" + str[20].ToString() + "','" + str[1].ToString() + "','" + str[1].ToString() + "','" + str[2].ToString() + "','" + str[3].ToString() + "','" + str[4].ToString() + "'";
                sql += ",'" + str[5].ToString() + "','" + str[6].ToString() + "','" + str[7].ToString() + "','" + str[8].ToString() + "','" + str[9].ToString() + "','" + str[10].ToString() + "','" + str[11].ToString() + "','" + str[12].ToString() + "','" + str[13].ToString() + "','" + str[14].ToString() + "','" + str[15].ToString() + "'," + str[16].ToString() + ",'" + str[17].ToString() + "'," + str[18].ToString() + "," + str[27].ToString() + ",'" + str[23].ToString() + "','" + str[24].ToString() + "'";
                sql += ",'" + xUpdate + "'," + str[25].ToString() + "," + str[26].ToString() + "," + str[28].ToString() + "";

                sql += sqlAddval;
                sql += ")";
            }
            else
            {
                sql = "update member_info set  " +
                "CARD_PERSON='" + str[22].ToString() + "'" +
                ",fname='" + str[19].ToString() + "'" +
                " ,member_Fullname='" + str[1].ToString() + "'" +
                " ,member_name='" + str[1].ToString() + "'" +
                " ,mate_name='" + str[20].ToString() + "'" +
                " ,member_group='" + str[2].ToString() + "'" +
                " ,member_groupname='" + str[3].ToString() + "'" +
                ",member_add='" + str[4].ToString() + "'" +
                " ,add1='" + str[5].ToString() + "'" +
                " ,add2='" + str[6].ToString() + "'" +
                " ,add3='" + str[7].ToString() + "'" +
                " ,add4='" + str[8].ToString() + "'" +
                " ,zipcode='" + str[9].ToString() + "'" +
                " ,tel='" + str[10].ToString() + "'" +
                " ,mobile='" + str[11].ToString() + "'" +
                " ,member_status='" + str[12].ToString() + "'" +
                " ,Position_Desc='" + str[13].ToString() + "'" +
                " ,Member_date='" + str[14].ToString() + "'" +
                " ,Birth_date='" + str[15].ToString() + "'" +
                " ,HUN=" + str[16].ToString() + "" +
                " ,Hun_Type='" + str[17].ToString() + "'" +
                " ,Hun_Value=" + str[18].ToString() + "" +
                " ,Hun_PerMM= " + str[27].ToString() +
                ",member_Fname='" + str[23].ToString() + "'" +
                ",member_Lname='" + str[24].ToString() + "'" +
                ",xUpdate='" + xUpdate + "'" +
                ",Resign_status=" + str[26].ToString() + "" +
                ",Resign_date=" + str[25].ToString() + "" +
                ",Salary=" + str[28].ToString();


                if (sqlAddfield != "") { sql += sqlAddfield + "=" + sqlAddval.Replace(",", ""); }

                sql += " where member_no='" + str[0].ToString() + "'";
            }
            Save_MsSqlDB(sql);

        }




















        // เกี่ยวข้องสวัสดิการ-----------------------------------------------------------------------------------

        public double Get_WF_Cover_Amt(string DataType)
        {
            double xRe = 0;
            sql = "select amt_Cover from Walfare_Method where DataType='" + DataType + "'";
            DataTable dts = SelectQuery_MsSql(sql);
            if (dts.Rows.Count>0)
            {
                xRe =Convert.ToDouble(dts.Rows[0][0].ToString());
            }

            return xRe;
        }



        // --------------------------------------------------------------------------------------------------------





























        public DataTable SelectQuery_MsSql(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                MSsql_Da = new SqlDataAdapter(str, MSsql_Connection);
                MSsql_Da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Form1.msg = "ผิดพลาด !! Query Class ฐานข้อมูล Oracle  ไม่ได้";
                MessageBox.Show("เกิดปัญหาแล้วพี่น้อง Query " + ex.ToString());
            }

            return dt;

        }

        public DataTable SelectQuery_MsSql(string str, string MSSQLServer)
        {
            GetDB_str_Registry(MSSQLServer);
            DataTable dt = new DataTable();
            try
            {
                MSsql_Da = new SqlDataAdapter(str, MSsql_Connection);
                MSsql_Da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Form1.msg = "ผิดพลาด !! Query Class ฐานข้อมูล Oracle  ไม่ได้";
                MessageBox.Show("เกิดปัญหาแล้วพี่น้อง Query " + ex.ToString());
            }

            return dt;

        }
        public DataSet SelectQuery_MsSqlDs(string str)
        {
            DataSet Ds = new DataSet();
            try
            {
                MSsql_Da = new SqlDataAdapter(str, MSsql_Connection);
                MSsql_Da.Fill(Ds, "ds");
            }
            catch (Exception ex)
            {
                // Form1.msg = "ผิดพลาด !! Query Class ฐานข้อมูล Oracle  ไม่ได้";
                //    MessageBox.Show("เกิดปัญหาแล้วพี่น้อง Query " + ex.ToString() );                
            }

            return Ds;

        }
        public string Save_MsSqlDB(string sql)
        {
            string xReturn = "";
            try
            {
                MSsql_Save = new SqlCommand(sql, MSsql_Connection);
                MSsql_Save.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + Environment.NewLine + ex.ToString());
                xReturn = ex.ToString();

            }
            return xReturn;


        }

        public string Save_AccessDB(string sql)
        {
            string xReturn = "";
            try
            {
                MSsql_Save = new SqlCommand(sql, MSsql_Connection);
                MSsql_Save.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + Environment.NewLine + ex.ToString());
                xReturn = ex.ToString();

            }
            return xReturn;

        }


        public string Save_ORACLE(string sql)
        {
            string xReturn = "";
            try
            {

                Ora_Save = new OracleCommand(sql, Ora_Connection);
                Ora_Save.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + Environment.NewLine + ex.ToString());


            }
            return xReturn;
        }
        public string Save_ORACLE(string sql, string db)
        {
            GetDB_Oracle();
            string xReturn = "";
            try
            {

                Ora_Save = new OracleCommand(sql, Ora_Connection);
                Ora_Save.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + Environment.NewLine + ex.ToString());


            }
            return xReturn;
        }


    
        public string Save_MsSqlDB(string sql,string Server)
        {
            GetDB_str_Registry(Server);
            string xReturn = "";
            try
            {
                MSsql_Save = new SqlCommand(sql, MSsql_Connection);
                MSsql_Save.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + Environment.NewLine + ex.ToString());
                xReturn = ex.ToString();

            }
            return xReturn;


        }

        public string Get_EndOFMonth(string M)
        {
            string xReturn = "";
            switch (Convert.ToInt16(M))
            {
                case 1: xReturn = "31"; break;
                case 2:
                    xReturn = "28";
                    if (DateTime.Now.Year % 4 > 0)
                    {
                        xReturn = "29";
                    }
                    break;
                case 3: xReturn = "31"; break;
                case 4: xReturn = "30"; break;
                case 5: xReturn = "31"; break;
                case 6: xReturn = "30"; break;
                case 7: xReturn = "31"; break;
                case 8: xReturn = "31"; break;
                case 9: xReturn = "30"; break;
                case 10: xReturn = "31"; break;
                case 11: xReturn = "30"; break;
                case 12: xReturn = "31"; break;

                default:
                    break;
            }
            return xReturn;
        }
        public Boolean Chk_Action_ByCBO(string ac, string dd)
        {
            Boolean xReturn = false;

            switch (ac.ToUpper())
            {
                case "D": xReturn = true; break;
                case "W":
                    if (dd == Get_DayOfweek()) { xReturn = true; };
                    break;
                case "M":
                    if (DateTime.Now.Day.ToString() == dd) { xReturn = true; }
                    break;
                default:
                    break;
            }
            return xReturn;
        }
        public string Get_DayOfweek()
        {
            return DateTime.Now.DayOfWeek.ToString();
        }

        public static string Get_WeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return Convert.ToString(weekNum);
        }
        public string Get_Amper(string Amper, string Province)
        {
            string xReturn = "";
            string sql = "select * from mbucfdistrict  where DISTRICT_CODE='" + Amper + "' and PROVINCE_CODE='" + Province + "' ";
            DataTable dt = SelectQuery(sql);
            if (dt.Rows.Count > 0)
            {
                xReturn = " อำเภอ" + dt.Rows[0]["district_desc"].ToString();
            }
            return xReturn;
        }
        public string Get_Province(string Province)
        {
            string xReturn = "";
            string sql = "select * from mbucfprovince  where  PROVINCE_CODE='" + Province + "' ";
            DataTable dt = SelectQuery(sql);
            if (dt.Rows.Count > 0)
            {
                xReturn = " จังหวัด" + dt.Rows[0]["Province_desc"].ToString();
            }
            return xReturn;
        }
        public string Get_FirstColumn_name_Datatable(DataTable dt)
        {
            string txtColumn = "";
            foreach (DataColumn col in dt.Columns)
            {
                txtColumn += "|" + col.ColumnName.ToString();
            }
            char[] st = { '|' };
            string[] Col_name = txtColumn.ToString().Split(st); // เก็บ Array  ไว้ แบ่ง Array โดยตัวอักษร  |
            return Col_name[1].ToString();

        }
        public DataTable Get_Member_GroupByCode(string code)
        {
            sql = "select MEMBGROUP_CODE,MEMBGROUP_CODE+' | '+ MEMBGROUP_name as mbGname from Member_Group where MEMBGROUP_CODE='" + code + "'";
            DataTable dt = SelectQuery_MsSql(sql);
            return dt;
        }

        public string Read_INFConfig(string  Conf)
        {
            // Read  INI  config  ไฟล์            
            //string fileName = @"\config\config.ini";
            string xReturn="";
            string filename = Application.StartupPath + @"\config\config.ini";
            string path = filename;
            

            inif = new INIFile(path);
            switch (Conf.ToUpper())
            {
                case "IPDATABASE": xReturn = inif.Read("ipdatabase", "ip"); break;
                default:
                    break;
            }

            // สั่ง Start Programe 
            return xReturn;




        }

        public string Get_Member_Format(string Mb)
        {
            string xReturn = "";
            try
            {
                Mb = Convert.ToInt32(Mb).ToString();
                switch (Mb.Length)
                {
                    case 1: xReturn = "0000000"; break;
                    case 2: xReturn = "000000"; break;
                    case 3: xReturn = "00000"; break;
                    case 4: xReturn = "0000"; break;
                    case 5: xReturn = "000"; break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Int16 Lng = Convert.ToInt16(Mb.Length);
                string[] S = new string[Lng];
                for (int i = 0; i < Mb.Length; i++)
                {
                    S[i] = Mb.Substring(i, 1);
                    if (S[i].ToString() == "ส")
                    {
                        Mb = Mb.Substring(i, Mb.Length - i);
                        break;
                    }
                }
            }
            return xReturn + Mb;
        }
           
            
       public string Get_Member_FormatORACLE(string Mb)
        {
            string xReturn = "";
            


            try
            {
                Mb = Convert.ToInt32(Mb).ToString();
                switch (Mb.Length)
                {
                    case 1: xReturn = "0000000"; break;
                    case 2: xReturn = "000000"; break;
                    case 3: xReturn = "00000"; break;
                    case 4: xReturn = "0000"; break;
                    case 5: xReturn = "00"; break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Int16 Lng = Convert.ToInt16(Mb.Length);
                string[] S = new string[Lng];
                for (int i = 0; i < Mb.Length; i++)
                {
                    S[i] = Mb.Substring(i, 1);
                    if (S[i].ToString() == "ส")
                    {
                        Mb = "00" + Mb.Substring(i, Mb.Length - i);
                        break;
                    }
                }
                
            }

            return xReturn + Mb;
        }
        public void Get_Local_member(string[] str)
        {

            //            ('020053',
            //'นางสาวพัทธนันท์     นพคุณ',
            //'04010101',
            //'สพม.35',
            //' เลขที่ 40 ตำบลทุ่งกว๋าว อำเภอเมืองปาน จังหวัดลำปาง',
            //' เลขที่ 40',
            //' ตำบลทุ่งกว๋าว',
            //' อำเภอเมืองปาน',
            //' จังหวัดลำปาง',
            //'52240',
            //'',
            //'',
            //'1',
            //'เจ้าพนักงานธุรการ',
            //'03/01/2013',
            //'07/30/1980')
            //            member_no	nvarchar(50)	Checked
            //member_name	nvarchar(200)	Checked
            //member_group	nvarchar(200)	Checked
            //member_groupname	nvarchar(1000)	Checked
            //member_add	nvarchar(4000)	Checked
            //add1	nvarchar(4000)	Checked
            //add2	nvarchar(4000)	Checked
            //add3	nvarchar(4000)	Checked
            //add4	nvarchar(4000)	Checked
            //zipcode	nvarchar(50)	Checked
            //tel	nvarchar(50)	Checked
            //mobile	nvarchar(50)	Checked
            //member_status	nvarchar(2)	Checked
            //member_statusUse	nvarchar(50)	Checked
            //Position_Desc	nvarchar(500)	Checked
            //Member_date	datetime	Checked
            //Birth_date	datetime	Checked


            string sql = "select member_no from member_info where member_no='" + str[0].ToString() + "'";
            DataTable dt = SelectQuery_MsSql(sql);

            if (dt.Rows.Count <= 0)
            {
                sql = "insert into member_info (member_no,member_name,member_group,member_groupname,member_add,add1,add2,add3,add4,zipcode,tel,mobile,member_status,Position_Desc,Member_date,Birth_date) values('" + str[0].ToString() + "','" + str[1].ToString() + "','" + str[2].ToString() + "','" + str[3].ToString() + "','" + str[4].ToString() + "'" +
                         ",'" + str[5].ToString() + "','" + str[6].ToString() + "','" + str[7].ToString() + "','" + str[8].ToString() + "','" + str[9].ToString() + "','" + str[10].ToString() + "','" + str[11].ToString() + "','" + str[12].ToString() + "','" + str[13].ToString() + "','" + str[14].ToString() + "','" + str[15].ToString() + "'" +
                        ")";
            }
            else
            {
                sql = "update member_info set  " +
                "member_name='" + str[1].ToString() + "'" +
                " ,member_group='" + str[2].ToString() + "'" +
                " ,member_groupname='" + str[3].ToString() + "'" +
                ",member_add='" + str[4].ToString() + "'" +
                " ,add1='" + str[5].ToString() + "'" +
                " ,add2='" + str[6].ToString() + "'" +
                " ,add3='" + str[7].ToString() + "'" +
                " ,add4='" + str[8].ToString() + "'" +
                " ,zipcode='" + str[9].ToString() + "'" +
                " ,tel='" + str[10].ToString() + "'" +
                " ,mobile='" + str[11].ToString() + "'" +
                " ,member_status='" + str[12].ToString() + "'" +
                " ,Position_Desc='" + str[13].ToString() + "'" +
                " ,Member_date='" + str[14].ToString() + "'" +
                " ,Birth_date='" + str[15].ToString() + "'" +
                " where member_no='" + str[0].ToString() + "'";
            }
            Save_MsSqlDB(sql);

        }

        public void Get_DataNoPay()
        {





            //string str="select * from Keeping_Nopay where status_Pay ='N' order by member_no";
            string str = "select distinct member_no from " +
                            "(SELECT     member_no, MMyyyy " +
                            "FROM         Keeping_Nopay " +
                            "WHERE     (status_Pay = 'N') " +
                            "GROUP BY member_no, MMyyyy,status_pay " +
                            "Having status_pay ='N'" +
                            ") as MB";


            DataTable dt = SelectQuery_MsSql(str);

            if (dt.Rows.Count <= 0) { return; }




            //Cndb.Save_MsSqlDB("delete  from TmpRptKeeping_Nopay");

            string sql, chk_mbNo = "";
            Int32 chk = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (chk_mbNo != dt.Rows[i]["member_no"].ToString())
                //{
                string Mb_no = dt.Rows[i][0].ToString();//"018929";

                sql = "select  count(member_no) from " +
                         "(SELECT     member_no, MMyyyy " +
                         "FROM         Keeping_Nopay " +
                         "WHERE     (status_Pay = 'N') " +
                         "GROUP BY member_no, MMyyyy,status_pay " +
                    //   "having member_no='" + dt.Rows[i][0].ToString() + "' and status_pay ='N'" +
                     "having member_no='" + Mb_no + "' and status_pay ='N'" +
                         ") as MB";

                DataTable dts = SelectQuery_MsSql(sql);
                if (dts.Rows.Count > 0) // แสดงว่ามีค่า แล้ว Update จำนวน ตัวแลขจำนวน เดือน ที่เป็นหนี้ไปในฐาน
                {
                    sql = "update  Keeping_Nopay set MM=" + dts.Rows[0][0] + " where member_no='" + Mb_no + "'";
                    Save_MsSqlDB(sql);

                    sql = "select top 1 MailMerg_Cnt from keeping_Nopay where mailmerg_cnt >= 1 and status_pay ='N' and member_no='" + Mb_no + "' order by MMyyyy";
                    DataTable Dt = SelectQuery_MsSql(sql);
                    if (Dt.Rows.Count > 0)
                    {

                        sql = "update  Keeping_Nopay set mailmerg_cnt=" + Dt.Rows[0][0].ToString() + " where  status_pay ='N' and member_no='" + Mb_no + "' ";
                        Save_MsSqlDB(sql);


                    }

                }
            }
        }

        public string Get_PeriodALL_LoanCode(string LNcode)
        {
            string xRe = "0";
            sql = "select period_payamt from lnreqloan where  (LOANCONTRACT_NO='" + LNcode + "')";
            DataTable dt = SelectQuery_ORA(sql);
            if (dt.Rows.Count > 0)
            {
                xRe = dt.Rows[0][0].ToString();
            }
            else
            {

                sql = "SELECT Period_paymentALL FROM LNCONTMASTER_OLDBASE WHERE  (Loancontract_no = '" + LNcode + "')";
                DataTable dts = SelectQuery_MsSql(sql);
                if (dts.Rows.Count > 0)
                {
                    xRe = dts.Rows[0][0].ToString();
                }
                else
                {
                    xRe = "0";
                }

            }

            return xRe;

        }
        public void Chk_Member_GroupExist(string Str)
        {
            char[] sp = { '|' };
            string[] GP_info = Str.Split(sp);
            string gp_code = "'" + GP_info[0] + "'"; if (gp_code == "''") { gp_code = "null"; }
            string gp_name = "'" + GP_info[1] + "'"; if (gp_name == "''") { gp_name = "null"; }
            string gp_Pro = "'" + GP_info[2] + "'"; if (gp_Pro == "''") { gp_Pro = "null"; }
            string gp_Amp = "'" + GP_info[3] + "'"; if (gp_Amp == "''") { gp_Amp = "null"; };
            string gp_Tam = "'" + GP_info[4] + "'"; if (gp_Tam == "''") { gp_Tam = "null"; }
            string gp_descrip = "'" + GP_info[5] + "'"; if (gp_descrip == "''") { gp_descrip = "null"; }


            str = "select MEMBGROUP_CODE from Member_Group where MEMBGROUP_CODE='" + GP_info[0] + "'";
            DataTable dt = SelectQuery_MsSql(str);
            if (dt.Rows.Count <= 0)
            {
                sql = "insert into Member_Group values(" + gp_code + "," + gp_name + "," + gp_Pro + "," + gp_Amp + "," + gp_Tam + "," + gp_descrip + ")";
            }
            else
            {
                sql = "update Member_Group set MEMBGROUP_name=" + gp_name + ",MEMBGROUP_PROVINCE=" + gp_Pro + ",MEMBGROUP_AMPER=" + gp_Amp + ",MEMBGROUP_TAMBON=" + gp_Tam + ",MEMBGROUP_Descript=" + gp_descrip + "  where MEMBGROUP_CODE=" + gp_code + "";
            }
            Save_MsSqlDB(sql);
        }

        public void Update_Garantee(string Str, int T)
        {
            char[] sp = { '|' };
            if (T == 0)
            {
                sql = "delete from LNCONCOLL";
                Save_MsSqlDB(sql);
            }
            string[] GP_info = Str.Split(sp);

            sql = "insert into LNCONTCOLL  values ('" + GP_info[0] + "','" + GP_info[1] + "','" + GP_info[2] + "','" + GP_info[3] + "')";
            Save_MsSqlDB(sql);

        }

        public void SaveTmp_ToPost(string sql)
        {
            char[] st = { '|' };
            string[] Tmp_info = sql.ToString().Split(st); // เก็บ Array  ไว้ แบ่ง Array โดยตัวอักษร  |

            //for (int i = 0; i < Tmp_info; i++)
            //{
            sql = "insert into Tmp_Mail_toPost values ('" + Tmp_info[0] + "','" + Tmp_info[1] + "','" + Tmp_info[2] + "','" + Tmp_info[3] + "','" + Environment.MachineName.ToString() + "','" + Tmp_info[4] + "')";
            Save_MsSqlDB(sql);
            //}
        }
        public string RPT_Path(string Type)
        {
            string xPath = Application.StartupPath.ToString();
            switch (Type.ToUpper())
            {
                case "MEMBER": xPath += @"\RPT\MEMBER\"; break;
                case "MAT": xPath += @"\RPT\MAT\"; break;
                case "ASSET": xPath += @"\RPT\ASSET\"; break;
                case "LOAN": xPath += @"\RPT\LOAN\"; break;
                case "LNSTRUCTURE": xPath += @"\RPT\LNSTRUCTURE\"; break;
                case "PROCESS": xPath += @"\RPT\PROCESS\"; break;
                case "KANG": xPath += @"\RPT\KANG\"; break;

                default:
                    break;
            }
            return xPath;
        }


        public string getMB_ST_LN()
        {
            string xRe = "";
            str = "select member_no from LN_Structure_MST where Status<>-9";
            dt = new DataTable();
            dt = SelectQuery_MsSql(str);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xRe += "'" + dt.Rows[i][0].ToString() + "',";
            }
            return xRe = xRe.Substring(0, xRe.Length - 1);
        }

        public void Gen_RPT_MB(int CntDoc, string Member_no, DateTime Ddate)
        {
            //  string Barcode_Lpt ="01"
            // sql = "insert into LETTER_det (Letter_Code,Barcode_LPT,Member_no,member_Fullname)values('"+ Fc.GetshotDate(Ddate,7) +"','"++"','"+ Member_no +"','"+Get_MB_info(Member_no) +"')";

        }

        public string Get_MB_info(string MB_no)
        {
            string xRe = "";

            string sql = "select * from member_info where member_no='" + MB_no + "'";
            DataTable dt = SelectQuery_MsSql(sql);
            if (dt.Rows.Count > 0)
            {
                xRe = dt.Rows[0]["member_Fullname"].ToString();
            }
            return xRe;
        }

        public string[] Get_MB_info_sprit(string MB_no)
        {
            string[] xRe = new string[3];

            string sql = "select * from member_info where member_no='" + MB_no + "'";
            DataTable dt = SelectQuery_MsSql(sql);
            if (dt.Rows.Count > 0)
            {
                xRe[0] = dt.Rows[0]["member_no"].ToString();
                xRe[1] = dt.Rows[0]["member_Fullname"].ToString();
                xRe[2] = dt.Rows[0]["member_groupname"].ToString();
            }
            return xRe;
        }
        public string[] INFConfig_Read()
        {
            string[] Read_Config = new string[3];
            inif = new INIFile(path);

            // สั่ง Start Programe             
            Read_Config[0] = inif.Read("xUser", "user");
            Read_Config[1] = inif.Read("xUser", "userID");
            Read_Config[2] = inif.Read("xUser", "username");

            return Read_Config;

        }
        public DataTable dt_cbo(string sql, string Field)
        {
            
            char[] sp = { '|' };
            string[] xField = Field.Split(sp);

            DataSet ds = SelectQuery_MsSqlDs(sql);
            DataTable dt = ds.Tables["ds"];

            DataRow dr = dt.NewRow();
            dr[xField[0]] = "0";
            dr[xField[1]] = "!--  เลือก ---";
            dt.Rows.InsertAt(dr, 0);
            return dt;


        }
        public DataTable dt_cbo(string sql, string Field,int Fst_txt)
        {

            char[] sp = { '|' };
            string[] xField = Field.Split(sp);

            DataSet ds = SelectQuery_MsSqlDs(sql);
            DataTable dt = ds.Tables["ds"];

            DataRow dr = dt.NewRow();
            dr[xField[0]] = "0";
            if (Fst_txt == 0)
            {
                dr[xField[1]] = "!--  เลือกทั้งหมด ---";
            }
            else
            { dr[xField[1]] = "!--  เลือก ---"; }
            
            dt.Rows.InsertAt(dr, 0);
            return dt;


        }

        public DataTable dt_cbo(string sql, string Field,string DATABASE)
        {
            GetDB_MsSql("EPOST");
            
            char[] sp = { '|' };
            string[] xField = Field.Split(sp);

            DataSet ds = SelectQuery_MsSqlDs(sql);
            DataTable dt = ds.Tables["ds"];

            DataRow dr = dt.NewRow();
            dr[xField[0]] = "0";
            dr[xField[1]] = "!--  เลือก ---";
            dt.Rows.InsertAt(dr, 0);
            return dt;


        }
        public Boolean VerifyDateFormat(string txtDateTime)
        {
            DateTime fromDateValue;
            string s = txtDateTime;
            var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };

            if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean VerifyPeopleID(String pid)
        {
            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข

            ////ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
            //if (PID.Trim().Length != 13)
            //    return false;

            //int sumValue = 0;
            //for (int i = 0; i < PID.Length - 1; i++)
            //    sumValue += int.Parse(PID[i].ToString()) * (13 - i);
            //int v = 11 - (sumValue % 11);
            //return PID[12].ToString() == v.ToString();


            char[] numberChars = pid.ToCharArray();

            int total = 0;
            int mul = 13;
            int mod = 0, mod2 = 0;
            int nsub = 0;
            int numberChars12 = 0;

            for (int i = 0; i < numberChars.Length - 1; i++)
            {
                int num = 0;
                int.TryParse(numberChars[i].ToString(), out num);

                total = total + num * mul;
                mul = mul - 1;

                //Debug.Log(total + " - " + num + " - "+mul);
            }

            mod = total % 11;
            nsub = 11 - mod;
            mod2 = nsub % 10;

            //Debug.Log(mod);
            //Debug.Log(nsub);
            //Debug.Log(mod2);


            int.TryParse(numberChars[12].ToString(), out numberChars12);

            //Debug.Log(numberChars12);

            if (mod2 != numberChars12)
                return false;
            else
                return true;


        }


        public void SaveCnt_Letter(int Type, int LTcnt, string ID, string Member_no,string Dt_lastAccess,string LT_Date,string LT_DateTOPay)
        {


            sql = "update Keeping_nopay set  LastAccess ='"+ Fc.GetshotDate(DateTime.Now.ToString(),0) +"' ";
            if (Type == 1) // = 1  คนกู้
            {
                sql += ",MBBarcode_lastAccess='" + Dt_lastAccess + "', MailMerg_Date= '" + LT_Date + "',MailMerg_DateTOPay= '" + LT_DateTOPay + "', MailMerg_Cnt=" + LTcnt;
            }
            else if (Type == 2) // = 1  คนกู้
            {
                sql += ",MailMerg_CntMBRef=" + LTcnt;
            }
            sql += " where member_no='" + Member_no + "' and id=" + ID;

            Save_MsSqlDB(sql);
        //    MessageBox.Show(sql);

        }

        public string[] Get_Letter_DocType(string DocType_Code, string MSSQLServer)
        {
            
            string[] DocType_info = new string[2];
            DocType_info[0] = ""; DocType_info[1] = "";

            sql = "select * from Method_Document  where DocType_code='"+ DocType_Code +"'";
            dt = SelectQuery_MsSql(sql, MSSQLServer);
            if (dt.Rows.Count >0)
            {
                DocType_info[0] = dt.Rows[0]["DocType_code"].ToString();
                DocType_info[1] = dt.Rows[0]["DocType_name"].ToString();
            }
            return DocType_info;
        }
    }
}

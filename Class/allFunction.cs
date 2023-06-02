using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
/// <summary>
/// Summary description for allFunction
/// </summary>
public class allFunction
{

    public string GetshotDate(string xdate, int type)
    {
        string DtAdd = "";
        CultureInfo _eng_culture = new CultureInfo("en-US");
        CultureInfo _thai_culture = new CultureInfo("th-TH");
        DateTimeFormatInfo _thai_dt = _thai_culture.DateTimeFormat;
        DateTimeFormatInfo _eng_dt = _eng_culture.DateTimeFormat;
      

        string convertDate = "";

        try
        {
            DateTime FDate = DateTime.Parse(xdate);
            switch (type)
            {
                    
                case 0: //  01/01/2003
                    convertDate = FDate.ToString("MM/dd/yyyy", _eng_dt); break;
                case 1:// 1 มกราคม 2546
                    convertDate = FDate.ToString("dd/MM/yyyy", _eng_dt); break;
                case 11:// 1 มกราคม 2546
                    convertDate = FDate.ToString("MM/dd/yyyy HH:mm:ss", _eng_dt); break;
                case 111:// 1 มกราคม 2546
                    convertDate = FDate.ToString("dd/MM/yyyy  HH:mm:ss", _thai_dt); break;
                case 12:// 1 มกราคม 2546
                    convertDate = FDate.ToString("yyyyMM", _thai_dt); break;
                case 122:// 1 มกราคม 2546
                    convertDate = FDate.ToString("yyyy-MMdd", _thai_dt); break;
                case 2: // วันพูธ ที่ 1 มกราคม 2546
                    convertDate = FDate.ToString("yyyy,MM,dd,00,00,00", _eng_dt); break;
                case 22: // วันพูธ ที่ 1 มกราคม 2546
                    convertDate = FDate.Month + "/" + FDate.Day + "/" + FDate.Year; break;
                case 3:// วันพูธ ที่ 1 มกราคม 2546 12:30 น.
                    convertDate =  FDate.ToString("yyyyMMdd_HHmmss", _thai_dt); break;
                case 4:// 2003-10-1 00:00:00
                    convertDate = FDate.Day + "/" + FDate.Month + "/" + FDate.Year; break;
                case 5:// 2003-10-1 23:59:59
                    convertDate = FDate.ToString("yyyyMM",  _thai_dt); break;
                case 6:// 2003-10-1
                    convertDate = FDate.ToString("dd/MM/yyyy", _thai_dt); break;
                case 66:// 2003-10-1
                    convertDate = FDate.ToString("d  MMM  yyyy", _thai_dt); break;
                case 7:// 1/01/2546
                    convertDate = FDate.ToString("yyyy-MMdd", _thai_dt); break;
                case 77:// 1/01/2546
                    convertDate = FDate.ToString("yyyy-MM-dd",_eng_dt); break;
                case 8:// 2546-06-30
                    convertDate = FDate.ToString("MMM yyyy", _thai_dt); break;
                //((FDate.Year)+543) + "-" + FDate.Month + "-" + FDate.Day; break;
                case 9:// 1 ม.ค.  2546
                    //convertDate = FDate.ToString("d MMM yyyy",_thai_dt); break;
                    convertDate = FDate.ToString("d MMM yyyy ", _thai_dt) ; break;
                case 909:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("d MMM yyyy", _thai_dt); break;
                case 9099:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("d     MMMM     yyyy", _thai_dt); break;
                case 908:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("MMMM yyyy", _thai_dt); break;
                //convertDate = FDate.ToString("d MMM yyyy ", _thai_dt) + FDate.Hour + ":" + FDate.Minute + " น."; break;
                case 10:// 2003-10-1 23:06:12
                    convertDate = FDate.ToString("dd/MM/yyyy", _thai_dt); break;
                    //convertDate = FDate.Year + "-" + FDate.Month + "-" + FDate.Day + " " + FDate.Hour + ":" + FDate.Minute + ":" + FDate.Second; break;
                case 101:// 2003-10-1
                    convertDate = FDate.Year + "-" + FDate.Day + "-" + FDate.Month; break;
                case 1011:// 2546-06-30
                    convertDate = FDate.ToString("MM/dd/yy", _eng_dt); break;
                //case 102: //12/01/2008  23:06:12
                //    convertDate = FDate.Month + "/" + FDate.Day + "/" + FDate.Year +" "+FDate.Hour+":"+FDate.Minute+":"+FDate.Second ; break;
                case 102: //12/01/2008  23:06:12
                    //   convertDate =  (Convert.ToInt32(FDate.Year)-543) + "-" + FDate.Month + "-" + FDate.Day + " " + FDate.Hour + ":" + FDate.Minute + ":" + FDate.Second; break;
                    convertDate = FDate.Month + "/" + FDate.Day + "/" + FDate.Year + " " + FDate.Hour + ":" + FDate.Minute + ":" + FDate.Second; break;
      
                
                case 13:// 4905011312
                    convertDate = FDate.ToString("yyMMdd", _thai_dt); break;
                case 14: //20081222
                    convertDate = FDate.ToString("yyyyMMdd", _eng_dt); break;
                case 15:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("d  MMM  yyyy", _thai_dt); break;
                case 155:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("d   MMMM   yyyy", _thai_dt); break;
                case 16:// 1 ม.ค.  2546
                    convertDate = FDate.Hour + ":" + FDate.Minute + " น."; break;
                case 17:// 1 ม.ค.  2546
                    convertDate = FDate.ToString("yyyyMM ", _thai_dt) ; break;
                case 18:// 2003-10-1 23:59:59
                    convertDate = FDate.ToString("01/MM/yyyy", _thai_dt); break;
                case 19:// 042019
                    convertDate = FDate.ToString("MM/yyyy", _eng_dt); break;
                case 98:
                        convertDate = FDate.ToString("ddMMyyyy", _eng_dt); break;
                case 99:// 1 ม.ค.  2546
                    convertDate = "xxxx"; break;
                
                
                
            }
        }
        catch (Exception)
        {
            
       }
        return convertDate.ToString();
    }

   
}

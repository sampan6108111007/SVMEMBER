using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

//using CrystalDecisions.CrystalReports;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.ReportSource;
//using CrystalDecisions.Shared;


namespace SVMember
{
    public partial class Frm_MUT : Form
    {
        public Frm_MUT()
        {
            InitializeComponent();
        }
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno, RPTconfig;
        string RPTCur_Path;
        string vWhere;
        string Temp_Path = @"C:\SVMember\";

        //ReportDocument MyReport = new ReportDocument();

        DirectoryInfo Di;
        //ExportOptions CrExportOption = new ExportOptions();
        //DiskFileDestinationOptions CrDest = new DiskFileDestinationOptions();
        //PdfRtfWordFormatOptions CrFormatType = new PdfRtfWordFormatOptions();

        private void Frm_MUT_Load(object sender, EventArgs e)
        {
            MBno = this.Text.Split(sp);

            //CrExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
            //CrExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            //CrExportOption.ExportDestinationOptions = CrDest;
            //CrExportOption.ExportFormatOptions = CrFormatType;

            //CrFormatType.FirstPageNumber = 1;
            //CrFormatType.LastPageNumber = 1;
            //CrFormatType.UsePageRange = true;



            //  สร้าง  Folder  Temps เพื่อเตรียมการสร้าง Export ไฟล์จดหมาย
            Di = new DirectoryInfo(Temp_Path);
            if (Di.Exists == false) { Di.Create(); } // สร้าง Directory
            //---------------------------------
            

            
            //----------------------------------------------------------------------------------------------------------------------
            RPTCur_Path = Application.StartupPath.ToString() + @"\RPT\SVMember\";
            ClsMST.GetDB_Oracle(); // Oracle
            RPTconfig = ClsMST.RPT_Config("LOCAL");

            
            
            GenReport();
            

        }

        private void GenReport()
        {
            //vWhere = "{Member_info.member_no} = '"+ MBno[0].Substring(2,6) +"'";
            //MyReport.Load(@RPTCur_Path + "SVMemberMBWF.rpt");
            //MyReport.SetDatabaseLogon(RPTconfig[0].ToString(), RPTconfig[1].ToString());
            
              
            //CrDest.DiskFileName = Temp_Path + MBno[0] + "wf.pdf";
            //MyReport.RecordSelectionFormula = vWhere;                  
            //MyReport.Refresh();
            //crystalReportViewer1.ReportSource = MyReport;
            //MyReport.Export(CrExportOption);

            

        }
    }
}

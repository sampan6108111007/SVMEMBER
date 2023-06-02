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
    public partial class Frm_UpdateDataByOne : Form
    {
        ClassMST ClsMST = new ClassMST();
        allFunction Fc = new allFunction();
        string sql = "", msgERR = "";
        char[] sp = { '|' };
        string[] MBno;

        public Frm_UpdateDataByOne()
        {
            InitializeComponent();
        }

        private void Frm_UpdateDataByOne_Load(object sender, EventArgs e)
        {

        }
        void Get_ObjPrename()
        {
            sql = "select * from mbucfprename order by prename_code";
            DataTable dt = ClsMST.SelectQuery_ORA(sql);
            obj_prename.DataSource = dt;
            obj_prename.DisplayMember = "prename_desc";
            obj_prename.ValueMember = "prename_code";
        }

        void Get_Province()
        {
            sql = "select * from mbucfprovince order by province_desc";
            DataTable dt = ClsMST.SelectQuery_ORA(sql);
            Obj_province.DataSource = dt;
            Obj_province.DisplayMember = "province_desc";
            Obj_province.ValueMember = "province_code";
        }

    }
}

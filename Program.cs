using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SVMember
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new Form1());

            Application.Run(new Frm_Main());
            //Application.Run(new Frm_ACIDcard());
            //Application.Run(new Frm_UpdateData());
            //Application.Run(new Frm_Seminar());
        }
    }
}

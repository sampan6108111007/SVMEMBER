﻿using System;
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

           // Application.Run(new Frm_Main());
            //Application.Run(new Frm_ACIDcard());
          // Application.Run(new Frm_UpdateData());
            //Application.Run(new Frm_Approve2());
           // Application.Run(new Frm_Login());
           // Application.Run(new Frm_EmAprove());
            Application.Run(new MDI());
             //Application.Run(new MDIParent1());
            //Application.Run(new Frm_Seminar());
            //Application.Run(new Frm_ApproveData());
        }
    }
}

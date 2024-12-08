using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static wdfxekhach.Program;
using wdfxekhach.Admin;

namespace wdfxekhach
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {       
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //frKhachHang fromkh = new frmKhungHomeAdmin();
            Application.Run(new FrQuanLyVeXeKhachcs());
            //Application.Run(new Home());

        }


    }
}

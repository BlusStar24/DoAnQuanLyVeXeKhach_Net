using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static wdfxekhach.Program;

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
            frKhachHang fromkh = new frKhachHang();
            Application.Run(new FRTaiKhoanKH());
        }
        
        //public class CONNECTION
        //{
        //    public SqlConnection connection;
        //    public DataSet ds;
        //    public DataTable dt;
        //    public CONNECTION()
        //    {
        //        connection = new SqlConnection(StrDATABASE);
        //        ds = new DataSet();
        //    }
        //    public DataTable LoadHoSoKhachHang()
        //    {
        //        ds = new DataSet();
        //        if (connection.State == ConnectionState.Closed) connection.Open();
        //        string sql = "select * from KHACHHANG where MaKhachHang = '" + MaKhachHang + "'";
        //        SqlDataAdapter da = new SqlDataAdapter(sql, connection);
        //        da.Fill(ds, "KhachHang");
        //        if (connection.State == ConnectionState.Open) connection.Close();
        //        return ds.Tables["KhachHang"];
        //    }
        //}
        
    }
}

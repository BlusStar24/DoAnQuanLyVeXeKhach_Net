using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static wdfxekhach.CONNECT;
namespace wdfxekhach.Admin
{
    public partial class FrThongKe : Form
    {
       
        public FrThongKe()
        {
            InitializeComponent();
            CONNECT con = new CONNECT();
        } 
        private void FrThongKe_Load(object sender, EventArgs e)
        {
            cbx_loaithongke.Items.Add("Thống kê doanh thu");
            cbx_loaithongke.Items.Add("Thống kê số lượng vé");
            cbx_loaithongke.Items.Add("Thống kê tuyến xe phổ biến");
            cbx_loaithongke.Items.Add("Thống kê doanh thu từng nhân viên");
            cbx_loaithongke.Items.Add("Thống kê doanh thu nhân viên");
            cbx_loaithongke.Items.Add("Thống kê doanh thu chuyến");
            cbx_loaithongke.Items.Add("Thống kê doanh thu chuyến của năm");
            cbx_nam.DataSource = Enumerable.Range(2010, DateTime.Now.Year - 2009).ToList();
            SqlConnection connn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            connn.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT TenNhanVien, MaNhanVien FROM NHANVIEN", connn);
            SqlDataReader reader = cmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader);

            cbx_nhanvien.SelectedIndexChanged -= cbx_nhanvien_SelectedIndexChanged; 
            cbx_nhanvien.DataSource = dt1;
            cbx_nhanvien.DisplayMember = "TenNhanVien";
            cbx_nhanvien.ValueMember = "MaNhanVien";
            cbx_nhanvien.SelectedIndex = -1;
            cbx_nhanvien.SelectedIndexChanged += cbx_nhanvien_SelectedIndexChanged;
            connn.Close();
            
            // Nạp danh sách chuyến xe
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MaChuyenXe FROM CHUYENXE", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            cbx_chuyen.DataSource = dt;
            cbx_chuyen.DisplayMember = "MaChuyenXe";
            cbx_chuyen.ValueMember = "MaChuyenXe";
            conn.Close();
        }
        public DataTable GetThongKeDoanhThu()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThu", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                return dt;
        }
       

        public DataTable GetThongKeSoLuongVeTheoChuyenXe()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");  
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeSoLuongVeTheoChuyenXe", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }
        public DataTable GetThongKeNhanVien()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeNhanVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }
        public DataTable GetThongKeTuyenXePhoBien()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeTuyenXePhoBien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }


        private void cbx_loaithongke_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cbx_loaithongke.SelectedItem.ToString();

            DataTable dt = new DataTable();

            switch (selectedOption)
            {
                case "Thống kê doanh thu":
                    dt = GetThongKeDoanhThu();
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = false; cbx_nhanvien.Enabled = false;
                    break;

                case "Thống kê số lượng vé":
                    dt = GetThongKeSoLuongVeTheoChuyenXe();
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = false; cbx_nhanvien.Enabled = false;
                    break;

                case "Thống kê tuyến xe phổ biến":
                    dt = GetThongKeTuyenXePhoBien();
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = false; cbx_nhanvien.Enabled = false;
                    break;
                case "Thống kê doanh thu từng nhân viên":
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = false; cbx_nhanvien.Enabled = true;
                    break;
                case "Thống kê doanh thu nhân viên":
                    dt = GetThongKeNhanVien();
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = false; cbx_nhanvien.Enabled = false;
                    break;
                
                case "Thống kê doanh thu chuyến":
                    cbx_nam.Enabled = false; cbx_chuyen.Enabled = true; cbx_nhanvien.Enabled = false;
                    break;
                case "Thống kê doanh thu chuyến của năm":
                    cbx_nam.Enabled = true; cbx_chuyen.Enabled = true; cbx_nhanvien.Enabled = false; 
                    break;
                default:
                    MessageBox.Show("Loại thống kê không hợp lệ!");
                    return;
            }

            dataGridView1.DataSource = dt;
        }
        public DataTable GetThongKeDoanhThuTheoNhanVien(int maNhanVien)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThuTheoNhanVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }
        public DataTable GetDoanhThuTheoNam(int nam)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThuTheoNam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }

        public DataTable GetDoanhThuTheoChuyen(int maChuyenXe)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThuTheoChuyen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenXe", maChuyenXe);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }

        public DataTable GetDoanhThuTheoNamVaChuyen(int nam, int maChuyenXe)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThuTheoNamVaChuyen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@MaChuyenXe", maChuyenXe);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            
        }


        private void cbx_nhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_nhanvien.SelectedIndex == -1)
                return;
            else
            {
                int maNhanVien = (int)cbx_nhanvien.SelectedValue;
                DataTable dt = GetThongKeDoanhThuTheoNhanVien(maNhanVien);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thống kê cho nhân viên này.");
                    dataGridView1.DataSource = null;
                }
            }    
        }

        private void cbx_chuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbx_chuyen.SelectedValue.ToString(), out int maChuyenXe))
            {
                if (cbx_nam.SelectedItem != null && int.TryParse(cbx_nam.SelectedItem.ToString(), out int nam))
                {
                    dataGridView1.DataSource = GetDoanhThuTheoNamVaChuyen(nam, maChuyenXe);
                }
                else
                {
                    dataGridView1.DataSource = GetDoanhThuTheoChuyen(maChuyenXe);
                }
            }
        }


        private void cbx_nam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbx_nam.SelectedItem.ToString(), out int nam))
            {
                if (cbx_chuyen.SelectedItem != null && int.TryParse(cbx_chuyen.SelectedValue.ToString(), out int maChuyenXe))
                {
                    dataGridView1.DataSource = GetDoanhThuTheoNamVaChuyen(nam, maChuyenXe);
                }
                else
                {
                    dataGridView1.DataSource = GetDoanhThuTheoNam(nam);
                }
            }
        }
    }
}

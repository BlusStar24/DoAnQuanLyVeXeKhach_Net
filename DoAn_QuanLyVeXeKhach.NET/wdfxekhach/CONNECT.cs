using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static wdfxekhach.CONNECT;

namespace wdfxekhach
{
    internal class CONNECT
    {
        SqlConnection connection = new SqlConnection(("Data Source=XUANCUONG-PC;Initial Catalog=QLBanVeXeKhach_net;Integrated Security=True;"));
        public DataSet ds;
        public DataTable dt;
        public static string MaKhachHang, ChuyenDangChon, GheDangChon, SoDienThoai, mavexe;
        public static int GiaVeDangChon;
        public static int chuyendangchon;
        public static string DiemDon, DiemTra;
        public static List<string> DsGheDangChon = new List<string>();
        public static bool ThanhToanXong = false;
        public bool TimKiemHanhKhachTheoSDT(string soDienThoai)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                // Tạo truy vấn để tìm kiếm hành khách dựa vào số điện thoại
                string sql = "SELECT * FROM HANHKHACH WHERE SoDienThoai = @SoDienThoai";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) // Nếu tìm thấy hành khách
                {
                   // Đóng SqlDataReader trước khi chuyển dữ liệu
                    reader.Close();

                    // Chuyển sang form khách hàng
                    frKhachHang formKhachHang = new frKhachHang();
                    FRTaiKhoanKH frtaikhoan = new FRTaiKhoanKH();
                    // Truyền thông tin số điện thoại sang form khách hàng
                    formKhachHang.loadKhachHang(soDienThoai);
                    formKhachHang.Show();
                    frtaikhoan.Hide();
                    return true; // Đã tìm thấy hành khách
                }
                    
                
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (có thể ghi log hoặc hiển thị thông báo lỗi)
                Console.WriteLine("Lỗi khi tìm kiếm hành khách: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return false; // Không tìm thấy hành khách
        }
        
        public DataTable LoadComboboxDiemDi()
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();

            // Truy vấn chỉ lấy dữ liệu từ bảng TINHTHANH
            string sql = "SELECT MaTinhThanh, TenTinhThanh FROM TINHTHANH";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds, "DiemDi");

            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["DiemDi"];
        }


        public DataTable LoadComboboxDiemDen()
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();

            // Truy vấn chỉ lấy dữ liệu từ bảng TINHTHANH
            string sql = "SELECT MaTinhThanh, TenTinhThanh FROM TINHTHANH";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds, "DiemDen");

            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["DiemDen"];
        }

        public void goiy_ComboBoxTINHTHANH(ComboBox comboBox)
        {
            // Tạo danh sách các gợi ý
            AutoCompleteStringCollection suggestList = new AutoCompleteStringCollection();
            suggestList.AddRange(new string[] { "HaNoi", "Hồ Chí Minh", "Đà Nẵng", "Huế", "Cần Thơ", "Bà Rịa Vũng Tàu", "Bình Dương", "Đồng Tháp", "Bến Tre", "Đà Lạt" });

            // Thiết lập thuộc tính AutoComplete cho ComboBox
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox.AutoCompleteCustomSource = suggestList;
        }

        public DataTable LoadVeXe(string sql)
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds, "VeXe");
            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["VeXe"];
        }
        public DataTable LoadThongTinKhachHang(string soDienThoai)
        {
            DataTable dtKhachHang = new DataTable();
            string sql = "SELECT * FROM HANHKHACH WHERE SoDienThoai = @SoDienThoai";

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtKhachHang);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải thông tin khách hàng: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return dtKhachHang;
        }
        public DataTable LoadVeCuaToi(int i)
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();
            string sql;
            if (i < 0)
                sql = "select v.MaVeXe, case when ThoiGianXuatPhat<getdate() then N'Hết hạn' else N'Còn hạn' end 'TinhTrang', ThoiGianXuatPhat as NgayDi, TenTuyenXe, MaGhe, LoaiGhe, ThoiGianXuatPhat, DiemDon, ThoiGianDenDuKien, DiemTra, GiaTien, BienSoXe from VEXE v, CHUYENXE c, TUYENXE t, CHITIETVEXE ct, XE x, LOAIXE l where v.MaChuyenXe=c.MaChuyenXe and c.MaTuyenXe=t.MaTuyenXe and v.MaVeXe=ct.MaVeXe and c.MaXe=x.MaXe and x.MaLoaiXe=l.MaLoaiXe and MaHanhKhach='" + MaKhachHang + "' and ThoiGianXuatPhat<getdate()";
            else if (i > 0)                                                                                                                                                                                                                                                                                                                                                                                                                                            
                sql = "select v.MaVeXe, case when ThoiGianXuatPhat<getdate() then N'Hết hạn' else N'Còn hạn' end 'TinhTrang', ThoiGianXuatPhat as NgayDi, TenTuyenXe, MaGhe, LoaiGhe, ThoiGianXuatPhat, DiemDon, ThoiGianDenDuKien, DiemTra, GiaTien, BienSoXe from VEXE v, CHUYENXE c, TUYENXE t, CHITIETVEXE ct, XE x, LOAIXE l where v.MaChuyenXe=c.MaChuyenXe and c.MaTuyenXe=t.MaTuyenXe and v.MaVeXe=ct.MaVeXe and c.MaXe=x.MaXe and x.MaLoaiXe=l.MaLoaiXe and MaHanhKhach='" + MaKhachHang + "' and ThoiGianXuatPhat>getdate()";
            else                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                sql = "select v.MaVeXe, case when ThoiGianXuatPhat<getdate() then N'Hết hạn' else N'Còn hạn' end 'TinhTrang', ThoiGianXuatPhat as NgayDi, TenTuyenXe, MaGhe, LoaiGhe, ThoiGianXuatPhat, DiemDon, ThoiGianDenDuKien, DiemTra, GiaTien, BienSoXe from VEXE v, CHUYENXE c, TUYENXE t, CHITIETVEXE ct, XE x, LOAIXE l where v.MaChuyenXe=c.MaChuyenXe and c.MaTuyenXe=t.MaTuyenXe and v.MaVeXe=ct.MaVeXe and c.MaXe=x.MaXe and x.MaLoaiXe=l.MaLoaiXe and MaHanhKhach='" + MaKhachHang + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds, "VeCuaToi");
            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["VeCuaToi"];
        }

        public DataTable TimChuyenXe(string DiemDi, string DiemDen, string NgayDi)
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();
            string sql = String.Format("set dateformat dmy select MaChuyenXe, TenTuyenXe, DiemDi, DiemDen, ThoiGianXuatPhat, SoGheTrong, SucChuaXe, GiaTien, LoaiGhe from CHUYENXE, TUYENXE, XE, LOAIXE where CHUYENXE.MaTuyenXe = TUYENXE.MaTuyenXe and CHUYENXE.MaXe = XE.MaXe and XE.MaLoaiXe = LOAIXE.MaLoaiXe and DiemDi = N'{0}' and DiemDen = N'{1}' and CONVERT(date, ThoiGianXuatPhat) = CONVERT(date, '{2}', 103)", DiemDi, DiemDen, NgayDi);
          
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            MessageBox.Show(sql);

            da.Fill(ds, "TimChuyen");
            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["TimChuyen"];
        }
        public DataTable LoadChuyen()
        {
            ds = new DataSet();
            if (connection.State == ConnectionState.Closed) connection.Open();

            // Cập nhật SoGheTrong trong bảng CHUYENXE
            string sqlreset = "UPDATE CHUYENXE SET SoGheTrong = (SELECT SucChuaXe FROM LOAIXE, XE WHERE LOAIXE.MaLoaiXe = XE.MaLoaiXe AND XE.MaXe = CHUYENXE.MaXe) WHERE SoGheTrong IS NULL";
            SqlCommand cmd = new SqlCommand(sqlreset, connection);
            cmd.ExecuteNonQuery();

            // Truy vấn lấy dữ liệu từ bảng CHUYENXE và thay đổi điểm đi, điểm đến thành tên tỉnh thành
            string sql = "SET DATEFORMAT dmy; " +
                         "SELECT MaChuyenXe, TenTuyenXe, " +
                         "DiemDi = (SELECT TenTinhThanh FROM TINHTHANH WHERE MaTinhThanh = TUYENXE.DiemDi), " +
                         "DiemDen = (SELECT TenTinhThanh FROM TINHTHANH WHERE MaTinhThanh = TUYENXE.DiemDen), " +
                         "ThoiGianXuatPhat, SoGheTrong, SucChuaXe AS SucChuaVe, GiaTien, LoaiGhe " +
                         "FROM CHUYENXE " +
                         "JOIN TUYENXE ON CHUYENXE.MaTuyenXe = TUYENXE.MaTuyenXe " +
                         "JOIN XE ON CHUYENXE.MaXe = XE.MaXe " +
                         "JOIN LOAIXE ON XE.MaLoaiXe = LOAIXE.MaLoaiXe";

            // Thực hiện truy vấn và đổ dữ liệu vào DataTable
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            da.Fill(ds, "TimChuyen");
            if (connection.State == ConnectionState.Open) connection.Close();
            return ds.Tables["TimChuyen"];
        }
        public int LayGiaVeDangChon(int maChuyenXe)
        {
            int giaVeDangChon = 0;
            string connectionString = "your_connection_string_here";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT GiaTien FROM CHUYENXE WHERE MaChuyenXe = @MaChuyenXe";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaChuyenXe", maChuyenXe);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        giaVeDangChon = (int)Convert.ToDouble(result);
                    }
                }
            }
            return giaVeDangChon;
        }

        public void ChonChuyen(string Chuyen)
        {
            ChuyenDangChon = Chuyen;
            if (connection.State == ConnectionState.Closed) connection.Open();
            string sql = "select GiaTien from CHUYENXE where MaChuyenXe = '" + ChuyenDangChon + "'";
            SqlCommand cmd = new SqlCommand(sql, connection);
            GiaVeDangChon = Convert.ToInt32(cmd.ExecuteScalar());
            if (connection.State == ConnectionState.Open) connection.Close();
        }
        public List<string> DuyetSoDoGhe()
        {
            ds = new DataSet();
            List<string> ghe = new List<string>();
            if (connection.State == ConnectionState.Closed) connection.Open();
            string sql = String.Format("select MaGhe from VEXE, CHITIETVEXE where VEXE.MaVeXe=CHITIETVEXE.MaVeXe and MaChuyenXe = '{0}'", ChuyenDangChon);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ghe.Add(rdr["MaGhe"].ToString());
            }
            rdr.Close();
            if (connection.State == ConnectionState.Open) connection.Close();
            DsGheDangChon = new List<string>();
            return ghe;
        }
        public List<string> LoadXacNhanThongTinMuaVe(string soDienThoai)
        {
            List<string> thongTinVe = new List<string>();
            
            
            // Gọi InsertVeXe để chèn vé mới
            bool insertSuccess = InsertVeXe( int.Parse(MaKhachHang), chuyendangchon);

            if (!insertSuccess)
            {
                MessageBox.Show("Không thể chèn vé xe mới.");
                return thongTinVe;
            }
            else
            {
                MessageBox.Show("Đã thể chèn vé xe mới.");
            }

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                string sql = @"
                                SELECT 
                                    HK.TenHanhKhach AS HoTen,
                                    HK.SoDienThoai AS SoDienThoai,
                                    HK.Email AS Email,
                                    TX.TenTuyenXe AS TenTuyen,
                                    CONVERT(date, CX.ThoiGianXuatPhat) AS NgayDi,
                                    CONVERT(time, CX.ThoiGianXuatPhat) AS GioDi,
                                    CONVERT(time, CX.ThoiGianDenDuKien) AS GioDen,
                                    CX.GiaTien AS GiaVe
                                FROM 
                                    HANHKHACH HK
                                JOIN 
                                    VEXE VX ON HK.MaHanhKhach = VX.MaHanhKhach
                                JOIN 
                                    CHUYENXE CX ON VX.MaChuyenXe = CX.MaChuyenXe
                                JOIN 
                                    TUYENXE TX ON CX.MaTuyenXe = TX.MaTuyenXe
                                WHERE 
                                    HK.SoDienThoai = @SoDienThoai";

                SqlCommand cmd = new SqlCommand(sql, connection);
               
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    thongTinVe.Add(reader["HoTen"].ToString());
                    thongTinVe.Add(reader["SoDienThoai"].ToString());
                    thongTinVe.Add(reader["Email"].ToString());
                    thongTinVe.Add(reader["TenTuyen"].ToString());
                    thongTinVe.Add(Convert.ToDateTime(reader["NgayDi"]).ToString("dd/MM/yyyy"));
                    thongTinVe.Add(reader["GioDi"].ToString());
                    thongTinVe.Add(reader["GioDen"].ToString());
                    thongTinVe.Add(Convert.ToDouble(reader["GiaVe"]).ToString("N0") + " VND");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin vé với số điện thoại đã cung cấp.");
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải thông tin xác nhận vé: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return thongTinVe;

        }
        public bool InsertVeXe(int maHanhKhach, int maChuyenXe)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                string sqlInsertVeXe = @"
            INSERT INTO VEXE (MaHanhKhach, MaChuyenXe) 
            VALUES (@MaHanhKhach, @MaChuyenXe);";

                SqlCommand cmd = new SqlCommand(sqlInsertVeXe, connection);
                cmd.Parameters.AddWithValue("@MaHanhKhach", maHanhKhach);
                cmd.Parameters.AddWithValue("@MaChuyenXe", maChuyenXe);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Trả về true nếu chèn thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chèn vé xe: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
        }



        public void DatVe()
        {
            foreach (var i in DsGheDangChon)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string sql = @"
                INSERT INTO VEXE (MaHanhKhach, MaChuyenXe) 
                VALUES (@MaKhachHang, @ChuyenDangChon);
                
                DECLARE @MaVeXe INT = SCOPE_IDENTITY();
                
                INSERT INTO CHITIETVEXE ( MaGhe, DiemDon, DiemTra, NgayXuat) 
                VALUES ( @MaGhe, @DiemDon, @DiemTra, GETDATE());";

                    SqlCommand cmd = new SqlCommand(sql, connection);

                    // Thêm các tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
                    cmd.Parameters.AddWithValue("@ChuyenDangChon", ChuyenDangChon);
                    cmd.Parameters.AddWithValue("@MaGhe", i); // Giá trị cho MaGhe từ DsGheDangChon
                    cmd.Parameters.AddWithValue("@DiemDon", DiemDon);
                    cmd.Parameters.AddWithValue("@DiemTra", DiemTra);

                    // Thực thi câu lệnh SQL
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đặt vé: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open) connection.Close();
                }
            }
        }





    }
}

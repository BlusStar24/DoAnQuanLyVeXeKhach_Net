using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using wdfxekhach.Admin;
using static wdfxekhach.CONNECT;

namespace wdfxekhach
{
    internal class CONNECT
    {
        //SqlConnection connection = new SqlConnection(("Data Source=DESKTOP-O92NJQA;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;"));
        //SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
        //QL_BANVEXEKHACH_NETEntities db = new QL_BANVEXEKHACH_NETEntities();
        public DataSet ds;
        public DataTable dt;
        
        public static string  ChuyenDangChon, GheDangChon, SoDienThoai, mavexe, thanhtien;
        public static int GiaVeDangChon, MaKhachHang;
        public static int chuyendangchon;
        public static string DiemDon, DiemTra;
        public static List<string> DsGheDangChon = new List<string>();
        public static bool ThanhToanXong = false;
        public static int manhanvien, mavexekhach;
        private static CONNECT _instance;
        public static CONNECT Instance => _instance ?? (_instance = new CONNECT());
        public static int MaNHanVien;
        public SqlConnection connection;
        public QL_BANVEXEKHACH_NETEntities1 db;

        // Constructor private để đảm bảo không ai có thể tạo instance mới
        public CONNECT()
        {
            connection = new SqlConnection("Data Source=.;Initial Catalog=QL_BANVEXEKHACH_NET;Integrated Security=True;");
            db = new QL_BANVEXEKHACH_NETEntities1();
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public List<cbxTINHTHANH> LoadComboboxDiemDi()
        {
            var diemDi = db.TINHTHANHs
                                     .Select(tt => new cbxTINHTHANH
                                     {
                                         MaTinhThanh = tt.MaTinhThanh,
                                         TenTinhThanh = tt.TenTinhThanh
                                     })
                                     .ToList();
            return diemDi;
        }

        public List<cbxTINHTHANH> LoadComboboxDiemDen()
        {
            // Lấy danh sách điểm đến từ bảng TINHTHANH
            var diemDen = db.TINHTHANHs
                                 .Select(tt => new cbxTINHTHANH
                                 {
                                     MaTinhThanh = tt.MaTinhThanh,
                                     TenTinhThanh = tt.TenTinhThanh
                                 })
                                 .ToList();
            return diemDen;
        }


        public void goiy_ComboBoxTINHTHANH(ComboBox comboBox)
        {
            // Tạo danh sách các gợi ý
            AutoCompleteStringCollection suggestList = new AutoCompleteStringCollection();
            suggestList.AddRange(new string[] { "Hà Nội", "TP Hồ Chí Minh", "Đà Nẵng", "Huế", "Cần Thơ", "Bà Rịa Vũng Tàu", "Bình Dương", "Đồng Tháp", "Bến Tre", "Đà Lạt" });

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
        public HANHKHACH LoadThongTinKhachHang(int maHanhKhach)
        {
            try
            {
                // Tìm thông tin hành khách theo MaHanhKhach
                var khachHang = db.HANHKHACHes.FirstOrDefault(kh => kh.MaHanhKhach == maHanhKhach);

                // Trả về đối tượng hành khách
                return khachHang;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải thông tin khách hàng: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }

        public DataTable LoadVeCuaToi(int i, int maHanhKhach)
        {
            DataTable dt = new DataTable();

            string sql = @"
SELECT 
    v.MaVeXe, 
    CASE 
        WHEN c.ThoiGianXuatPhat < GETDATE() THEN N'Hết hạn' 
        ELSE N'Còn hạn' 
    END AS TinhTrang,
    c.ThoiGianXuatPhat AS NgayDi,
    t.TenTuyenXe,
    v.SoGhe AS MaGhe,
    l.LoaiGhe,
    c.ThoiGianXuatPhat,
    ct.DiemDon,
    c.ThoiGianDenDuKien,
    ct.DiemTra,
    c.GiaTien,
    x.BienSoXe,
    ct.NgayXuat -- Cột ngày xuất
FROM VEXE v
INNER JOIN CHUYENXE c ON v.MaChuyenXe = c.MaChuyenXe
INNER JOIN TUYENXE t ON c.MaTuyenXe = t.MaTuyenXe
INNER JOIN CHITIETVEXE ct ON v.MaVeXe = ct.MaVeXe
INNER JOIN XE x ON c.MaXe = x.MaXe
INNER JOIN LOAIXE l ON x.MaLoaiXe = l.MaLoaiXe
WHERE v.MaHanhKhach = @MaHanhKhach
AND (
    (@Filter < 0 AND c.ThoiGianXuatPhat < GETDATE()) OR
    (@Filter > 0 AND c.ThoiGianXuatPhat > GETDATE()) OR
    (@Filter = 0)
);
";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Filter", i);
            command.Parameters.AddWithValue("@MaHanhKhach", maHanhKhach);

            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            return dt;
        }

        public List<object> TimChuyenXe(int diemDi, int diemDen, DateTime ngayDi)
        {
            try
            {
                var result = (from cx in db.CHUYENXEs
                              join tx in db.TUYENXEs on cx.MaTuyenXe equals tx.MaTuyenXe
                              join xe in db.XEs on cx.MaXe equals xe.MaXe
                              join lx in db.LOAIXEs on xe.MaLoaiXe equals lx.MaLoaiXe
                              where tx.DiemDi == diemDi
                                    && tx.DiemDen == diemDen
                                    && DbFunctions.TruncateTime(cx.ThoiGianXuatPhat) == DbFunctions.TruncateTime(ngayDi)
                              select new
                              {
                                  cx.MaChuyenXe,
                                  TenTuyenXe = tx.TenTuyenXe,
                                  DiemDi = db.TINHTHANHs.Where(tt => tt.MaTinhThanh == tx.DiemDi).Select(tt => tt.TenTinhThanh).FirstOrDefault(),
                                  DiemDen = db.TINHTHANHs.Where(tt => tt.MaTinhThanh == tx.DiemDen).Select(tt => tt.TenTinhThanh).FirstOrDefault(),
                                  cx.ThoiGianXuatPhat,
                                  cx.SoGheTrong,
                                  SucChuaXe = lx.SucChuaXe,
                                  cx.GiaTien,
                                  lx.LoaiGhe
                              }).ToList();

                return result.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm chuyến xe: " + ex.Message);
                return null;
            }
        }

        public DataTable LoadChuyen()
        {
            DataTable dt = new DataTable();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            // Cập nhật SoGheTrong trong bảng CHUYENXE
            string sqlReset = @"
        UPDATE CHUYENXE 
        SET SoGheTrong = (
            SELECT SucChuaXe 
            FROM LOAIXE, XE 
            WHERE LOAIXE.MaLoaiXe = XE.MaLoaiXe 
            AND XE.MaXe = CHUYENXE.MaXe
        ) 
        WHERE SoGheTrong IS NULL";
            SqlCommand cmdReset = new SqlCommand(sqlReset, connection);
            cmdReset.ExecuteNonQuery();

            // Truy vấn lấy dữ liệu từ bảng CHUYENXE, TUYENXE, XE, LOAIXE và TINHTHANH
            string sqlQuery = @"
        SET DATEFORMAT dmy; 
        SELECT 
            MaChuyenXe, 
            TenTuyenXe, 
            DiemDi = (SELECT TenTinhThanh FROM TINHTHANH WHERE MaTinhThanh = TUYENXE.DiemDi), 
            DiemDen = (SELECT TenTinhThanh FROM TINHTHANH WHERE MaTinhThanh = TUYENXE.DiemDen), 
            ThoiGianXuatPhat, 
            SoGheTrong, 
            SucChuaXe AS SucChuaVe, 
            GiaTien, 
            LoaiGhe 
        FROM CHUYENXE 
        JOIN TUYENXE ON CHUYENXE.MaTuyenXe = TUYENXE.MaTuyenXe 
        JOIN XE ON CHUYENXE.MaXe = XE.MaXe 
        JOIN LOAIXE ON XE.MaLoaiXe = LOAIXE.MaLoaiXe";

            // Thực hiện truy vấn và đổ dữ liệu vào DataTable
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connection);
            da.Fill(dt);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return dt;
        }

        public int LayGiaVeDangChon(int maChuyenXe)
        {
            int giaVeDangChon = 0;
           
                connection.Open();
                string query = "SELECT GiaTien FROM CHUYENXE WHERE MaChuyenXe = @MaChuyenXe";
            SqlCommand cmd = new SqlCommand(query, connection);
                
                    cmd.Parameters.AddWithValue("@MaChuyenXe", maChuyenXe);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        giaVeDangChon = (int)Convert.ToDouble(result);
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
            string sql = String.Format("select SoGhe from VEXE, CHITIETVEXE where VEXE.MaVeXe=CHITIETVEXE.MaVeXe and MaChuyenXe = '{0}'", ChuyenDangChon);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ghe.Add(rdr["SoGhe"].ToString());
            }
            rdr.Close();
            if (connection.State == ConnectionState.Open) connection.Close();
            DsGheDangChon = new List<string>();
            return ghe;
        }
        public List<string> LayDanhSachGheDaDat(int maChuyenXe)
        {
            List<string> danhSachGhe = new List<string>();

            try
            {
                var gheDaDat = (from v in db.VEXEs
                                where v.MaChuyenXe == maChuyenXe
                                select v.SoGhe.ToString()).ToList();

                danhSachGhe.AddRange(gheDaDat);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách ghế đã đặt: " + ex.Message);
            }
            DsGheDangChon = new List<string>();
            return danhSachGhe;
        }

        public List<string> LoadXacNhanThongTinMuaVe(int mahanhk)
        {
            List<string> thongTinVe = new List<string>();
           
            try
            {
                var thongTin = (from hk in db.HANHKHACHes
                                join vx in db.VEXEs on hk.MaHanhKhach equals vx.MaHanhKhach
                                join cx in db.CHUYENXEs on vx.MaChuyenXe equals cx.MaChuyenXe
                                join tx in db.TUYENXEs on cx.MaTuyenXe equals tx.MaTuyenXe
                                where hk.MaHanhKhach == mahanhk
                                select new
                                {
                                    HoTen = hk.TenHanhKhach,
                                    SoDienThoai = hk.SoDienThoai,
                                    Email = hk.Email,
                                    TenTuyen = tx.TenTuyenXe,
                                    NgayDi = cx.ThoiGianXuatPhat,  // Giữ nguyên để lấy ngày giờ đầy đủ
                                    GioDi = cx.ThoiGianXuatPhat,  // Lấy ngày và giờ từ cơ sở dữ liệu
                                    GioDen = cx.ThoiGianDenDuKien,  // Lấy ngày và giờ từ cơ sở dữ liệu

                                    GiaVe = cx.GiaTien
                                }).FirstOrDefault();

                if (thongTin != null)
                {
                    thongTinVe.Add(thongTin.HoTen);
                    thongTinVe.Add(thongTin.SoDienThoai);
                    thongTinVe.Add(thongTin.Email);
                    thongTinVe.Add(thongTin.TenTuyen);

                    // Chuyển đổi NgayDi, GioDi và GioDen sang chuỗi sau khi truy vấn
                    if (thongTin.NgayDi.HasValue)
                    {
                        thongTinVe.Add(thongTin.NgayDi.Value.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        thongTinVe.Add("N/A");
                    }

                    thongTinVe.Add(thongTin.GioDi.HasValue ? thongTin.GioDi.Value.ToString("HH:mm") : string.Empty);
                    thongTinVe.Add(thongTin.GioDen.HasValue ? thongTin.GioDen.Value.ToString("HH:mm") : string.Empty);

                    thongTinVe.Add(thongTin.GiaVe.ToString("N0") + " VND");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy thông tin vé với số điện thoại đã cung cấp.");
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải thông tin xác nhận vé: " + ex.Message);
            }

            return thongTinVe;
        }



        public bool InsertVeXe(int maHanhKhach, int maChuyenXe)
        {
            try
            {
               
                    // Lấy mã vé xe mới
                    var maxVeXe = db.VEXEs.Max(v => (int?)v.MaVeXe) ?? 0;
                   
                    foreach (var ghe in DsGheDangChon)
                    {
                        // Tạo một vé xe mới cho mỗi ghế
                        var veXe = new VEXE
                        {
                           // Nếu cần có thể tạo mã vé khác cho mỗi ghế
                            MaHanhKhach = maHanhKhach,
                            MaChuyenXe = maChuyenXe,
                            SoGhe = int.Parse(ghe), // Ghế cho vé này
                            TrangThai = true // Trạng thái vé là true (đã đặt)
                        };

                        // Thêm vé vào bảng VEXEs
                        db.VEXEs.Add(veXe);
                        
                    }
                // Lấy các vé đã chèn thành công từ bảng VEXE
               

                // Lưu các thay đổi vào cơ sở dữ liệu
                int rowsAffected = db.SaveChanges();
                    return rowsAffected > 0;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chèn vé xe: " + ex.Message);
                return false;
            }
        }





        public void DatVe(int maHanhKhach, int maChuyenXe)
        {
            try
            {
                // Kiểm tra nếu vé đã tồn tại với trạng thái đã đặt và không phải vé của hành khách này
                var existingVeXe = db.VEXEs
                    .FirstOrDefault(v => v.MaHanhKhach == maHanhKhach && v.MaChuyenXe == maChuyenXe && v.TrangThai == true); // TrangThai = true là đã đặt

                if (existingVeXe != null)
                {
                    foreach (var ghe in DsGheDangChon)
                    {
                        int soGhe = int.Parse(ghe);

                        // Truy vấn lấy MaVeXe
                        var veXe = db.VEXEs
                            .Where(v => v.MaHanhKhach == maHanhKhach
                                     && v.MaChuyenXe == maChuyenXe
                                     && v.SoGhe == soGhe)
                            .Select(v => v.MaVeXe)
                            .FirstOrDefault();
                     
                       ;
                        if (MaNHanVien == 0)
                        {
                            MaNHanVien = 1;
                        }
                       
                            // Tạo chi tiết vé
                            var chiTietVeXe = new CHITIETVEXE
                            {
                                MaNhanVien = MaNHanVien, // Thay bằng mã nhân viên thực tế
                                MaVeXe = veXe,  // Dùng MaVeXe của vé đã chèn
                                DiemDon = DiemDon,
                                DiemTra = DiemTra,
                                NgayXuat = DateTime.Now
                            };

                            db.CHITIETVEXEs.Add(chiTietVeXe);

                        
                    }
                       


                    // Sau khi lấy max MaVeXe, bạn có thể sử dụng maxMaVeXe trong các thao tác khác.

                   

                    // Lưu các thay đổi vào cơ sở dữ liệu
                      // Không cho phép đặt vé mới nếu vé đã được đặt
                }

                // Chèn vé vào bảng VEXE
                bool isInserted = InsertVeXe(maHanhKhach, maChuyenXe);
                if (!isInserted)
                {
                    Console.WriteLine("Lỗi khi chèn vé vào bảng VEXE.");
                    return;
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đặt vé và thêm chi tiết vé: " + ex.Message);
            }
        }





        public int KT_TaiKhoan(string username, string password)
        {
            string caulenh = $"select * from TAIKHOAN where UserName = '{username}' and Pass = '{password}'";

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                SqlDataReader reder = cmd.ExecuteReader();



                while (reder.Read())
                {
                    Staff.taiKhoan = username;
                    Staff.matKhau = password;
                    Staff.maNV = int.Parse(reder["UserID"].ToString());
                    if (reder["UserRole"].ToString() == "Admin")
                    {
                        Staff.maNV = int.Parse(reder["UserID"].ToString());
                        connection.Close();
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }                    
                }

                connection.Close();
                return 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }


        public bool IsEmail(string email)
        {
            string dinhdang = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            Regex regex = new Regex(dinhdang);
            return regex.IsMatch(email);
        }

        

        public int KT_NhanVien(string email)
        {
            try
            {
                string caulenh = "select MaNhanVien  from NHANVIEN where Email = '" + email + "'";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                if (cmd.ExecuteScalar() == null)
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        public string GetPassword(string email)
        {
            try
            {
                string caulenh = "select Pass from NHANVIEN, TAIKHOAN where Email = '" + email + "' and MaNhanVien = UserID";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                string pass = cmd.ExecuteScalar().ToString();

                if(pass != null)
                {
                    return pass;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public void Change_Password(int manv, string pass)
        {
            try
            {
                string caulenh = $"update TAIKHOAN set Pass = '{pass}' where UserID = '{manv}'";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch( Exception ex)
            {
                return;
            }
        }


        // TAI XE
        public DataTable LoadTaiXe(string lenh = "select * from TAIXE")
        {
            try
            {
                string caulenh = lenh;

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }
        public int KT_TaiXe(string cccd)
        {
            try
            {
                string kt = $"select * from TAIXE where CCCD = '{cccd}'";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(kt, connection);

                SqlDataReader reder = cmd.ExecuteReader();

                while (reder.Read())
                {
                    connection.Close();
                    return 0;
                }

                connection.Close();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public int ThemTaiXe(int ma, string ten, string sdt, string cccd, string diachi)
        {
            try
            {                

                string caulenh = "insert into TAIXE(MaTaiXe, TenTaiXe, SoDienThoai, CCCD, DiaChi) values(@ma,@ten,@sdt,@cccd,@diachi)";

                if(KT_TaiXe(cccd) != 0)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlCommand cmd = new SqlCommand(caulenh, connection);

                    cmd.Parameters.AddWithValue("@ma", ma);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@cccd", cccd);
                    cmd.Parameters.AddWithValue("@diachi", diachi);

                    cmd.ExecuteNonQuery();

                    connection.Close();
                    return 1;
                }

                return 0;                
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int XoaTaiXe(int ma)
        {
            try
            {
                string caulenh = $"delete from TAIXE where MaTaiXe = '{ma}'";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);


                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int SuaTaiXe(int ma, string name, string sdt, string cccd, string diachi)
        {
            try
            {
                string caulenh = $"update TAIXE set TenTaiXe = N'{name}', SoDienThoai = '{sdt}', CCCD = '{cccd}', DiaChi = N'{diachi}' where MaTaiXe = '{ma}'";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int LayMaTaiXe()
        {
            try
            {
                string caulenh = "select MAX(MaTaiXe) from TAIXE";
                
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int count = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();

                return count + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }

            return 0;
        }


        // LOAI XE
        public DataTable LayLoaiXe(string lenh = "select * from LOAIXE")
        {
            try
            {
                string caulenh = lenh;

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int ThemLoaiXe(int ma, string name, int succhua, string loaighe)
        {
            try
            {
                string caulenh = "insert into LOAIXE(MaLoaiXe,TenLoaiXe,SucChuaXe,LoaiGhe) values(@ma,@name,@succhua,@loaighe)";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@succhua", succhua);
                cmd.Parameters.AddWithValue("@loaighe", loaighe);

                cmd.ExecuteNonQuery();

                connection.Close();

                return ma;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int XoaLoaiXe(int ma)
        {
            try
            {
                string caulenh = $"delete from LOAIXE where MaLoaiXe = {ma}";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SuaLoaiXe(int ma, string name, int succhua, string loaighe)
        {
            try
            {
                string caulenh = "update LOAIXE set TenLoaiXe = @name, SucChuaXe = @succhua, LoaiGhe = @loaighe where MaLoaiXe = @ma";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@succhua", succhua);
                cmd.Parameters.AddWithValue("@loaighe", loaighe);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int LayMaLX()
        {
            try
            {
                string caulenh = "select MAX(MaLoaiXe)  from LOAIXE";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int count = int.Parse(cmd.ExecuteScalar().ToString()) + 1;

                connection.Close();

                return count;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }


        // Tinh Thanh
        public DataTable LayTinhThanh()
        {
            try
            {
                string caulenh = "select * from TINHTHANH";

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        // Tuyen Xe

        public DataTable LayTuyenXe(string lenh = "select * from TUYENXE")
        {
            try
            {
                string caulenh = lenh;

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int LayMaTX()
        {
            try
            {
                string caulenh = "select MAX(MaTuyenXe) from TUYENXE";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int count = int.Parse(cmd.ExecuteScalar().ToString());                

                return count + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int ThemTuyenXe(int ma, string ten, string diemdi, string diemden)
        {
            try
            {
                string caulenh = "insert into TUYENXE(MaTuyenXe, TenTuyenXe, DiemDi, DiemDen) values(@ma,@ten,@diemdi,@diemden)";

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@diemdi", diemden);
                cmd.Parameters.AddWithValue("@diemden", diemden);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;

            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int SuaTuyenXe(int ma, string ten, string diemdi, string diemden)
        {
            try
            {
                string caulenh = $"update TUYENXE set TenTuyenXe = @ten, DiemDi = @diemdi, DiemDen = @diemden where MaTuyenXe = {ma}";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);
                
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@diemdi", diemden);
                cmd.Parameters.AddWithValue("@diemden", diemden);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int XoaTuyenXe(int ma)
        {
            try
            {
                string caulenh = $"delete from TUYENXE where MaTuyenXe = '{ma}'";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        // Xe
        public DataTable LayXe(string lenh = "select * from XE")
        {
            try
            {
                string caulenh = lenh;

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int LayMaXe()
        {
            try
            {
                string caulenh = "select MAX(MaXe) from XE";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int count = int.Parse(cmd.ExecuteScalar().ToString());

                return count + 1;
            }
            catch(Exception ex)
            {
                return 1;
            }
        }
        public int ThemXe(int ma, string bienso, int maloai, int matx)
        {
            try
            {
                string caulenh = "insert into XE(MaXe, BienSoXe, MaLoaiXe, MaTaiXe) values(@ma,@bienso,@maloai,@matx)";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@bienso", bienso);
                cmd.Parameters.AddWithValue("@maloai", maloai);
                cmd.Parameters.AddWithValue("@matx", matx);

                cmd.ExecuteNonQuery();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int XoaXe(int ma)
        {
            try
            {
                string caulenh = $"delete from XE where MaXe = {ma}";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                return 1;
            }
            catch( Exception ex)
            {
                return 0;
            }
        }

        public int SuaXe(int ma, string bienso, int maloai, int matx)
        {
            try
            {
                string caulenh = $"update XE set BienSoXe = '{bienso}', MaLoaiXe = '{maloai}', MaTaiXe = '{matx}' where MaXe = {ma}";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        // Chuyen xe
        public int LayMaChuyenXe()
        {
            try
            {
                string caulenh = "select MAX(MaChuyenXe) from CHUYENXE";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);


                int count = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();

                return count + 1;

            }
            catch(Exception ex)
            {
                return 1;
            }
        }

        public DataTable LayChuyenXe(string lenh = "select * from CHUYENXE")
        {
            try
            {
                string caulenh = lenh;

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public DataTable LayTuyenXe()
        {
            try
            {
                string caulenh = "select * from TUYENXE";

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public DataTable LayXe()
        {
            try
            {
                string caulenh = "select * from XE";

                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);

                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int LaySoGhe(int machuyenxe)
        {
            try
            {
                string caulenh = $"select SucChuaXe from CHUYENXE as cx, XE as xe, LOAIXE as lx where cx.MaChuyenXe = {machuyenxe} and cx.MaXe = xe.MaXe and xe.MaLoaiXe = lx.MaLoaiXe";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int soluong = int.Parse(cmd.ExecuteScalar().ToString());

                return soluong;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public void CapNhat_SoLuongGhe(int machuyen)
        {
            try
            {
                string caulenh = $"update ChuyenXe set SoGheTrong = {LaySoGhe(machuyen)} where MaChuyenXe = '{machuyen}'";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch(Exception ex)
            {
                return;
            }
        }
        public int ThemChuyenXe(int machuyen, int matuyen, int maxe, float giatien, DateTime xuatphat, DateTime dukien)
        {
            try
            {
                string caulenh = "insert into CHUYENXE(MaChuyenXe,MaTuyenXe,MaXe,GiaTien,ThoiGianXuatPhat,ThoiGianDenDuKien) values(@machuyen,@matuyen,@maxe,@giatien,@xuatphat,@dukien)";

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.Parameters.AddWithValue("@machuyen", machuyen);
                cmd.Parameters.AddWithValue("@matuyen", matuyen);
                cmd.Parameters.AddWithValue("@maxe", maxe);
                cmd.Parameters.AddWithValue("@giatien", giatien);                
                cmd.Parameters.AddWithValue("@xuatphat", xuatphat);
                cmd.Parameters.AddWithValue("@dukien", dukien);

                cmd.ExecuteNonQuery();                

                connection.Close();

                // cap nhat soghetrong(chuyenxe) = succhuaxe(loaixe)
                CapNhat_SoLuongGhe(machuyen);

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int KiemTraLienKet_ChuyenXe(int machuyen)
        {
            try
            {
                string caulenh = $"select * from VEXE as vx, CHUYENXE as cx where vx.MaChuyenXe = cx.MaChuyenXe and cx.MaChuyenXe = {machuyen}";

                if(connection.State != ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                int count = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();

                if(count > 0)
                {
                    return 0; // khi chuyen xe da co nguoi dat ve
                }

                return 1; // khi chuyen xe chua co nguoi dat ve
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        
        public int XoaChuyenXe(int machuyen)
        {
            try
            {
                string caulenh = $"delete from CHUYENXE where MaChuyenXe = {machuyen}";

                if(KiemTraLienKet_ChuyenXe(machuyen) == 1)
                {
                    if(connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlCommand cmd = new SqlCommand(caulenh, connection);

                    cmd.ExecuteNonQuery();

                    connection.Close();

                    return 1;
                }

                return 0;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        // kiem tra chuyen xe da co ai dat ve chua
        public int SuaChuyenXe(int machuyen, int matuyen, int maxe, float giatien, DateTime xuatphat, DateTime dukien)
        {
            try
            {
                string caulenh = $"Update CHUYENXE set MaTuyenXe = @matuyen,MaXe = @maxe,GiaTien = @giatien,ThoiGianXuatPhat = @xuatphat,ThoiGianDenDuKien = @dukien where MaChuyenXe = {machuyen}";

                if (KiemTraLienKet_ChuyenXe(machuyen) == 1)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlCommand cmd = new SqlCommand(caulenh, connection);

                    cmd.Parameters.AddWithValue("@machuyen", machuyen);
                    cmd.Parameters.AddWithValue("@matuyen", matuyen);
                    cmd.Parameters.AddWithValue("@maxe", maxe);
                    cmd.Parameters.AddWithValue("@giatien", giatien);
                    cmd.Parameters.AddWithValue("@xuatphat", xuatphat);
                    cmd.Parameters.AddWithValue("@dukien", dukien);

                    cmd.ExecuteNonQuery();

                    connection.Close();

                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        // lay thong tin nhan vien
        public DataTable LayTTNhanVien(string lenh = "exec LayTTNhanVien")
        {
            try
            {
                string caulenh = lenh;


                SqlDataAdapter adap = new SqlDataAdapter(caulenh, connection);


                dt = new DataTable();

                adap.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int LayMaNV()
        {
            try
            {
                string caulenh = "select MAX(MaNhanVien) from NHANVIEN";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);


                int count = int.Parse(cmd.ExecuteScalar().ToString());

                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public int ThemNhanVien(int ma, string ten, string email, string sdt, string tk, string mk, string roles = "BanVe")
        {
            try
            {
                string caulenh = @" INSERT INTO NHANVIEN (TenNhanVien, SDT, Email) 
                                    VALUES (@ten, @sdt, @email);";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);


                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@tk", tk);
                cmd.Parameters.AddWithValue("@mk", mk);
                cmd.Parameters.AddWithValue("@roles", roles);

                cmd.ExecuteNonQuery();

                connection.Close();

                caulenh = @" INSERT INTO TAIKHOAN (UserID, UserName, Pass, UserRole) 
                                    VALUES (@ma, @tk, @mk, @roles);";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                cmd = new SqlCommand(caulenh, connection);


                cmd.Parameters.AddWithValue("@ma", LayMaNV());
                cmd.Parameters.AddWithValue("@tk", tk);
                cmd.Parameters.AddWithValue("@mk", mk);
                cmd.Parameters.AddWithValue("@roles", roles);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int XoaNhanVien(int ma)
        {
            try
            {
                string caulenh = $"delete from TAIKHOAN where UserID = {ma}";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                caulenh = $"delete from NHANVIEN where MaNhanVien = {ma}";
                cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SuaThongTinNhanVien(int ma, string ten, string email, string sdt, string tk, string mk, string roles = "BanVe")
        {
            try
            {
                string caulenh = $"Update NHANVIEN set TenNhanVien = N'{ten}', SDT = '{sdt}', Email = '{email}' where MaNhanVien = {ma}";

                string caulenh2 = $"Update TAIKHOAN set UserName = '{tk}', Pass = '{mk}' where UserID = {ma}";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand(caulenh, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                cmd = new SqlCommand(caulenh2, connection);

                cmd.ExecuteNonQuery();

                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }




    }
    
    internal static class Staff
    {
        public static int maNV { get; set; }

        public static string taiKhoan { get; set; }

        public static string matKhau { get; set; }
    } 
}

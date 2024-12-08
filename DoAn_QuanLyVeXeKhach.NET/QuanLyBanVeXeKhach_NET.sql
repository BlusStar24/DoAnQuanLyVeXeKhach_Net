-- Da Sua

CREATE DATABASE QLBanVeXeKhach_net
GO
USE QLBanVeXeKhach_net
GO

CREATE TABLE TAIKHOAN
(
    UserID int IDENTITY(1,1) NOT NULL,
    UserName nvarchar(20) NOT NULL,
    Pass char(16) NOT NULL,
    UserRole nvarchar(20) NOT NULL CHECK (UserRole IN ('Admin', 'KhachHang')),
    constraint PK_TAIKHOAN primary key(UserID)
)
GO

CREATE TABLE HANHKHACH
(
	MaHanhKhach int identity (1,1) NOT NULL,
	UserID int NOT NULL,
	TenHanhKhach nvarchar(30) NOT NULL,
	SoDienThoai char(10),
	GioiTinh nvarchar(10),
	Email nvarchar(30),
	DiaChi nvarchar(30),
	constraint PK_HANHKHACH primary key(MaHanhKhach)
)
GO

CREATE TABLE VEXE
(
	MaVeXe int identity (1,1) NOT NULL,
	MaHanhKhach int NOT NULL,
    MaChuyenXe int NOT NULL,
	TrangThai bit DEFAULT 0, -- 0: CHUA THANH TOÁN, 1: ĐÃ THANH TOÁN
	constraint PK_VEXE primary key(MaVeXe)
)
GO

CREATE TABLE CHITIETVEXE
(
    MaVeXe int identity (1,1) NOT NULL,
    MaGhe int NOT NULL,
    DiemDon nvarchar(50),
    DiemTra nvarchar(50),
    NgayXuat date NOT NULL,
    constraint PK_CHITIETVEXE primary key(MaVeXe)
)
GO

CREATE TABLE LOAIXE
(
    MaLoaiXe int NOT NULL,
    TenLoaiXe nvarchar(50) NOT NULL,
    SucChuaXe int NOT NULL,
    LoaiGhe nvarchar(50),
    constraint PK_LOAIXE primary key(MaLoaiXe)
)
GO

CREATE TABLE XE
(
    MaXe int NOT NULL,
    BienSoXe nvarchar(50) NOT NULL,
    MaLoaiXe int NOT NULL,
	MaTaiXe INT NOT NULL,
    constraint PK_XE primary key(MaXe)
)
GO

CREATE TABLE TAIXE
(
    MaTaiXe int NOT NULL,
    TenTaiXe nvarchar(50) NOT NULL,	
	SoDienThoai char(10) NOT NULL,
    CCCD char(12) NOT NULL,
    DiaChi nvarchar(50) NOT NULL,
    constraint PK_TAIXE primary key(MaTaiXe)
)
GO

CREATE TABLE TUYENXE 
(
    MaTuyenXe int NOT NULL,
    TenTuyenXe nvarchar(50) NOT NULL,
    DiemDi int NOT NULL,
    DiemDen int NOT NULL,
    constraint PK_TUYENXE primary key(MaTuyenXe)
)
GO

CREATE TABLE CHUYENXE 
(
    MaChuyenXe int NOT NULL,
    MaTuyenXe int NOT NULL,
    MaXe int NOT NULL,	
    GiaTien float NOT NULL,
    SoGheTrong int,
    ThoiGianXuatPhat datetime,
    ThoiGianDenDuKien datetime,
    constraint PK_CHUYENXE primary key(MaChuyenXe)
)

CREATE TABLE TINHTHANH
(
	MaTinhThanh int NOT NULL,
	TenTinhThanh nvarchar(50) NOT NULL,
	constraint PK_TINHTHANH primary key(MaTinhThanh)
)
GO


-- KHACH HANG VA TAI KHOAN
ALTER TABLE HANHKHACH
add constraint FK_HANHKHACH_TAIKHOAN foreign key(UserID) references TAIKHOAN(UserID)

-- TÀI XẾ VÀ XE
ALTER TABLE XE
add constraint FK_XE_TAIXE foreign key(MaTaiXe) references TAIXE(MaTaiXe)

-- TUYẾN XE VỚI TỈNH THÀNH (ĐIỂM ĐI)
ALTER TABLE TUYENXE
add constraint FK_TUYENXE_TINHTHANH_DIEMDI foreign key(DiemDi) references TINHTHANH(MaTinhThanh)

-- TUYẾN XE VỚI TỈNH THÀNH (ĐIỂM ĐẾN)
ALTER TABLE TUYENXE
add constraint FK_TUYENXE_TINHTHANH_DIEMDEN foreign key(DiemDen) references TINHTHANH(MaTinhThanh)

-- VÉ XE VỚI KHÁCH HÀNG
ALTER TABLE VEXE
add constraint FK_VEXE_KHACHHANG foreign key(MaHanhKhach) references HANHKHACH(MaHanhKhach)

-- VÉ XE VỚI CHUYẾN XE
ALTER TABLE VEXE
add constraint FK_VEXE_CHUYENXE foreign key(MaChuyenXe) references CHUYENXE(MaChuyenXe)

-- CHI TIẾT VÉ XE VỚI VÉ XE
ALTER TABLE CHITIETVEXE
add constraint FK_CHITIETVEXE_VEXE foreign key(MaVeXe) references VEXE(MaVeXe)

-- XE VỚI LOẠI XE
ALTER TABLE XE
add constraint FK_XE_LOAIXE foreign key(MaLoaiXe) references LOAIXE(MaLoaiXe)

-- CHUYẾN XE VÀ TUYẾN XE
ALTER TABLE CHUYENXE
add constraint FK_CHUYENXE_TUYENXE foreign key(MaTuyenXe) references TUYENXE(MaTuyenXe)

-- CHUYẾN XE VÀ XE
ALTER TABLE CHUYENXE
add constraint FK_CHUYENXE_XE foreign key(MaXe) references XE(MaXe)
GO

-- CHUYẾN XE VÀ TÀI XẾ


-- Thêm dữ liệu cho bảng TAIKHOAN
INSERT INTO TAIKHOAN (UserName, Pass, UserRole) VALUES 
('admin1', '1234567890abcdef', 'Admin'),
('khachhang1', '1234567890abcdef', 'KhachHang'),
('khachhang2', '1234567890abcdef', 'KhachHang');

-- Thêm dữ liệu cho bảng HANHKHACH
INSERT INTO HANHKHACH (UserID, TenHanhKhach, SoDienThoai, GioiTinh, Email, DiaChi) VALUES 
(2, 'Nguyen Van A', '0123456789', 'Nam', 'a.nguyen@gmail.com', '123 Le Loi'),
(3, 'Le Thi B', '0987654321', 'Nu', 'b.le@gmail.com', '456 Tran Hung Dao');

-- Thêm dữ liệu cho bảng TAIXE
INSERT INTO TAIXE (MaTaiXe, TenTaiXe, SoDienThoai, CCCD, DiaChi) VALUES 
(1, 'Pham Van C', '0901234567', '123456789012', '789 Ngo Gia Tu'),
(2, 'Tran Van D', '0912345678', '234567890123', '234 Nguyen Trai');

-- Thêm dữ liệu cho bảng LOAIXE
INSERT INTO LOAIXE (MaLoaiXe, TenLoaiXe, SucChuaXe, LoaiGhe) VALUES 
(1, 'Xe 16 Cho', 16, 'Ghe Thuong'),
(2, 'Xe 29 Cho', 29, 'Ghe VIP');

-- Thêm dữ liệu cho bảng XE
INSERT INTO XE (MaXe, BienSoXe, MaLoaiXe, MaTaiXe) VALUES 
(1, '51A-12345', 1, 1),
(2, '51B-67890', 2, 2);

-- Thêm dữ liệu cho bảng TINHTHANH
INSERT INTO TINHTHANH (MaTinhThanh, TenTinhThanh) VALUES 
(1, 'Ha Noi'),
(2, 'Ho Chi Minh'),
(3, 'Da Nang');

-- Thêm dữ liệu cho bảng TUYENXE
INSERT INTO TUYENXE (MaTuyenXe, TenTuyenXe, DiemDi, DiemDen) VALUES 
(1, 'Ha Noi - Ho Chi Minh', 1, 2),
(2, 'Ho Chi Minh - Da Nang', 2, 3);

-- Thêm dữ liệu cho bảng CHUYENXE
INSERT INTO CHUYENXE (MaChuyenXe, MaTuyenXe, MaXe, GiaTien, SoGheTrong, ThoiGianXuatPhat, ThoiGianDenDuKien) VALUES 
(1, 1, 1, 500000, 16, '2024-12-01 08:00:00', '2024-12-01 20:00:00'),
(2, 2, 2, 700000, 29, '2024-12-01 10:00:00', '2024-12-01 22:00:00');

-- Thêm dữ liệu cho bảng VEXE
INSERT INTO VEXE (MaHanhKhach, MaChuyenXe, TrangThai) VALUES 
(1, 1, 0),
(2, 2, 1);

-- Thêm dữ liệu cho bảng CHITIETVEXE
INSERT INTO CHITIETVEXE (MaGhe, DiemDon, DiemTra, NgayXuat) VALUES 
( 5, 'Ben Xe My Dinh', 'Ben Xe Mien Dong', '2024-12-01'),
( 10, 'Ben Xe Mien Dong', 'Ben Xe Da Nang', '2024-12-01');

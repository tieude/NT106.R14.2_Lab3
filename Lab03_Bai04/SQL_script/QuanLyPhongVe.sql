-- 1. Tạo Database
CREATE DATABASE QuanLyPhongVe;
GO

USE QuanLyPhongVe;
GO

-- 2. Tạo Bảng PHIM
CREATE TABLE PHIM(
    MaPhim INT IDENTITY(1,1) PRIMARY KEY,
    TenPhim NVARCHAR(100) NOT NULL,
    GiaGoc DECIMAL(18,2)
);

-- 3. Tạo Bảng PHONG (Đã sửa TenPhim -> TenPhong)
CREATE TABLE PHONG(
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    MaPhim INT FOREIGN KEY REFERENCES PHIM(MaPhim)
);

-- 4. Tạo Bảng GHE
CREATE TABLE GHE(
    MaGhe INT IDENTITY(1,1) PRIMARY KEY,
    TenGhe NVARCHAR(100) NOT NULL,
    LoaiGhe INT DEFAULT 1, -- 1: Thường (x1.0), 2: VIP (x1.5), 3: Promo (x0.8)
    TTGhe INT DEFAULT 0,   -- 0: Trống, 1: Đã đặt
    MaPhong INT FOREIGN KEY REFERENCES PHONG(MaPhong)
);
GO


INSERT INTO PHIM (TenPhim, GiaGoc) VALUES 
(N'Lật Mặt 7: Một Điều Ước', 90000),
(N'Mai', 100000),
(N'Dune 2: Hành Tinh Cát', 120000);


INSERT INTO PHONG (TenPhong, MaPhim) VALUES 
(N'Phòng 01 - Lật Mặt', 1),
(N'Phòng VIP 01 - Mai', 2),
(N'Phòng VIP 02 - Mai', 2),
(N'Phòng IMAX 01 - Dune', 3),
(N'Phòng IMAX 02 - Dune', 3),
(N'Phòng Standard - Dune', 3);

DECLARE @MaPhong INT = 1;
WHILE @MaPhong <= 6
BEGIN
    INSERT INTO GHE (TenGhe, LoaiGhe, TTGhe, MaPhong) VALUES
    ('A1', 1, 0, @MaPhong), ('A2', 1, 0, @MaPhong), ('A3', 1, 0, @MaPhong), ('A4', 1, 0, @MaPhong), ('A5', 1, 0, @MaPhong),
    ('B1', 2, 0, @MaPhong), ('B2', 2, 0, @MaPhong), ('B3', 2, 0, @MaPhong), ('B4', 2, 0, @MaPhong), ('B5', 2, 0, @MaPhong),
    ('C1', 3, 0, @MaPhong), ('C2', 3, 0, @MaPhong), ('C3', 3, 0, @MaPhong), ('C4', 3, 0, @MaPhong), ('C5', 3, 0, @MaPhong);
    
    SET @MaPhong = @MaPhong + 1;
END;


UPDATE GHE SET TTGhe = 1 WHERE MaPhong = 1 AND TenGhe IN ('A2', 'B3');

GO

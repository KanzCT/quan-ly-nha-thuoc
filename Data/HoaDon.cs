using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public string MaNv { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayCan { get; set; }

    public DateTime? NgayGiao { get; set; }

    public string HoTen { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string DienThoai { get; set; } = null!;

    public string CachThanhToan { get; set; } = null!;

    public string CachVanChuyen { get; set; } = null!;

    public decimal? PhiVanChuyen { get; set; }

    public int MaTrangThai { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}

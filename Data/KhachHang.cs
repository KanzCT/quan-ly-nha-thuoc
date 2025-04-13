using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string TenKh { get; set; } = null!;

    public string? Sdt { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public bool? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string Email { get; set; } = null!;

    public bool HieuLuc { get; set; }

    public int VaiTro { get; set; }

    public string? RandomKey { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}

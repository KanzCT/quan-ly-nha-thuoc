using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? Gmail { get; set; }

    public string? Sdt { get; set; }

    public string? Gt { get; set; }

    public decimal? Luong { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}

using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class ChiTietHoaDon
{
    public int MaCt { get; set; }

    public int MaHd { get; set; }

    public int MaThuoc { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public double? GiamGia { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; } = null!;

    public virtual Thuoc MaThuocNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class Thuoc
{
    public int MaThuoc { get; set; }

    public string TenThuoc { get; set; } = null!;

    public decimal GiaBan { get; set; }

    public DateOnly NgaySanXuat { get; set; }

    public DateOnly NgayHetHan { get; set; }

    public int SoLuongThuocCon { get; set; }

    public string? MoTaNgan { get; set; }

    public string? ChiTietThuoc { get; set; }

    public string Dvt { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public string MaNcc { get; set; } = null!;

    public int MaDanhMuc { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual DanhMuc MaDanhMucNavigation { get; set; } = null!;

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;
}

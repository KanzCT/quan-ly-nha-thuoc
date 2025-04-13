using System;
using System.Collections.Generic;

namespace QuanLyThuoc.Data;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenCongTy { get; set; } = null!;

    public string NguoiLienLac { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? Gmail { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<Thuoc> Thuocs { get; set; } = new List<Thuoc>();
}

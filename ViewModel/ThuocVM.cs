using QuanLyThuoc.Data;

namespace QuanLyThuoc.ViewModel
{
    public class ThuocVM
    {
        public int MaThuoc { get; set; }  // Mã thuốc (Khóa chính)

        public string TenThuoc { get; set; } = null!; // Tên thuốc

        public decimal GiaBan { get; set; } // Giá bán

        public string MoTaNgan { get; set; } = null!;  // Mota ngắn sản phẩm

        public string TenLoai { get; set; } = null!; // Tên Loại thuốc

        public string? HinhAnh { get; set; }
    }

    public class ChiTietThuocVM
    {
        public int MaThuoc { get; set; }  // Mã thuốc (Khóa chính)

        public string TenThuoc { get; set; } = null!; // Tên thuốc

        public decimal GiaBan { get; set; } // Giá bán

        public string MoTaNgan { get; set; } = null!;  // Mota ngắn sản phẩm

        public string TenLoai { get; set; } = null!; // Tên Loại thuốc

        public string? HinhAnh { get; set; }

        public string? ChiTietThuoc { get; set; }

        public int DiemDanhGia { get; set; }

        public int SoLuongTon { get; set; }

	}
}

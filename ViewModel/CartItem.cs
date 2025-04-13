namespace QuanLyThuoc.ViewModel
{
    public class CartItem
    {
        public int MaThuoc { get; set; }// Mã thuốc (Khóa chính)

        public string TenThuoc { get; set; } = null!; // Tên thuốc

        public decimal GiaBan { get; set; } // Giá bán

        public int SoLuong { get; set; }  // Soluong sản phẩm

        public string? HinhAnh { get; set; }

        public decimal ThanhTien => SoLuong * GiaBan;

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuoc.ViewModel
{
    public class ThemThuocVM
    {
        [Required]
        public int MaThuoc { get; set; }

        [Required]
        public string TenThuoc { get; set; }

        [Required]
        public decimal GiaBan { get; set; }

        [Required]
        [Display(Name = "Ngày sản xuất")]
        public DateOnly NgaySanXuat { get; set; }

        [Required]
        [Display(Name = "Ngày hết hạn")]
        public DateOnly NgayHetHan { get; set; }

        [Required]
        public int SoLuongThuocCon { get; set; }

        public string? MoTaNgan { get; set; }

        public string? ChiTietThuoc { get; set; }

        [Required]
        public string Dvt { get; set; }

        public IFormFile? HinhAnh { get; set; }

        [Required]
        public string MaNcc { get; set; }

        [Required]
        public int MaDanhMuc { get; set; }

        // Dropdown data
        public List<SelectListItem>? DanhMucList { get; set; }
        public List<SelectListItem>? NhaCungCapList { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuoc.Data;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.Controllers
{
    public class ThuocController : Controller
    {
        private readonly QuanlythuocContext db;

        public ThuocController(QuanlythuocContext context)
        {
            db = context;
        }
        public IActionResult Index(int? DanhMuc)
        {
            var thuocs = db.Thuocs.AsQueryable();

            if (DanhMuc != null)
            {
                thuocs = thuocs.Where(p => p.MaDanhMuc == DanhMuc);
            }

            var result = thuocs.Select(p => new ThuocVM
            {
                MaThuoc = p.MaThuoc,
                TenThuoc = p.TenThuoc ?? "",
                GiaBan = p.GiaBan,
                HinhAnh = p.HinhAnh ?? "",
                MoTaNgan = p.MoTaNgan ?? "",
                TenLoai = p.MaDanhMucNavigation.TenDanhMuc ?? ""
            });

            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var thuocs = db.Thuocs.AsQueryable();

            if (query != null)
            {
                thuocs = thuocs.Where(p => p.TenThuoc.Contains(query));
            }

            var result = thuocs.Select(p => new ThuocVM
            {
                MaThuoc = p.MaThuoc,
                TenThuoc = p.TenThuoc ?? "",
                GiaBan = p.GiaBan,
                HinhAnh = p.HinhAnh ?? "",
                MoTaNgan = p.MoTaNgan ?? "",
                TenLoai = p.MaDanhMucNavigation.TenDanhMuc
            });

            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var data = db.Thuocs
                .Include(p => p.MaDanhMucNavigation)
                .SingleOrDefault(p => p.MaThuoc == id);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            var result = new ChiTietThuocVM
            {
                MaThuoc = data.MaThuoc,
                TenThuoc = data.TenThuoc ?? "",
                GiaBan = data.GiaBan,
                ChiTietThuoc = data.ChiTietThuoc ?? "",
                HinhAnh = data.HinhAnh ?? "",
                MoTaNgan = data.MoTaNgan ?? "",
                TenLoai = data.MaDanhMucNavigation.TenDanhMuc,
                SoLuongTon = 10,
                DiemDanhGia = 5,
			};
            return View(result);
        }
    }
}

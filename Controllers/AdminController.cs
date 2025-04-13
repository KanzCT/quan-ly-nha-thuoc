using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyThuoc.Data;
using QuanLyThuoc.Helper;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly QuanlythuocContext db;

        public IActionResult Index()
        {
            return View();
        }

        public AdminController(QuanlythuocContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemThuoc()
        {
            var model = new ThemThuocVM
            {
                DanhMucList = db.DanhMucs.Select(dm => new SelectListItem
                {
                    Value = dm.MaDanhMuc.ToString(),
                    Text = dm.TenDanhMuc
                }).ToList(),

                NhaCungCapList = db.NhaCungCaps.Select(ncc => new SelectListItem
                {
                    Value = ncc.MaNcc,
                    Text = ncc.TenCongTy
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemThuoc(ThemThuocVM model)
        {
            if (ModelState.IsValid)
            {
                var thuoc = new Thuoc
                {
                    MaThuoc = model.MaThuoc,
                    TenThuoc = model.TenThuoc,
                    GiaBan = model.GiaBan,
                    NgaySanXuat = model.NgaySanXuat,
                    NgayHetHan = model.NgayHetHan,
                    SoLuongThuocCon = model.SoLuongThuocCon,
                    MoTaNgan = model.MoTaNgan,
                    ChiTietThuoc = model.ChiTietThuoc,
                    Dvt = model.Dvt,
                    HinhAnh = model.HinhAnh != null ? MyUtil.UploadHinh(model.HinhAnh, "Thuoc") : null,
                    MaNcc = model.MaNcc,
                    MaDanhMuc = model.MaDanhMuc
                };

                db.Thuocs.Add(thuoc);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thêm thuốc thành công!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ThongKe()
        {
            var soLuongThuoc = db.Thuocs.Sum(t => t.SoLuongThuocCon);
            ViewBag.SoLuongThuoc = soLuongThuoc;

            var sonhacungcap = db.NhaCungCaps.Count();
            ViewBag.SoNhaCungCap = sonhacungcap;

            var giatritongthuoc = db.Thuocs.Sum(t => t.GiaBan);
            ViewBag.GiaTriTongThuoc = soLuongThuoc;

            var today = DateOnly.FromDateTime(DateTime.Today);

            var thuocGanHetHan = db.Thuocs
                .AsEnumerable() // Chuyển sang xử lý phía client
                .Where(t =>
                    (t.NgayHetHan.DayNumber - today.DayNumber) <= 7 &&
                    (t.NgayHetHan.DayNumber - today.DayNumber) >= 0
                )
                .Count();

            ViewBag.ThuocGanHetHan = thuocGanHetHan;


            var tongloaithuoc = db.DanhMucs.Sum(t => t.MaDanhMuc);
            ViewBag.TongLoaiThuoc = soLuongThuoc;

            var tongsodonhang = db.HoaDons.Sum(t => t.MaHd);
            ViewBag.TongSoDonHang = soLuongThuoc;


            return View(); // Tạo file ThongKe.cshtml sau
        }

   


    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuoc.Data;
using QuanLyThuoc.Helper;
using QuanLyThuoc.ViewModel;
using System.Security.Claims;

namespace QuanLyThuoc.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly QuanlythuocContext db;
        private readonly IMapper _mapper; 

        public KhachHangController(QuanlythuocContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy([FromForm] RegisterVM model, [FromForm] IFormFile? Hinh) // SỬA ĐỔI: Thêm [FromForm]
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState không hợp lệ:");
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"{error.Key}: {subError.ErrorMessage}");
                    }
                }
                TempData["ErrorMessage"] = "Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
                return View(model);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                    if (db.KhachHangs.Any(kh => kh.MaKh == model.MaKh))
                    {
                        ModelState.AddModelError("MaKh", "Tên đăng nhập đã tồn tại.");
                        return View(model);
                    }

                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRamdomKey();
                    var hashedPassword  = khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    Console.WriteLine("Mật khẩu nhập vào: " + model.MatKhau);
                    Console.WriteLine("RandomKey trong DB: " + khachHang.RandomKey);
                    Console.WriteLine("Hash tạo ra: " + hashedPassword);
                    Console.WriteLine("Hash trong DB: " + khachHang.MatKhau);
                    khachHang.HieuLuc = true;//sẽ xử lý khi dùng Mail để active
                    khachHang.VaiTro = 0; // mặc định là khách hàng

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }

                    db.Add(khachHang);
                    //db.SaveChanges();
                    int result = db.SaveChanges(); // Trả về số dòng bị ảnh hưởng
                    Console.WriteLine($"SaveChanges() trả về: {result}");

                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                        return RedirectToAction("DangNhap");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra, vui lòng thử lại.";
                        return View(model);
                    }
                }
                catch (DbUpdateException dbEx) // SỬA ĐỔI: Bắt lỗi cập nhật database
                {
                    TempData["ErrorMessage"] = $"Lỗi database: {dbEx.InnerException?.Message}";
                    Console.WriteLine($"Lỗi database: {dbEx.InnerException?.Message}");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Lỗi không xác định: {ex.Message}";
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }

            }
            
            return View();
        }
        #endregion

        #region Login in

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, String? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Tên đăng nhập không tồn tại.");
                    return View(model);
                }

                if (!khachHang.HieuLuc)
                {
                    ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
                    return View(model);
                }

                if (khachHang.MatKhau != model.Password)
                {
                    ModelState.AddModelError("loi", "Mật khẩu không đúng.");
                    return View(model);
                }


                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, khachHang.Email),
                    new Claim(ClaimTypes.Name, khachHang.MaKh),
                    new Claim("CustomerID", khachHang.MaKh),
                    new Claim(ClaimTypes.Role, khachHang.VaiTro == 1 ? "Admin" : "Customer") // Phân quyền
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                // Nếu là admin thì vào trang quản trị
                if (khachHang.VaiTro == 1)
                {
                    return RedirectToAction("Index", "Admin"); // Chuyển hướng đến trang admin
                }

                return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : Redirect("/");
            }
            return View(model);
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {

            var user = db.KhachHangs.FirstOrDefault(kh => kh.MaKh == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("DangNhap");
            }

            var model = new ProfileVM
            {
                MaKh = user.MaKh,
                TenKh = user.TenKh,
                Sdt = user.Sdt,
                Email = user.Email,
                Hinh = user.Hinh
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> TroVe()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("DangNhap");
        }
    }
}

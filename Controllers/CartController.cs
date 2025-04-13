using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuoc.Data;
using QuanLyThuoc.Helper;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.Controllers
{
    public class CartController : Controller
    {
        private readonly QuanlythuocContext db;

        public CartController(QuanlythuocContext context)
        {
            db = context;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quanlity = 1)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaThuoc == id);
            if (item == null)
            {
                var hanghoa = db.Thuocs.SingleOrDefault(p => p.MaThuoc == id);
                if (hanghoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa nào có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaThuoc = hanghoa.MaThuoc,
                    TenThuoc = hanghoa.TenThuoc,
                    GiaBan = hanghoa.GiaBan,
                    HinhAnh = hanghoa.HinhAnh,
                    SoLuong = quanlity
                };
                giohang.Add(item);
            }
            else
            {
                item.SoLuong += quanlity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, giohang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaThuoc == id);
            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, giohang); // update lại giỏ
            }
            return RedirectToAction("Index"); // quay về index
        }

		[Authorize]
		[HttpGet]
		public IActionResult Checkout()
		{
			if (Cart.Count == 0)
			{
				return Redirect("/");
			}

			return View(Cart);
		}

		[Authorize]
		[HttpPost]
		public IActionResult Checkout(CheckoutVM model)
		{
			if (ModelState.IsValid)
			{
				var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;
				var khachHang = new KhachHang();
				if (model.GiongKhachHang)
				{
					khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerId);
				}

				var hoadon = new HoaDon
				{
					MaKh = customerId,
					HoTen = model.HoTen ?? khachHang.TenKh,
					DiaChi = model.DiaChi ?? khachHang.DiaChi,
					DienThoai = model.DienThoai ?? khachHang.Sdt,
					NgayDat = DateTime.Now,
					CachThanhToan = "COD",
					CachVanChuyen = "GRAB",
					MaTrangThai = 0,
					GhiChu = model.GhiChu
				};

				db.Database.BeginTransaction();
				try
				{
					db.Database.CommitTransaction();
					db.Add(hoadon);
					db.SaveChanges();

					var cthds = new List<ChiTietHoaDon>();
					foreach (var item in Cart)
					{
						cthds.Add(new ChiTietHoaDon
						{
							MaHd = hoadon.MaHd,
							SoLuong = item.SoLuong,
							DonGia = item.GiaBan,
							MaThuoc = item.MaThuoc,
							GiamGia = 0
						});
					}
					db.AddRange(cthds);
					db.SaveChanges();

					HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

					return View("Success");
				}
				catch
				{
					db.Database.RollbackTransaction();
				}
			}

			return View(Cart);
		}
	}
}

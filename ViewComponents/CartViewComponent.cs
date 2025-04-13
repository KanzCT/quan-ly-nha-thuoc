using Microsoft.AspNetCore.Mvc;
using QuanLyThuoc.Helper;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>
                    (MySetting.CART_KEY) ?? new List<CartItem>();

            var model = new CartModel
            {
                Quanlity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            };

            return View("CartPanel", model);
        }
    }
}

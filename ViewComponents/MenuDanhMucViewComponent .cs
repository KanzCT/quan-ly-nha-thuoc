using Microsoft.AspNetCore.Mvc;
using QuanLyThuoc.Data;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.ViewComponents
{
    public class MenuDanhMucViewComponent : ViewComponent
    {
        private readonly QuanlythuocContext db;
        public MenuDanhMucViewComponent(QuanlythuocContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.DanhMucs
            .Select(dm => new MenuDanhmucVM
            {
                MaDanhMuc = dm.MaDanhMuc,
                TenDanhMuc = dm.TenDanhMuc,
                Soluong = dm.Thuocs.Count
            })
            .ToList() // Lấy dữ liệu trước
            .OrderBy(p => p.TenDanhMuc) // Sau đó mới sắp xếp trên bộ nhớ
            .ToList();



            return View(data);
        }
    }
}

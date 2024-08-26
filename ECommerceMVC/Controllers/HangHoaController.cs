using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context) 
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var hangHoa = db.HangHoas.AsQueryable();

            if (loai.HasValue)
            {
                hangHoa = hangHoa.Where(p => p.MaLoai == loai.Value);
            }
            var result = hangHoa.Select(p => new HangHoaVM
            {
                Id = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai


            });
            return View(result);
        }
        public IActionResult Search(string? query) 
        {
            var hangHoa = db.HangHoas.AsQueryable();

            if (query != null)
            {
                hangHoa = hangHoa.Where(p => p.TenHh.Contains(query));
            }
            var result = hangHoa.Select(p => new HangHoaVM
            {
                Id = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        
        public IActionResult ProductDetail (int? id)
        {
            var dataProduct = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (dataProduct == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã{id}";
                return Redirect("/404");
            }

            var result = new ChiTietHangHoaVM
            {
                MaHH = dataProduct.MaHh,
                TenHH = dataProduct.TenHh,
                DonGia = dataProduct.DonGia ?? 0,
                ChiTiet = dataProduct.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                Hinh =dataProduct.Hinh ?? string.Empty,
                TenLoai = dataProduct.MaLoaiNavigation.TenLoai,
                MoTaNgan = dataProduct.MoTa ?? string.Empty,
                SoLuongTon = 10,

            };

            return View(result);
        }
    }
}

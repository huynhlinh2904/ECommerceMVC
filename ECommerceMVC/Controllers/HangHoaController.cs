using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    }
}

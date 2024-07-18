using ECommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class MenuLoai : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoai(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new
            {
                loai.MaLoai,
                loai.TenLoai,
                soluong = loai.HangHoas.Count
            }).ToList();
            return View();
        }


    }
}

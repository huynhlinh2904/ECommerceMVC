using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class MenuLoai : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoai(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new MenuLoaiVM
            {
                maLoai = loai.MaLoai,
                tenLoai = loai.TenLoai,
                soLuong = loai.HangHoas.Count
            }).OrderBy(p => p.tenLoai);
            return View(data); // default cshtml
        }


    }
}

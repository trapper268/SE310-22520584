using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.Authentication;
using ThucHanhWebMVC.ViewModels;
using X.PagedList;

namespace ThucHanhWebMVC.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (int)((page == null || page < 0) ? 1 : page);
            var listsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x=>x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(listsanpham, pageNumber, pageSize);
            return View(lst);
        }
        //[Authentication]
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = (int)((page == null || page < 0) ? 1 : page);
            var listsanpham = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(listsanpham, pageNumber, pageSize);
            ViewBag.MaLoai = maloai;
            return View(lst);
        }

        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x=>x.MaSp == maSp);
            var anhSanPham=db.TAnhSps.Where(x=>x.MaSp==maSp).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel=new HomeProductDetailViewModel { 
                danhMucSp = sanPham, 
                anhSps = anhSanPham 
            };
            return View(homeProductDetailViewModel);
        }

        public IActionResult Privacy()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(TUser user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra username đã tồn tại chưa
                var checkUser = db.TUsers.FirstOrDefault(u => u.Username == user.Username);
                if (checkUser != null)
                {
                    ModelState.AddModelError("", "Username đã tồn tại!");
                    return View(user);
                }

                try
                {
                    // Set default values
                    user.LoaiUser = 0; // Mặc định là customer (0)

                    db.TUsers.Add(user);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi đăng ký: " + ex.Message);
                    return View(user);
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TUser user) 
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u=db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}

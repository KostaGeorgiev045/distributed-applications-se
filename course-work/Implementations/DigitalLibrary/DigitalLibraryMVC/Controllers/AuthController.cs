using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login(string token)
        {
            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckToken()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return Content("Token not found");
            }
            return Content("Token: " + token);
        }
    }
}

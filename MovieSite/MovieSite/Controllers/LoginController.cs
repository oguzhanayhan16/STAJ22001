using EntityLayer.Concrete;
using DataAccessLayer.Concrate;
using BusinessLayer.ValidationRules;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using DataAccessLayer.EntitiyFramework;
using BusinessLayer.Concrate;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace MovieSite.Controllers
{
    public class LoginController : Controller
    {
        UserManager userManager = new UserManager(new EFUserRepository());
        Context c = new Context();
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            LoginValidator validator = new LoginValidator();
            ValidationResult results = validator.Validate(user);

            if (results.IsValid)
            {
                string hashedPassword = HashPassword(user.Password);
                var values = c.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == hashedPassword);

                if (values != null)
                {
                    values.LastLogin = DateTime.Now;  // Doğru kullanıcıyı güncelleme
                    userManager.TUpdate(values);      // Güncellenen kullanıcıyı veritabanına kaydet

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("LogMovie", "Movie");  // Kullanıcıyı doğru şekilde yönlendirin
                }
                else
                {
                    ModelState.AddModelError("Password", "Bu e-posta adresiniz ya da şifreniz yanlıştır.");
                    ModelState.AddModelError("Email", "Bu e-posta adresiniz ya da şifreniz yanlıştır.");
                }
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(user);
        }

    private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Movie");
        }
    }


}

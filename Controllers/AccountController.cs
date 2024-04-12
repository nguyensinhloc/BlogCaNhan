using BlogCaNhan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogCaNhan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        // GET: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);
        }
        // GET: Account/Login
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByNameAsync(login.UserName);
                if(appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
                        appUser, login.Password, false, false
                        );
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Blog", new {area = "Admin"});
                    }
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View(login);
        }
        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

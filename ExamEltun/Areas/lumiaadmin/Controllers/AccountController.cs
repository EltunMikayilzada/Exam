using ExamEltun.Models;
using ExamEltun.Utilities.Enum;
using ExamEltun.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamEltun.Areas.lumiaadmin.Controllers
{
    [Area("lumiaadmin")]

        public class AccountController : Controller
        {
            readonly UserManager<AppUser> _userManager;
            readonly SignInManager<AppUser> _signInManager;
            readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
        }
            public IActionResult Register()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Register(RegisterVM newuser)
            {
                AppUser user = new AppUser
                {
                    Name = newuser.Name,
                    Email = newuser.Email,
                    Surname = newuser.Surname,
                    UserName = newuser.Username
                };

                IdentityResult error = await _userManager.CreateAsync(user, newuser.Password);
                if (!error.Succeeded)
                {
                    foreach (var item in error.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                        return View();
                    }
                }
               

                await _signInManager.SignInAsync(user, false);


                return RedirectToAction("Index", "Home");

            }
            public async Task<IActionResult> LogOut()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");

            }
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Login(LoginVM user)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                AppUser existed = await _userManager.FindByEmailAsync(user.UsernameOrEmail);
                if (existed == null)
                {
                    existed = await _userManager.FindByNameAsync(user.UsernameOrEmail);
                    if (existed == null)
                    {
                        ModelState.AddModelError(string.Empty, "Email Username ve password sehfdir");
                        return View();
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(existed, user.Password, user.RememberMe, false);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Email Username ve password sehfdir");
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (!(await _roleManager.RoleExistsAsync(item.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
            return View();
        }

    }
}

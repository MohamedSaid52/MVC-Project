using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.DAL.Entities;
using MVC.PL.Helper;
using MVC.PL.Models;
using NuGet.Common;

namespace MVC.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Sign Up
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email=model.Email,
                    IsAgree=model.IsAgree

                };
                var result=await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                
                    return RedirectToAction("SignIn");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
            }
            return View(model);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
              var user=await userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    ModelState.AddModelError("","Invalid Email");
                var password = await userManager.CheckPasswordAsync(user,model.Password);
                if (password)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)

                        return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }
        #endregion

        #region Sign Out
        public async  Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var token=await userManager.GeneratePasswordResetTokenAsync(user);
                    var restpasswordlink = Url.Action("RestPassword", "Account", new {Email=model.Email,Token=token},Request.Scheme);
                    var email = new Email()
                    {
                        Title = "RestPassword",
                        Body=restpasswordlink,
                        To = model.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError("","InvalidEmail");
            }
            return View(model);
        }
        #endregion

        #region Complete Forget Password
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region Rest Password
        public IActionResult RestPassword(string email,string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RestPassword(RestPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user,model.Token,model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(RestPasswordDone));
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);  
        }
        #endregion
        public IActionResult RestPasswordDone()
        {
            return View();
        }
    }
}

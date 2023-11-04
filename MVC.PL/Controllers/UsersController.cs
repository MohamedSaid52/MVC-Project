using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.BLL.Repositories;
using MVC.DAL.Entities;
using MVC.PL.Models;

namespace MVC.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UsersController> logger;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
           
        }
        public async Task<IActionResult> Index(string SearchValue="")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = userManager.Users;
                return View(users); 
            }
            else
            {
                var users=await userManager.Users.Where(x=>x.NormalizedEmail.Contains(SearchValue.ToUpper())).ToListAsync();
                return View(users);
            }
        }
        public async Task<IActionResult> Details(string id,string viewName="Deatils")
        {
            if (id is null)
                return NotFound();
            var user=await userManager.FindByIdAsync(id);
            if(user is null)
                return NotFound();
            return View(viewName,user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id,"Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var appuser=await userManager.FindByIdAsync(id);
                    appuser.UserName=user.UserName;
                    appuser.NormalizedUserName=user.UserName.ToUpper();
                    appuser.PhoneNumber=user.PhoneNumber;
                    var result= await userManager.UpdateAsync(appuser);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id,ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();
            try
            {
                var appuser= await userManager.FindByIdAsync(id);
                var result=await userManager.DeleteAsync(appuser);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                ViewBag.Errors = result.Errors;
            }
           catch(Exception ex)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }             
    }
}

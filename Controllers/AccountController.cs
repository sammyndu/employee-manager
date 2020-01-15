using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EManager3.Models;
using EManager3.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
 
namespace EManager3.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<EManager3User> userManager;
        private SignInManager<EManager3User> signInManager;
 
        public AccountController(UserManager<EManager3User> userMgr, SignInManager<EManager3User> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [TempData]
        public string StatusMessage { get; set; }
 
        // public IActionResult Index()
        // {
        //     return View();
        // }
 
        
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }
 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                EManager3User appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    if (!appUser.isActive){
                        // StatusMessage = "Your account has not been activated";
                        ModelState.AddModelError(string.Empty, "Your account has not been activated");
                        return View(login);
                        // return RedirectToAction("Login");
                    }
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                    if (result.Succeeded)
                        if (await userManager.IsInRoleAsync(appUser, "Admin")){
                            return RedirectToAction("Index", "Admin");
                        }
                        else {
                            return RedirectToAction("Index", "Employee");
                        }
                        // return Redirect(login.ReturnUrl ?? "/");
                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        
        public ViewResult Register() => View();
 
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                EManager3User appUser = new EManager3User
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Sex = user.Sex,
                    DOB = user.DOB,
                    MaritalStatus = user.MaritalStatus,
                    NumberOfChildren = user.NumberOfChildren,
                };
 
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded){
                    result = await userManager.AddToRoleAsync(appUser, "Employee");
                    if (result.Succeeded){
                        ViewBag.StatusMessage = "Your account has been registered";
                        return View(user);
                    }
                }                    
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
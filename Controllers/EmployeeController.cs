using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EManager3.Areas.Identity.Data;
using EManager3.Data;
using EManager3.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace EManager3.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private  UserManager<EManager3User> userManager;
        private IPasswordHasher<EManager3User> passwordHasher;
        private SignInManager<EManager3User> signInManager;

        public EmployeeController(
            UserManager<EManager3User> usrMgr,
            IPasswordHasher<EManager3User> passwordHash,
            SignInManager<EManager3User> signinMgr)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            signInManager = signinMgr;
        }
        public async Task<IActionResult> Index()
        {            
            var user = await userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            EManager3User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(EManager3User updateduser)
        {

            EManager3User user = await userManager.FindByIdAsync(updateduser.Id);
            if (user != null)
            {
                user.Email = updateduser.Email;
                user.UserName = updateduser.Email;
                user.FirstName = updateduser.FirstName;
                user.LastName = updateduser.LastName;
                user.Sex = updateduser.Sex;
                user.DOB = updateduser.DOB;
                user.MaritalStatus = updateduser.MaritalStatus;
                user.NumberOfChildren = updateduser.NumberOfChildren;

                if (!string.IsNullOrEmpty(updateduser.Email) && !string.IsNullOrEmpty(updateduser.FirstName) && !string.IsNullOrEmpty(updateduser.LastName)
                && !string.IsNullOrEmpty(updateduser.Sex) && !string.IsNullOrEmpty(updateduser.MaritalStatus))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else{
                        Errors(result);
                    }
                }                    

            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
            
        }

        public string oldpassword {get; set;}
        public string newpassword {get; set;}
        public string confirmpassword {get; set;}

        public IActionResult ChangePassword()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                IdentityResult result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);                
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(model);
        }

        public async Task<IActionResult> Delete()
        {
            var user = await userManager.GetUserAsync(User);
            return View(user);            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            EManager3User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded){
                    await signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No User Found");
            return View("Index", user);
            
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        
    }
}
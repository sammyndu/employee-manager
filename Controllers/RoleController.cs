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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace EManager3.Controllers
{
    public class RoleController : Controller
    {
        private  RoleManager<IdentityRole> roleManager;
        private  UserManager<EManager3User> userManager;


        public RoleController(
            RoleManager<IdentityRole> roleMgr,
            UserManager<EManager3User> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }
        public ViewResult Index() => View(roleManager.Roles);

        [Authorize(Roles="Developer")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles="Developer")]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }
        
        [Authorize(Roles="Developer, Admin")]
        public async Task<IActionResult> Update(string id)
        {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<EManager3User> members = new List<EManager3User>();
            List<EManager3User> nonmembers = new List<EManager3User>();
            foreach (EManager3User user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers,
            });
            
        }

        [Authorize(Roles="Developer, Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    EManager3User user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    EManager3User user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                            Errors(result);
                    }
                }
                                   

            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return await Update(model.RoleId);            
        }


        [Authorize(Roles="Developer")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
                                   

            }
            else
                ModelState.AddModelError("", "No Role Found");
            return View("Index", roleManager.Roles);
            
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

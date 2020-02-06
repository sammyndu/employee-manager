using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EManager3.Areas.Identity.Data;
using EManager3.Data;
using EManager3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EManager3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private  UserManager<EManager3User> userManager;
        private IPasswordHasher<EManager3User> passwordHasher;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<EManager3User> usrMgr,
            IPasswordHasher<EManager3User> passwordHash,
            ApplicationDbContext context)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            _context = context;
        }
        
        public List<EManager3User> users { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public async Task<IActionResult> Details(string id)
        {
            EManager3User user = await userManager.Users.Include(x => x.Subordinates).Where(x => x.Id == id).SingleOrDefaultAsync();
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string id)
        {
            List<EManager3User> users = new List<EManager3User>();
            List<Subordinates> members = new List<Subordinates>();
            EManager3User user = await userManager.Users.Include(x => x.Subordinates).Where(x => x.Id == id).SingleOrDefaultAsync();
            members = _context.Subordinates.Where(x => x.UserId == id).ToList();
            
            foreach (EManager3User u in userManager.Users)
            {
                users.Add(u);
            }
            if (user != null)
            {
                // return View(user);
                return View(new SubordinateEdit{
                    Users = users,
                    User=user,
                    Members = members
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(SubordinateMod updateduser)
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
                user.isActive = updateduser.isActive;
                user.NumberOfChildren = updateduser.NumberOfChildren;
                user.EmploymentDate = updateduser.EmploymentDate;
                user.HighestEducationQualification = updateduser.HighestEducationQualification;
                user.School = updateduser.School;
                user.ServiceYear = updateduser.ServiceYear;
                user.CompanyPosition = updateduser.CompanyPosition;
                user.LastPromotionDate = updateduser.LastPromotionDate;
                user.YearlySalary = Convert.ToInt32(updateduser.YearlySalary);

                if (!string.IsNullOrEmpty(updateduser.Email) && !string.IsNullOrEmpty(updateduser.FirstName) && !string.IsNullOrEmpty(updateduser.LastName)
                && !string.IsNullOrEmpty(updateduser.Sex) && !string.IsNullOrEmpty(updateduser.MaritalStatus))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        {
                            var result2 = _context.Subordinates.Where(x => x.UserId == updateduser.Id).ToList();
                            foreach (var sub in result2){
                                _context.Subordinates.Remove(sub);
                            }
                            await _context.SaveChangesAsync();
                            
                            foreach (var newsub in updateduser.AddIds ?? new string[] { }){
                                var subs = new Subordinates{
                                    UserId=updateduser.Id,
                                    SubordinateId=newsub
                                };
                                _context.Subordinates.Add(subs);
                            }

                            await _context.SaveChangesAsync();

                            return RedirectToAction("Index");
                        }
                    else{
                        Errors(result);
                    }
                }                    

            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
            
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            // var context = new Data.ApplicationDbContext();
            EManager3User user = await userManager.FindByIdAsync(id);
            
            return View(new PasswordEdit{
                User = user,
                Password =password,
                ConfirmPassword = confirmpassword
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, string password)
        {
            // var context = new Data.ApplicationDbContext();
            EManager3User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {        
                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");
        
                if (!string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            EManager3User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No User Found");
            return View("Index", userManager.Users);
            
        }

        public IActionResult Search(string Active)
        {
            if (Active == "yes"){
                var users = userManager.Users.Where(x => x.isActive==true).ToList();
                return View("Index", users);
            }
            else if(Active == "no"){
                var users = userManager.Users.Where(x => x.isActive==false).ToList();
                return View("Index", users);
            }
            else{
                return RedirectToAction("Index");
            }
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

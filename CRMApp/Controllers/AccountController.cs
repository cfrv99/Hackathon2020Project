using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMApp.Models;
using CRMApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            AppUser appUser = new AppUser
            {
                Name = viewModel.Name,
                UserName = viewModel.UserName,
                Email = viewModel.Email

            };

            var isUserHas = await userManager.FindByEmailAsync(viewModel.Email);
            if (isUserHas != null)
            {
                return BadRequest("User already exist");
            }

            var result = await userManager.CreateAsync(appUser, viewModel.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(appUser, viewModel.Password, false, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("","User Can not be created");
            return View(viewModel);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vM)
        {
            if (ModelState.IsValid)
            {
                var userExist = await userManager.FindByEmailAsync(vM.Email);
                if (userExist != null)
                {
                    var result = await signInManager.PasswordSignInAsync(userExist, vM.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    ModelState.AddModelError("", "user password or username incorrect");
                }
            }

            return View(vM);
        }
    }
}
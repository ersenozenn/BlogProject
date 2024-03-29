﻿using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")] //api/post
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ICoreService<User> us;
        public AccountController(ICoreService<User> _us)
        {
            us = _us;
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(User item)
        {
            if (us.Any(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password))
            {
                User logged = us.GetbyDefault(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password);

                var claims = new List<Claim>()
                {
                    new Claim("ID",logged.ID.ToString()),
                    new Claim(ClaimTypes.Name,logged.FirstName),
                    new Claim(ClaimTypes.Surname,logged.LastName),
                    new Claim(ClaimTypes.Email,logged.EmailAddress),
                    new Claim("Image",logged.ImageURL)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                //localhost:PortNo/Adminstrator/Home/Index
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(item);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

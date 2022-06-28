﻿using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {    
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        public HomeController(ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us)
        {           
            cs = _cs;
            ps = _ps;
            us = _us;
        }
        public IActionResult Index()
        {
            ViewBag.PostsCount = ps.GetActive().Count;
            ViewBag.CategoriesCount = cs.GetActive().Count;
            ViewBag.UsersCount = us.GetActive().Count;     
            return View();
        }
    }
}

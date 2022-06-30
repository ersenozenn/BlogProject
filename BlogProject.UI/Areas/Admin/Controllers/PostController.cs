using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PostController : Controller
    {
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        public PostController(ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us)
        {
            cs = _cs;
            ps = _ps;
            us = _us;
        }
        public IActionResult ListPosts()
        {
            return View(ps.GetAll());
        }
        public IActionResult AddPost()
        {
            ViewBag.Categories = cs.GetActive();
            ViewBag.Users = us.GetActive();
            //It is showing AddPost View
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(IFormCollection gelenMakale)
        {
           
            try
            {
                Post eklenecekPost = new Post();
                eklenecekPost.Title = gelenMakale["title"];
                eklenecekPost.PostDetail = gelenMakale["detail"];
                eklenecekPost.Tags = gelenMakale["tags"];
                eklenecekPost.ImagePath = gelenMakale["imagePath"];
                eklenecekPost.ViewCount = Convert.ToInt32(gelenMakale["viewCount"]);
                eklenecekPost.CategoryID = Guid.Parse(gelenMakale["categoryId"]);
                eklenecekPost.UserID = Guid.Parse(HttpContext.User.FindFirst("ID").Value);

                ps.Add(eklenecekPost);

                return RedirectToAction("ListPosts");
            }
            catch
            {

                return View();
            }
        }
        public IActionResult ActivatePost(Guid id)
        {
            ps.Activate(id);
            return RedirectToAction("ListPosts");
        }
    }
}

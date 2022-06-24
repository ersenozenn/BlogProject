using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using BlogProject.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogProject.UI.Controllers
{
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
        public IActionResult PostByID(Guid id)
        {
            SinglePostVM singlePost = new SinglePostVM();
            singlePost.Post = ps.GetByID(id);
            singlePost.User = us.GetbyDefault(x => x.ID == singlePost.Post.UserID);
            ViewBag.Categories = cs.GetActive();
            return View(singlePost);
        }
        public IActionResult PostByAuthor(Guid id)
        {
            ViewBag.Author = us.GetByID(id).FirstName+" "+ us.GetByID(id).LastName;
            return View(ps.GetDefault(x=>x.UserID==id));
        }
    }
}

using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using BlogProject.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            ViewBag.RandomPosts = ps.GetActive().Where(x => x.CategoryID == ps.GetByID(id).CategoryID).Take(3).ToList();
            return View(singlePost);
        }
        public IActionResult PostByAuthor(Guid id)
        {
            ViewBag.Author = us.GetByID(id).FirstName+" "+ us.GetByID(id).LastName;
            return View(ps.GetDefault(x=>x.UserID==id));
        }
        public IActionResult PostByCategory(Guid id)
        {
            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = ps.GetDefault(x=>x.CategoryID==id);
            postUserVM.Users = us.GetAll();

            return View(postUserVM);
        }
        [HttpPost]
        public IActionResult PostBySearch(string query) 
        {
            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = ps.GetDefault(x => x.Title.Contains(query));
            postUserVM.Users = us.GetAll();
            return View(postUserVM);
   
        }
    }
}

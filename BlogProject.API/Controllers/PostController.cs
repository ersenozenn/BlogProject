using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")] //api/post
    [ApiController]  //Validasyon işlemleri otomatik olarak yapılır.    
    public class PostController : ControllerBase
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
        [HttpGet]
        [Route("[action]")] //api/post/listposts
        public IActionResult ListPosts()
        {
            var allPost = ps.GetAll();
            return Ok(allPost); //200 status code ve post datasını döndürecek.
        }
        //public IActionResult AddPost()
        //{
        //    ViewBag.Categories = cs.GetActive();
        //    ViewBag.Users = us.GetActive();
        //    //It is showing AddPost View
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddPost(IFormCollection gelenMakale)
        //{

        //    try
        //    {
        //        Post eklenecekPost = new Post();
        //        eklenecekPost.Title = gelenMakale["title"];
        //        eklenecekPost.PostDetail = gelenMakale["detail"];
        //        eklenecekPost.Tags = gelenMakale["tags"];
        //        eklenecekPost.ImagePath = gelenMakale["imagePath"];
        //        eklenecekPost.ViewCount = Convert.ToInt32(gelenMakale["viewCount"]);
        //        eklenecekPost.CategoryID = Guid.Parse(gelenMakale["categoryId"]);
        //        eklenecekPost.UserID = Guid.Parse(HttpContext.User.FindFirst("ID").Value);

        //        ps.Add(eklenecekPost);

        //        return RedirectToAction("ListPosts");
        //    }
        //    catch
        //    {

        //        return View();
        //    }
        //}
        [HttpGet]
        [Route("[action]/{id?}")]
        public IActionResult ActivatePost(Guid id)
        {
            ps.Activate(id);
            return Redirect("https://localhost:44326/Adminstrator/Post/ListPosts");
        }
    }
}

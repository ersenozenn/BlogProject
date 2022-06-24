using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using BlogProject.UI.Models;
using BlogProject.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
      


        public HomeController(ILogger<HomeController> logger, ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us)
        {
            _logger = logger;
            cs=_cs; 
            ps=_ps;
            us=_us;
        }

        public IActionResult Index()
        {
            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = ps.GetActive();
            postUserVM.Users = us.GetAll();
            
            return View(postUserVM);
        }
        public IActionResult PostByID(Guid id)
        {
            SinglePostVM singlePost = new SinglePostVM();
            singlePost.Post = ps.GetByID(id);
            singlePost.User = us.GetbyDefault(x=>x.ID== singlePost.Post.UserID);
            return View(singlePost);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

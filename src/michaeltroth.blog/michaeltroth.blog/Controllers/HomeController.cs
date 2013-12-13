using michaeltroth.blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace michaeltroth.blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration=600)]
        public ActionResult BlogEntry(DateTime publishedDate, string topic)
        {
            var blog = Blog.GetBlogById(publishedDate, topic);

            if (blog == null)
                return HttpNotFound("The post could not be found");


            return View(blog);
        }

        [OutputCache(Duration=60)]
        public PartialViewResult BlogRoll()
        {
            var blogs = Blog.GetBlogs();
            return PartialView(blogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to get in contact with me.";

            return View();
        }
    }
}
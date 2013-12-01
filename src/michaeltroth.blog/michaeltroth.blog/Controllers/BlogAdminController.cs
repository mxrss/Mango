using MagicSauce.Filters;
using michaeltroth.blog.Models;
using michaeltroth.blog.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace michaeltroth.blog.Controllers
{
    public class BlogAdminController : Controller
    {
        //
        // GET: /BlogAdmin/
        public ActionResult Index()
        {
            var blogs = Blog.GetBlogs();
            return View(blogs);
        }

        

        [HttpGet, ImportModelStateFromTempData]
        public ActionResult Create()
        {
            BlogCreationRequestInputModel model = new BlogCreationRequestInputModel();
            return View(model);
        }

        [HttpPost, ExportModelStateToTempData]
        public ActionResult Create(BlogCreationRequestInputModel model)
        {
            if (ModelState.IsValid)
            {
                Blog.CreateBlog(model);
                TempData.Add("status", "Craeted Successfully");
                return RedirectToAction("index");
            }

            return RedirectToAction("create");
        }

        [HttpGet, ImportModelStateFromTempData]
        public ActionResult Edit(string id)
        {
            var blog = Blog.GetBlogById(id);
            BlogEditRequestInputModel model = new BlogEditRequestInputModel();
            return View(model.ToInputModel(blog));
        }


        [HttpPost, ExportModelStateToTempData]
        public ActionResult Edit(string id, BlogEditRequestInputModel model)
        {
            if (ModelState.IsValid)
            {
                Blog.EditBlog(model);
                TempData.Add("status", "Craeted Successfully");
                return RedirectToAction("index");
            }

            return RedirectToAction("edit", new { id = id });
        }
	}
}
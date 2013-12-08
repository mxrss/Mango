using michaeltroth.blog.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace michaeltroth.blog.Controllers
{
    public class SessionController : Controller
    {
        //
        // GET: /Session/
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(LoginInputModel model)
        {
            if (!FormsAuthentication.Authenticate(model.UserName, model.Password))
            {
                TempData.Add("invalid_user", "Username or password is invalid");
                return RedirectToAction("new"); 
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("BlogAdmin");
            }

        }

	}
}
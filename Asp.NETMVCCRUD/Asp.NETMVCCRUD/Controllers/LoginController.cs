using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
namespace Asp.NETMVCCRUD.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(User userModel)
        {
            using (DBModel db = new DBModel())
            {
                var UserDetail = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if(UserDetail == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = UserDetail.UserID;
                    Session["userName"] = UserDetail.UserName;
                    return RedirectToAction("Index", "Employee");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
using Doan.Models;
using Doan.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Db_Doan _db = new Db_Doan();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //method Login
        [HttpGet]
        public ActionResult Authen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authen(User _user)
        {
            var f_password = GetMD5(_user.Password);
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email)
            && s.Password.Equals(f_password)).FirstOrDefault();
            if (check == null)
            {
                ViewBag.error = "Login failed";
                return View("Index", _user);
            }
            else
            {
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (test.Email == "admin@gmail.com")
                {
                  
                    var userSession = new UserLogin();
                    userSession.UserName = test.UserName;
                    userSession.UserID = test.IDUser;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Products");
                    //Session["IDUser"] = _user.IDUser;
                    //Session["Email"] = _user.Email;
                    //return RedirectToAction("Index", "Products");
                }
                else
                {
                    return RedirectToAction("IndexUser", "Home");
                }

            }
        }
        //method Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null) //email chua co nguoi dang ky
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exist! Use another email!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");//method login la Index
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}
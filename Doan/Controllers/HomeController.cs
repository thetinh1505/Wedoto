using Doan.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Doan.Models.MD;

namespace Doan.Controllers
{
    public class HomeController : Controller
    {
        Db_Doan db = new Db_Doan();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexUser()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Menu()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult slide()
        {
            return View(db.Slides.ToList());
        }

        public ActionResult NewProducts()
        {
            return View(db.Products.OrderBy(m=>m.CreatedDate).Take(10).ToList());
        }

        public ActionResult bestbook()
        {
            var result = from r in db.Products
                         orderby r.Quantity
                         select r;
            return View(result.ToList()); 
        }

        public ActionResult BestSale()
        {
            var bs = new BestSaleMD();
            return View(bs.GetBestSale());
        }

         public ActionResult CategoryList()
        {
            return View(db.Categories.ToList());
        }

        public ViewHistory GetViewHistory()
        {
            ViewHistory vh = Session["ViewHistory"] as ViewHistory;
            if (vh == null || Session["ViewHistory"] == null)
            {
                vh = new ViewHistory();
                Session["ViewHistory"] = vh;
            }
            return vh;
        }

        public ActionResult ProductDetail(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var pro = db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetViewHistory().Add(pro);
            }
            return View(product);
        }
        public ActionResult ShowToGetViewHistoryt()
        {
            ViewHistory vh = Session["ViewHistory"] as ViewHistory;
            return View(vh);
        }

        public ActionResult MoreProduct(long? id)
        {
            var result = from r in db.Products
                         where r.CategoryID == id
                         select r;
            //ViewBag.Count = result.Count();
            return PartialView("MoreProduct", result);
        }

        public ActionResult ProductList(long? id, int ? page)
        {
            var result = from r in db.Products
                         where r.CategoryID == id
                         select r;
            //ViewBag.Count = result.Count();
            return PartialView("ProductList", result.ToList().ToPagedList(page??1,9));
        } 
    }
}
using Doan.Models;
using Doan.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        Db_Doan db = new Db_Doan();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BestSale()
        {
            var bs = new BestSaleMD();
            return View(bs.GetBestSale().OrderByDescending(m=>m.Quantity));
        }
    }
}
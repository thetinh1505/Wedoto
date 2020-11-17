using Doan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        Db_Doan db = new Db_Doan();
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            cus.CreatedDay = DateTime.Now;
            db.Customers.Add(cus);
            db.SaveChanges();
            return RedirectToAction("Order_Success", "ShoppingCart");
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Customers.Add(cus);
            //        db.SaveChanges();
            //        return RedirectToAction("ShowToCart", "ShoppingCart");
            //    }
            //    return View();
            //}
            //catch
            //{
            //    return Content("Error");
            //}
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ModifiedDay = DateTime.Now;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}

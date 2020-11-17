using Doan.Models;
using Doan.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        Db_Doan db = new Db_Doan();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(long id)
        {
            var pro = db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowToCart()
        {

            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Order_Success()
        {
            return View();
        }

        public ActionResult CartShow()
        {
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult CartShowMenu()
        {
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_Cart_Item(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        public ActionResult Shopping_Success()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProceedOrder()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProceedOrder(Customer cus)
        {

            var check = db.Customers.Where(s => s.Email_Cus.Equals(cus.Email_Cus)
             && s.Password.Equals(cus.Password)).FirstOrDefault();
            if (check == null)
            {
               return Content("Error checkout!!!!!");
            }
            else
            {
                try
                {
                    Cart cart = Session["Cart"] as Cart;

                    var result = from r in db.Customers
                                 where r.Email_Cus == cus.Email_Cus
                                 select r;
                    var cus2 = result.ToList().First();
                    Order _order = new Order();
                    _order.OrderDate = DateTime.Now;
                    _order.Email_Cus = cus2.Email_Cus;
                    _order.SDT_Cus = cus2.Phone_Cus;
                    _order.Password_cus = cus2.Password;
                    _order.Descriptions = cus2.Address_Cus;
                    _order.CodeCus = cus2.CodeCus;
                    db.Orders.Add(_order);
                    foreach (var item in cart.Items)
                    {
                        OrderDetail _order_Detail = new OrderDetail();
                        _order_Detail.IDOrder = _order.IDOrder;
                        _order_Detail.IDProduct = item._shopping_product.IDProduct;
                        _order_Detail.UnitPriceSale = item._shopping_product.Price;
                        _order_Detail.QuantitySale = item._shopping_quantity;
                        db.OrderDetails.Add(_order_Detail);
                    }
                    db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("Shopping_Success", "ShoppingCart");
                }
                catch
                {
                    return Content("Error checkout!!!!!");
                }

            }
        }
    }
}
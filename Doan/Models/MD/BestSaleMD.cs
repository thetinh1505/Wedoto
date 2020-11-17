using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doan.Models.MD
{
    public class BestSaleMD:BestSale
    {
        public IEnumerable<BestSale> GetBestSale()
        {
            Db_Doan db = new Db_Doan();
            var bestsale = from bs in db.OrderDetails.AsEnumerable()
                           group bs by bs.IDProduct into g
                           orderby g.Sum(x => x.IDProduct) descending
                           select new BestSale
                           {
                               IDPro = g.Key,
                               Image=g.First().Product.Image,
                               NamePro = g.First().Product.ProductName,
                               Price = g.First().Product.Price,
                               Quantity = g.Sum(x => x.QuantitySale)
                           };
            return bestsale.Take(9).ToList();
        }
    }
}
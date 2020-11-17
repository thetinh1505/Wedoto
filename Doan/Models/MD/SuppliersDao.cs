using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Doan.Models.Dao
{
    public class SuppliersDao
    {
        Db_Doan db = null;
        public SuppliersDao()
        {
            db = new Db_Doan();
        }



        public IEnumerable<Supplier> ListAllPaging(string searchString, int page = 1, int pageSize = 5)
        {
            IQueryable<Supplier> model = db.Suppliers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.SupplierName.Contains(searchString) || x.SupplierName.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
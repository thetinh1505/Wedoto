using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Doan.Models.Dao
{
    public class CategoryDao
    {
        Db_Doan db = null;
        public CategoryDao()
        {
            db = new Db_Doan();
        }
      
       
       
        public IEnumerable<Category> ListAllPaging(string searchString, int page = 1, int pageSize =5)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CategoryName.Contains(searchString) || x.CategoryName.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }  
    }
}
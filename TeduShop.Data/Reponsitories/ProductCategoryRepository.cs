using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface IProductCategory
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategory
    {
        public ProductCategoryRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);

        }
    }
}

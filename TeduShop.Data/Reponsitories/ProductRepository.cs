using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> GetAllByTag(string tag,int pageIndex, int pageSize, out int totalRow);
    }
    public class ProductRepository:RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory):base(dbFactory)
        {
           
        }

        public IEnumerable<Product> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface ISizeRepository : IRepository<Size>
    {
        IEnumerable<Size> GetSizeByProductId(int? productId);
    }
   public class SizeRepository:RepositoryBase<Size>,ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

        public IEnumerable<Size> GetSizeByProductId(int? productId)
        {
            var query = from s in DbContext.Sizes
                        join q in DbContext.ProductQuantities
                        on s.ID equals q.SizeId
                        where q.ProductId == productId
                        orderby s.Name
                        select s;
            return query;

        }
    }
}

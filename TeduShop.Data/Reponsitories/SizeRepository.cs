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

    }
   public class SizeRepository:RepositoryBase<Size>,ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}

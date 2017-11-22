using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface IPostCategoryRepository: IRepository<PostCategory>
    {
        

    }
    public class PostCategotyRepository:RepositoryBase<PostCategory>,IPostCategoryRepository
    {
        public PostCategotyRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

       
    }
}

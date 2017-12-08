using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface ITagRepository: IRepository<Tag>
    {

    }
   public class TagRepository:RepositoryBase<Tag>,ITagRepository
    {
        public TagRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}

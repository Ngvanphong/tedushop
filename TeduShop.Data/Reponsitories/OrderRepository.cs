using TeduShop.Data.Inframestructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Reponsitories
{
    public interface IOrderRepository : IRepository<Order>
    {
       
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
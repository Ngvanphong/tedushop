using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;

namespace TeduShop.Service
{
    public interface IUserRoleServie
    {
        void Delete(string roleId);
        void DeleteByUserId(string userId);

        void SaveChange();
    }

    public class UserRoleService : IUserRoleServie
    {
        private IUserRoleRepository _userRoleRepository;
        private IUnitOfWork _unitOfWork;

        public UserRoleService(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
        {
            this._userRoleRepository = userRoleRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Delete(string roleId)
        {
            _userRoleRepository.DeleteMulti(x => x.RoleId == roleId);
        }

        public void DeleteByUserId(string userId)
        {
            _userRoleRepository.DeleteMulti(x => x.UserId == userId);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
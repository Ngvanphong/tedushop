using System.Collections.Generic;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IPostCategoryService
    {
        int Add(PostCategory postCategory);

        void Delete(int id);

        void Update(PostCategory postCategory);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAllByParentID(int parentID);

        PostCategory GetByID(int id);

        void SaveChange();
    }

    public class PostCategoryService : IPostCategoryService
    {
        IPostCategoryRepository _postCategoryRepository;
        IUnitOfWork _unitOfWork;
        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        public int Add(PostCategory postCategory)
        {
           var query= this._postCategoryRepository.Add(postCategory);
            return query.ID;
        }

        public void Delete(int id)
        {
            this._postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return this._postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentID(int parentID)
        {
            return this._postCategoryRepository.GetMulti(x => x.ParentID == parentID && x.Status);
        }

        public PostCategory GetByID(int id)
        {
            return this._postCategoryRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            this._unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            this._postCategoryRepository.Update(postCategory);
        }
    }
}
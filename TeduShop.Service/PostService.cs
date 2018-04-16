using System.Collections.Generic;
using TeduShop.Common;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IPostService
    {
        int Add(Post post);

        void Update(Post post);

        void Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagReponsitory;
        private IPostTagRepository _postTagRepository;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ITagRepository tagReponsitory, IPostTagRepository postTagRepository)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
            this._tagReponsitory = tagReponsitory;
            this._postTagRepository = postTagRepository;
        }

        public int Add(Post post)
        {
            Post query = _postRepository.Add(post);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] listTag = post.Tags.Split(',');
                for (int i = 0; i < listTag.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(listTag[i]);
                    if (_tagReponsitory.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = listTag[i],
                            Type = CommonConstant.PostTag,
                        };
                        _tagReponsitory.Add(tag);
                    }
                    PostTag postTag = new PostTag()
                    {
                        PostID = query.ID,
                        TagID = tagId,
                    };
                    _postTagRepository.Add(postTag);
                }
            }
            return query.ID;
        }

        public void Delete(int id)
        {
            this._postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return this._postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return this._postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return this._postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int id)
        {
            return this._postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void Update(Post post)
        {
           this._postRepository.Update(post);
        }
    }
}
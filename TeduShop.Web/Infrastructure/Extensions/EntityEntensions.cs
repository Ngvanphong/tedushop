using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityEntensions
    {
        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.DisplayOrder = postVm.DisplayOrder;
            post.Description = postVm.Description;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.HotFlag = postVm.HotFlag;
            post.ViewCount = postVm.ViewCount;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDiscription = postVm.MetaDiscription;
            post.CreateDate = postVm.CreateDate;
            post.CreateBy = postVm.CreateBy;
            post.UpdatedBy = postVm.UpdatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.Status = postVm.Status;
        }

        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Description = postCategoryVm.Description;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDiscription = postCategoryVm.MetaDiscription;
            postCategory.CreateDate = postCategoryVm.CreateDate;
            postCategory.CreateBy = postCategoryVm.CreateBy;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.Status = postCategoryVm.Status;
        }
    }
}
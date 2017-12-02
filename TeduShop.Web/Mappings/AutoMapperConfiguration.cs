using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static IMapper Mapping;
        public static void ConfigureMapping()
        {
           Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostCategory>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
            });
        }
    }
}
namespace Restarauntly.Web.ViewModels.Dishes
{
    using System.Linq;

    using AutoMapper;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class DishViewModel : BaseDishViewModel, IMapFrom<Dish>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dish, DishViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/dishes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}

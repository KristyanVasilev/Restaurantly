namespace Restarauntly.Web.ViewModels.Dishes
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class SingleDishViewModel : IMapFrom<Dish>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<DishViewModel> Dishes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dish, SingleDishViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/dishes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}

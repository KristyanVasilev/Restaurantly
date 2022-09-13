namespace Restarauntly.Web.ViewModels.Events
{
    using System.Linq;

    using AutoMapper;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EventViewModel : BaseEventsViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().EventId == x.Id &&
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/events/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}

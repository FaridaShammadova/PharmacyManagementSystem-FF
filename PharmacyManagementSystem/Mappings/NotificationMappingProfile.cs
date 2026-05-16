using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Notification;

namespace PharmacyManagementSystem.Mappings
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
            CreateMap<NotificationCreateViewModel, Notification>();
        }
    }
}

using AutoMapper;
using ItServiceApp.Core.Entities;
using ItServiceApp.Core.ViewModels;

namespace ItServiceApp.Business.MapperProfies
{
    public class EntityProfile :Profile
    {
        public EntityProfile()
        {
            CreateMap<SubscriptionType, SubscriptionTypeViewModel>().ReverseMap();
        }
    }
}

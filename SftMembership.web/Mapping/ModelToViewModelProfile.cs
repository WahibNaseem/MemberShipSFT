using AutoMapper;
using SftMembership.web.Domain.Models;
using SftMembership.web.Models;


namespace SftMembership.web.Mapping
{
    public class ModelToViewModelProfile:Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<User, UserDetialViewModel>().ReverseMap();
        }
    }
}

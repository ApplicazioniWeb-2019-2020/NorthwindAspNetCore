using AutoMapper;
using NorthwindAspNetCore.Models;
using NorthwindAspNetCore.ViewModels;

namespace NorthwindAspNetCore.Data
{
    public class NorthwindMappingProfile : Profile
    {
        public NorthwindMappingProfile()
        {
            CreateMap<SiteUser, SiteUserViewModel>();
            CreateMap<SiteUserViewModel, SiteUser>();
        }
    }
}

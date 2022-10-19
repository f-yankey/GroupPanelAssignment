using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Pages.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<NewUserModel.UserAddModel, AppUser>();

            CreateMap<NewUserModel.ExtraProperty, AppUserClaim>()
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<NewUserModel.ExtraProperty, UserRole>()
                 .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.r))
                 .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        }


    }
}

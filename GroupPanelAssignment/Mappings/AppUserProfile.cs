using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            CreateMap<UserAddViewModel, AppUser>();

            CreateMap<ExtraProperty, AppUserClaim>()
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<AppUser, UserViewModel>();

            CreateMap<UserViewModel, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.FirstName} {src.Othernames} {src.Surname} - {src.Username} - {src.Email}"));
        }


    }
}

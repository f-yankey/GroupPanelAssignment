using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using GroupPanelAssignment.Utils;
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
            CreateMap<AppUser, SupervisorsForAssignmentViewModel>();
            

            CreateMap<AppUser, StudentViewModel>()
               .ForMember(dest => dest.ProgrammeName, opt => opt.MapFrom(src => decimal.Parse(src.AppUserClaims.Where(x => x.Claim.ClaimName == ApplicationConstants.ProgrammeClaim).FirstOrDefault().Value)))
               .ForMember(dest => dest.CWA, opt => opt.MapFrom(src => src.AppUserClaims.Where(x => x.Claim.ClaimName == ApplicationConstants.CWAClaim).FirstOrDefault().Value));

            CreateMap<UserViewModel, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.Firstname} {src.Othernames} {src.Surname} - {src.Username} - {src.Email}"));
        }


    }
}

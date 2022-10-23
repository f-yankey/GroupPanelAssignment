using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Mappings
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamViewModel>()
               .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
               .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.TeamName))
               .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.TeamMembers))
               .ForMember(dest => dest.Supervisors, opt => opt.MapFrom(src => src.TeamSupervisors));

            CreateMap<TeamMember, TeamMemberViewModel>()
              .ForMember(dest => dest.TeamMemberId, opt => opt.MapFrom(src => src.TeamMemberId))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.Firstname} {src.User.Othernames} {src.User.Surname}"));

            CreateMap<TeamSupervisor, TeamSupervisorViewModel>()
              .ForMember(dest => dest.TeamSupervisorId, opt => opt.MapFrom(src => src.TeamSupervisorId))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.Firstname} {src.User.Othernames} {src.User.Surname}"));

            CreateMap<SupervisorsForAssignmentViewModel, TeamSupervisorViewModel>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Firstname} {src.Othernames} {src.Surname}"));

        }
    }
}

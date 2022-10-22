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
               .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.TeamName));
        }
    }
}

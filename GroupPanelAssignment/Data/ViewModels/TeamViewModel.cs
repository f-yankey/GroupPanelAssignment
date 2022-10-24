﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.ViewModels
{
    public class TeamViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamTopic { get; set; }
        public List<TeamMemberViewModel> Members { get; set; }
        public List<TeamSupervisorViewModel> Supervisors { get; set; }

    }

    public class TeamMemberViewModel
    {
        public string UserId { get; set; }
        public string Programme { get; set; }
        public string CWA { get; set; }
        public int TeamMemberId { get; set; }
        public string FullName { get; set; }
    }

    public class TeamSupervisorViewModel
    {
        public string UserId { get; set; }
        public int TeamSupervisorId { get; set; }
        public string FullName { get; set; }
    }
}

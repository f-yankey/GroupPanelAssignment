using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class PanelTeamRepository : BaseRepository, IPanelTeamRepository
    {
        public PanelTeamRepository(GroPanDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}

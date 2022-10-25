using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class CwaGroupingRepository : BaseRepository, ICwaGroupingRepository
    {
        public CwaGroupingRepository(GroPanDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public List<CwaGrouping> GetAll()
        {
            var currentAssignmentSession = GetCurrentSession();
            var results = _dbContext.CwaGroupings
                .Where(x => x.AssignmentSessionId == currentAssignmentSession.AssignmentSessionId)
                .ToList();

            return results;
        }
    }
}

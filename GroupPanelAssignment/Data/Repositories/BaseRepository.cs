using AutoMapper;
using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class BaseRepository
    {
        protected GroPanDbContext _dbContext;
        protected IMapper _mapper;

        public BaseRepository(GroPanDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AssignmentSession GetCurrentSession()
        {
            var currentSession = _dbContext.AssignmentSessions.Where(x => x.IsCurrent == true).FirstOrDefault();
            return currentSession;
        }

        public async Task SaveDatabase()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

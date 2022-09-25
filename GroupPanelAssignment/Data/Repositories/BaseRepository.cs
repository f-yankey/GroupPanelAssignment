using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class BaseRepository
    {
        private PanelTeamAssignDbContext _dbContext;

        public BaseRepository(PanelTeamAssignDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

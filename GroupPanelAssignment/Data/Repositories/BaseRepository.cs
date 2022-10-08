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

        public BaseRepository(GroPanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

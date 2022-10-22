using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Middlewares
{
    public class RolesMiddleware
    {
        private readonly RequestDelegate _next;
      
        public RolesMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, GroPanDbContext groPanDbContext)
        {
            bool rolesInSession = context.Session.Get(ApplicationConstants.RolesInSession) != null ? true : false;
            if (!rolesInSession)
            {
                var roles = groPanDbContext.Roles.OrderBy(x => x.DisplayOrder).ToList();
                context.Session.Set(ApplicationConstants.RolesInSession, roles);
            }
            await _next(context);
        }
    }
}

using GroupPanelAssignment.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRolesInitializer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RolesMiddleware>();
        }
    }
}

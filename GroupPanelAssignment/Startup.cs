using GroupPanelAssignment.Data;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Services;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
                //.AddRazorPagesOptions(opts =>
                //{
                //    opts.Conventions.Add(
                //    new PageRouteTransformerConvention(
                //    new KebabCaseParameterTransformer()));
                //    //opts.Conventions.AddPageRoute(
                //    //"/Search/Products/StartSearch", "/search-products");
                //});

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
                //options.LowercaseQueryStrings = true;
            });

            services.AddDbContext<GroPanDbContext>
            (
                options => options.UseSqlServer(Configuration.GetConnectionString("default"))
            );

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });

            #region Service
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ITeamManagementService, TeamManagementService>();
            #endregion

            #region Repositories
            services.AddScoped<BaseRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAssignmentSessionRepository, AssignmentSessionRepository>();
            services.AddScoped<IPanelMemberRepository, PanelMemberRepository>();
            services.AddScoped<IPanelMemberTeamMemberScoreRepository, PanelMemberTeamMemberScoreRepository>();
            services.AddScoped<IPanelMemberTeamScoreRepository, PanelMemberTeamScoreRepository>();
            services.AddScoped<IPanelRepository, PanelRepository>();
            services.AddScoped<IPanelTeamRepository, PanelTeamRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IScoreItemRepository, ScoreItemRepository>();
            services.AddScoped<IScoreItemTypeRepository, ScoreItemTypeRepository>();
            services.AddScoped<IScoringSessionRepository, ScoringSessionRepository>();
            services.AddScoped<ISessionScoreItemRepository, SessionScoreItemRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamSplitHistoryRepository, TeamSplitHistoryRepository>();
            services.AddScoped<ITeamSupervisorRepository, TeamSupervisorRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IAppUserClaimRepository, AppUserClaimRepository>();
            services.AddScoped<ICwaGroupingRepository, CwaGroupingRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseRolesInitializer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

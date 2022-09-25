using GroupPanelAssignment.Data;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.Services;
using GroupPanelAssignment.Data.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
            services.AddDbContext<PanelTeamAssignDbContext>
            (
                options => options.UseSqlServer(Configuration.GetConnectionString("default"))
            );

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

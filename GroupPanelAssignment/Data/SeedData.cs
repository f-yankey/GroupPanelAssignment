using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GroPanDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<GroPanDbContext>>()))
            {

                bool containsRole = context.Roles.Any();
                bool containsScoreItemTypes = context.ScoreItemTypes.Any();
                bool containsLocations = context.Locations.Any();
                bool containsClaims = context.Claims.Any();
                bool containsAssignmentSession = context.AssignmentSessions.Any();
                bool containsCWAGroups = context.CwaGroupings.Any();
                bool doesNotContainAny = !containsRole || !containsScoreItemTypes || 
                    !containsLocations || !containsClaims || 
                    !containsAssignmentSession || !containsCWAGroups;

                if (!containsRole)
                {
                    PopulateRoles(context);
                }

                if (!containsScoreItemTypes)
                {
                    PopulateScoreItemTypes(context);
                }

                if (!containsLocations)
                {
                    PopulateLocations(context);
                }

                if (!containsClaims)
                {
                    PopulateClaims(context);
                }

                if (!containsAssignmentSession)
                {
                    PopulateAssignmentSession(context);
                    SaveContext(context);
                }

                if (!containsCWAGroups)
                {
                    PopulateCWAGroups(context);
                }

                if (doesNotContainAny)
                {
                    SaveContext(context);
                }
                
            }
        }

        private static void SaveContext(GroPanDbContext context)
        {
            context.SaveChanges();
        }

        private static void PopulateScoreItemTypes(GroPanDbContext context)
        {
            context.ScoreItemTypes.AddRange(
                new ScoreItemType
                {
                    ScoreItemTypeName = "Group Score Item",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new ScoreItemType
                {
                    ScoreItemTypeName = "Individual Score Item",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
            );

        }

        private static void PopulateRoles(GroPanDbContext context)
        {
            context.Roles.AddRange(
                new Role
                {
                    DisplayOrder = 1,
                    RoleName = ApplicationConstants.StudentRole,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 2,
                    RoleName = ApplicationConstants.SupervisorRole,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 3,
                    RoleName = ApplicationConstants.PanelMemberRole,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 4,
                    RoleName = ApplicationConstants.AdminRole,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 5,
                    RoleName = ApplicationConstants.SuperAdminRole,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
                
            );

        }

        private static void PopulateLocations(GroPanDbContext context)
        {
            context.Locations.AddRange(
                 new Location
                 {
                     LocationName = "Engineering Auditorium",
                     Created = DateTime.Now,
                     CreatedBy = "admin"
                 },
                new Location
                {
                    LocationName = "Petroleum LT 1",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
            );

        }

        private static void PopulateClaims(GroPanDbContext context)
        {
            context.Claims.AddRange(
                 new Claim
                 {
                     ClaimName = ApplicationConstants.ProgrammeClaim,
                     Created = DateTime.Now,
                     CreatedBy = "admin"
                 },
                new Claim
                {
                    ClaimName = ApplicationConstants.CWAClaim,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
            );
        }


        private static void PopulateAssignmentSession(GroPanDbContext context)
        {
            context.AssignmentSessions.Add(
                 new AssignmentSession
                 {
                     SessionName = "Default Test Session",
                     IsCurrent = true,
                     Created = DateTime.Now,
                     CreatedBy = "admin"
                 }
            );
        }

        private static void PopulateCWAGroups(GroPanDbContext context)
        {
            var currentAssignmentSession = context.AssignmentSessions.Where(x => x.IsCurrent == true).FirstOrDefault();
            context.CwaGroupings.AddRange(
                new CwaGrouping
                {
                    AssignmentSessionId = currentAssignmentSession.AssignmentSessionId,
                    Min = 70,
                    Max = 100,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new CwaGrouping
                {
                    AssignmentSessionId = currentAssignmentSession.AssignmentSessionId,
                    Min = 60,
                    Max = (decimal)69.99,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new CwaGrouping
                {
                    AssignmentSessionId = currentAssignmentSession.AssignmentSessionId,
                    Min = 50,
                    Max = (decimal)59.99,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new CwaGrouping
                {
                    AssignmentSessionId = currentAssignmentSession.AssignmentSessionId,
                    Min = 45,
                    Max = (decimal)49.99,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new CwaGrouping
                {
                    AssignmentSessionId = currentAssignmentSession.AssignmentSessionId,
                    Min = 1,
                    Max = (decimal)44.99,
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
            );
        }
    }
}

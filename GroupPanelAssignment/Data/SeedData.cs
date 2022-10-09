using GroupPanelAssignment.Data.Models;
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
                bool doesNotContainAny = !containsRole || !containsScoreItemTypes || !containsLocations || !containsClaims;

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
                    RoleName = "Student",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 2,
                    RoleName = "Supervisor",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 3,
                    RoleName = "Panel Member",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 4,
                    RoleName = "Admin",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                },
                new Role
                {
                    DisplayOrder = 5,
                    RoleName = "Super Admin",
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
                     ClaimName = "Programme Name",
                     Created = DateTime.Now,
                     CreatedBy = "admin"
                 },
                //new Claim
                //{
                //    ClaimName = "Programme ID",
                //    Created = DateTime.Now,
                //    CreatedBy = "admin"
                //},
                new Claim
                {
                    ClaimName = "CWA",
                    Created = DateTime.Now,
                    CreatedBy = "admin"
                }
            );

        }
    }
}

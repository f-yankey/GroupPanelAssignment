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
            using (var context = new PanelTeamAssignDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PanelTeamAssignDbContext>>()))
            {

                bool containsRole = context.Roles.Any();
                bool containsScoreItemTypes = context.ScoreItemTypes.Any();
                bool containsLocations = context.Locations.Any();
                bool doesNotContainEither = !containsRole || !containsScoreItemTypes || !containsLocations;

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

                if (doesNotContainEither)
                {
                    SaveContext(context);
                }
            }
        }

        private static void SaveContext(PanelTeamAssignDbContext context)
        {
            context.SaveChanges();
        }

        private static void PopulateScoreItemTypes(PanelTeamAssignDbContext context)
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

        private static void PopulateRoles(PanelTeamAssignDbContext context)
        {
            context.Roles.AddRange(
                                    new Role
                                    {
                                        RoleName = "Student",
                                        Created = DateTime.Now,
                                        CreatedBy = "admin"
                                    },

                                    new Role
                                    {
                                        RoleName = "Supervisor",
                                        Created = DateTime.Now,
                                        CreatedBy = "admin"
                                    },

                                    new Role
                                    {
                                        RoleName = "Panel Member",
                                        Created = DateTime.Now,
                                        CreatedBy = "admin"
                                    }
                                );
        }

        private static void PopulateLocations(PanelTeamAssignDbContext context)
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
    }
}

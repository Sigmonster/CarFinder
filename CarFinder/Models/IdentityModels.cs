using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarFinder.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Car> Car { get; set; }



        public async Task<List<string>> GetYears()
        {
            return await this.Database.SqlQuery<string>("UniqueModelYears").ToListAsync();
        }


        public async Task<List<string>> GetModels(string year, string make)
        {
            return await this.Database.SqlQuery<string>("ModelsByYearAndMake @Year, @Make",
                new SqlParameter("year", year),
                new SqlParameter("make", make))
            .ToListAsync();
        }

        public async Task<List<string>> GetTrims(string year, string make, string model)
        {
            return await this.Database.SqlQuery<string>("ModelTrimsByYearMakeAndModel @Year, @Make, @Model_Name",
                new SqlParameter("year", year),
                new SqlParameter("make", make),
                new SqlParameter("model_name", model))
                .ToListAsync();
        }

        public async Task<List<Car>> GetCarsByYearMakeModelTrim(string year, string make, string model, string trim)
        {
            return await this.Database.SqlQuery<Car>("CarsByYearMakeModelAndTrim @Year, @Make, @Model, @Trim",
                new SqlParameter("year", year),
                new SqlParameter("make", make),
                new SqlParameter("model", model),
                new SqlParameter("trim", trim))
              .ToListAsync();
        }

        


    }
}
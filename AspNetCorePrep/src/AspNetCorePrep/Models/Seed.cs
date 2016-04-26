using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePrep.Models
{
    public static class Seed
    {
        public static async Task Initialize(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            if (!db.Users.Any())
            {
                var user = new ApplicationUser { UserName = "inigo@intellitect.com", Email = "inigo@intellitect.com", Name = "Inigo" };
                var result = await userManager.CreateAsync(user, "Abc123!");
            }

            if (!db.Customers.Any())
            {
                db.Customers.Add(new Customer() { Name = "Cloak Makers Inc." });
                db.Customers.Add(new Customer() { Name = "Six Fingered Glove Manufacturing" });
                db.Customers.Add(new Customer() { Name = "Buttercup's Teas" });
                await db.SaveChangesAsync();
            }
        }


    }
}

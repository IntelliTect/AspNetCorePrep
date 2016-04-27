using AspNetCorePrep.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePrep.Tests
{
    public class DatabaseFixtureInMemory : IDisposable
    {
        public ApplicationDbContext Db { get; private set; }

        public DatabaseFixtureInMemory()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddLogging();

            // IHttpContextAccessor is required for SignInManager, and UserManager
            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature() { Handler = null });
            services.AddInstance<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = context,
                });


            var _serviceProvider = services.BuildServiceProvider();

            // Get services we need.
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Db = _serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Seed Data
            Task.Run(() => Seed.Initialize(Db, userManager)).Wait();

            // Get objects we need to create rows.
            var u1 = Db.Users.First();
            var c1 = Db.Customers.First();

            // Create TimeEntry rows.
            Db.TimeEntries.Add(new TimeEntry() { Customer = c1, ApplicationUser = u1, Date = new DateTime(2000, 1, 1), Hours = 4 });
            Db.TimeEntries.Add(new TimeEntry() { Customer = c1, ApplicationUser = u1, Date = new DateTime(2000, 1, 1), Hours = 8 });

            Db.SaveChanges();
        }

        public void Dispose()
        {
            // Do something once at the end.
            Db.Dispose();
        }
    }
}

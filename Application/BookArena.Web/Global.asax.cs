using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BookArena.DAL;
using BookArena.Model;

namespace BookArena.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
        }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<BookArenaDbContext>
    {
        protected override void Seed(BookArenaDbContext context)
        {
            context.ApplicationUser.Add(new ApplicationUser
            {
                UserName = "admin",
                Password = "123456",
                Name = "Shibbir Ahmed",
                Email = "shibbir.cse@gmail.com",
                Website = "http://shibbir.net/",
                Address = "Dhaka, Bangladesh"
            });

            var categories = new List<Category>
            {
                new Category {Title = "ASP"},
                new Category {Title = "Apple/Mac"},
                new Category {Title = "CMS"},
                new Category {Title = "CSS"},
                new Category {Title = "C#"},
                new Category {Title = "Databases"},
                new Category {Title = "Game Programming"},
                new Category {Title = "Graphics"},
                new Category {Title = "HTML5"},
                new Category {Title = "Java"},
                new Category {Title = "JavaScript"},
                new Category {Title = "Miscellaneous"},
                new Category {Title = "PHP"},
                new Category {Title = "Ruby"},
                new Category {Title = "Web Development"},
                new Category {Title = "Windows 8"}
            };

            foreach (var category in categories)
            {
                context.Category.Add(category);
            }

            base.Seed(context);
        }
    }
}
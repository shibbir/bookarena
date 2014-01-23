using System.Collections.Generic;
using System.Data.Entity;
using BookArena.DAL;
using BookArena.Model.EntityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.Web
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<BookArenaDbContext>
    {
        protected override void Seed(BookArenaDbContext context)
        {
            const string role = "SuperAdmin";
            const string password = "Hakuna matata";
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = new ApplicationUser
            {
                Name = "Shibbir Ahmed",
                UserName = "admin",
                Email = "shibbir@shibbir.net",
                Website = "http://shibbir.net/",
                Address = "Dhaka, Bangladesh",
                ImageFileName = "profile.jpg"
            };

            if (!roleManager.RoleExists(role))
            {
                roleManager.Create(new IdentityRole(role));
            }

            var adminresult = userManager.Create(user, password);
            if (adminresult.Succeeded)
            {
                userManager.AddToRole(user.Id, role);
            }

            context.Config.Add(new Config
            {
                FineAmount = 10,
                BookRentDurationInDays = 10,
                Institute = "Stamford University Bangladesh"
            });

            new List<Student>
            {
                new Student
                {
                    FirstName = "Nilufa",
                    LastName = "Yesmin",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305803"
                },
                new Student
                {
                    FirstName = "Arfina",
                    LastName = "Hossain",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305804"
                },
                new Student
                {
                    FirstName = "Farjana",
                    LastName = "Sultana",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305805"
                },
                new Student
                {
                    FirstName = "Md. Imran",
                    LastName = "Hossain",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305806"
                },
                new Student
                {
                    FirstName = "Md. Tarak",
                    LastName = "Abdullah",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305807"
                },
                new Student
                {
                    FirstName = "Md. Shible",
                    LastName = "Sadiqe",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305809"
                },
                new Student
                {
                    FirstName = "Saydunnesa",
                    LastName = "Shirin",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305812"
                },
                new Student
                {
                    FirstName = "Shahed",
                    LastName = "Hasnayeen",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305816"
                },
                new Student
                {
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305817"
                },
                new Student
                {
                    FirstName = "Afreen",
                    LastName = "Chowdhury",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305819"
                },
                new Student
                {
                    FirstName = "Alaka",
                    LastName = "Bhattacharjee",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305820"
                },
                new Student
                {
                    FirstName = "Md. Kamruzzaman",
                    LastName = "Raju",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305821"
                },
                new Student
                {
                    Id = 13,
                    FirstName = "Suchana",
                    LastName = "Rani Das",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305822"
                },
                new Student
                {
                    FirstName = "Md. Abdullah",
                    LastName = "Al Mamun",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305824"
                },
                new Student
                {
                    FirstName = "Polash",
                    LastName = "Singha",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305834"
                },
                new Student
                {
                    FirstName = "R.K",
                    LastName = "Aboy",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305835"
                },
                new Student
                {
                    FirstName = "Md. Noman",
                    LastName = "Hossain",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305836"
                }
            }.ForEach(student => context.Student.Add(student));

            new List<Category>
            {
                new Category
                {
                    Title = "ASP",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Ultra-Fast ASP.NET 4.5",
                            Author = "Rick Kiessig",
                            ShortDescription =
                                "Ultra-Fast ASP.NET 4.5 provides a practical guide to building extremely fast and scalable web sites using ASP.NET and SQL Server, with eminently usable advice and all of the detail you need to understand the recommendations.",
                            ImageFileName = "asp_1.png",
                            Rating = 3,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "ASP.NET MVC 4 Recipes",
                            Author = "John Ciliberti",
                            ShortDescription =
                                "ASP.NET MVC 4 Recipes is a practical guide for developers creating modern web applications on the Microsoft platform. It cuts through the complexities of ASP.NET, jQuery, Knockout.js and HTML 5 to provide straightforward solutions to common web development problems using proven methods based on best practices.",
                            ImageFileName = "asp_2.png",
                            Rating = 3,
                            StatusId = 1
                        }
                    }
                },
                new Category
                {
                    Title = "Apple/Mac",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Objective-C Programmer's Reference",
                            Author = "Carlos Oliveira",
                            ShortDescription =
                                "Objective-C Programmer's Reference is a swift and to-the-point reference for professional programmers to the language of choice in developing applications for iOS and OSX.",
                            ImageFileName = "apple_1.png",
                            Rating = 3.5,
                            StatusId = 2
                        },
                        new Book
                        {
                            Title = "Learn Design for iOS Development",
                            Author = "Sian Morson",
                            ShortDescription =
                                "Learn Design for iOS Development is for you if you're an iOS developer and you want to design your own apps to look great and be in tune with the latest Apple guidelines. You'll learn how to design your apps to work with the exciting new iOS 7 look and feel, which your users expect within their latest apps.",
                            ImageFileName = "apple_2.png",
                            Rating = 3,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "Beginning 3D Game Development with Unity 4",
                            Author = "Sue Blackman",
                            ShortDescription =
                                "Beginning 3D Game Development with Unity 4 introduces key game production concepts in an artist-friendly manner, removes the hurdles to understanding scripting. It enables independent game artists to learn how to produce casual games for mobile, desktop, and console platforms.",
                            ImageFileName = "apple_3.png",
                            Rating = 4,
                            StatusId = 2
                        }
                    }
                },
                new Category {Title = "CMS", Books = new List<Book> {}},
                new Category {Title = "CSS", Books = new List<Book> {}},
                new Category
                {
                    Title = "Databases",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "The Definitive Guide to MongoDB",
                            Author = "David Hows, Eelco Plugge, Peter Membrey, Tim Hawkins",
                            ShortDescription =
                                "The Definitive Guide to MongoDB, Second Edition, shows you how to install, model, and work with data in MongoDB, and write applications for MongoDB using PHP and Python.",
                            ImageFileName = "database_1.png",
                            Rating = 3,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "Entity Framework 6 Recipes",
                            Author = "Brian Driscoll, Nitin Gupta, Robert Vettor, Zeeshan Hirani, Larry Tenny",
                            ShortDescription =
                                "Entity Framework 6 Recipes teaches the core concepts of Entity Framework through a broad range of clear and concise solutions to everyday data access tasks.",
                            ImageFileName = "database_2.png",
                            Rating = 3.5,
                            StatusId = 2
                        }
                    }
                },
                new Category {Title = "Game Programming", Books = new List<Book> {}},
                new Category {Title = "Graphics", Books = new List<Book> {}},
                new Category
                {
                    Title = "HTML5",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "HTML5 Enterprise Application Development",
                            Author = "Nehal Shah, Gabriel José Balda Ortíz",
                            ShortDescription =
                                "HTML5 Enterprise Application Development will guide you through the process of building an enterprise application with HTML5, CSS3, and JavaScript through creating a movie finder application. You will learn how to apply HTML5 capabilities in real development problems and how to support consistent user experiences across multiple browsers and operating systems, including mobile platforms.",
                            ImageFileName = "html5_1.jpg",
                            Rating = 3,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "HTML5 Data and Services Cookbook",
                            Author = "Gorgi Kosev, Mite Mitreski",
                            ShortDescription =
                                "HTML5 Data and Services Cookbook contains over 100 recipes explaining how to utilize modern features and techniques when building websites or web applications. This book will help you to explore the full power of HTML5 - from number rounding to advanced graphics to real-time data binding.",
                            ImageFileName = "html5_2.jpg",
                            Rating = 4,
                            StatusId = 3
                        }
                    }
                },
                new Category
                {
                    Title = "Java",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Beginning Java EE 7",
                            Author = "Antonio Goncalves",
                            ShortDescription =
                                "Beginning Java EE 7 is one of the first tutorials written with definitive expertise on the Java EE 7 platform. Step by step and easy to follow, this book describes the Java EE 7 features and how to use them.",
                            ImageFileName = "java_1.png",
                            Rating = 4,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "Pro JPA 2",
                            Author = "Mike Keith , Merrick Schincariol",
                            ShortDescription =
                                "Pro JPA 2, Second Edition introduces, explains, and demonstrates how to use the new Java Persistence API (JPA) 2.1 from the perspective of one of the specification creators. A one-of-a-kind resource, it provides both theoretical and extremely practical coverage of JPA usage for both beginning and advanced developers.",
                            ImageFileName = "java_2.png",
                            Rating = 4.5,
                            StatusId = 1
                        }
                    }
                },
                new Category
                {
                    Title = "JavaScript",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Dependency Injection with AngularJS",
                            Author = "Alex Knol",
                            ShortDescription =
                                "This book is a practical, hands-on approach to using dependency injection and implementing test-driven development using AngularJS.",
                            ImageFileName = "javascript_1.gif",
                            Rating = 4,
                            StatusId = 1
                        },
                        new Book
                        {
                            Title = "Beginning Backbone.js",
                            Author = "James Sugrue",
                            ShortDescription =
                                "Beginning Backbone.js is your step-by-step guide to mastering Backbone.js, taking you from downloading Backbone.js to architecting rich, stable, and robust JavaScript applications.",
                            ImageFileName = "javascript_2.png",
                            Rating = 3.5,
                            StatusId = 1
                        }
                    }
                },
                new Category {Title = "Miscellaneous", Books = new List<Book> {}},
                new Category
                {
                    Title = "PHP",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "PHP Objects, Patterns, and Practice",
                            Author = "Matt Zandstra",
                            ShortDescription =
                                "PHP Objects, Patterns, and Practice is designed to help readers develop elegant and rock-solid systems through mastery of three key elements: object fundamentals, design principles, and development best practice.",
                            ImageFileName = "php_1.png",
                            Rating = 4,
                            StatusId = 3
                        }
                    }
                },
                new Category {Title = "Ruby", Books = new List<Book>()},
                new Category
                {
                    Title = "Windows 8",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Real World Windows 8 Development",
                            Author = "Samidip Basu",
                            ShortDescription =
                                "Real World Windows 8 Development is a developer’s handbook - an essential guide to building complete, end-user ready Windows 8 applications on the XAML and C# programming stack from start to finish.",
                            ImageFileName = "windows8_1.png",
                            Rating = 4.5,
                            StatusId = 2
                        },
                        new Book
                        {
                            Title = "Windows 8 App Projects - XAML and C# Edition",
                            Author = "Nico Vermeir",
                            ShortDescription =
                                "Windows 8 App Projects - XAML and C# Edition takes you through the process of building your own apps for Windows 8 in a project oriented, example driven way. The book is aimed at developers looking to build Windows 8 apps in a variety of contexts.",
                            ImageFileName = "windows8_2.png",
                            Rating = 3.5,
                            StatusId = 1
                        }
                    }
                }
            }.ForEach(category => context.Category.Add(category));

            base.Seed(context);
        }
    }
}
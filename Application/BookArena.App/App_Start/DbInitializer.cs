using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using BookArena.Data;
using BookArena.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.App
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            const string role = "SuperAdmin";
            const string password = "HakunaMatata71";
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = new ApplicationUser
            {
                Name = "Shibbir Ahmed",
                UserName = "shibbir.cse@gmail.com",
                Email = "shibbir.cse@gmail.com"
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
                            BookId = 1,
                            Title = "Pro ASP.NET MVC 5",
                            Author = "Adam Freeman",
                            ShortDescription =
                                "The ASP.NET MVC 5 Framework is the latest evolution of Microsoft’s ASP.NET web platform. It provides a high-productivity programming model that promotes cleaner code architecture, test-driven development, and powerful extensibility, combined with all the benefits of ASP.NET.",
                            ImageFileName = "asp_1.png",
                            Rating = 3,
                            Quantity = 10
                        },
                        new Book
                        {
                            BookId = 2,
                            Title = "ASP.NET MVC 4 Recipes",
                            Author = "John Ciliberti",
                            ShortDescription =
                                "ASP.NET MVC 4 Recipes is a practical guide for developers creating modern web applications on the Microsoft platform. It cuts through the complexities of ASP.NET, jQuery, Knockout.js and HTML 5 to provide straightforward solutions to common web development problems using proven methods based on best practices.",
                            ImageFileName = "asp_2.png",
                            Rating = 3,
                            Quantity = 5
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
                            BookId = 3,
                            Title = "Objective-C Programmer's Reference",
                            Author = "Carlos Oliveira",
                            ShortDescription =
                                "Objective-C Programmer's Reference is a swift and to-the-point reference for professional programmers to the language of choice in developing applications for iOS and OSX.",
                            ImageFileName = "apple_1.png",
                            Rating = 3.5,
                            Quantity = 4
                        },
                        new Book
                        {
                            BookId = 4,
                            Title = "Learn Design for iOS Development",
                            Author = "Sian Morson",
                            ShortDescription =
                                "Learn Design for iOS Development is for you if you're an iOS developer and you want to design your own apps to look great and be in tune with the latest Apple guidelines. You'll learn how to design your apps to work with the exciting new iOS 7 look and feel, which your users expect within their latest apps.",
                            ImageFileName = "apple_2.png",
                            Rating = 3,
                            Quantity = 7
                        },
                        new Book
                        {
                            BookId = 5,
                            Title = "Beginning 3D Game Development with Unity 4",
                            Author = "Sue Blackman",
                            ShortDescription =
                                "Beginning 3D Game Development with Unity 4 introduces key game production concepts in an artist-friendly manner, removes the hurdles to understanding scripting. It enables independent game artists to learn how to produce casual games for mobile, desktop, and console platforms.",
                            ImageFileName = "apple_3.png",
                            Rating = 4,
                            Quantity = 8
                        }
                    }
                },
                new Category
                {
                    Title = "CMS",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 6,
                            Title = "WordPress for Web Developers",
                            Author = "Stephanie Leary",
                            ShortDescription =
                                "WordPress for Web Developers is a complete guide for web designers and developers who want to begin building sites with WordPress. You'll learn how to publish content, add media, manage users, and keep your site secure. Developers with a little PHP experience can learn to create custom themes and plugins.",
                            ImageFileName = "cms_1.png",
                            Rating = 3.5,
                            Quantity = 9
                        }
                    }
                },
                new Category {Title = "CSS", Books = new List<Book>()},
                new Category
                {
                    Title = "Databases",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 7,
                            Title = "The Definitive Guide to MongoDB",
                            Author = "David Hows, Eelco Plugge, Peter Membrey, Tim Hawkins",
                            ShortDescription =
                                "The Definitive Guide to MongoDB, Second Edition, shows you how to install, model, and work with data in MongoDB, and write applications for MongoDB using PHP and Python.",
                            ImageFileName = "database_1.png",
                            Rating = 3,
                            Quantity = 9
                        },
                        new Book
                        {
                            BookId = 8,
                            Title = "Entity Framework 6 Recipes",
                            Author = "Brian Driscoll, Nitin Gupta, Robert Vettor, Zeeshan Hirani, Larry Tenny",
                            ShortDescription =
                                "Entity Framework 6 Recipes teaches the core concepts of Entity Framework through a broad range of clear and concise solutions to everyday data access tasks.",
                            ImageFileName = "database_2.png",
                            Rating = 3.5,
                            Quantity = 8
                        },
                        new Book
                        {
                            BookId = 9,
                            Title = "Pro Oracle SQL",
                            Author = "Karen Morton, Kerry Osborne, Robyn Sands, Riyaj Shamsudeen, Jared Still",
                            ShortDescription =
                                "Pro Oracle SQL, Second Edition unlocks the power of SQL in the Oracle database—one of the most potent SQL implementations on the market today.",
                            ImageFileName = "database_3.png",
                            Rating = 4,
                            Quantity = 10
                        }
                    }
                },
                new Category
                {
                    Title = "Gaming",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 10,
                            Title = "Beginning Android C++ Game Development",
                            Author = "Bruce Sutherland",
                            ShortDescription =
                                "Beginning Android C++ Game Development introduces general and Android game developers like you to Android's Native Development Kit (NDK).  The Android NDK platform allows you to build the most sophisticated, complex and best performing game apps that leverage C++.",
                            ImageFileName = "game_1.png",
                            Rating = 4,
                            Quantity = 7
                        },
                        new Book
                        {
                            BookId = 11,
                            Title = "Learn 2D Game Development with C#",
                            Author = "Kelvin Sung",
                            ShortDescription =
                                "2D games are hugely popular across a wide range of platforms and the ideal place to start if you’re new to game development. With Learn 2D Game Development with C#, you'll learn your way around the universal building blocks of game development, and how to put them together to create a real working game.",
                            ImageFileName = "game_2.png",
                            Rating = 4.5,
                            Quantity = 9
                        }
                    }
                },
                new Category
                {
                    Title = "Graphics",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 12,
                            Title = "Processing: Creative Coding and Generative Art in Processing 2",
                            Author = "Ira Greenberg, Dianna Xu, Deepak Kumar",
                            ShortDescription =
                                "Processing: Creative Coding and Generative Art in Processing 2 is a fun and creative approach to learning programming using the latest release of the Processing 2.0 programming language.",
                            ImageFileName = "graphics_1.png",
                            Rating = 4,
                            Quantity = 11
                        }
                    }
                },
                new Category
                {
                    Title = "HTML5",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 13,
                            Title = "HTML5 Enterprise Application Development",
                            Author = "Nehal Shah, Gabriel José Balda Ortíz",
                            ShortDescription =
                                "HTML5 Enterprise Application Development will guide you through the process of building an enterprise application with HTML5, CSS3, and JavaScript through creating a movie finder application. You will learn how to apply HTML5 capabilities in real development problems and how to support consistent user experiences across multiple browsers and operating systems, including mobile platforms.",
                            ImageFileName = "html5_1.jpg",
                            Rating = 3,
                            Quantity = 12
                        },
                        new Book
                        {
                            BookId = 14,
                            Title = "HTML5 Data and Services Cookbook",
                            Author = "Gorgi Kosev, Mite Mitreski",
                            ShortDescription =
                                "HTML5 Data and Services Cookbook contains over 100 recipes explaining how to utilize modern features and techniques when building websites or web applications. This book will help you to explore the full power of HTML5 - from number rounding to advanced graphics to real-time data binding.",
                            ImageFileName = "html5_2.jpg",
                            Rating = 4,
                            Quantity = 7
                        },
                        new Book
                        {
                            BookId = 15,
                            Title = "The Truth About HTML5",
                            Author = "Luke Stevens, RJ Owen",
                            ShortDescription =
                                "The Truth About HTML5 is for web designers, web developers, and front-end coders who want to get up to speed with HTML5. The book isn't afraid to point out what everyone gets wrong about HTML5's new markup, so you don’t make the same mistakes. It will show you what rocks in HTML5 today and what the future holds.",
                            ImageFileName = "html5_3.png",
                            Rating = 4.5,
                            Quantity = 12
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
                            BookId = 16,
                            Title = "Beginning Java EE 7",
                            Author = "Antonio Goncalves",
                            ShortDescription =
                                "Beginning Java EE 7 is one of the first tutorials written with definitive expertise on the Java EE 7 platform. Step by step and easy to follow, this book describes the Java EE 7 features and how to use them.",
                            ImageFileName = "java_1.png",
                            Rating = 4,
                            Quantity = 10
                        },
                        new Book
                        {
                            BookId = 17,
                            Title = "Pro JPA 2",
                            Author = "Mike Keith, Merrick Schincariol",
                            ShortDescription =
                                "Pro JPA 2, Second Edition introduces, explains, and demonstrates how to use the new Java Persistence API (JPA) 2.1 from the perspective of one of the specification creators. A one-of-a-kind resource, it provides both theoretical and extremely practical coverage of JPA usage for both beginning and advanced developers.",
                            ImageFileName = "java_2.png",
                            Rating = 4.5,
                            Quantity = 12
                        },
                        new Book
                        {
                            BookId = 18,
                            Title = "Beginning Java with WebSphere",
                            Author = "Robert W. Janson",
                            ShortDescription =
                                "Java and WebSphere provide a solid base for developing large-enterprise applications. Beginning Java with WebSphere provides a step-by-step guide for creating and installing both client- and server-based Java applications using RAD, WebSphere and Java.",
                            ImageFileName = "java_3.png",
                            Rating = 4,
                            Quantity = 11
                        },
                        new Book
                        {
                            BookId = 19,
                            Title = "Pro Hibernate and MongoDB",
                            Author = "Anghel Leonard",
                            ShortDescription =
                                "Pro Hibernate and MongoDB shows you how to use and integrate Hibernate and MongoDB together as a complete out of the box persistence and database application solution.  More specifically, this book guides you through bootstrap, queries and mappings for an enterprise application and then migrating to the Cloud.",
                            ImageFileName = "java_4.png",
                            Rating = 4.5,
                            Quantity = 15
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
                            BookId = 20,
                            Title = "Dependency Injection with AngularJS",
                            Author = "Alex Knol",
                            ShortDescription =
                                "This book is a practical, hands-on approach to using dependency injection and implementing test-driven development using AngularJS.",
                            ImageFileName = "javascript_1.gif",
                            Rating = 4,
                            Quantity = 13
                        },
                        new Book
                        {
                            BookId = 21,
                            Title = "Beginning Backbone.js",
                            Author = "James Sugrue",
                            ShortDescription =
                                "Beginning Backbone.js is your step-by-step guide to mastering Backbone.js, taking you from downloading Backbone.js to architecting rich, stable, and robust JavaScript applications.",
                            ImageFileName = "javascript_2.png",
                            Rating = 3.5,
                            Quantity = 10
                        }
                    }
                },
                new Category
                {
                    Title = "Other Topics",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 22,
                            Title = "Git Recipes",
                            Author = "Wodzimierz Gajda",
                            ShortDescription =
                                "Whether you're relatively new to git or you need a refresher, or if you just need a quick, handy reference for common tasks in git, Git Recipes is just the reference book you need. Git Recipes is your #1 reference for everything you ever need to do with Git.",
                            ImageFileName = "misc_1.png",
                            Rating = 5,
                            Quantity = 12
                        }
                    }
                },
                new Category
                {
                    Title = "PHP",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 23,
                            Title = "PHP Objects, Patterns, and Practice",
                            Author = "Matt Zandstra",
                            ShortDescription =
                                "PHP Objects, Patterns, and Practice is designed to help readers develop elegant and rock-solid systems through mastery of three key elements: object fundamentals, design principles, and development best practice.",
                            ImageFileName = "php_1.png",
                            Rating = 4,
                            Quantity = 14
                        },
                        new Book
                        {
                            BookId = 24,
                            Title = "Practical PHP and MySQL Website Databases",
                            Author = "Adrian W. West",
                            ShortDescription =
                                "Practical PHP and MySQL Website Databases is a project-oriented book that demystifies PHP and MySQL and explains how to create interactive web pages step-by step.",
                            ImageFileName = "php_2.png",
                            Rating = 4.5,
                            Quantity = 11
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
                            BookId = 25,
                            Title = "Real World Windows 8 Development",
                            Author = "Samidip Basu",
                            ShortDescription =
                                "Real World Windows 8 Development is a developer’s handbook - an essential guide to building complete, end-user ready Windows 8 applications on the XAML and C# programming stack from start to finish.",
                            ImageFileName = "windows8_1.png",
                            Rating = 4.5,
                            Quantity = 11
                        },
                        new Book
                        {
                            BookId = 26,
                            Title = "Windows 8 App Projects - XAML and C# Edition",
                            Author = "Nico Vermeir",
                            ShortDescription =
                                "Windows 8 App Projects - XAML and C# Edition takes you through the process of building your own apps for Windows 8 in a project oriented, example driven way. The book is aimed at developers looking to build Windows 8 apps in a variety of contexts.",
                            ImageFileName = "windows8_2.png",
                            Rating = 3.5,
                            Quantity = 9
                        }
                    }
                }
            }.ForEach(category => context.Category.Add(category));

            var uniqueKeys = new List<string>();

            foreach (var book in context.Book.Local)
            {
                for (var i = 0; i < book.Quantity; i++)
                {
                    var uniqueKey = "ISBN " + new Random().Next(1000, 9000).ToString(CultureInfo.InvariantCulture);
                    while (uniqueKeys.Contains(uniqueKey))
                    {
                        uniqueKey = "ISBN " + new Random().Next(1000, 9000).ToString(CultureInfo.InvariantCulture);
                    }
                    uniqueKeys.Add(uniqueKey);
                    context.BookMetaData.Add(new BookMetaData
                    {
                        BookId = book.BookId,
                        IsAvailable = true,
                        UniqueKey = uniqueKey
                    });
                }
            }

            base.Seed(context);
        }
    }
}
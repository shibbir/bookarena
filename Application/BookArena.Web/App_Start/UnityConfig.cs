using System;
using BookArena.DAL;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using Microsoft.Practices.Unity;

namespace BookArena.Web
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
        }
    }
}
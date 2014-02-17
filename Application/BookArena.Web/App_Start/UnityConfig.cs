using System;
using BookArena.DAL;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Web.Controllers;
using BookArena.Web.Helper;
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

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<ModelFactory, ModelFactory>();
        }
    }
}
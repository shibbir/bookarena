using System;
using BookArena.App.Controllers;
using Microsoft.Practices.Unity;

namespace BookArena.App
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
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default
                );

            container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}
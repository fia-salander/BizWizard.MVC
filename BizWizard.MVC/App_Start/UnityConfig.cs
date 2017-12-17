using BizWizard.Domain.Data.Repository;
using BizWizard.MVC.Models.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace BizWizard.MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMailMessageRepository, MailMessageRepository > ();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
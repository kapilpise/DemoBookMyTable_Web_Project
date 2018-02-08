
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.UI;

using FoodOrderingWebService.Repository;

using System.Web.Configuration;
using System.Data.Entity;
using BookMyTableDataLayer.Repository;

namespace BookMyTableUI.App_Start
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        /// <summary>
        /// Initialize the dependency injections
        /// </summary>
        /// <param name="config">Http Configuration</param>
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }
        /// <summary>
        /// Initialize the dependency injections
        /// </summary>
        /// <param name="config">HTTP Configuration.</param>
        /// <param name="container">Container parameter</param>
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        /// <summary>
        /// Method to register the types.
        /// </summary>
        /// <param name="builder">Container builder parameter.</param>
        /// <returns></returns>
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // EF ParkContext      


            builder.RegisterType<FoodOrderingService>()
               .As<IFoodOrderService>()
               .InstancePerRequest();

            builder.RegisterType<UserManagement>()
            .As<IUserManangement>()
            .InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}
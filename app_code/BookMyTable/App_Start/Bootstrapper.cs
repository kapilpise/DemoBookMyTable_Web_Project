using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookMyTableUI.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {

            // Configure Autofac
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure AutoMapper
          //  AutoMapperConfiguration.Configure();

        }
    }
}
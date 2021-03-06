﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Web;
using Ninject;
using Customers.Domain.TableDef;
using Customers.Domain.Security;

namespace Customers.WebUI.Controllers
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext
            requestContext, Type controllerType)
        {

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IDemandRepository>().To<FakeDemandRepository>();
            ninjectKernel.Bind<IUserInformationRepository>().To<FakeUserRepository>();
            ninjectKernel.Bind<IProgressRepository>().To<FakeProgressRepository>();
        }
    }
}
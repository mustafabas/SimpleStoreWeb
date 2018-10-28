using Autofac;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using MvcProjectArch.Core.Data;
using MvcProjectArch.Core.Infrastructure;
using MvcProjectArch.Core.Infrastructure.DependencyManagement;
using MvcProjectArch.Data;
using MvcProjectArch.Services.Catolog;
using MvcProjectArch.Services.Checkouts;
using MvcProjectArch.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MvcProjectArch.Ulitilities
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
              .As<HttpRequestBase>()
              .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            builder.Register(x => new EfDataProviderManager()).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            //var efDataProviderManager = new EfDataProviderManager();
            //var dataProvider = efDataProviderManager.LoadDataProvider();
            //dataProvider.InitConnectionFactory();

            builder.Register<IDbContext>(c => new EcommerceObjectContext()).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //cache manager


            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutService>().As<ICheckoutService>().InstancePerLifetimeScope();
           
        }

        public int Order
        {
            get { return 1; }
        }
    }
}

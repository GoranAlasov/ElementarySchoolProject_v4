using ElementarySchoolProject.Infrastructure;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Providers;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Services.UsersServices;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

[assembly: OwinStartup(typeof(ElementarySchoolProject.Startup))]
namespace ElementarySchoolProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SetupUnity();
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);            
            app.UseWebApi(config);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "dd.MM.yyyy.";
        }        

        public void ConfigureOAuth(IAppBuilder app, UnityContainer container)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(15),
                Provider = new SimpleAuthorizationServerProvider(container)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private UnityContainer SetupUnity()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, DataAccessContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IAuthRepository, AuthRepository>();

            container.RegisterType<IGenericRepository<ApplicationUser>, GenericRepository<ApplicationUser>>();

            container.RegisterType<IGenericRepository<Admin>, GenericRepository<Admin>>();
            container.RegisterType<IGenericRepository<Teacher>, GenericRepository<Teacher>>();
            container.RegisterType<IGenericRepository<Parent>, GenericRepository<Parent>>();
            container.RegisterType<IGenericRepository<Student>, GenericRepository<Student>>();

            container.RegisterType<IGenericRepository<SchoolClass>, GenericRepository<SchoolClass>>();
            container.RegisterType<IGenericRepository<SchoolSubject>, GenericRepository<SchoolSubject>>();
            container.RegisterType<IGenericRepository<TeacherSchoolSubject>, GenericRepository<TeacherSchoolSubject>>();
            container.RegisterType<IGenericRepository<Grade>, GenericRepository<Grade>>();

            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<IParentsSerivce, ParentsService>();

            //TODO 0.1: Adding dependencies
            
            return container;
        }
    }
}
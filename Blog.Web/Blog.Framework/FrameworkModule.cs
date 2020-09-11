using Autofac;
using Blog.Framework.BookBLOG;
using Blog.Framework.SettingBLOG;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework
{
    public class FrameworkModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FrameworkModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameworkContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<BlogUnitOfWork>().As<IBlogUnitOfWork>()
                .InstancePerLifetimeScope();



            builder.RegisterType<BookRepository>().As<IBookRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<BookService>().As<IBookService>()
               .InstancePerLifetimeScope();


            builder.RegisterType<SettingRepository>().As<ISettingRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<SettingService>().As<ISettingService>()
               .InstancePerLifetimeScope();



            base.Load(builder);
        }
    }
}

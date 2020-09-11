using Autofac;
using Blog.Web.Areas.Admin.Models.BookModel;
using Blog.Web.Areas.Admin.Models.SettingModel;
using Blog.Web.Areas.Admin.Models.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookModel>();
            base.Load(builder);

            builder.RegisterType<SettingModel>();
            base.Load(builder);
        }
    }
}


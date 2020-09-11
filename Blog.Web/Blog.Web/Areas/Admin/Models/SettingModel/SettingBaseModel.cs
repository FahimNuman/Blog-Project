using Autofac;
using Blog.Framework.SettingBLOG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models.SettingModel
{
    public class SettingBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly ISettingService _settingService;
        public SettingBaseModel(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public SettingBaseModel()
        {
            _settingService = Startup.AutofacContainer.Resolve<ISettingService>();
        }

        public void Dispose()
        {
            _settingService?.Dispose();
        }
    }
}
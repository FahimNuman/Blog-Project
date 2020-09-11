using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework.SettingBLOG
{
    public interface ISettingRepository : IRepository<Setting, int, FrameworkContext>
    {
        IList<Setting> GetLatestSettings();
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework.SettingBLOG
{
    public interface ISettingService : IDisposable
    {
        (IList<Setting> records, int total, int totalDisplay) GetSettings(int pageIndex,
                                                                    int pageSize,
                                                                    string searchText,
                                                                    string sortText);
        void CreateSetting(Setting setting);
        void EditSetting(Setting setting);
        Setting GetSetting(int id);
        Setting DeleteSetting(int id);
    }
}


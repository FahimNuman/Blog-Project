using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Blog.Framework.SettingBLOG
{
    public class SettingService : ISettingService, IDisposable
    {
        private IBlogUnitOfWork _blogUnitOfWork;

        public SettingService(IBlogUnitOfWork blogUnitOfWork)
        {
            _blogUnitOfWork = blogUnitOfWork;
        }

        public void CreateSetting(Setting setting)
        {
            var count = _blogUnitOfWork.SettingRepository.GetCount(x => x.Title == setting.Title);
            if (count > 0)
                throw new DuplicationException("Setting title already exists", nameof(setting.Title));

            _blogUnitOfWork.SettingRepository.Add(setting);
            _blogUnitOfWork.Save();
        }

        public Setting DeleteSetting(int id)
        {
            var setting = _blogUnitOfWork.SettingRepository.GetById(id);
            _blogUnitOfWork.SettingRepository.Remove(setting);
            _blogUnitOfWork.Save();
            return setting;
        }

        public void Dispose()
        {
            _blogUnitOfWork?.Dispose();
        }

        public void EditSetting(Setting setting)
        {
            var count = _blogUnitOfWork.SettingRepository.GetCount(x => x.Title == setting.Title
                    && x.Id != setting.Id);

            if (count > 0)
                throw new DuplicationException("Setting title already exists", nameof(setting.Title));

            var existingSetting = _blogUnitOfWork.SettingRepository.GetById(setting.Id);
            existingSetting.ImageName = setting.ImageName;
            existingSetting.Description = setting.Description;
            existingSetting.Title = setting.Title;

            _blogUnitOfWork.Save();
        }

        public Setting GetSetting(int id)
        {
            return _blogUnitOfWork.SettingRepository.GetById(id);
        }

        public (IList<Setting> records, int total, int totalDisplay) GetSettings(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var result = _blogUnitOfWork.SettingRepository.GetAll().ToList();
            return (result, 0, 0);
        }
    }
}


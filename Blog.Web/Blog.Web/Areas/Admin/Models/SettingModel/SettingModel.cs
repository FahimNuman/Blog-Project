using Blog.Framework.SettingBLOG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models.SettingModel
{
    public class SettingModel : SettingBaseModel
    {
        public SettingModel(ISettingService settingService) : base(settingService) { }
        public SettingModel() : base() { }

        internal object GetSettings(DataTablesAjaxRequestModel tableModel)
        {
            var data = _settingService.GetSettings(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Title", "ImageName", "Description","Id" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Title,
                                record.ImageName,
                                record.Description,
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        internal string Delete(int id)
        {
            var deletedSetting = _settingService.DeleteSetting(id);
            return deletedSetting.Title;
        }
    }
}


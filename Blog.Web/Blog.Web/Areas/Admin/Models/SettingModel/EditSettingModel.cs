using Autofac;
using Blog.Framework.SettingBLOG;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models.SettingModel
{
    public class EditSettingModel : SettingBaseModel
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile imageFile { get; set; }
        public string Description { get; set; }
        

        public EditSettingModel(ISettingService settingService) : base(settingService) { }
        public EditSettingModel() : base() { }

        public void Edit()
        {
            var hostingEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();

            string wwwRootpath = hostingEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootpath + "/about/", fileName);

            var stream = new FileStream(path, FileMode.Create);
            imageFile.CopyToAsync(stream);
            var setting = new Setting
            {
                Id = this.Id,
                Title = this.Title,
                ImageName = fileName,
                Description = this.Description,
              
            };

            _settingService.EditSetting(setting);
        }

        internal void Load(int id)
        {
            var setting = _settingService.GetSetting(id);
            if (setting != null)
            {
                Id = setting.Id;
                Title = setting.Title;
                ImageName = setting.ImageName;
                Description = setting.Description;
               
            }
        }
    }
}


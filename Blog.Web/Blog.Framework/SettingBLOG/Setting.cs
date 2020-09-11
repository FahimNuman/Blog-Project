using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework.SettingBLOG
{
    public class Setting : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
      
    }
}

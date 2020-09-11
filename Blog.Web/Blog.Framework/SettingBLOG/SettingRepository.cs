using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Framework.SettingBLOG
{
    public class SettingRepository : Repository<Setting, int, FrameworkContext>, ISettingRepository
    {
        public SettingRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }

        public IList<Setting> GetLatestSettings()
        {
            throw new NotImplementedException();
        }

        //public IList<Book> GetLatestBooks()
        //{
        //    return Get(x => x.PublicationDate < DateTime.Now.AddDays(-7)).ToList();
        //}
    }
}

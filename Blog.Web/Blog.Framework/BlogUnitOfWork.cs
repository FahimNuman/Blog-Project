using Blog.Data;
using Blog.Framework.BookBLOG;
using Blog.Framework.SettingBLOG;
using Blog.Framework.SettingBLOG;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework
{
    public class BlogUnitOfWork : UnitOfWork, IBlogUnitOfWork
    {

        public IBookRepository BookRepository { get; set; }
        public ISettingRepository SettingRepository { get; set; }

        public BlogUnitOfWork(FrameworkContext context,IBookRepository bookRepositroy, ISettingRepository settingRepository)
            : base(context)
        {
            BookRepository = bookRepositroy;
            SettingRepository = settingRepository;
           
        }
    }
}
using Blog.Data;
using Blog.Framework.BookBLOG;
using Blog.Framework.SettingBLOG;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Framework
{
    public interface IBlogUnitOfWork : IUnitOfWork
    {
        IBookRepository BookRepository { get; set; }
        ISettingRepository SettingRepository { get; set; }
     
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
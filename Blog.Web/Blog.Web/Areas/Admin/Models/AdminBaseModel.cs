using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Blog.Framework;

namespace Blog.Web.Areas.Admin.Models
{
    public abstract class AdminBaseModel
    {
        public MenuModel MenuModel { get; set; }
        public ResponseModel Response
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.IsAvailable
                    && _httpContextAccessor.HttpContext.Session.Keys.Contains(nameof(Response)))
                {
                    var response = _httpContextAccessor.HttpContext.Session.Get<ResponseModel>(nameof(Response));
                    _httpContextAccessor.HttpContext.Session.Remove(nameof(Response));

                    return response;
                }
                else
                    return null;
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set(nameof(Response),
                    value);
            }
        }

        protected IHttpContextAccessor _httpContextAccessor;
        public AdminBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }

        public AdminBaseModel()
        {
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            SetupMenu();
        }

        private void SetupMenu()
        {
            MenuModel = new MenuModel
            {
                MenuItems = new List<MenuItem>
                {
                    {
                        new MenuItem
                        {
                            Title = "Books",
                            Childs = new List<MenuChildItem>
                            {
                                new MenuChildItem{ Title = "View Books", Url = "/Admin/Books" },
                                new MenuChildItem{ Title = "Add Book", Url ="/Admin/Books/AddBook"},
                                new MenuChildItem{ Title = "Edit Book", Url ="/Admin/Books/EditBook"}
                            }
                        }

                    },
            {
                new MenuItem
                {
                    Title = "Settings",
                    Childs = new List<MenuChildItem>
                            {
                                new MenuChildItem{ Title = "View Settings", Url = "/Admin/Settings" },
                                new MenuChildItem{ Title = "Add Setting", Url ="/Admin/Settings/AddSetting"},
                                new MenuChildItem{ Title = "Edit Setting", Url ="/Admin/Settings/EditSetting"}
                            }
                }

                    }
        }
            };
        }
    }
}


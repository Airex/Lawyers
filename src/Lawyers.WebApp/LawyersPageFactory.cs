using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Lawyers.WebApp.Models;

namespace Lawyers.WebApp
{
    public interface ILawyersPageFactory
    {
        ResultData HandlePage(string param);
    }

    public class MockLawyersPageFactory : ILawyersPageFactory
    {
        public ResultData HandlePage(string param)
        {
            return new ResultData()
            {
                Model = new LawyersPageModel()
                {
                    SeoModel = new SeoModel()
                    {
                        H1 = "Test",
                        Block = "Test"
                    },
                    Breadcrumbs = new List<BreadcrumbModel>()
                    {
                        new BreadcrumbModel() {Label = "L"},
                        new BreadcrumbModel() {Label = "L1"},
                        new BreadcrumbModel() {Label = "L2"},
                    },
                    Lawyers = new LawyerModel[]
                    {
                        new LawyerModel() {Address = "ssd",Lat = 234,Name = "Name1", Phone = "4535345",Lng = 56.5}, 
                        new LawyerModel() {Address = "ssd",Lat = 234,Name = "Name1", Phone = "4535345",Lng = 56.5}, 
                        new LawyerModel() {Address = "ssd",Lat = 234,Name = "Name1", Phone = "4535345",Lng = 56.5}, 
                    },
                    ShowLawyers = true,
                    List = new List<NavigationModel>()
                    {
                        new NavigationModel()
                        {
                            Link = "fdgfdg",
                            Label = "fgbmbnm"
                        },  new NavigationModel()
                        {
                            Link = "fdgfdg",
                            Label = "fgbmbnm"
                        },  new NavigationModel()
                        {
                            Link = "fdgfdg",
                            Label = "fgbmbnm"
                        }
                    },
                    PagingViewModel = new PagingViewModel() { Current = 1,PagesCount = 10}
                },
                ViewName = "Index"
            };
        }
    }

    public class DefaultLageFactory : ILawyersPageFactory
    {
        public ResultData HandlePage(string param)
        {
            var actors = new IActor[]
            {
                new DefaultByPlaceActor(), 
                new DefaultActor(), 
            };
            return actors.Select(actor => actor.Handle(param)).FirstOrDefault(resultData => resultData != null);
        }
    }

    public class ResultData
    {
        public string ViewName { get; set; }
        public LawyersPageModel Model { get; set; }
    }

    public interface IActor
    {
        ResultData Handle(string param);
    }

    public class DefaultActor:IActor
    {
        public ResultData Handle(string param)
        {
            
            return new ResultData()
            {
                ViewName = "Navigation",
                Model = new LawyersPageModel()
                {
                    Breadcrumbs =new []
                    {
                        new BreadcrumbModel("Lawyers","/"),
                    },
                    List = new []
                    {
                        new NavigationModel("Search by Place","/lawyers-by-place"),
                        new NavigationModel("Search by Practice Area","/lawyers-by-practice-area"),
                    },
                    ShowBreadcrumbs = true
                }
            };
        }
    }

    public class DefaultByPlaceActor : IActor
    {
        public ResultData Handle(string param)
        {
            if (!string.Equals(param,"lawyers-by-place",StringComparison.OrdinalIgnoreCase)) return null;
            return new ResultData()
            {
                ViewName = "List",
                Model = new LawyersPageModel()
                {
                    Breadcrumbs = new[]
                    {
                        new BreadcrumbModel("Lawyers","/lawyers-by-place"),
                        new BreadcrumbModel("Search lawyers by place","/"),
                    },
                    List = new[]
                    {
                        new NavigationModel("South Australia","/lawyers-in-South-Australia-state"),
                        new NavigationModel("Queensland","/lawyers-in-Queensland-state"),
                        new NavigationModel("New South Wales","/lawyers-in-New-South-Wales-state"),
                        new NavigationModel("Victoria","/lawyers-in-Victoria-state"),
                        new NavigationModel("Northern Territory","/lawyers-in-Northern-Territory-state"),
                        new NavigationModel("Western Australia","/lawyers-in-Western-Australia-state"),
                        new NavigationModel("Tasmania","/lawyers-in-Tasmania-state"),
                        new NavigationModel("Australian Capital Territory","/lawyers-in-Australian-Capital-Territory-state"),
                    },
                    ShowBreadcrumbs = true,
                    ShowLawyers = true
                }
            };
        }
    }
}
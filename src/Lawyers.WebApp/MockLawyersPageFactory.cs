using System.Collections.Generic;
using Lawyers.WebApp.Models;

namespace Lawyers.WebApp
{
    public class MockLawyersPageFactory : ILawyersPageFactory
    {
        public ResultData HandlePage(string param, int page)
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
}
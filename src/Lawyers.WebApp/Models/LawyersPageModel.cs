using System.Collections;
using System.Collections.Generic;

namespace Lawyers.WebApp.Models
{
    public class LawyersPageModel
    {
        public PagingViewModel PagingViewModel { get; set; } = new PagingViewModel();
        public IEnumerable<BreadcrumbModel> Breadcrumbs { get; set; } = new List<BreadcrumbModel>();
        public SeoModel SeoModel { get; set; } = new SeoModel();
        public bool ShowBreadcrumbs { get; set; }
        public bool ShowLawyers { get; set; }
        public IEnumerable<NavigationModel> List { get; set; } = new List<NavigationModel>();
        public IEnumerable<LawyerModel> Lawyers { get; set; } = new LawyerModel[0];
    }

    public class NavigationModel
    {
        public NavigationModel()
        {
        }

        public NavigationModel(string label, string link)
        {
            Label = label;
            Link = link;
        }

        public string Link { get; set; }
        public string Label { get; set; }
    }
}
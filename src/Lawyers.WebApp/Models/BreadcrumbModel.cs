namespace Lawyers.WebApp.Models
{
    public class BreadcrumbModel
    {
        public BreadcrumbModel()
        {
        }

        public BreadcrumbModel(string label, string link)
        {
            Label = label;
            Link = link;
        }

        public string Link { get; set; }
        public string Label { get; set; }
    }
}
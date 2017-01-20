using System;
using System.Collections.Generic;
using System.Linq;
using Lawyers.Contracts.Models;
using Lawyers.WebApp.Models;

namespace Lawyers.WebApp
{

    public class BreadcrumbBuilder
    {
        readonly IList<BreadcrumbModel> _list = new List<BreadcrumbModel>();
        public BreadcrumbBuilder Root()
        {
            _list.Add(new BreadcrumbModel("Lawyers","/"));
            return this;
        }

        public BreadcrumbBuilder ByPlace()
        {
            _list.Add(new BreadcrumbModel("Search lawyers by place", "/lawyers-by-place"));
            return this;
        }

        public BreadcrumbBuilder ByPracticeArea()
        {
            _list.Add(new BreadcrumbModel("Search lawyers by practice area", "/lawyers-by-practice-area"));
            return this;
        }

        public BreadcrumbBuilder ByPlaceInState(string state)
        {
            _list.Add(new BreadcrumbModel("Search lawyers by place in "+state.Replace("-"," "), "/lawyers-in-"+state+"-state"));
            return this;
        }

        public BreadcrumbBuilder ByPlaceAndPostcodeInState(string state)
        {
            _list.Add(new BreadcrumbModel("Search lawyers by place and post code in " + state.Replace("-", " "), "/zip-codes-in-" + state + "-state"));
            return this;
        }

        



        public IList<BreadcrumbModel> Build()
        {
            return _list;
        }

        public BreadcrumbBuilder ByPracticeAreaInZip(string zip)
        {
            _list.Add(new BreadcrumbModel("Search lawyers by practice areas in "+zip, "/practice-areas-in-"+zip+"-zip"));
            return this;
        }
    }

    public enum ViewEnum
    {
        List,
        Navigation,
        Lawyers
    }

    public class ResultDataBuilder
    {

       
        readonly ResultData _resultData = new ResultData() { Model = new LawyersPageModel() };

        public ResultDataBuilder ForView(ViewEnum viewEnum)
        {
            switch (viewEnum)
            {
                case ViewEnum.List:
                    _resultData.ViewName = "List";
                    break;
                case ViewEnum.Navigation:
                    _resultData.ViewName = "Navigation";
                    break;
                case ViewEnum.Lawyers:
                    _resultData.ViewName = "Index";
                    break;

            }
            return this;
        }

        public ResultDataBuilder WithBreadCrumbs(Action<BreadcrumbBuilder> builder)
        {
            _resultData.Model.ShowBreadcrumbs = true;
            var breadcrumbBuilder = new BreadcrumbBuilder();
            builder(breadcrumbBuilder);
            _resultData.Model.Breadcrumbs = breadcrumbBuilder.Build();
            return this;
        }

        public ResultDataBuilder WithList(IEnumerable<NavigationModel> navigationModels)
        {
            _resultData.Model.List = navigationModels.ToList();
            return this;
        }

        public ResultDataBuilder ShowLawyers()
        {
            _resultData.Model.ShowLawyers = true;
            return this;
        }

        public ResultDataBuilder WithLawyers(IEnumerable<Lawyer> models)
        {
            _resultData.Model.Lawyers = models.Select(AutoMapper.Mapper.Map<Lawyer,LawyerModel>).ToList();
            return this;
        }   

        public ResultData Build()
        {
            if (string.IsNullOrEmpty(_resultData.ViewName))
                throw new InvalidOperationException("View name must be set");
            return _resultData;
        }
    }
}
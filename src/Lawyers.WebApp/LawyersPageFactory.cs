using System;
using System.Linq;
using System.Text.RegularExpressions;
using Lawyers.Contracts;
using Lawyers.Contracts.Models;
using Lawyers.WebApp.Models;

namespace Lawyers.WebApp
{
    public interface ILawyersPageFactory
    {
        ResultData HandlePage(string param, int page);
    }

    public class DefaultLageFactory : ILawyersPageFactory
    {
        private readonly ILawyersService _lawyersService;
        private readonly ILookupsService _lookupsService;

        public DefaultLageFactory(ILawyersService lawyersService, ILookupsService lookupsService)
        {
            _lawyersService = lawyersService;
            _lookupsService = lookupsService;
        }

        public ResultData HandlePage(string param, int page)
        {
            var actors = new IActor[]
            {
                new EmptyActor(),
                new ByPlaceActor(_lawyersService),
                new ByStateActor(_lawyersService),
                new ByZipCodesInState(_lookupsService,_lawyersService),
                new ByPracticeAreaInZip(_lookupsService,_lawyersService), 
                
                
                new ByPracticeAreaActor(_lawyersService, _lookupsService),

                new DefaultActor(), 
              
                
            };
            return actors.Select(actor => actor.Handle(param, page)).FirstOrDefault(resultData => resultData != null);
        }
    }

    public class ResultData
    {
        public string ViewName { get; set; }
        public LawyersPageModel Model { get; set; }
    }

    public class DefaultActor : IActor
    {
        public ResultData Handle(string param, int page)
        {
            return new ResultDataBuilder()
                .ForView(ViewEnum.Navigation)
                .WithBreadCrumbs(builder => builder.Root())
                .WithList(new []
                {
                    new NavigationModel("Search by Place","/lawyers-by-place"), 
                    new NavigationModel("Search by Practice Area","/lawyers-by-practice-area"), 
                    new NavigationModel("Search by postcode", "/lawyers-by-zip-code"), 
                })
                .Build();
        }
    }

    public interface IActor
    {
        ResultData Handle(string param, int page);
    }

    class ByPracticeAreaActor : IActor
    {
        private readonly ILawyersService _lawyersService;
        private readonly ILookupsService _lookupsService;

        public ByPracticeAreaActor(ILawyersService lawyersService, ILookupsService lookupsService)
        {
            _lawyersService = lawyersService;
            _lookupsService = lookupsService;
        }

        public ResultData Handle(string param, int page)
        {
            if (!string.Equals(param, "lawyers-by-practice-area", StringComparison.OrdinalIgnoreCase)) return null;
            var resultDataBuilder = new ResultDataBuilder();
            return resultDataBuilder
                .ForView(ViewEnum.List)
                .WithBreadCrumbs(builder => builder.Root().ByPracticeArea())
                .WithList(_lookupsService.GetPracticeAreas().Select(area => new NavigationModel(area.Name, '/' + area.Name)))
                .ShowLawyers()
                .WithLawyers(_lawyersService.GetAll(page))
                .Build();


        }
    }

    public class EmptyActor : IActor
    {
        public ResultData Handle(string param, int page)
        {
            if (!string.IsNullOrEmpty(param)) return null;
            return new ResultDataBuilder()
                .ForView(ViewEnum.Navigation)
                .WithBreadCrumbs(builder => builder.Root())
                .WithList(new[]
                    {
                        new NavigationModel("Search by Place","/lawyers-by-place"),
                        new NavigationModel("Search by Practice Area","/lawyers-by-practice-area"),
                    })
                .Build();
        }
    }

    public class ByPlaceActor : IActor
    {
        private readonly ILawyersService _lawyersService;

        public ByPlaceActor(ILawyersService lawyersService)
        {
            _lawyersService = lawyersService;
        }

        public ResultData Handle(string param, int page)
        {
            if (!string.Equals(param, "lawyers-by-place", StringComparison.OrdinalIgnoreCase)) return null;
            return new ResultDataBuilder()
                .ForView(ViewEnum.List)
                .ShowLawyers()
                .WithBreadCrumbs(builder => builder.Root().ByPlace())
                .WithList(new[]
                    {
                        new NavigationModel("South Australia","/lawyers-in-South-Australia-state"),
                        new NavigationModel("Queensland","/lawyers-in-Queensland-state"),
                        new NavigationModel("New South Wales","/lawyers-in-New-South-Wales-state"),
                        new NavigationModel("Victoria","/lawyers-in-Victoria-state"),
                        new NavigationModel("Northern Territory","/lawyers-in-Northern-Territory-state"),
                        new NavigationModel("Western Australia","/lawyers-in-Western-Australia-state"),
                        new NavigationModel("Tasmania","/lawyers-in-Tasmania-state"),
                        new NavigationModel("Australian Capital Territory","/lawyers-in-Australian-Capital-Territory-state"),
                    })
                .WithLawyers(_lawyersService.GetAll(page))
                .Build();
        }
    }

    public class ByStateActor : BaseRegexActor
    {
        private readonly ILawyersService _lawyersService;

        public ByStateActor(ILawyersService lawyersService)
            : base("lawyers-in-([A-Za-z-]{8,30})-state")
        {
            _lawyersService = lawyersService;
        }

        protected override ResultData InternalHandle(string s, int page, GroupCollection matchGroups)
        {
            string state = matchGroups[1].Value;
            string stateString = state.Replace("-", " ");
            return new ResultDataBuilder()
                .ForView(ViewEnum.Navigation)
                .WithBreadCrumbs(builder => builder.Root().ByPlace().ByPlaceInState(state))
                .WithList(new[]
                {
                    new NavigationModel("Search by postcode", "/zip-codes-in-"+state+"-state"),
                    new NavigationModel("Search by location", "/locations-in-"+state+"-state"),
                })
                .ShowLawyers()
                .WithLawyers(_lawyersService.GetByState(stateString, page))
                .Build();
        }
    }

    public class ByZipCodesInState : BaseRegexActor
    {
        private readonly ILookupsService _lookupsService;
        private readonly ILawyersService _lawyersService;
        public ByZipCodesInState(ILookupsService lookupsService, ILawyersService lawyersService) : base("zip-codes-in-([A-Za-z-]{8,30})-state")
        {
            _lookupsService = lookupsService;
            _lawyersService = lawyersService;
        }

        protected override ResultData InternalHandle(string s, int page, GroupCollection matchGroups)
        {
            var state = matchGroups[1].Value;
            return new ResultDataBuilder()
                .ForView(ViewEnum.List)
                .WithBreadCrumbs(builder => builder.Root().ByPlace().ByPlaceInState(state).ByPlaceAndPostcodeInState(state))
                .ShowLawyers()
                .WithLawyers(_lawyersService.GetByState(state.Replace("-", " "), page))
                .WithList(_lookupsService.GetZipCodesForState(state.Replace("-", " ")).Select(code => new NavigationModel(code.Name, "/practice-areas-in-" + code.Name.Replace(" ", "-") + "-zip")))
                .Build();
        }
    }

    public class ByPracticeAreaInZip : BaseRegexActor
    {
        private readonly ILookupsService _lookupsService;
        private readonly ILawyersService _lawyersService;

        public ByPracticeAreaInZip(ILookupsService lookupsService, ILawyersService lawyersService) : base("practice-areas-in-([A-Z0-9]{5,7})-zip")
        {
            _lookupsService = lookupsService;
            _lawyersService = lawyersService;
        }

        protected override ResultData InternalHandle(string s, int page, GroupCollection matchGroups)
        {
            var zip = matchGroups[1].Value;
            var state = _lookupsService.GetStateByZip(zip).Replace(" ", "-");

            return new ResultDataBuilder()
                .ForView(ViewEnum.List)
                .WithBreadCrumbs(
                    builder =>
                        builder.Root()
                            .ByPlace()
                            .ByPlace()
                            .ByPlaceInState(state)
                            .ByPlaceAndPostcodeInState(state)
                            .ByPracticeAreaInZip(zip))
                .WithLawyers(_lawyersService.GetByZip(zip, page))
                .ShowLawyers()
                .WithList(_lookupsService.GetPracticeAreasByZip(zip).Select(area => new NavigationModel(area.Name, state + "-state-" + zip + "-zip-" + area.Name.Replace(" ", "-") + ".html")))
                .Build();
        }
    }

}
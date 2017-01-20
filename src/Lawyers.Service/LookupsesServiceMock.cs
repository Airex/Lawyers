using System.Collections.Generic;
using System.Linq;
using Lawyers.Contracts;
using Lawyers.Contracts.Models;

namespace Lawyers.Service
{
    public class LookupsesServiceMock:ILookupsService
    {
        public IEnumerable<PracticeArea> GetPracticeAreas()
        {
            return Enumerable.Range(1, 10).Select(ni => new PracticeArea()
            {
                Name = "PracticeArea" + ni
            });
        }

        public IEnumerable<ZipCode> GetZipCodesForState(string state)
        {
            return Enumerable.Range(1, 10).Select(i => new ZipCode()
            {
                Name = "Zip" + state + i
            });
        }

        public string GetStateByZip(string zip)
        {
            return "Victoria";
        }

        public IEnumerable<PracticeArea> GetPracticeAreasByZip(string zip)
        {
            return Enumerable.Range(1, 50).Select(i => new PracticeArea() {Name = "PracticeArea" + zip + i});
        }
    }
}
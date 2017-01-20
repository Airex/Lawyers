using System.Collections.Generic;
using Lawyers.Contracts.Models;

namespace Lawyers.Contracts
{
    public interface ILookupsService
    {
        IEnumerable<PracticeArea> GetPracticeAreas();
        IEnumerable<ZipCode> GetZipCodesForState(string state);
        string GetStateByZip(string zip);
        IEnumerable<PracticeArea> GetPracticeAreasByZip(string zip);
    }
}
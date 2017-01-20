using System.Collections.Generic;
using Lawyers.Contracts.Models;

namespace Lawyers.Contracts
{
    public interface ILawyersService
    {
        IEnumerable<Lawyer> GetAll(int page, int count = 50);
        IEnumerable<Lawyer> GetByState(string state, int page, int count = 50);
        IEnumerable<Lawyer> GetByZip(string zip, int page, int count = 50);
    }
}
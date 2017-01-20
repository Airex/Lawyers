using System.Collections.Generic;
using System.Linq;
using Lawyers.Contracts;
using Lawyers.Contracts.Models;

namespace Lawyers.Service
{
    public class LawyerServiceMock :ILawyersService
    {
        public IEnumerable<Lawyer> GetAll(int page, int count = 50)
        {
            return Enumerable.Range(1, 20).Select(i => new Lawyer()
            {
                Name = "L"+i,
                Address = "A"+i,
                Phone = "P"+i,
                Lng = 100+i,
                Lat = 200+i
            });
            
        }

        public IEnumerable<Lawyer> GetByState(string state, int page, int count = 50)
        {
            return Enumerable.Range(1, 20).Select(i => new Lawyer()
            {
                Name = "L"+state + i,
                Address = "A" + i,
                Phone = "P" + i,
                Lng = 100 + i,
                Lat = 200 + i
            });
        }

        public IEnumerable<Lawyer> GetByZip(string zip, int page, int count = 50)
        {
            return Enumerable.Range(1, 20).Select(i => new Lawyer()
            {
                Name = "L" + zip + i,
                Address = "A" + i,
                Phone = "P" + i,
                Lng = 100 + i,
                Lat = 200 + i
            });
        }
    }
}
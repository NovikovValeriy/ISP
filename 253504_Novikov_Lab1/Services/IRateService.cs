using _253504_Novikov_Lab1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253504_Novikov_Lab1.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetRates(DateTime date);
    }
}

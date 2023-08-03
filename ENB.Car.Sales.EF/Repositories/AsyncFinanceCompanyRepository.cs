using ENB.Car.Sales.EF;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.EF.Repositories
{

    /// <summary>
    /// A concrete repository to work with case in the system.
    /// </summary>
    public class AsyncFinanceCompanyRepository : AsyncRepository<FinanceCompany>, IAsyncFinanceCompanyRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly CarSalesContext _carSalesContext;
        public AsyncFinanceCompanyRepository(CarSalesContext carSalesContext) : base(carSalesContext)
        {
            _carSalesContext = carSalesContext;
        }
        public IEnumerable<FinanceCompany> FindByName(string lastname)
        {
            return _carSalesContext.Set<FinanceCompany>().Where(x => x.Finance_Company_Name == lastname);
        }
    }
}

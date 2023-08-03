using ENB.Car.Sales.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.EF
{
  public  class AsyncEFUnitOfWorkFactory :IAsyncUnitOfWorkFactory
    {
        private readonly CarSalesContext  _carSalesContext;

      

        public AsyncEFUnitOfWorkFactory(CarSalesContext  carSalesContext)
        {
            _carSalesContext = carSalesContext;

        }
        public AsyncEFUnitOfWorkFactory(bool forcenew, CarSalesContext  carSalesContext)
        {
                _carSalesContext = carSalesContext;

        }
        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        public async Task<IAsyncUnitOfWork> Create()
        {
            return await Create(false);
        }

        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
        public async Task<IAsyncUnitOfWork> Create(bool forceNew)
        {
            var asyncEFUnitOfWork = await Task.FromResult(new AsyncEFUnitOfWork(forceNew,_carSalesContext));


            return asyncEFUnitOfWork!;

        }


    }
}

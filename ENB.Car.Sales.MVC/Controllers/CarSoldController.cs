using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Car.Sales.EF.Repositories;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Car.Sales.MVC.Controllers
{
    public class CarSoldController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncCarManufacturerRepository _asyncCarManufacturerRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<CarSoldController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public CarSoldController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncCarManufacturerRepository asyncCarManufacturerRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<CarSoldController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncCarModelRepository = asyncCarModelRepository;
            _asyncCarManufacturerRepository = asyncCarManufacturerRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _imapper = imapper;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int customerId)
        {
            ViewBag.Idcustomer = customerId;
           // ViewBag.Idmanuf = carManufacturerId;

            var carModel = await _asyncCarModelRepository.FindById(customerId);
           

            ViewBag.Message = carModel.Model_Name;
            //  ViewBag.ArtNumber = policy.PolicyType;

            return View();
        }

        public async Task<IActionResult> GetCarSoldData(int customerId)
        {

            var CarSoldlist = (from crsld in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(csld => csld.CarSolds)                                                                  
                                  join cfs in _asyncCarModelRepository.FindAll().SelectMany(cfs=>cfs.CarForSales) on crsld.CarForSaleId equals cfs.Id
                                  join csm in _asyncCustomerRepository.FindAll() on crsld.CustomerId equals csm.Id
                                  select new DisplayCarSold
                                  {
                                      Id = crsld.Id,
                                      Agreed_Price = crsld.Agreed_Price,
                                      Date_Sold = crsld.Date_Sold,
                                      Monthly_Payment_Amount = crsld.Monthly_Payment_Amount,
                                      Monthly_Payment_Date = crsld.Monthly_Payment_Date,
                                      DateCreated = crsld.DateCreated,
                                      DateModified = crsld.DateModified,
                                      NameCustomer = csm.FullName,    
                                      Other_Details = crsld.Other_Details

                                  }).ToList();


           

            var lst = await Task.FromResult(CarSoldlist);

            // _imapper.Map(lst, Mpdata);

            return Json(new { data = lst });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int customerId)
        {
            ViewBag.Idcustomer = customerId;            
           

            var data = new CreateAndEditCarSold()
            {

                ListCarForSale = _asyncCarModelRepository.FindAll().SelectMany(x => x.CarForSales)

                       .Select(d => new SelectListItem
                       {
                           Text = d.Other_details,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            };


            var customer = await _asyncCustomerRepository.FindById(customerId);
            data.Date_Sold = DateTime.Today;            

            ViewBag.Message = customer.FullName;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCarSold  createAndEditCarSold
                                               , int customerId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(customerId);
                        

                        CarSold CarSold = new();

                        _imapper.Map(createAndEditCarSold, CarSold);

                           customer.CarSolds.Add(CarSold);
                       

                        _notyf.Success("Car Sold  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { customerId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            createAndEditCarSold.ListCarForSale = _asyncCarModelRepository.FindAll().SelectMany(x => x.CarForSales)
                        .Select(d => new SelectListItem
                        {
                            Text = d.Other_details,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList();

            ViewBag.Idcustomer = createAndEditCarSold.CustomerId;

            return View("Create",createAndEditCarSold);
        }



        public async Task<IActionResult> Edit(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarSolds);
            var carSold = customer.CarSolds.Single(y => y.Id == id);

            ViewBag.Message = customer.FullName;
            ViewBag.Idcustomer = customerId;
            ViewBag.Id = id;
           


            if (customer is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditCarSold()
            {

                ListCarForSale = _asyncCarModelRepository.FindAll().SelectMany(x=>x.CarForSales)
                       .Select(d => new SelectListItem
                       {
                           Text = d.Other_details,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            }; 
            _imapper.Map(carSold, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCarSold createAndEditCarSold, 
                                            int customerId)
        {

            ViewBag.Idcustomer = customerId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarSolds);
                        var carSold = customer.CarSolds.Single(y => y.Id == createAndEditCarSold.Id);

                        _imapper.Map(createAndEditCarSold, carSold);

                        _notyf.Success("Car Sold updated Successfully");

                        return RedirectToAction(nameof(List), new { customerId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int customerId, int id)
        {

          
            var carSoldlist = (from crsld in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(cfs => cfs.CarSolds)
                                  join crfsl in _asyncCarModelRepository.FindAll().SelectMany(x=>x.CarForSales) on crsld.CarForSaleId equals crfsl.Id
                                  join csm in _asyncCustomerRepository.FindAll() on crsld.CustomerId equals csm.Id
                                  select new DisplayCarSold
                                  {
                                      Id = crsld.Id,
                                      Agreed_Price = crsld.Agreed_Price,
                                      Date_Sold = crsld.Date_Sold,
                                      Monthly_Payment_Amount = crsld.Monthly_Payment_Amount,
                                      Monthly_Payment_Date = crsld.Monthly_Payment_Date,
                                      DateCreated = crsld.DateCreated,
                                      DateModified = crsld.DateModified,
                                      NameCustomer = csm.FullName,
                                      CarForSaleCode = crfsl.Other_details!,
                                      Other_Details = crsld.Other_Details

                                  }).ToList();



         //   ViewBag.Message = CarSold.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.Id = id;
            
            var carSold = await Task.FromResult(carSoldlist.Single(y => y.Id == id));


            if (carSold is null)
            {
                return NotFound();
            }
           
           
            return View(carSold);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int customerId, int id)
        {

            var carSoldlist = (from crsld in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(cfs => cfs.CarSolds)
                               join crfsl in _asyncCarModelRepository.FindAll().SelectMany(x => x.CarForSales) on crsld.CarForSaleId equals crfsl.Id
                               join csm in _asyncCustomerRepository.FindAll() on crsld.CustomerId equals csm.Id
                               select new DisplayCarSold
                               {
                                   Id = crsld.Id,
                                   Agreed_Price = crsld.Agreed_Price,
                                   Date_Sold = crsld.Date_Sold,
                                   Monthly_Payment_Amount = crsld.Monthly_Payment_Amount,
                                   Monthly_Payment_Date = crsld.Monthly_Payment_Date,
                                   DateCreated = crsld.DateCreated,
                                   DateModified = crsld.DateModified,
                                   NameCustomer = csm.FullName,
                                   CarForSaleCode = crfsl.Other_details!,
                                   Other_Details = crsld.Other_Details

                               }).ToList();



            //   ViewBag.Message = CarSold.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.Id = id;

            var carSold = await Task.FromResult(carSoldlist.Single(y => y.Id == id));


            if (carSold is null)
            {
                return NotFound();
            }


            return View(carSold);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayCarSold displayCarSold ,
                                   int customerId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarSolds);
                var carSold = customer.CarSolds.Single(y => y.Id == displayCarSold.Id);

                customer.CarSolds.Remove(carSold);

                _notyf.Error("Car Sold removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { customerId });
        }
    }

}


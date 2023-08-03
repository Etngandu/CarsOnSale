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
    public class CarForSaleController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncCarManufacturerRepository _asyncCarManufacturerRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<CarForSaleController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public CarForSaleController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncCarManufacturerRepository asyncCarManufacturerRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<CarForSaleController> logger,
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

        public async Task<IActionResult> List(int carModelId)
        {
            ViewBag.Idcarmodel = carModelId;
           // ViewBag.Idmanuf = carManufacturerId;

            var carModel = await _asyncCarModelRepository.FindById(carModelId);
           

            ViewBag.Message = carModel.Model_Name;
            //  ViewBag.ArtNumber = policy.PolicyType;

            return View();
        }

        public async Task<IActionResult> GetCarForSaleData(int carModelId)
        {

            var carForSalelist = (from crsf in _asyncCarModelRepository.FindAll().Where(cm => cm.Id == carModelId).SelectMany(cfs => cfs.CarForSales)                                                                  
                                  join crm in _asyncCarModelRepository.FindAll() on crsf.CarModelId equals crm.Id
                                  join cmf in _asyncCarManufacturerRepository.FindAll() on crsf.CarManufacturerId equals cmf.Id
                                  select new DisplayCarForSale
                                  {
                                      Id = crsf.Id,
                                      Asking_Price = crsf.Asking_Price,
                                      Current_Mileage = crsf.Current_Mileage,
                                      Date_Acquired = crsf.Date_Acquired,
                                      Registration_Year = crsf.Registration_Year,
                                      DateCreated = crsf.DateCreated,
                                      DateModified = crsf.DateModified,
                                      Model_Name = crm.Model_Name,
                                      Manufacturer_Name = cmf.Manufacturer_FullName,
                                      
                                      Other_details = crsf.Other_details

                                  }).ToList();


           

            var lst = await Task.FromResult(carForSalelist);

            // _imapper.Map(lst, Mpdata);

            return Json(new { data = lst });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int carModelId)
        {
            ViewBag.Idcarmodel = carModelId;

            var data = new CreateAndEditCarForSale()
            {

                ListMafacturers = _asyncCarManufacturerRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.Manufacturer_Shortname,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            };


            var carModel = await _asyncCarModelRepository.FindById(carModelId);
            data.Date_Acquired = DateTime.Today;
            data.Registration_Year = DateTime.Today;

            ViewBag.Message = carModel.Model_Name;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCarForSale  createAndEditCarForSale
                                               , int carModelId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var carModel = await _asyncCarModelRepository.FindById(carModelId);
                        

                        CarForSale carForSale = new();

                        _imapper.Map(createAndEditCarForSale, carForSale);

                           carModel.CarForSales.Add(carForSale);
                       

                        _notyf.Success("Car For Sale  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { carModelId });
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
            createAndEditCarForSale.ListMafacturers = _asyncCarManufacturerRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Manufacturer_Shortname,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList();

            ViewBag.Idcarmodel = createAndEditCarForSale.CarModelId;

            return View("Create",createAndEditCarForSale);
        }



        public async Task<IActionResult> Edit(int carModelId, int id)
        {

            var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.CarForSales);
            var carForSale = carModel.CarForSales.Single(y => y.Id == id);

            ViewBag.Message = carModel.Model_Name;
            ViewBag.Idcarmodel = carModelId;
            ViewBag.Id = id;
           


            if (carModel is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditCarForSale()
            {

                ListMafacturers = _asyncCarManufacturerRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.Manufacturer_Shortname,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            }; 
            _imapper.Map(carForSale, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCarForSale createAndEditCarForSale, 
                                            int carModelId)
        {

            ViewBag.Idcarmodel = carModelId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.CarForSales);
                        var carForsale = carModel.CarForSales.Single(y => y.Id == createAndEditCarForSale.Id);

                        _imapper.Map(createAndEditCarForSale, carForsale);

                        _notyf.Success("Car For Sale updated Successfully");

                        return RedirectToAction(nameof(List), new { carModelId });
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

        public async Task<IActionResult> Details(int carModelId, int id)
        {

          
            var carForSalelist = (from crsf in _asyncCarModelRepository.FindAll().Where(cm => cm.Id == carModelId).SelectMany(cfs => cfs.CarForSales)
                                  join crm in _asyncCarModelRepository.FindAll() on crsf.CarModelId equals crm.Id
                                  join cmf in _asyncCarManufacturerRepository.FindAll() on crsf.CarManufacturerId equals cmf.Id
                                  select new DisplayCarForSale
                                  {
                                      Id = crsf.Id,
                                      Asking_Price = crsf.Asking_Price,
                                      Current_Mileage = crsf.Current_Mileage,
                                      Date_Acquired = crsf.Date_Acquired,
                                      Registration_Year = crsf.Registration_Year,
                                      DateCreated = crsf.DateCreated,
                                      DateModified = crsf.DateModified,
                                      Model_Name = crm.Model_Name,
                                      Manufacturer_Name = cmf.Manufacturer_FullName,

                                      Other_details = crsf.Other_details

                                  }).ToList();



         //   ViewBag.Message = carForSale.Model_Name;
            ViewBag.Idcarmodel = carModelId;
            ViewBag.Id = id;
            
            var carForSale = await Task.FromResult(carForSalelist.Single(y => y.Id == id));


            if (carForSale is null)
            {
                return NotFound();
            }
           
           
            return View(carForSale);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int carModelId, int id)
        {

            var carForSalelist = (from crsf in _asyncCarModelRepository.FindAll().Where(cm => cm.Id == carModelId).SelectMany(cfs => cfs.CarForSales)
                                  join crm in _asyncCarModelRepository.FindAll() on crsf.CarModelId equals crm.Id
                                  join cmf in _asyncCarManufacturerRepository.FindAll() on crsf.CarManufacturerId equals cmf.Id
                                  select new DisplayCarForSale
                                  {
                                      Id = crsf.Id,
                                      Asking_Price = crsf.Asking_Price,
                                      Current_Mileage = crsf.Current_Mileage,
                                      Date_Acquired = crsf.Date_Acquired,
                                      Registration_Year = crsf.Registration_Year,
                                      DateCreated = crsf.DateCreated,
                                      DateModified = crsf.DateModified,
                                      Model_Name = crm.Model_Name,
                                      Manufacturer_Name = cmf.Manufacturer_FullName,

                                      Other_details = crsf.Other_details

                                  }).ToList();



            //   ViewBag.Message = carForSale.Model_Name;
            ViewBag.Idcarmodel = carModelId;
            ViewBag.Id = id;

            var carForSale = await Task.FromResult(carForSalelist.Single(y => y.Id == id));


            if (carForSale is null)
            {
                return NotFound();
            }


            return View(carForSale);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayCarForSale displayCarForSale ,
                                   int carModelId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.CarForSales);
                var carForSale = carModel.CarForSales.Single(y => y.Id == displayCarForSale.Id);

                carModel.CarForSales.Remove(carForSale);

                _notyf.Error("Car For Sale removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { carModelId });
        }
    }

}


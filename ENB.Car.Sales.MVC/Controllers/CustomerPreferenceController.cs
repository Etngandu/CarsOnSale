using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ENB.Car.Sales.Entities.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ENB.Car.Sales.Entities.Repositories;

namespace ENB.Car.Sales.MVC.Controllers
{
    public class CustomerPreferenceController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;      
        private readonly IAsyncCarFeatureRepository  _asyncCarFeatureRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<CustomerPreferenceController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public CustomerPreferenceController(IAsyncCustomerRepository asyncCustomerRepository,                                       
                                      IAsyncCarFeatureRepository  asyncCarFeatureRepository,                                   
                                      IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<CustomerPreferenceController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncCarFeatureRepository = asyncCarFeatureRepository;           
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
            ViewBag.Idcust = customerId;
            var customer = await _asyncCustomerRepository.FindById(customerId);

            ViewBag.Message = customer.FullName;

            return View();
        }


        public async Task<IActionResult> GetCustomerPreferences(int customerId)
        {

            var customer = (from cstpref in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(x => x.CustomerPreferences)
                            join crf in _asyncCarFeatureRepository.FindAll() on cstpref.CarFeatureId equals crf.Id
                            select new DisplayCustomerPreference
                            {
                                Id = cstpref.Id,
                                Car_Feature_description = crf.Car_Feature_description,  
                                Customer_Preference_Details=cstpref.Customer_Preference_Details,
                                DateCreated = cstpref.DateCreated,
                                DateModified = cstpref.DateModified

                            }).ToList();

            

            var Mpdata = new List<DisplayCustomerPreference>();

            _imapper.Map(await Task.FromResult(customer), Mpdata);

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int customerId)
        {
            ViewBag.Idcust = customerId;

            var data = new CreateAndEditCustomerPreference()
            {

                ListCarFeature = _asyncCarFeatureRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.Car_Feature_description,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()



            };



            var customer = await _asyncCustomerRepository.FindById(customerId);

            ViewBag.Message = customer.FullName;



            return View(data);
        }

        



        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCustomerPreference  createAndEditCustomerPreference, int customerId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(customerId);

                        CustomerPreference customerPreference = new();

                        _imapper.Map(createAndEditCustomerPreference, customerPreference);


                        customer.CustomerPreferences.Add(customerPreference);

                        _notyf.Success("CustomerPreference  Added  Successfully! ");

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
            createAndEditCustomerPreference.ListCarFeature = _asyncCarFeatureRepository.FindAll().Select(d => new SelectListItem
            {
                Text = d.Car_Feature_description,
                Value = d.Id.ToString(),
                Selected = true

            }).Distinct().ToList();
            ViewBag.Idcust = createAndEditCustomerPreference.CustomerId;

            return View("Create", createAndEditCustomerPreference);
        }




        public async Task<IActionResult> Edit(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPreferences);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcust = customerId;
            ViewBag.Id = id;



            if (customer is null)
            {
                return NotFound();
            }

            CreateAndEditCustomerPreference data = new()
            {

                ListCarFeature = _asyncCarFeatureRepository.FindAll().Select(d => new SelectListItem
                {
                    Text = d.Car_Feature_description,
                    Value = d.Id.ToString(),
                    Selected = true

                }).Distinct().ToList()
            };

            _imapper.Map(customer.CustomerPreferences.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCustomerPreference  createAndEditCustomerPreference, int customerId)
        {

            ViewBag.Idcust = customerId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPreferences);
                        var customerpreference = customer.CustomerPreferences.Single(x => x.Id == createAndEditCustomerPreference.Id);

                        _imapper.Map(createAndEditCustomerPreference, customerpreference);

                        _notyf.Success(" customerpreference updated Successfully");

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

            createAndEditCustomerPreference.ListCarFeature = _asyncCarFeatureRepository.FindAll().Select(d => new SelectListItem
            {
                Text = d.Car_Feature_description,
                Value = d.Id.ToString(),
                Selected = true

            }).Distinct().ToList();
            ViewBag.Idcust = createAndEditCustomerPreference.CustomerId;
            return View("Edit", createAndEditCustomerPreference);
        }

        public async Task<IActionResult> Details(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcust = customerId;
            ViewBag.Id = id;


            var lstCustomerPreferences = from cpfs in _asyncCustomerRepository.FindAll().Where(x => x.Id == customerId).SelectMany(cp => cp.CustomerPreferences)
                              join cust in _asyncCustomerRepository.FindAll() on cpfs.CustomerId equals cust.Id
                              join crft in _asyncCarFeatureRepository.FindAll() on cpfs.CarFeatureId equals crft.Id
                              select new DisplayCustomerPreference
                              {
                                  Id = cpfs.Id,
                                  CustomerId = cust.Id,                                  
                                 
                                  DateCreated = cpfs.DateCreated,
                                  DateModified = cpfs.DateModified,
                                  

                              };


            if (lstCustomerPreferences is null)
            {
                return NotFound();
            }

            var sglCustomerPreference = lstCustomerPreferences.Single(x => x.Id == id);

            //ViewBag.police = sglPolicy.PolicyTypeCode;

            return View(sglCustomerPreference);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int customerId, int id)
        {
            var customer = await _asyncCustomerRepository.FindById(customerId);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcust = customerId;
            ViewBag.Id = id;


            var lstCustomerPreferences = from cpfs in _asyncCustomerRepository.FindAll().Where(x => x.Id == customerId).SelectMany(cp => cp.CustomerPreferences)
                                         join cust in _asyncCustomerRepository.FindAll() on cpfs.CustomerId equals cust.Id
                                         join crft in _asyncCarFeatureRepository.FindAll() on cpfs.CarFeatureId equals crft.Id
                                         select new DisplayCustomerPreference
                                         {
                                             Id = cpfs.Id,
                                             CustomerId = cust.Id,
                                             Car_Feature_description=crft.Car_Feature_description,
                                             Customer_Preference_Details=cpfs.Customer_Preference_Details,
                                             DateCreated = cpfs.DateCreated,
                                             DateModified = cpfs.DateModified,


                                         };


            if (lstCustomerPreferences is null)
            {
                return NotFound();
            }

            var sglCustomerPreference = lstCustomerPreferences.Single(x => x.Id == id);

            //ViewBag.police = sglPolicy.PolicyTypeCode;

            return View(sglCustomerPreference);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayCustomerPreference displayCustomerPreference, int customerId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPreferences);
                var customerpreference = customer.CustomerPreferences.Single(x => x.Id == displayCustomerPreference.Id);

                customer.CustomerPreferences.Remove(customerpreference);

                _notyf.Error("Customerpreference related to Customer removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { customerId });
        }
    }
}

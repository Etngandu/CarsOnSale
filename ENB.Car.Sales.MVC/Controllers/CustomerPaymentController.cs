using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Car.Sales.EF.Repositories;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Car.Sales.MVC.Controllers
{
    public class CustomerPaymentController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncCarManufacturerRepository _asyncCarManufacturerRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<CustomerPaymentController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public CustomerPaymentController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncCarManufacturerRepository asyncCarManufacturerRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<CustomerPaymentController> logger,
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

        public async Task<IActionResult> List(int customerId, int carSoldId)
        {
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;

            var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.CustomerPayments);
            var customerpayments= customer.CustomerPayments.Where(y=>y.CarSoldId==carSoldId).ToList();

            ViewBag.Message = customer.FullName;
            //  ViewBag.ArtNumber = policy.PolicyType;

            return View();
        }

        public async Task<IActionResult> GetCustomerPaymentData(int customerId, int carSoldId)
        {

            var CustomerPaymentlist = (from cspyt in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(csp => csp.CustomerPayments)   
                                              .Where(x=>x.CarSoldId== carSoldId)    
                                        join cfs in _asyncCustomerRepository.FindAll().SelectMany(cfs=>cfs.CarSolds) on cspyt.CarSoldId equals cfs.Id
                                        join csm in _asyncCustomerRepository.FindAll() on cspyt.CustomerId equals csm.Id
                                  select new DisplayCustomerPayment
                                  {
                                      Id = cspyt.Id,
                                      Payment_Status = cspyt.Payment_Status,
                                      Customer_Payment_Date_Due = cspyt.Customer_Payment_Date_Due,
                                      Customer_Payment_Date_Made = cspyt.Customer_Payment_Date_Made,
                                      Actual_Payment_Amount = cspyt.Actual_Payment_Amount,
                                      DateCreated = cspyt.DateCreated,
                                      DateModified = cspyt.DateModified,
                                      NameCustomer = csm.FullName  
                                      
                                  }).ToList();


           

            var lst = await Task.FromResult(CustomerPaymentlist);           

            return Json(new { data = lst });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int customerId, int carSoldId)
        {
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            


            //var customer = await _asyncCustomerRepository.FindById(customerId);
            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarSolds);
               var carSold=  customer.CarSolds.Single(x=>x.Id==carSoldId);        

            ViewBag.Message = customer.FullName;
            ViewBag.carSold = carSold.Other_Details;

            return View();
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCustomerPayment  createAndEditCustomerPayment
                                               , int customerId, int carSoldId)
        {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.CustomerPayments);
                        var customePayment = customer.CustomerPayments.Where(y => y.CarSoldId == carSoldId);
                                            

                        CustomerPayment customerPayment = new();

                        _imapper.Map(createAndEditCustomerPayment, customerPayment);

                           customer.CustomerPayments.Add(customerPayment);
                       

                        _notyf.Success("Customer Payment  Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { customerId, carSoldId });
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



        public async Task<IActionResult> Edit(int customerId, int carSoldId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPayments);
            var customerPayment = customer.CustomerPayments.Where(y => y.CarSoldId== carSoldId)
                                    .Single(z=>z.Id==id);

            ViewBag.Message = customer.FullName;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
           


            if (customer is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditCustomerPayment();
            
            _imapper.Map(customerPayment, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCustomerPayment createAndEditCustomerPayment, 
                                            int customerId, int carSoldId)
        {

            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPayments);
                        var customerPayment = customer.CustomerPayments.Where(y => y.CarSoldId == carSoldId)
                                               .Single(z=>z.Id==createAndEditCustomerPayment.Id);

                        _imapper.Map(createAndEditCustomerPayment, customerPayment);

                        _notyf.Success("Customer Payment updated Successfully");

                        return RedirectToAction(nameof(List), new { customerId , carSoldId});
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

        public async Task<IActionResult> Details(int customerId,int carSoldId, int id)
        {

          
            var CustomerPaymentlist = (from cspt in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(cfs => cfs.CustomerPayments)
                                          .Where(y=>y.CarSoldId==carSoldId)
                                  join crsl in _asyncCustomerRepository.FindAll().SelectMany(x=>x.CarSolds) on cspt.CarSoldId equals crsl.Id
                                  join csm in _asyncCustomerRepository.FindAll() on cspt.CustomerId equals csm.Id
                                  select new DisplayCustomerPayment
                                  {
                                      Id = cspt.Id,
                                      Payment_Status = cspt.Payment_Status,
                                      Customer_Payment_Date_Due = cspt.Customer_Payment_Date_Due,
                                      Customer_Payment_Date_Made = cspt.Customer_Payment_Date_Made,
                                      Actual_Payment_Amount = cspt.Actual_Payment_Amount,
                                      DateCreated = cspt.DateCreated,
                                      DateModified = cspt.DateModified,
                                      NameCustomer = csm.FullName,                                     

                                  }).ToList();



         //   ViewBag.Message = CustomerPayment.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
            
            var customerPayment = await Task.FromResult(CustomerPaymentlist.Single(y => y.Id == id));


            if (customerPayment is null)
            {
                return NotFound();
            }
           
           
            return View(customerPayment);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int customerId, int carSoldId, int id)
        {

            var CustomerPaymentlist = (from cspt in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(cfs => cfs.CustomerPayments)
                                         .Where(y => y.CarSoldId == carSoldId)
                                       join crsl in _asyncCustomerRepository.FindAll().SelectMany(x => x.CarSolds) on cspt.CarSoldId equals crsl.Id
                                       join csm in _asyncCustomerRepository.FindAll() on cspt.CustomerId equals csm.Id
                                       select new DisplayCustomerPayment
                                       {
                                           Id = cspt.Id,
                                           Payment_Status = cspt.Payment_Status,
                                           Customer_Payment_Date_Due = cspt.Customer_Payment_Date_Due,
                                           Customer_Payment_Date_Made = cspt.Customer_Payment_Date_Made,
                                           Actual_Payment_Amount = cspt.Actual_Payment_Amount,
                                           DateCreated = cspt.DateCreated,
                                           DateModified = cspt.DateModified,
                                           NameCustomer = csm.FullName,

                                       }).ToList();



            //   ViewBag.Message = CustomerPayment.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;

            var customerPayment = await Task.FromResult(CustomerPaymentlist.Single(y => y.Id == id));


            if (customerPayment is null)
            {
                return NotFound();
            }


            return View(customerPayment);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayCustomerPayment displayCustomerPayment ,
                                   int customerId, int carSoldId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CustomerPayments);
                var customerPayment = customer.CustomerPayments.Where(y => y.CarSoldId == carSoldId)
                                      .Single(z=>z.Id==displayCustomerPayment.Id);

                customer.CustomerPayments.Remove(customerPayment);

                _notyf.Error("Customer Payment removed Successfully");
            }
            return RedirectToAction(nameof(List), new { customerId, carSoldId });
        }
    }

}


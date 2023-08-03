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
    public class CarLoanController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncFinanceCompanyRepository _asyncFinanceCompanyRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<CarLoanController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public CarLoanController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncFinanceCompanyRepository asyncFinanceCompanyRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<CarLoanController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncCarModelRepository = asyncCarModelRepository;
            _asyncFinanceCompanyRepository = asyncFinanceCompanyRepository;
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

            var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.CarLoans);
            var carLoans= customer.CarLoans.Where(y=>y.CarSoldId==carSoldId).ToList();

            ViewBag.Message = customer.FullName;
            //  ViewBag.ArtNumber = policy.PolicyType;

            return View();
        }

        public async Task<IActionResult> GetCarLoanData(int customerId, int carSoldId)
        {

            var CarLoanlist = (from cln in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(x => x.CarLoans)   
                                              .Where(y=>y.CarSoldId== carSoldId)    
                                        join cfs in _asyncCustomerRepository.FindAll().SelectMany(cfs=>cfs.CarSolds) on cln.CarSoldId equals cfs.Id
                                        join csm in _asyncCustomerRepository.FindAll() on cln.CustomerId equals csm.Id
                                  select new DisplayCarLoan
                                  {
                                      Id = cln.Id,
                                      Repayment_Start_Date = cln.Repayment_Start_Date,
                                      Repayment_End_Date = cln.Repayment_End_Date,
                                      Monthtly_Repayment = cln.Monthtly_Repayment,                                      
                                      DateCreated = cln.DateCreated,
                                      DateModified = cln.DateModified,
                                      NameCustomer = csm.FullName,
                                      Other_Details=cln.Other_Details                                      
                                      
                                  }).ToList();


           

            var lst = await Task.FromResult(CarLoanlist);           

            return Json(new { data = lst });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int customerId, int carSoldId)
        {
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;

            var data = new CreateAndEditCarLoan()
            {

                ListFinanceCompany = _asyncFinanceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Finance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()



            };


            var customer = await _asyncCustomerRepository.FindById(customerId);
                 
            data.Repayment_Start_Date = DateTime.Today;
            data.Repayment_End_Date= DateTime.Today;
            ViewBag.Message = customer.FullName;
           

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCarLoan  createAndEditCarLoan
                                               , int customerId, int carSoldId)
        {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.CarLoans);
                        var customePayment = customer.CarLoans.Where(y => y.CarSoldId == carSoldId);
                                            

                        CarLoan CarLoan = new();

                        _imapper.Map(createAndEditCarLoan, CarLoan);

                           customer.CarLoans.Add(CarLoan);
                       

                        _notyf.Success("CarLoans Added  Successfully! ");

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

            createAndEditCarLoan.ListFinanceCompany = _asyncFinanceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Finance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

            ViewBag.Idcustomer = createAndEditCarLoan.CustomerId;
            return View("Create",createAndEditCarLoan);
        }



        public async Task<IActionResult> Edit(int customerId, int carSoldId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarLoans);
            var carLoan = customer.CarLoans.Where(y => y.CarSoldId== carSoldId)
                                    .Single(z=>z.Id==id);

            ViewBag.Message = customer.FullName;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
           


            if (customer is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditCarLoan()
            {

                ListFinanceCompany = _asyncFinanceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Finance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()



            };
            
            _imapper.Map(carLoan, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCarLoan createAndEditCarLoan, 
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

                        var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarLoans);
                        var CarLoan = customer.CarLoans.Where(y => y.CarSoldId == carSoldId)
                                               .Single(z=>z.Id==createAndEditCarLoan.Id);

                        _imapper.Map(createAndEditCarLoan, CarLoan);

                        _notyf.Success("Car Loan updated Successfully");

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
            createAndEditCarLoan.ListFinanceCompany = _asyncFinanceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Finance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

            ViewBag.Idcustomer = createAndEditCarLoan.CustomerId;
            return View("Edit",createAndEditCarLoan);
        }

        public async Task<IActionResult> Details(int customerId,int carSoldId, int id)
        {

          
            var carLoanlist = (from cln in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(x => x.CarLoans)
                                          .Where(y=>y.CarSoldId==carSoldId)
                                  join crsl in _asyncCustomerRepository.FindAll().SelectMany(x=>x.CarSolds) on cln.CarSoldId equals crsl.Id
                                  join csm in _asyncCustomerRepository.FindAll() on cln.CustomerId equals csm.Id
                                  select new DisplayCarLoan
                                  {
                                      Id = cln.Id,
                                      Repayment_Start_Date = cln.Repayment_Start_Date,
                                      Repayment_End_Date = cln.Repayment_End_Date,
                                      Monthtly_Repayment = cln.Monthtly_Repayment,
                                      DateCreated = cln.DateCreated,
                                      DateModified = cln.DateModified,
                                      NameCustomer = csm.FullName

                                  }).ToList();



         //   ViewBag.Message = CarLoan.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
            
            var carLoan = await Task.FromResult(carLoanlist.Single(y => y.Id == id));


            if (carLoan is null)
            {
                return NotFound();
            }
           
           
            return View(carLoan);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int customerId, int carSoldId, int id)
        {

            var carLoanlist = (from cln in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(x => x.CarLoans)
                                          .Where(y => y.CarSoldId == carSoldId)
                               join crsl in _asyncCustomerRepository.FindAll().SelectMany(x => x.CarSolds) on cln.CarSoldId equals crsl.Id
                               join csm in _asyncCustomerRepository.FindAll() on cln.CustomerId equals csm.Id
                               select new DisplayCarLoan
                               {
                                   Id = cln.Id,
                                   Repayment_Start_Date = cln.Repayment_Start_Date,
                                   Repayment_End_Date = cln.Repayment_End_Date,
                                   Monthtly_Repayment = cln.Monthtly_Repayment,
                                   DateCreated = cln.DateCreated,
                                   DateModified = cln.DateModified,
                                   NameCustomer = csm.FullName

                               }).ToList();



            //   ViewBag.Message = CarLoan.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;

            var carLoan = await Task.FromResult(carLoanlist.Single(y => y.Id == id));


            if (carLoan is null)
            {
                return NotFound();
            }


            return View(carLoan);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayCarLoan displayCarLoan ,
                                   int customerId, int carSoldId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.CarLoans);
                var carLoan = customer.CarLoans.Where(y => y.CarSoldId == carSoldId)
                                      .Single(z=>z.Id==displayCarLoan.Id);

                customer.CarLoans.Remove(carLoan);

                _notyf.Error("Car Loan  removed Successfully");
            }
            return RedirectToAction(nameof(List), new { customerId, carSoldId });
        }
    }

}


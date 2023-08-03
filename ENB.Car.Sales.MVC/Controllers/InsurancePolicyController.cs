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
    public class InsurancePolicyController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncInsuranceCompanyRepository _asyncInsuranceCompanyRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<InsurancePolicyController> _logger;
        private readonly IMapper _imapper;
        private readonly INotyfService _notyf;
        public InsurancePolicyController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncInsuranceCompanyRepository asyncInsuranceCompanyRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                      ILogger<InsurancePolicyController> logger,
                                     IMapper imapper,
                                     INotyfService notyf)
        {
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncCarModelRepository = asyncCarModelRepository;
            _asyncInsuranceCompanyRepository = asyncInsuranceCompanyRepository;
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

            var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.InsurancePolicies);
            var insurancePolicys= customer.InsurancePolicies.Where(y=>y.CarSoldId==carSoldId).ToList();

            ViewBag.Message = customer.FullName;
            //  ViewBag.ArtNumber = policy.PolicyType;

            return View();
        }

        public async Task<IActionResult> GetInsurancePolicyData(int customerId, int carSoldId)
        {

            var insurancePolicylist = (from ins in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(x => x.InsurancePolicies)   
                                              .Where(y=>y.CarSoldId== carSoldId)    
                                        join cfs in _asyncCustomerRepository.FindAll().SelectMany(cfs=>cfs.CarSolds) on ins.CarSoldId equals cfs.Id
                                        join csm in _asyncCustomerRepository.FindAll() on ins.CustomerId equals csm.Id
                                  select new DisplayInsurancePolicy
                                  {
                                      Id = ins.Id,
                                      Policy_Start_Date = ins.Policy_Start_Date,
                                      Policy_Renewal_Date = ins.Policy_Renewal_Date,
                                      Monthtly_payment = ins.Monthtly_payment,                                      
                                      DateCreated = ins.DateCreated,
                                      DateModified = ins.DateModified,
                                      NameCustomer = csm.FullName  ,
                                      Other_Details=ins.Other_Details
                                      
                                  }).ToList();


           

            var lst = await Task.FromResult(insurancePolicylist);           

            return Json(new { data = lst });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int customerId, int carSoldId)
        {
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;

            var data = new CreateAndEditInsurancePolicy()
            {

                ListInsuranceCompany = _asyncInsuranceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Insurance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()



            };


            var customer = await _asyncCustomerRepository.FindById(customerId);
                 
            data.Policy_Start_Date = DateTime.Today;
            data.Policy_Renewal_Date= DateTime.Today;
            ViewBag.Message = customer.FullName;
           

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditInsurancePolicy  createAndEditInsurancePolicy
                                               , int customerId, int carSoldId)
        {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(customerId, x=>x.InsurancePolicies);
                        var customePayment = customer.InsurancePolicies.Where(y => y.CarSoldId == carSoldId);
                                            

                        InsurancePolicy insurancePolicy = new();

                        _imapper.Map(createAndEditInsurancePolicy, insurancePolicy);

                           customer.InsurancePolicies.Add(insurancePolicy);
                       

                        _notyf.Success("Insurance Policy Added  Successfully! ");

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

            createAndEditInsurancePolicy.ListInsuranceCompany = _asyncInsuranceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Insurance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

            ViewBag.Idcustomer = createAndEditInsurancePolicy.CustomerId;
            return View("Create",createAndEditInsurancePolicy);
        }



        public async Task<IActionResult> Edit(int customerId, int carSoldId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId, x => x.InsurancePolicies);
            var insurancePolicy = customer.InsurancePolicies.Where(y => y.CarSoldId== carSoldId)
                                    .Single(z=>z.Id==id);

            ViewBag.Message = customer.FullName;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
           


            if (customer is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditInsurancePolicy()
            {

                ListInsuranceCompany = _asyncInsuranceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Insurance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()



            };
            
            _imapper.Map(insurancePolicy, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditInsurancePolicy createAndEditInsurancePolicy, 
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

                        var customer = await _asyncCustomerRepository.FindById(customerId, x => x.InsurancePolicies);
                        var insurancePolicy = customer.InsurancePolicies.Where(y => y.CarSoldId == carSoldId)
                                               .Single(z=>z.Id==createAndEditInsurancePolicy.Id);

                        _imapper.Map(createAndEditInsurancePolicy, insurancePolicy);

                        _notyf.Success("Insurance Policy updated Successfully");

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
            createAndEditInsurancePolicy.ListInsuranceCompany = _asyncInsuranceCompanyRepository.FindAll()

                      .Select(d => new SelectListItem
                      {
                          Text = d.Insurance_Company_Name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList();

            ViewBag.Idcustomer = createAndEditInsurancePolicy.CustomerId;
            return View("Edit",createAndEditInsurancePolicy);
        }

        public async Task<IActionResult> Details(int customerId,int carSoldId, int id)
        {

          
            var insurancePolicylist = (from ins in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(x => x.InsurancePolicies)
                                          .Where(y=>y.CarSoldId==carSoldId)
                                  join crsl in _asyncCustomerRepository.FindAll().SelectMany(x=>x.CarSolds) on ins.CarSoldId equals crsl.Id
                                  join csm in _asyncCustomerRepository.FindAll() on ins.CustomerId equals csm.Id
                                  select new DisplayInsurancePolicy
                                  {
                                      Id = ins.Id,
                                      Policy_Start_Date = ins.Policy_Start_Date,
                                      Policy_Renewal_Date = ins.Policy_Renewal_Date,
                                      Monthtly_payment = ins.Monthtly_payment,
                                      DateCreated = ins.DateCreated,
                                      DateModified = ins.DateModified,
                                      NameCustomer = csm.FullName,
                                      Other_Details=ins.Other_Details
                                  }).ToList();



         //   ViewBag.Message = InsurancePolicy.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;
            
            var insurancePolicy = await Task.FromResult(insurancePolicylist.Single(y => y.Id == id));


            if (insurancePolicy is null)
            {
                return NotFound();
            }
           
           
            return View(insurancePolicy);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int customerId, int carSoldId, int id)
        {

            var insurancePolicylist = (from ins in _asyncCustomerRepository.FindAll().Where(cm => cm.Id == customerId).SelectMany(x => x.InsurancePolicies)
                                          .Where(y => y.CarSoldId == carSoldId)
                                       join crsl in _asyncCustomerRepository.FindAll().SelectMany(x => x.CarSolds) on ins.CarSoldId equals crsl.Id
                                       join csm in _asyncCustomerRepository.FindAll() on ins.CustomerId equals csm.Id
                                       select new DisplayInsurancePolicy
                                       {
                                           Id = ins.Id,
                                           Policy_Start_Date = ins.Policy_Start_Date,
                                           Policy_Renewal_Date = ins.Policy_Renewal_Date,
                                           Monthtly_payment = ins.Monthtly_payment,
                                           DateCreated = ins.DateCreated,
                                           DateModified = ins.DateModified,
                                           NameCustomer = csm.FullName,
                                           Other_Details = ins.Other_Details
                                       }).ToList();



            //   ViewBag.Message = InsurancePolicy.Model_Name;
            ViewBag.Idcustomer = customerId;
            ViewBag.IdcarSold = carSoldId;
            ViewBag.Id = id;

            var insurancePolicy = await Task.FromResult(insurancePolicylist.Single(y => y.Id == id));


            if (insurancePolicy is null)
            {
                return NotFound();
            }


            return View(insurancePolicy);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayInsurancePolicy displayInsurancePolicy ,
                                   int customerId, int carSoldId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.InsurancePolicies);
                var insurancePolicy = customer.InsurancePolicies.Where(y => y.CarSoldId == carSoldId)
                                      .Single(z=>z.Id==displayInsurancePolicy.Id);

                customer.InsurancePolicies.Remove(insurancePolicy);

                _notyf.Error("Insurance Policy  removed Successfully");
            }
            return RedirectToAction(nameof(List), new { customerId, carSoldId });
        }
    }

}


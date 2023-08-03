using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Help;
using ENB.Car.Sales.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;



namespace ENB.Car.Sales.MVC.Controllers
{
    //[Authorize]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditCustomer> _validator;
        public CustomerController(IMapper mapper, ILogger<CustomerController> logger,
                                   IAsyncCustomerRepository asyncCustomerRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditCustomer> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCustomerData()
        {
            IQueryable<Customer> allcustomer = _asyncCustomerRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayCustomer>>(allcustomer).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Customer not found");

            Customer dbCustomer = await _asyncCustomerRepository.FindById(id);

            ViewBag.Message = dbCustomer.FullName;

            _logger.LogInformation($"Details of Customer: {ViewBag.Message}");

            if (dbCustomer is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayCustomer>(dbCustomer);

            return View(data);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditCustomer createAndEditCustomer)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditCustomer);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditCustomer);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Customer dbCustomer = new();

                    _mapper.Map(createAndEditCustomer, dbCustomer);
                    await _asyncCustomerRepository.Add(dbCustomer);

                    _notyf.Success("Customer Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Customer dbCustomer = new(); 

            //            _mapper.Map(createAndEditCustomer, dbCustomer);
            //            await _asyncCustomerRepository.Add(dbCustomer);

            //            _notyf.Success("Customer Created  Successfully! ");

            //            return RedirectToAction("Index");
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //        // result.AddToModelState(this.ModelState);
            //    }
            //}
            //return View();
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Customer {id} not found");

            Customer dbCustomer = await _asyncCustomerRepository.FindById(id);

            if (dbCustomer is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditCustomer>(dbCustomer));

            return View(data);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditCustomer createAndEditCustomer)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Customer dbCustomerToUpdate = await _asyncCustomerRepository.FindById(createAndEditCustomer.Id);

            //            _mapper.Map(createAndEditCustomer, dbCustomerToUpdate, typeof(CreateAndEditCustomer), typeof(Customer));

            //             _notyf.Success("Customer Update  Successfully! ");

            //            return RedirectToAction(nameof(Index));
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //    }
            //}
            //return View();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditCustomer);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditCustomer);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Customer dbCustomerToUpdate = await _asyncCustomerRepository.FindById(createAndEditCustomer.Id);

                    _mapper.Map(createAndEditCustomer, dbCustomerToUpdate, typeof(CreateAndEditCustomer), typeof(Customer));

                    _notyf.Success("Customer Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Customer dbCustomer = await _asyncCustomerRepository.FindById(id);
            ViewBag.Message = dbCustomer.FullName;

            if (dbCustomer is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayCustomer>(dbCustomer);
            return View(data);
        }

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Customer dbCustomer = await _asyncCustomerRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncCustomerRepository.Remove(dbCustomer);

                _notyf.Error("Customer Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

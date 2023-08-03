using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ENB.Car.Sales.MVC.Controllers
{
   // [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
       // private readonly IAsyncStaffRepository _asyncStaffRepository;        
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        /// <summary>
        /// Initializes a new instance of the AddressesController class.
        /// </summary>
        public AddressesController(IAsyncCustomerRepository asyncCustomerRepository,                                                                    
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   IMapper mapper,
                                   INotyfService notyf)
        {
            _asyncCustomerRepository = asyncCustomerRepository; 
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Edit(int CustomerId)
        {
            ViewBag.CustId = CustomerId;
            
           
            
            var address = new Address();
            var message = "";

            if (CustomerId != 0)
            {
                var customer = await _asyncCustomerRepository.FindById(CustomerId);
                
                address = customer.AddressCustomer;
                message = customer.FullName;
            }

           

            

            var data = new EditAddress();

            ViewBag.Message = message;

            _mapper.Map(address, data);
            return View(data);
        }

        public  IActionResult Redirect(int CustomerId)
        {
            ViewBag.CustId = CustomerId;         
            var redirect= RedirectToAction("");      
            

            if (CustomerId != 0)
            {
              redirect=  RedirectToAction("Index","Customer");
            }         


            return redirect;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddress editAddressModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        if (editAddressModel.CustomerId != 0)
                        {
                            var customer = await _asyncCustomerRepository.FindById(editAddressModel.CustomerId);
                            _mapper.Map(editAddressModel, customer.AddressCustomer);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Customer");
                        }


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
    }
}

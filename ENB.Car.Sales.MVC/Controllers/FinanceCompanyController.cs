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
    public class FinanceCompanyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FinanceCompanyController> _logger;
        private readonly IAsyncFinanceCompanyRepository  _asyncFinanceCompanyRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
      
        public FinanceCompanyController(IMapper mapper, ILogger<FinanceCompanyController> logger,
                                   IAsyncFinanceCompanyRepository  asyncFinanceCompanyRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf         )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncFinanceCompanyRepository = asyncFinanceCompanyRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;           
        }

        // GET: FinanceCompanyController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFinanceCompanyData()
        {
            IQueryable<FinanceCompany> allFinanceCompany = _asyncFinanceCompanyRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayFinanceCompany>>(allFinanceCompany).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // POST: FinanceCompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditFinanceCompany createAndEditFinanceCompany)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (createAndEditFinanceCompany.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            FinanceCompany dbFinanceCompany = new();

                            _mapper.Map(createAndEditFinanceCompany, dbFinanceCompany);
                            await _asyncFinanceCompanyRepository.Add(dbFinanceCompany);

                            _notyf.Success("FinanceCompany Created  Successfully! ");

                            return RedirectToAction("Index");
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
                
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            FinanceCompany dbFinanceCompanyToUpdate = await _asyncFinanceCompanyRepository.FindById(createAndEditFinanceCompany.Id);

                            _mapper.Map(createAndEditFinanceCompany, dbFinanceCompanyToUpdate, typeof(CreateAndEditFinanceCompany), typeof(FinanceCompany));

                            _notyf.Success("FinanceCompany Update  Successfully! ");

                            return RedirectToAction(nameof(Index));
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
            }

            return View("Index");
        }


       

        //// POST: FinanceCompanyController/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            FinanceCompany dbFinanceCompany = await _asyncFinanceCompanyRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncFinanceCompanyRepository.Remove(dbFinanceCompany);

                _notyf.Error("FinanceCompany Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

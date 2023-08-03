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
    public class InsuranceCompanyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<InsuranceCompanyController> _logger;
        private readonly IAsyncInsuranceCompanyRepository  _asyncInsuranceCompanyRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
      
        public InsuranceCompanyController(IMapper mapper, ILogger<InsuranceCompanyController> logger,
                                   IAsyncInsuranceCompanyRepository  asyncInsuranceCompanyRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf         )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncInsuranceCompanyRepository = asyncInsuranceCompanyRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;           
        }

        // GET: InsuranceCompanyController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetInsuranceCompanyData()
        {
            IQueryable<InsuranceCompany> allInsuranceCompany = _asyncInsuranceCompanyRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayInsuranceCompany>>(allInsuranceCompany).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // POST: InsuranceCompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditInsuranceCompany createAndEditInsuranceCompany)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (createAndEditInsuranceCompany.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            InsuranceCompany dbInsuranceCompany = new();

                            _mapper.Map(createAndEditInsuranceCompany, dbInsuranceCompany);
                            await _asyncInsuranceCompanyRepository.Add(dbInsuranceCompany);

                            _notyf.Success("InsuranceCompany Created  Successfully! ");

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

                            InsuranceCompany dbInsuranceCompanyToUpdate = await _asyncInsuranceCompanyRepository.FindById(createAndEditInsuranceCompany.Id);

                            _mapper.Map(createAndEditInsuranceCompany, dbInsuranceCompanyToUpdate, typeof(CreateAndEditInsuranceCompany), typeof(InsuranceCompany));

                            _notyf.Success("InsuranceCompany Update  Successfully! ");

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


       

        //// POST: InsuranceCompanyController/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            InsuranceCompany dbInsuranceCompany = await _asyncInsuranceCompanyRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncInsuranceCompanyRepository.Remove(dbInsuranceCompany);

                _notyf.Error("InsuranceCompany Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

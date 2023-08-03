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
    public class CarManufacturerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CarManufacturerController> _logger;
        private readonly IAsyncCarManufacturerRepository  _asyncCarManufacturerRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
      
        public CarManufacturerController(IMapper mapper, ILogger<CarManufacturerController> logger,
                                   IAsyncCarManufacturerRepository  asyncCarManufacturerRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf         )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCarManufacturerRepository = asyncCarManufacturerRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;           
        }

        // GET: CarManufacturerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCarManufacturerData()
        {
            IQueryable<CarManufacturer> allCarManufacturer = _asyncCarManufacturerRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayCarManufacturer>>(allCarManufacturer).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // POST: CarManufacturerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditCarManufacturer createAndEditCarManufacturer)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (createAndEditCarManufacturer.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            CarManufacturer dbCarManufacturer = new();

                            _mapper.Map(createAndEditCarManufacturer, dbCarManufacturer);
                            await _asyncCarManufacturerRepository.Add(dbCarManufacturer);

                            _notyf.Success("CarManufacturer Created  Successfully! ");

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

                            CarManufacturer dbCarManufacturerToUpdate = await _asyncCarManufacturerRepository.FindById(createAndEditCarManufacturer.Id);

                            _mapper.Map(createAndEditCarManufacturer, dbCarManufacturerToUpdate, typeof(CreateAndEditCarManufacturer), typeof(CarManufacturer));

                            _notyf.Success("CarManufacturer Update  Successfully! ");

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


       

        //// POST: CarManufacturerController/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CarManufacturer dbCarManufacturer = await _asyncCarManufacturerRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncCarManufacturerRepository.Remove(dbCarManufacturer);

                _notyf.Error("CarManufacturer Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

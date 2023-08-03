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
    public class CarFeatureController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CarFeatureController> _logger;
        private readonly IAsyncCarFeatureRepository  _asyncCarFeatureRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
      
        public CarFeatureController(IMapper mapper, ILogger<CarFeatureController> logger,
                                   IAsyncCarFeatureRepository  asyncCarFeatureRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf         )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCarFeatureRepository = asyncCarFeatureRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;           
        }

        // GET: CarFeatureController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCarFeatureData()
        {
            IQueryable<CarFeature> allCarFeature = _asyncCarFeatureRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayCarFeature>>(allCarFeature).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // POST: CarFeatureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditCarFeature createAndEditCarFeature)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (createAndEditCarFeature.CarFeatureId == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            CarFeature dbCarFeature = new();

                            _mapper.Map(createAndEditCarFeature, dbCarFeature);
                            await _asyncCarFeatureRepository.Add(dbCarFeature);

                            _notyf.Success("CarFeature Created  Successfully! ");

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

                            CarFeature dbCarFeatureToUpdate = await _asyncCarFeatureRepository.FindById(createAndEditCarFeature.CarFeatureId);

                            _mapper.Map(createAndEditCarFeature, dbCarFeatureToUpdate, typeof(CreateAndEditCarFeature), typeof(CarFeature));

                            _notyf.Success("CarFeature Update  Successfully! ");

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


       

        //// POST: CarFeatureController/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CarFeature dbCarFeature = await _asyncCarFeatureRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncCarFeatureRepository.Remove(dbCarFeature);

                _notyf.Error("CarFeature Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

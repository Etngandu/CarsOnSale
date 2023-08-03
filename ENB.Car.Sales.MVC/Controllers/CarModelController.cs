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
    public class CarModelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CarModelController> _logger;
        private readonly IAsyncCarModelRepository  _asyncCarModelRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
      
        public CarModelController(IMapper mapper, ILogger<CarModelController> logger,
                                   IAsyncCarModelRepository  asyncCarModelRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf         )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCarModelRepository = asyncCarModelRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;           
        }

        // GET: CarModelController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCarModelData()
        {
            IQueryable<CarModel> allCarModel = _asyncCarModelRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayCarModel>>(allCarModel).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // POST: CarModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditCarModel createAndEditCarModel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (createAndEditCarModel.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {

                            CarModel dbCarModel = new();

                            _mapper.Map(createAndEditCarModel, dbCarModel);
                            await _asyncCarModelRepository.Add(dbCarModel);

                            _notyf.Success("CarModel Created  Successfully! ");

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

                            CarModel dbCarModelToUpdate = await _asyncCarModelRepository.FindById(createAndEditCarModel.Id);

                            _mapper.Map(createAndEditCarModel, dbCarModelToUpdate, typeof(CreateAndEditCarModel), typeof(CarModel));

                            _notyf.Success("CarModel Update  Successfully! ");

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


       

        //// POST: CarModelController/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CarModel dbCarModel = await _asyncCarModelRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncCarModelRepository.Remove(dbCarModel);

                _notyf.Error("CarModel Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}

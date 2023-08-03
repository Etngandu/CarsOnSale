using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Help;
using ENB.Car.Sales.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;



namespace ENB.Car.Sales.MVC.Controllers
{
    //[Authorize]
    public class FeaturesOnCarForSaleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FeaturesOnCarForSaleController> _logger;
        private readonly IAsyncCarModelRepository _asyncCarModelRepository;
        private readonly IAsyncCarFeatureRepository _asyncCarFeatureRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;

        public FeaturesOnCarForSaleController(IMapper mapper, ILogger<FeaturesOnCarForSaleController> logger,
                                   IAsyncCarModelRepository asyncCarModelRepository,
                                   IAsyncCarFeatureRepository asyncCarFeatureRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCarModelRepository = asyncCarModelRepository;
            _asyncCarFeatureRepository = asyncCarFeatureRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }

        // GET: FinanceCompanyController
        [HttpGet]
        public async Task<IActionResult> Index(int carForSaleId, int carModelId)
        {
            ViewBag.IdcarForSale = carForSaleId;
            ViewBag.IdcarModel = carModelId;

            var data = new CreateAndEditFeaturesOnCarsForSale()
            {

                ListCarFeatures = _asyncCarFeatureRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.Car_Feature_description,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var carModel = await _asyncCarModelRepository.FindById(carModelId);

            //ViewBag.Message = carModel.Model_Name;

            return View(data);
        }

        public async Task<IActionResult> FeaturesperCarForSaleList(int carForSaleId, int carModelId)
        {
            ViewBag.IdcarForSale = carForSaleId;
            ViewBag.IdcarModel = carModelId;

            var carmodel = await _asyncCarModelRepository.FindById(carModelId, x => x.CarForSales);
            var carforsale = carmodel.CarForSales.Where(y => y.Id == carForSaleId);
            ViewBag.Message = carmodel.Model_Name;

            return View();
        }

        public IActionResult GetFeaturesperCarForSaleData(int carForSaleId, int carModelId)
        {

            var allfeat = (from ftrs in _asyncCarFeatureRepository.FindAll().SelectMany(y => y.FeaturesOnCarsForSales)
                           join cfs in _asyncCarModelRepository.FindAll().Where(x => x.Id == carModelId).SelectMany(z => z.CarForSales)
                                .Where(csf => csf.Id == carForSaleId) on ftrs.CarForSaleId equals cfs.Id
                           join ft in _asyncCarFeatureRepository.FindAll() on ftrs.CarFeatureId equals ft.Id
                           select new DisplayFeaturesOnCarsForSale
                           {

                               Id = ftrs.Id,
                               CarFeatureId = ftrs.CarFeatureId,
                               NameCarFeature = ft.Car_Feature_description,
                               Other_Details=cfs.Other_details!,
                               DateCreated = ftrs.DateCreated,
                               DateModified = ftrs.DateModified

                           }).ToList();


            return Json(new { data = allfeat });


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CreateAndEditFeaturesOnCarsForSale createAndEditFeaturesOnCarsForSale,
                                                 int carForSaleId, int carModelId, IFormCollection form)
        {
            

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (createAndEditFeaturesOnCarsForSale.Id == 0)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {


                            var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.CarForSales);
                            var carforsale = carModel.CarForSales.Single(y => y.Id == carForSaleId);


                            FeaturesOnCarsForSale featuresOnCarsForSale = new();

                            _mapper.Map(createAndEditFeaturesOnCarsForSale, featuresOnCarsForSale);

                            carforsale.FeaturesOnCarsForSales.Add(featuresOnCarsForSale);

                            _notyf.Success("FeaturesOnCarsForSales  Added  Successfully! ");

                            return RedirectToAction(nameof(Index), new { carForSaleId,carModelId });
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


                createAndEditFeaturesOnCarsForSale.ListCarFeatures = _asyncCarFeatureRepository.FindAll()
                            .Select(d => new SelectListItem
                            {
                                Text = d.Car_Feature_description,
                                Value = d.Id.ToString(),
                                Selected = true

                            }).Distinct().ToList();
                ViewBag.IdcarForSaler = createAndEditFeaturesOnCarsForSale.CarForSaleId;
            }
            else
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        await using (await _asyncUnitOfWorkFactory.Create())
                        {


                            var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.FeaturesOnCarsForSales);
                            var featuresOnCarForSale = await Task.FromResult(carModel.FeaturesOnCarsForSales.Where(y => y.CarForSaleId == carForSaleId)
                                                        .Single(z=>z.Id==createAndEditFeaturesOnCarsForSale.Id));
                           

                            _mapper.Map(createAndEditFeaturesOnCarsForSale, featuresOnCarForSale);
                           

                            _notyf.Success("subjectTeacher  Added  Successfully! ");

                            return RedirectToAction(nameof(Index), new { carForSaleId,carModelId });
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
            return View("Index", createAndEditFeaturesOnCarsForSale);
        }

        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> FeaturesperCarForSaleDelete(int carForSaleId, int carModelId, int carFeatureId)
        {
            ViewBag.IdcarForsale = carForSaleId;
            ViewBag.IdcarModel = carModelId;
            ViewBag.IdcarFeature = carFeatureId;


            var allFeat = (from ftrs in _asyncCarFeatureRepository.FindAll().Where(x => x.Id == carFeatureId).SelectMany(y => y.FeaturesOnCarsForSales)
                                     .Where(y => y.CarModelId == carModelId)
                           join ft in _asyncCarFeatureRepository.FindAll() on ftrs.CarFeatureId equals ft.Id
                           join cfs in _asyncCarModelRepository.FindAll().Where(x => x.Id == carModelId).SelectMany(z => z.CarForSales) on ftrs.CarForSaleId equals cfs.Id
                           select new DisplayFeaturesOnCarsForSale
                           {

                               Id = ftrs.CarForSaleId,
                               NameCarFeature = ft.Car_Feature_description,
                               DateCreated = ftrs.DateCreated,
                               DateModified = ftrs.DateModified

                           }).ToList();





            var mysglFeat = await Task.FromResult(allFeat.Single(y => y.CarForSaleId == carForSaleId));


            ViewBag.Message = mysglFeat.NameCarFeature;
            return View(mysglFeat);
        }

      //  POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeaturesperCarForSaleDelete(DisplayFeaturesOnCarsForSale displayFeaturesOnCarsForSale, 
                                                                      int carForSaleId, 
                                                                      int carFeatureId,
                                                                      int carModelId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var carModel = await _asyncCarModelRepository.FindById(carModelId, x => x.FeaturesOnCarsForSales);
                var featureperCarForsale = await Task.FromResult(carModel.FeaturesOnCarsForSales.Where(cf=>cf.CarFeatureId==carFeatureId)
                                               .Where(cfs=>cfs.CarForSaleId==carForSaleId).Single(y => y.Id == displayFeaturesOnCarsForSale.Id));

                carModel.FeaturesOnCarsForSales.Remove(featureperCarForsale);

               
                _notyf.Error("subjectTeacher  removed  Successfully");
            }
            return RedirectToAction(nameof(Index), new { carForSaleId,carModelId });
        }
    }
}


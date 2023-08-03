using AspNetCoreHero.ToastNotification;
using AutoMapper;
using ENB.Car.Sales.EF;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Infrastructure;
using ENB.Car.Sales.MVC.Factory;
using ENB.Car.Sales.MVC.Help;
using ENB.Car.Sales.EF.Repositories;
using ENB.Car.Sales.Entities.Repositories;
using ENB.Car.Sales.MVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CarSalesContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("CarSalesCtr")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               opt =>
               {
                   opt.Password.RequiredLength = 7;
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireUppercase = false;
               })
                .AddEntityFrameworkStores<CarSalesContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddAutoMapper(typeof(CarSalesProfile));
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAsyncCustomerRepository, AsyncCustomerRepository>();
builder.Services.AddScoped<IAsyncCarFeatureRepository, AsyncCarFeatureRepository>();
builder.Services.AddScoped<IAsyncInsuranceCompanyRepository, AsyncInsuranceCompanyRepository>();
builder.Services.AddScoped<IAsyncFinanceCompanyRepository, AsyncFinanceCompanyRepository>();
builder.Services.AddScoped<IAsyncCarModelRepository, AsyncCarModelRepository>();
builder.Services.AddScoped<IAsyncCarManufacturerRepository, AsyncCarManufacturerRepository>();
builder.Services.AddScoped<IAsyncUnitOfWorkFactory, AsyncEFUnitOfWorkFactory>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<IValidator<CreateAndEditCustomer>, CreateAndEditCustomerValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

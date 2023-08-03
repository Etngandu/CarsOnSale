using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.MVC.Models;

namespace ENB.Car.Sales.MVC.Help
{
    public class CarSalesProfile : Profile
    {
        public CarSalesProfile()
        {


            #region Customer 
            CreateMap<Customer, DisplayCustomer>();

            CreateMap<CreateAndEditCustomer, Customer>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.AddressCustomer, t => t.Ignore());
            CreateMap<Customer, CreateAndEditCustomer>();
            #endregion


            #region CarFeature
            CreateMap<CarFeature, DisplayCarFeature>()
             .ForMember(d => d.CustomerPreferences, t => t.Ignore());
            CreateMap<CreateAndEditCarFeature, CarFeature>()
              .ForMember(d => d.CustomerPreferences, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<CarFeature, CreateAndEditCarFeature>();

            #endregion

            #region Identity
            CreateMap<UserRegistrationModel, ApplicationUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #endregion

            #region InsuranceCompany
            CreateMap<InsuranceCompany, DisplayInsuranceCompany>()
             .ForMember(d => d.InsurancePolicies, t => t.Ignore());
            CreateMap<CreateAndEditInsuranceCompany, InsuranceCompany>()
              .ForMember(d => d.InsurancePolicies, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<InsuranceCompany, CreateAndEditInsuranceCompany>();

            #endregion

            #region FinanceCompany
            CreateMap<FinanceCompany, DisplayFinanceCompany>()
             .ForMember(d => d.CarLoans, t => t.Ignore());
            CreateMap<CreateAndEditFinanceCompany, FinanceCompany>()
              .ForMember(d => d.CarLoans, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<FinanceCompany, CreateAndEditFinanceCompany>();

            #endregion

            #region CarModel
            CreateMap<CarModel, DisplayCarModel>()
             .ForMember(d => d.CarForSales, t => t.Ignore());
            CreateMap<CreateAndEditCarModel, CarModel>()
              .ForMember(d => d.CarForSales, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<CarModel, CreateAndEditCarModel>();

            #endregion

            #region CarManufacturer
            CreateMap<CarManufacturer, DisplayCarManufacturer>()
             .ForMember(d => d.CarForSales, t => t.Ignore());
            CreateMap<CreateAndEditCarManufacturer, CarManufacturer>()
              .ForMember(d => d.CarForSales, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<CarManufacturer, CreateAndEditCarManufacturer>();

            #endregion
            #region CustomerPreference 
            CreateMap<CustomerPreference, DisplayCustomerPreference>()
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.CarFeature, t => t.Ignore())
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId));
            CreateMap<CreateAndEditCustomerPreference, CustomerPreference>()
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.CarFeature, t => t.Ignore())
              .ForMember(d => d.Customer, t => t.Ignore());          
            CreateMap<CustomerPreference, CreateAndEditCustomerPreference>();
            #endregion


            #region CarForSale
            CreateMap<CarForSale, DisplayCarForSale>()
              .ForMember(d => d.CarModel, t => t.Ignore())             
              .ForMember(d => d.CarModelId, t => t.MapFrom(y => y.CarModelId));
            CreateMap<CreateAndEditCarForSale, CarForSale>()
              .ForMember(d => d.CarModel, t => t.Ignore())              
              .ForMember(d => d.CarModelId, t => t.MapFrom(y => y.CarModelId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<CarForSale, CreateAndEditCarForSale>();

            #endregion

            #region CarSold
            CreateMap<CarSold, DisplayCarSold>()
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.CarForSale, t => t.Ignore())
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.CarForSaleId, t => t.MapFrom(y => y.CarForSaleId));
            CreateMap<CreateAndEditCarSold, CarSold>()
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))             
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.CarForSale, t => t.Ignore())
              .ForMember(d => d.Customer, t => t.Ignore());
            CreateMap<CarSold, CreateAndEditCarSold>();
            #endregion


            #region CustomerPayment
            CreateMap<CustomerPayment, DisplayCustomerPayment>()
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.CarSold, t => t.Ignore())              
              .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId));
            CreateMap<CreateAndEditCustomerPayment, CustomerPayment>()
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.CarSold, t => t.Ignore())
              .ForMember(d => d.Customer, t => t.Ignore());             
            CreateMap<CustomerPayment, CreateAndEditCustomerPayment>();
            #endregion

            #region CarLoan
            CreateMap<CarLoan, DisplayCarLoan>()
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.CarSold, t => t.Ignore())             
              .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId));
            CreateMap<CreateAndEditCarLoan, CarLoan>()
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))             
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.CarSold, t => t.Ignore())
              .ForMember(d => d.Customer, t => t.Ignore());
            CreateMap<CarLoan, CreateAndEditCarLoan>();
            #endregion


            #region InsurancePolicy 
            CreateMap<InsurancePolicy, DisplayInsurancePolicy>()
              .ForMember(d => d.Customer, t => t.Ignore())
             .ForMember(d => d.CarSold, t => t.Ignore())
             .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId));
            CreateMap<CreateAndEditInsurancePolicy, InsurancePolicy>()
              .ForMember(d => d.DateCreated, t => t.Ignore())             
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.CarSoldId, t => t.MapFrom(y => y.CarSoldId))
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.CarSold, t => t.Ignore());
            CreateMap<InsurancePolicy, CreateAndEditInsurancePolicy>();
            #endregion

            #region FeaturesOnCarsForSales
            CreateMap<FeaturesOnCarsForSale, DisplayFeaturesOnCarsForSale>()
             .ForMember(d => d.CarFeature, t => t.Ignore())
             .ForMember(d => d.CarForSale, t => t.Ignore())
             .ForMember(d => d.CarModel, t => t.Ignore())
             .ForMember(d => d.CarForSaleId, t => t.MapFrom(y => y.CarForSaleId))
              .ForMember(d => d.CarFeatureId, t => t.MapFrom(y => y.CarFeatureId))
             .ForMember(d => d.CarModelId, t => t.MapFrom(y => y.CarModelId));

            CreateMap<CreateAndEditFeaturesOnCarsForSale, FeaturesOnCarsForSale>()
              .ForMember(d => d.CarModelId, t => t.MapFrom(y => y.CarModelId))
              .ForMember(d => d.CarForSaleId, t => t.MapFrom(y => y.CarForSaleId))
              .ForMember(d => d.CarModel, t => t.Ignore())
              .ForMember(d => d.CarForSale, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ReverseMap();

            #endregion

            //#region PatientStaff
            //CreateMap<Staff_Patient_Association, DisplayStaff_Patient_Association>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditStaff_Patient_Association, Staff_Patient_Association>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            //#region Appointment
            //CreateMap<Appointment, DisplayAppointment>()
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditAppointment, Appointment>()
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.Color, t => t.MapFrom(y => y.EventStatus.ToString()))
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();
            //#endregion


            //#region PatientRecord
            //CreateMap<Patient_Record, DisplayPatientRecord>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditPatientRecord, Patient_Record>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion


            #region Address

            CreateMap<Address, EditAddress>()
                  .ForMember(d => d.CustomerId, t => t.Ignore())
                  .ForMember(d => d.StaffId, t => t.Ignore());
            CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Number_street!, s.City!, s.Zipcode!, s.State_province_county!, s.Country!));
            #endregion
        }
    }
}

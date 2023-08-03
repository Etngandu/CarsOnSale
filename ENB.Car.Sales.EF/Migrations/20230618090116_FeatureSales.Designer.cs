﻿// <auto-generated />
using System;
using ENB.Car.Sales.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ENB.Car.Sales.EF.Migrations
{
    [DbContext(typeof(CarSalesContext))]
    [Migration("20230618090116_FeatureSales")]
    partial class FeatureSales
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ENB.Car.Sales.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Car_Feature_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CarFeature");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarForSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Asking_Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CarManufacturerId")
                        .HasColumnType("int");

                    b.Property<int>("CarModelId")
                        .HasColumnType("int");

                    b.Property<string>("Current_Mileage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Acquired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Other_details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Registration_Year")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehiculeCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarManufacturerId");

                    b.HasIndex("CarModelId");

                    b.ToTable("CarForSales");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarLoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarSoldId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinanceCompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Monthtly_Repayment")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Other_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Repayment_End_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Repayment_Start_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarSoldId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FinanceCompanyId");

                    b.ToTable("CarLoan");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarManufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer_FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer_OtherDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer_Shortname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarManufacturer");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarModel");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarSold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Agreed_Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CarForSaleId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Sold")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monthly_Payment_Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Monthly_Payment_Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarForSaleId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CarSold");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Other_details")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CustomerPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Actual_Payment_Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CarSoldId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Customer_Payment_Date_Due")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Customer_Payment_Date_Made")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Payment_Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarSoldId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPayment");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CustomerPreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarFeatureId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Customer_Preference_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarFeatureId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPreference");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.FeaturesOnCarsForSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarFeatureId")
                        .HasColumnType("int");

                    b.Property<int>("CarForSaleId")
                        .HasColumnType("int");

                    b.Property<int?>("CarModelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarFeatureId");

                    b.HasIndex("CarForSaleId");

                    b.HasIndex("CarModelId");

                    b.ToTable("FeaturesOnCarsForSale");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.FinanceCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Finance_Company_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinanceCompany");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.InsuranceCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Insurance_Company_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InsuranceCompany");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.InsurancePolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarSoldId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceCompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Monthtly_payment")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Other_Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Policy_Renewal_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Policy_Start_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarSoldId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InsuranceCompanyId");

                    b.ToTable("InsurancePolicy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f541896a-3a14-485f-a56c-360105cff76c",
                            Name = "Visitor",
                            NormalizedName = "VISITOR"
                        },
                        new
                        {
                            Id = "2a1bb9f2-a099-427c-9f64-e3b8c999857c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "1694d51b-96ce-4321-8e25-a2601696f895",
                            Name = "Visitor",
                            NormalizedName = "VISITOR"
                        },
                        new
                        {
                            Id = "2201cc13-6c0b-41bb-aea1-d1ed35b9223b",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarForSale", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarManufacturer", "CarManufacturer")
                        .WithMany("CarForSales")
                        .HasForeignKey("CarManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.CarModel", "CarModel")
                        .WithMany("CarForSales")
                        .HasForeignKey("CarModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarManufacturer");

                    b.Navigation("CarModel");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarLoan", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarSold", "CarSold")
                        .WithMany("CarLoans")
                        .HasForeignKey("CarSoldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.Customer", "Customer")
                        .WithMany("CarLoans")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ENB.Car.Sales.Entities.FinanceCompany", "FinanceCompany")
                        .WithMany("CarLoans")
                        .HasForeignKey("FinanceCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarSold");

                    b.Navigation("Customer");

                    b.Navigation("FinanceCompany");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarSold", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarForSale", "CarForSale")
                        .WithMany("CarSolds")
                        .HasForeignKey("CarForSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.Customer", "Customer")
                        .WithMany("CarSolds")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarForSale");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.Customer", b =>
                {
                    b.OwnsOne("ENB.Car.Sales.Entities.Address", "AddressCustomer", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasDefaultValue("")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasDefaultValue("")
                                .HasColumnName("Country");

                            b1.Property<string>("Number_street")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)")
                                .HasDefaultValue("")
                                .HasColumnName("Numberstreet");

                            b1.Property<string>("State_province_county")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasDefaultValue("")
                                .HasColumnName("State_province_county");

                            b1.Property<string>("Zipcode")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(12)
                                .HasColumnType("nvarchar(12)")
                                .HasDefaultValue("")
                                .HasColumnName("ZipCode");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("AddressCustomer")
                        .IsRequired();
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CustomerPayment", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarSold", "CarSold")
                        .WithMany("CustomerPayments")
                        .HasForeignKey("CarSoldId");

                    b.HasOne("ENB.Car.Sales.Entities.Customer", "Customer")
                        .WithMany("CustomerPayments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarSold");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CustomerPreference", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarFeature", "CarFeature")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("CarFeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.Customer", "Customer")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarFeature");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.FeaturesOnCarsForSale", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarFeature", "CarFeature")
                        .WithMany("FeaturesOnCarsForSales")
                        .HasForeignKey("CarFeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.CarForSale", "CarForSale")
                        .WithMany("FeaturesOnCarsForSales")
                        .HasForeignKey("CarForSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.CarModel", "CarModel")
                        .WithMany("FeaturesOnCarsForSales")
                        .HasForeignKey("CarModelId");

                    b.Navigation("CarFeature");

                    b.Navigation("CarForSale");

                    b.Navigation("CarModel");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.InsurancePolicy", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.CarSold", "CarSold")
                        .WithMany("InsurancePolicies")
                        .HasForeignKey("CarSoldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.Customer", "Customer")
                        .WithMany("InsurancePolicies")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ENB.Car.Sales.Entities.InsuranceCompany", "InsuranceCompany")
                        .WithMany("InsurancePolicies")
                        .HasForeignKey("InsuranceCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarSold");

                    b.Navigation("Customer");

                    b.Navigation("InsuranceCompany");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENB.Car.Sales.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ENB.Car.Sales.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarFeature", b =>
                {
                    b.Navigation("CustomerPreferences");

                    b.Navigation("FeaturesOnCarsForSales");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarForSale", b =>
                {
                    b.Navigation("CarSolds");

                    b.Navigation("FeaturesOnCarsForSales");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarManufacturer", b =>
                {
                    b.Navigation("CarForSales");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarModel", b =>
                {
                    b.Navigation("CarForSales");

                    b.Navigation("FeaturesOnCarsForSales");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.CarSold", b =>
                {
                    b.Navigation("CarLoans");

                    b.Navigation("CustomerPayments");

                    b.Navigation("InsurancePolicies");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.Customer", b =>
                {
                    b.Navigation("CarLoans");

                    b.Navigation("CarSolds");

                    b.Navigation("CustomerPayments");

                    b.Navigation("CustomerPreferences");

                    b.Navigation("InsurancePolicies");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.FinanceCompany", b =>
                {
                    b.Navigation("CarLoans");
                });

            modelBuilder.Entity("ENB.Car.Sales.Entities.InsuranceCompany", b =>
                {
                    b.Navigation("InsurancePolicies");
                });
#pragma warning restore 612, 618
        }
    }
}

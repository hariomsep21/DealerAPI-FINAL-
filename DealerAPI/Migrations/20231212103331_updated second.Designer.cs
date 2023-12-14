﻿// <auto-generated />
using System;
using DealerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DealerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231212103331_updated second")]
    partial class updatedsecond
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dealer.Model.BankDetail", b =>
                {
                    b.Property<int>("RepaymentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepaymentDetailId"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IFSCCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepaymentDetailId");

                    b.ToTable("BankDetails");
                });

            modelBuilder.Entity("Dealer.Model.Car", b =>
                {
                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Variant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Dealer.Model.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Amount_Due")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Facility_Availed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Invoice_Charges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaymentProofImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<decimal?>("ProcessingCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("CarId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Dealer.Model.ProcDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ClosedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilterId")
                        .HasColumnType("int");

                    b.Property<int>("PayId")
                        .HasColumnType("int");

                    b.Property<string>("Purchased_Amount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilterId");

                    b.HasIndex("PayId");

                    b.ToTable("procDetails");
                });

            modelBuilder.Entity("Dealer.Model.ProcurementFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ProcurementFilters");
                });

            modelBuilder.Entity("Dealer.Model.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("Dealer.Model.StockAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("image1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("varified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("StockAudits");
                });

            modelBuilder.Entity("Dealer.Model.Vehiclerecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Blacklist")
                        .HasColumnType("bit");

                    b.Property<int>("CId")
                        .HasColumnType("int");

                    b.Property<bool>("Challan")
                        .HasColumnType("bit");

                    b.Property<bool>("Fitness")
                        .HasColumnType("bit");

                    b.Property<bool>("Hypothecation")
                        .HasColumnType("bit");

                    b.Property<bool>("OwnerName")
                        .HasColumnType("bit");

                    b.Property<bool>("RcStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CId");

                    b.ToTable("VehicleRecords");
                });

            modelBuilder.Entity("DealerAPI.Models.AccountDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("IFSCCode")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserInfoId");

                    b.ToTable("AccountDetailstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.CustomerSupport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Call")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerSupporttbl");
                });

            modelBuilder.Entity("DealerAPI.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Notificationstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.Order_StockAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChooseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockAudit_PurposeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockAudit_PurposeId");

                    b.ToTable("Order_StockAuditstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Make", b =>
                {
                    b.Property<int>("MakeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MakeId"));

                    b.Property<int>("MakeCode")
                        .HasColumnType("int");

                    b.Property<string>("MakeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MakeId");

                    b.ToTable("PVA_Maketbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<int>("ModelCode")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelId");

                    b.ToTable("PVA_Modeltbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Variant", b =>
                {
                    b.Property<int>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariantId"));

                    b.Property<int>("VariantCode")
                        .HasColumnType("int");

                    b.Property<string>("VariantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VariantId");

                    b.ToTable("PVA_Varianttbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_YearOfReg", b =>
                {
                    b.Property<int>("YearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("YearId"));

                    b.Property<int>("YearCode")
                        .HasColumnType("int");

                    b.HasKey("YearId");

                    b.ToTable("PVA_YearOfRegtbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PV_NewCarDealer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Invoice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OdometerPicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictOfOrginalRC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehiclePicFromBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehiclePicFromFront")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("PV_NewCarDealerstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.PV_OpenMarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OdometerPicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentProof")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureOfOriginalRC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerAdhaar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerContactNumber")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("SellerEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerPAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehiclePictureFromBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehiclePictureFromFront")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WithholdAmount")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("PV_OpenMarketstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.ProfileInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountDetails")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("AlternativeNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ResidenceAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShopAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("ProfileInformationtbl");
                });

            modelBuilder.Entity("DealerAPI.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"));

                    b.Property<string>("StateCode")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StateId");

                    b.ToTable("Statetbl");
                });

            modelBuilder.Entity("DealerAPI.Models.StockAudit_Purpose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PurposeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StockAudit_Purposestbl");
                });

            modelBuilder.Entity("DealerAPI.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OTP")
                        .HasColumnType("int");

                    b.Property<DateTime>("OTPExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SId");

                    b.ToTable("Userstbl");
                });

            modelBuilder.Entity("DealerAPI.Models.UserPhone", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneId"));

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhoneId");

                    b.ToTable("userPhonestbl");
                });

            modelBuilder.Entity("PV_Aggregator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("PriceBreak")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RCAvailable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockIn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.Property<int>("VariantId")
                        .HasColumnType("int");

                    b.Property<int>("YearOfRegistration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("UserInfoId");

                    b.HasIndex("VariantId");

                    b.HasIndex("YearOfRegistration");

                    b.ToTable("PV_Aggregatorstbl");
                });

            modelBuilder.Entity("Dealer.Model.Car", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfo")
                        .WithMany("cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Dealer.Model.Payment", b =>
                {
                    b.HasOne("Dealer.Model.BankDetail", "BankDetail")
                        .WithMany("Payments")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dealer.Model.Car", "Car")
                        .WithMany("Payments")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankDetail");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Dealer.Model.ProcDetails", b =>
                {
                    b.HasOne("Dealer.Model.ProcurementFilter", "ProcurementFilter")
                        .WithMany()
                        .HasForeignKey("FilterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dealer.Model.Payment", "Payment")
                        .WithMany("procDetails")
                        .HasForeignKey("PayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("ProcurementFilter");
                });

            modelBuilder.Entity("Dealer.Model.StockAudit", b =>
                {
                    b.HasOne("Dealer.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Dealer.Model.Vehiclerecord", b =>
                {
                    b.HasOne("Dealer.Model.Car", "Car")
                        .WithMany("vehiclerecords")
                        .HasForeignKey("CId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("DealerAPI.Models.AccountDetails", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("accountDetails")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.Notification", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("Notifications")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.Order_StockAudit", b =>
                {
                    b.HasOne("DealerAPI.Models.StockAudit_Purpose", "StockAudit_Purpose")
                        .WithMany("Order_StockAudits")
                        .HasForeignKey("StockAudit_PurposeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockAudit_Purpose");
                });

            modelBuilder.Entity("DealerAPI.Models.PV_NewCarDealer", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("pV_NewCarDealers")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.PV_OpenMarket", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("pV_OpenMarkets")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.ProfileInformation", b =>
                {
                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("profileInformations")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.UserInfo", b =>
                {
                    b.HasOne("DealerAPI.Models.State", "State")
                        .WithMany("UserInfos")
                        .HasForeignKey("SId");

                    b.Navigation("State");
                });

            modelBuilder.Entity("PV_Aggregator", b =>
                {
                    b.HasOne("DealerAPI.Models.PVA_Make", "Make")
                        .WithMany("PV_Aggregators")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DealerAPI.Models.PVA_Model", "Model")
                        .WithMany("PV_Aggregators")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DealerAPI.Models.UserInfo", "UserInfos")
                        .WithMany("pV_Aggregators")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DealerAPI.Models.PVA_Variant", "Variant")
                        .WithMany("PV_Aggregators")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DealerAPI.Models.PVA_YearOfReg", "PVA_YearOfReg")
                        .WithMany("PV_Aggregators")
                        .HasForeignKey("YearOfRegistration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");

                    b.Navigation("Model");

                    b.Navigation("PVA_YearOfReg");

                    b.Navigation("UserInfos");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("Dealer.Model.BankDetail", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Dealer.Model.Car", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("vehiclerecords");
                });

            modelBuilder.Entity("Dealer.Model.Payment", b =>
                {
                    b.Navigation("procDetails");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Make", b =>
                {
                    b.Navigation("PV_Aggregators");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Model", b =>
                {
                    b.Navigation("PV_Aggregators");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_Variant", b =>
                {
                    b.Navigation("PV_Aggregators");
                });

            modelBuilder.Entity("DealerAPI.Models.PVA_YearOfReg", b =>
                {
                    b.Navigation("PV_Aggregators");
                });

            modelBuilder.Entity("DealerAPI.Models.State", b =>
                {
                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DealerAPI.Models.StockAudit_Purpose", b =>
                {
                    b.Navigation("Order_StockAudits");
                });

            modelBuilder.Entity("DealerAPI.Models.UserInfo", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("accountDetails");

                    b.Navigation("cars");

                    b.Navigation("pV_Aggregators");

                    b.Navigation("pV_NewCarDealers");

                    b.Navigation("pV_OpenMarkets");

                    b.Navigation("profileInformations");
                });
#pragma warning restore 612, 618
        }
    }
}

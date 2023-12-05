using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class phon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    RepaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.RepaymentDetailId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSupporttbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Call = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSupporttbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PVA_Maketbl",
                columns: table => new
                {
                    MakeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakeCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVA_Maketbl", x => x.MakeId);
                });

            migrationBuilder.CreateTable(
                name: "PVA_Modeltbl",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVA_Modeltbl", x => x.ModelId);
                });

            migrationBuilder.CreateTable(
                name: "PVA_Varianttbl",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariantCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVA_Varianttbl", x => x.VariantId);
                });

            migrationBuilder.CreateTable(
                name: "PVA_YearOfRegtbl",
                columns: table => new
                {
                    YearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVA_YearOfRegtbl", x => x.YearId);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statetbl",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statetbl", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Statustbl",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statustbl", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "StockAudit_Purposestbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurposeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAudit_Purposestbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userPhonestbl",
                columns: table => new
                {
                    PhoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPhonestbl", x => x.PhoneId);
                });

            migrationBuilder.CreateTable(
                name: "Order_StockAuditstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockAudit_PurposeId = table.Column<int>(type: "int", nullable: false),
                    ChooseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_StockAuditstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_StockAuditstbl_StockAudit_Purposestbl_StockAudit_PurposeId",
                        column: x => x.StockAudit_PurposeId,
                        principalTable: "StockAudit_Purposestbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Userstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    PhnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Userstbl_Statetbl_StateId",
                        column: x => x.StateId,
                        principalTable: "Statetbl",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Userstbl_Statustbl_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statustbl",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Userstbl_userPhonestbl_PhnId",
                        column: x => x.PhnId,
                        principalTable: "userPhonestbl",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetailstbl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IFSCCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetailstbl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountDetailstbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificationstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificationstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificationstbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileInformationtbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResidenceAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AlternativeNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountDetails = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileInformationtbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileInformationtbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PV_Aggregatorstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    YearOfRegistration = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    PriceBreak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RCAvailable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PV_Aggregatorstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PV_Aggregatorstbl_PVA_Maketbl_MakeId",
                        column: x => x.MakeId,
                        principalTable: "PVA_Maketbl",
                        principalColumn: "MakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PV_Aggregatorstbl_PVA_Modeltbl_ModelId",
                        column: x => x.ModelId,
                        principalTable: "PVA_Modeltbl",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PV_Aggregatorstbl_PVA_Varianttbl_VariantId",
                        column: x => x.VariantId,
                        principalTable: "PVA_Varianttbl",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PV_Aggregatorstbl_PVA_YearOfRegtbl_YearOfRegistration",
                        column: x => x.YearOfRegistration,
                        principalTable: "PVA_YearOfRegtbl",
                        principalColumn: "YearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PV_Aggregatorstbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PV_NewCarDealerstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdometerPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePicFromFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePicFromBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictOfOrginalRC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PV_NewCarDealerstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PV_NewCarDealerstbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PV_OpenMarketstbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WithholdAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerContactNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    SellerEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentProof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerAdhaar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureOfOriginalRC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdometerPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePictureFromFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePictureFromBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PV_OpenMarketstbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PV_OpenMarketstbl_Userstbl_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount_Due = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProcessingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Facility_Availed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Invoice_Charges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_BankDetails_BankId",
                        column: x => x.BankId,
                        principalTable: "BankDetails",
                        principalColumn: "RepaymentDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAudits_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Challan = table.Column<bool>(type: "bit", nullable: false),
                    RcStatus = table.Column<bool>(type: "bit", nullable: false),
                    Fitness = table.Column<bool>(type: "bit", nullable: false),
                    OwnerName = table.Column<bool>(type: "bit", nullable: false),
                    Hypothecation = table.Column<bool>(type: "bit", nullable: false),
                    Blacklist = table.Column<bool>(type: "bit", nullable: false),
                    CId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleRecords_Cars_CId",
                        column: x => x.CId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "procDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    FilterId = table.Column<int>(type: "int", nullable: false),
                    Purchased_Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_procDetails_Payment_PayId",
                        column: x => x.PayId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_procDetails_ProcurementFilters_FilterId",
                        column: x => x.FilterId,
                        principalTable: "ProcurementFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetailstbl_UserInfoId",
                table: "AccountDetailstbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserInfoId",
                table: "Cars",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificationstbl_UserInfoId",
                table: "Notificationstbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StockAuditstbl_StockAudit_PurposeId",
                table: "Order_StockAuditstbl",
                column: "StockAudit_PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BankId",
                table: "Payment",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CarId",
                table: "Payment",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_procDetails_FilterId",
                table: "procDetails",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_procDetails_PayId",
                table: "procDetails",
                column: "PayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileInformationtbl_UserInfoId",
                table: "ProfileInformationtbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_Aggregatorstbl_MakeId",
                table: "PV_Aggregatorstbl",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_Aggregatorstbl_ModelId",
                table: "PV_Aggregatorstbl",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_Aggregatorstbl_UserInfoId",
                table: "PV_Aggregatorstbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_Aggregatorstbl_VariantId",
                table: "PV_Aggregatorstbl",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_Aggregatorstbl_YearOfRegistration",
                table: "PV_Aggregatorstbl",
                column: "YearOfRegistration");

            migrationBuilder.CreateIndex(
                name: "IX_PV_NewCarDealerstbl_UserInfoId",
                table: "PV_NewCarDealerstbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PV_OpenMarketstbl_UserInfoId",
                table: "PV_OpenMarketstbl",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAudits_CarId",
                table: "StockAudits",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Userstbl_PhnId",
                table: "Userstbl",
                column: "PhnId");

            migrationBuilder.CreateIndex(
                name: "IX_Userstbl_StateId",
                table: "Userstbl",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Userstbl_StatusId",
                table: "Userstbl",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRecords_CId",
                table: "VehicleRecords",
                column: "CId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetailstbl");

            migrationBuilder.DropTable(
                name: "CustomerSupporttbl");

            migrationBuilder.DropTable(
                name: "Notificationstbl");

            migrationBuilder.DropTable(
                name: "Order_StockAuditstbl");

            migrationBuilder.DropTable(
                name: "procDetails");

            migrationBuilder.DropTable(
                name: "ProfileInformationtbl");

            migrationBuilder.DropTable(
                name: "PV_Aggregatorstbl");

            migrationBuilder.DropTable(
                name: "PV_NewCarDealerstbl");

            migrationBuilder.DropTable(
                name: "PV_OpenMarketstbl");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "StockAudits");

            migrationBuilder.DropTable(
                name: "VehicleRecords");

            migrationBuilder.DropTable(
                name: "StockAudit_Purposestbl");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ProcurementFilters");

            migrationBuilder.DropTable(
                name: "PVA_Maketbl");

            migrationBuilder.DropTable(
                name: "PVA_Modeltbl");

            migrationBuilder.DropTable(
                name: "PVA_Varianttbl");

            migrationBuilder.DropTable(
                name: "PVA_YearOfRegtbl");

            migrationBuilder.DropTable(
                name: "BankDetails");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Userstbl");

            migrationBuilder.DropTable(
                name: "Statetbl");

            migrationBuilder.DropTable(
                name: "Statustbl");

            migrationBuilder.DropTable(
                name: "userPhonestbl");
        }
    }
}

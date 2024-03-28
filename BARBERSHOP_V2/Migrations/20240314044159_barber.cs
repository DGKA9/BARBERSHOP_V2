using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BARBERSHOP_V2.Migrations
{
    public partial class barber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    cateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cateName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.cateID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    countryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.countryID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notiTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notiID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    payID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payMethod = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    payStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.payID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    roleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    serCateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serCateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.serCateID);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    workingHourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    endTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.workingHourID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    cityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    countryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.cityID);
                    table.ForeignKey(
                        name: "FK_City_Country_countryID",
                        column: x => x.countryID,
                        principalTable: "Country",
                        principalColumn: "countryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    roleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                    table.CheckConstraint("CK_userName_no_whitespace", "CHARINDEX(' ', userName) = 0");
                    table.ForeignKey(
                        name: "FK_Users_Role_roleID",
                        column: x => x.roleID,
                        principalTable: "Role",
                        principalColumn: "roleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    serID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serPrice = table.Column<float>(type: "real", nullable: false),
                    serTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    serCateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.serID);
                    table.ForeignKey(
                        name: "FK_Service_ServiceCategories_serCateID",
                        column: x => x.serCateID,
                        principalTable: "ServiceCategories",
                        principalColumn: "serCateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    storeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    workingHourID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.storeID);
                    table.ForeignKey(
                        name: "FK_Stores_WorkingHours_workingHourID",
                        column: x => x.workingHourID,
                        principalTable: "WorkingHours",
                        principalColumn: "workingHourID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    addressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currentAddress = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    subDistrict = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    cityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.addressID);
                    table.ForeignKey(
                        name: "FK_Address_City_cityID",
                        column: x => x.cityID,
                        principalTable: "City",
                        principalColumn: "cityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    numberphone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerID);
                    table.ForeignKey(
                        name: "FK_Customer_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationStore",
                columns: table => new
                {
                    locationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cityID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationStore", x => x.locationID);
                    table.ForeignKey(
                        name: "FK_LocationStore_City_cityID",
                        column: x => x.cityID,
                        principalTable: "City",
                        principalColumn: "cityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationStore_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceManagement",
                columns: table => new
                {
                    serManagerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storeID = table.Column<int>(type: "int", nullable: false),
                    serID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceManagement", x => x.serManagerID);
                    table.ForeignKey(
                        name: "FK_ServiceManagement_Service_serID",
                        column: x => x.serID,
                        principalTable: "Service",
                        principalColumn: "serID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceManagement_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    numberphone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    wordDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false),
                    addressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Address_addressID",
                        column: x => x.addressID,
                        principalTable: "Address",
                        principalColumn: "addressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    producerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.producerID);
                    table.ForeignKey(
                        name: "FK_Producers_Address_addressID",
                        column: x => x.addressID,
                        principalTable: "Address",
                        principalColumn: "addressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    warehouseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    warehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalAsset = table.Column<float>(type: "real", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    addressID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.warehouseID);
                    table.ForeignKey(
                        name: "FK_Warehouse_Address_addressID",
                        column: x => x.addressID,
                        principalTable: "Address",
                        principalColumn: "addressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouse_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    cusAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    addressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.cusAddressId);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Address_addressID",
                        column: x => x.addressID,
                        principalTable: "Address",
                        principalColumn: "addressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerNotification",
                columns: table => new
                {
                    cNotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    notiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNotification", x => x.cNotificationID);
                    table.ForeignKey(
                        name: "FK_CustomerNotification_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerNotification_Notifications_notiID",
                        column: x => x.notiID,
                        principalTable: "Notifications",
                        principalColumn: "notiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluate",
                columns: table => new
                {
                    evaluateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluate", x => x.evaluateID);
                    table.ForeignKey(
                        name: "FK_Evaluate_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluate_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalInvoice = table.Column<int>(type: "int", nullable: false),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    payID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payment_payID",
                        column: x => x.payID,
                        principalTable: "Payment",
                        principalColumn: "payID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    bookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateFounded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    endTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    storeID = table.Column<int>(type: "int", nullable: false),
                    employeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.bookingID);
                    table.CheckConstraint("CK_Booking_StartDate_CurrentDate", "startDate <= GETDATE()");
                    table.CheckConstraint("CK_Booking_StartDate_DateFounded", "startDate <= dateFounded");
                    table.ForeignKey(
                        name: "FK_Bookings_Customer_customerID",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Employees_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID");
                    table.ForeignKey(
                        name: "FK_Bookings_Stores_storeID",
                        column: x => x.storeID,
                        principalTable: "Stores",
                        principalColumn: "storeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesEmployee",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false),
                    serID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesEmployee", x => new { x.employeeID, x.serID });
                    table.ForeignKey(
                        name: "FK_ServicesEmployee_Employees_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesEmployee_Service_serID",
                        column: x => x.serID,
                        principalTable: "Service",
                        principalColumn: "serID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    proID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    proDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    producerID = table.Column<int>(type: "int", nullable: false),
                    warehouseID = table.Column<int>(type: "int", nullable: false),
                    cateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.proID);
                    table.ForeignKey(
                        name: "FK_Products_Category_cateID",
                        column: x => x.cateID,
                        principalTable: "Category",
                        principalColumn: "cateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Producers_producerID",
                        column: x => x.producerID,
                        principalTable: "Producers",
                        principalColumn: "producerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Warehouse_warehouseID",
                        column: x => x.warehouseID,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BookingsService",
                columns: table => new
                {
                    bookingID = table.Column<int>(type: "int", nullable: false),
                    serID = table.Column<int>(type: "int", nullable: false),
                    employeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingsService", x => new { x.bookingID, x.serID });
                    table.ForeignKey(
                        name: "FK_BookingsService_Bookings_bookingID",
                        column: x => x.bookingID,
                        principalTable: "Bookings",
                        principalColumn: "bookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingsService_Employees_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BookingsService_Service_serID",
                        column: x => x.serID,
                        principalTable: "Service",
                        principalColumn: "serID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingStateDescription",
                columns: table => new
                {
                    stateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bookingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStateDescription", x => x.stateID);
                    table.ForeignKey(
                        name: "FK_BookingStateDescription_Bookings_bookingID",
                        column: x => x.bookingID,
                        principalTable: "Bookings",
                        principalColumn: "bookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    proOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    orderID = table.Column<int>(type: "int", nullable: false),
                    proID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.proOrderID);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Orders_orderID",
                        column: x => x.orderID,
                        principalTable: "Orders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Products_proID",
                        column: x => x.proID,
                        principalTable: "Products",
                        principalColumn: "proID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_cityID",
                table: "Address",
                column: "cityID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_currentAddress_subDistrict_district",
                table: "Address",
                columns: new[] { "currentAddress", "subDistrict", "district" },
                unique: true,
                filter: "[currentAddress] IS NOT NULL AND [subDistrict] IS NOT NULL AND [district] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customerID",
                table: "Bookings",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_employeeID",
                table: "Bookings",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_storeID",
                table: "Bookings",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingsService_employeeID",
                table: "BookingsService",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingsService_serID",
                table: "BookingsService",
                column: "serID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingStateDescription_bookingID",
                table: "BookingStateDescription",
                column: "bookingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_cateName",
                table: "Category",
                column: "cateName",
                unique: true,
                filter: "[cateName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_cityName",
                table: "City",
                column: "cityName",
                unique: true,
                filter: "[cityName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_countryID",
                table: "City",
                column: "countryID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_countryName",
                table: "Country",
                column: "countryName",
                unique: true,
                filter: "[countryName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_email",
                table: "Customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_numberphone",
                table: "Customer",
                column: "numberphone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_userID",
                table: "Customer",
                column: "userID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_addressID",
                table: "CustomerAddress",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_customerID_addressID",
                table: "CustomerAddress",
                columns: new[] { "customerID", "addressID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotification_customerID",
                table: "CustomerNotification",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotification_notiID",
                table: "CustomerNotification",
                column: "notiID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_addressID",
                table: "Employees",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_email",
                table: "Employees",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_numberphone",
                table: "Employees",
                column: "numberphone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_storeID",
                table: "Employees",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_userID",
                table: "Employees",
                column: "userID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_customerID",
                table: "Evaluate",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_storeID",
                table: "Evaluate",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationStore_cityID",
                table: "LocationStore",
                column: "cityID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationStore_storeID",
                table: "LocationStore",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerID",
                table: "Orders",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_payID",
                table: "Orders",
                column: "payID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_payMethod",
                table: "Payment",
                column: "payMethod",
                unique: true,
                filter: "[payMethod] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_addressID",
                table: "Producers",
                column: "addressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_orderID",
                table: "ProductOrder",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_proID",
                table: "ProductOrder",
                column: "proID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_cateID",
                table: "Products",
                column: "cateID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_producerID",
                table: "Products",
                column: "producerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_warehouseID",
                table: "Products",
                column: "warehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_roleName",
                table: "Role",
                column: "roleName",
                unique: true,
                filter: "[roleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Service_serCateID",
                table: "Service",
                column: "serCateID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceManagement_serID",
                table: "ServiceManagement",
                column: "serID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceManagement_storeID",
                table: "ServiceManagement",
                column: "storeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesEmployee_serID",
                table: "ServicesEmployee",
                column: "serID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_workingHourID",
                table: "Stores",
                column: "workingHourID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleID",
                table: "Users",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userName",
                table: "Users",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_addressID",
                table: "Warehouse",
                column: "addressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_storeID",
                table: "Warehouse",
                column: "storeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingsService");

            migrationBuilder.DropTable(
                name: "BookingStateDescription");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerNotification");

            migrationBuilder.DropTable(
                name: "Evaluate");

            migrationBuilder.DropTable(
                name: "LocationStore");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "ServiceManagement");

            migrationBuilder.DropTable(
                name: "ServicesEmployee");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

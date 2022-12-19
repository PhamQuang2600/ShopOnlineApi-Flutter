using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sortOrder = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Originals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Originals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    imageProduct = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAuth = table.Column<bool>(type: "bit", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "OriginalProducts",
                columns: table => new
                {
                    OriginalId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginalProducts", x => new { x.ProductId, x.OriginalId });
                    table.ForeignKey(
                        name: "FK_OriginalProducts_Originals_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Originals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OriginalProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uid = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 20, nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    numberProduct = table.Column<int>(type: "int", nullable: true),
                    feeShipping = table.Column<decimal>(name: "feeShipping  ", type: "decimal(18,2)", nullable: true),
                    DateAddCart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAll = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    counterInCart = table.Column<int>(type: "int", nullable: true),
                    StatusPayment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_uid",
                        column: x => x.uid,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "name", "sortOrder", "status" },
                values: new object[,]
                {
                    { 1, "smartphone", 1, 1 },
                    { 2, "keybroad", 2, 1 },
                    { 3, "laptop", 3, 1 },
                    { 4, "headPhone", 4, 1 },
                    { 5, "tablet", 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Originals",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "samsung" },
                    { 2, "apple" },
                    { 3, "xiaomi" },
                    { 4, "vivo" },
                    { 5, "mobiistar" },
                    { 6, "oppo" },
                    { 7, "vinsmart" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "createdDate", "description", "imageProduct", "name", "price", "stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.", "1.jfif", "Iphone 13 pro max", 1000m, 0 },
                    { 2, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.", "1.jfif", "Iphone 13 pro max", 1000m, 0 },
                    { 3, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.", "1.jfif", "Iphone 13 pro max", 1000m, 0 },
                    { 4, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.", "1.jfif", "Iphone 13 pro max", 1000m, 0 },
                    { 5, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.", "1.jfif", "Iphone 13 pro max", 1000m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "ImageUser", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isAuth", "token" },
                values: new object[] { new Guid("c72c25a6-805c-4085-98e9-8a4dffd8a7ff"), 0, "Phuong Liet - Thanh Xuan - Ha Noi", "44f73659-55a3-41ae-b906-10cea17b05d1", new DateTime(2000, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangpham2kst@gmail.com", true, null, false, null, "PhamQuang", "quangpham2kst@gmail.com", "PhamQuang", "quangpham2k", "0395523926", false, "", false, "PhamQuang", false, null });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "counterInCart", "DateAddCart", "feeShipping  ", "numberProduct", "productId", "productImage", "productName", "productPrice", "StatusPayment", "total", "TotalAll", "uid" },
                values: new object[] { 1, 1, new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2m, 1, 1, "1.jfif", "Iphone 13 pro max", 1000m, 0, 1000m, null, new Guid("c72c25a6-805c-4085-98e9-8a4dffd8a7ff") });

            migrationBuilder.InsertData(
                table: "OriginalProducts",
                columns: new[] { "OriginalId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_productId",
                table: "Carts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_uid",
                table: "Carts",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "IX_OriginalProducts_OriginalId",
                table: "OriginalProducts",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "OriginalProducts");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Originals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Data;

namespace Shop.Data.EF
{
    public partial class ShopOnlineAppContext : DbContext
    {
        public ShopOnlineAppContext()
        {
            
        }

        public ShopOnlineAppContext(DbContextOptions<ShopOnlineAppContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Original> Originals { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<OriginalProduct> OriginalProducts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PHAMQUANG2K\\SQLFLUTTER;Initial Catalog=ShopOnlineApp;Persist Security Info=True;User ID=sa;Password=quangpham2k");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Carts");
                entity.HasKey(e => new {e.Id});

                entity.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
                entity.Property(e => e.Uid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uid");
                entity.Property(e=>e.ProductImage).HasColumnName("productImage");
                entity.Property(e => e.ProductName).HasColumnName("productName");
                entity.Property(e => e.ProductPrice).HasColumnName("productPrice");
                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.CounterInCart).HasColumnName("counterInCart");

                entity.Property(e => e.FeeShipping).HasColumnName("feeShipping  ");

                entity.Property(e => e.NumberProduct).HasColumnName("numberProduct");

                entity.Property(e => e.Total).HasColumnName("total");
                entity
            .HasOne(s => s.Products)
            .WithMany(g => g.Carts)
            .HasForeignKey(s => s.ProductId);
                entity
                .HasOne(s => s.Users)
                .WithMany(g => g.Carts)
                .HasForeignKey(s => s.Uid);

            });
            

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id).UseIdentityColumn().HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageProduct)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imageProduct");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Stock).HasColumnName("stock");

            });
            modelBuilder.Entity<Original>(entity =>
            {
                entity.ToTable("Originals");
                entity.HasKey(e => new {e.Id });
                entity.Property(e => e.Id).UseIdentityColumn().HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
               
        });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategories");
                entity.HasKey(e => new { e.CategoryId, e.ProductId });

                entity.HasOne(x => x.Products).WithMany(x => x.ProductCategories).HasForeignKey(x => x.ProductId);
                entity.HasOne(x => x.Categories).WithMany(x => x.ProductCategories).HasForeignKey(x => x.CategoryId);

            });

            modelBuilder.Entity<OriginalProduct>(entity =>
            {
                entity.ToTable("OriginalProducts");
                entity.HasKey(e => new { e.ProductId, e.OriginalId });

                entity.HasOne(x => x.Products).WithMany(x => x.OriginalProducts).HasForeignKey(x => x.ProductId);
                entity.HasOne(x => x.Originals).WithMany(x => x.OriginalProducts).HasForeignKey(x => x.OriginalId);

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => new { e.Id});

                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SortOrder).HasColumnName("sortOrder");

                entity.Property(e => e.Status).HasColumnName("status");
            });



            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);


            modelBuilder.Entity<Category>().HasData(
        new Category {Id =1,  Name = "smartphone", SortOrder = 1, Status= Enum.EnumType.Status.success },
        new Category { Id = 2, Name = "keybroad", SortOrder = 2, Status = Enum.EnumType.Status.success },
        new Category {Id = 3, Name = "laptop", SortOrder = 3, Status = Enum.EnumType.Status.success },
        new Category {Id = 4, Name = "headPhone", SortOrder = 4, Status = Enum.EnumType.Status.success },
        new Category {Id = 5, Name = "tablet", SortOrder = 5, Status = Enum.EnumType.Status.success });
            
            modelBuilder.Entity<Original>().HasData(
        new Original { Id = 1, Name = "samsung" },
        new Original {Id = 2, Name = "apple" },
        new Original {Id = 3,Name = "xiaomi" },
        new Original {Id = 4, Name = "vivo" },
        new Original {Id = 5, Name = "mobiistar" },
        new Original {Id = 6, Name = "oppo" },
        new Original {Id = 7, Name = "vinsmart" });
         
            modelBuilder.Entity<Product>().HasData(
        new Product {
            Id = 1,
            Name = "Iphone 13 pro max", Description = "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.",
        Price = 1000, CreatedDate = new DateTime(2022,11,19), ImageProduct = "1.jfif"
        },
       new Product
       {
           Id = 2,
           Name = "Iphone 13 pro max",
           Description = "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.",
           Price = 1000,
           CreatedDate = new DateTime(2022,11,19),
           ImageProduct = "1.jfif"
       },
       new Product
       {
           Id = 3,
           Name = "Iphone 13 pro max",
           Description = "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.",
           Price = 1000,
           CreatedDate = new DateTime(2022,11,19),
           ImageProduct = "1.jfif"
       },
       new Product
       {
           Id = 4,
           Name = "Iphone 13 pro max",
           Description = "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.",
           Price = 1000,
           CreatedDate = new DateTime(2022,11,19),
           ImageProduct = "1.jfif"
       },
       new Product
       {
           Id = 5,
           Name = "Iphone 13 pro max",
           Description = "iPhone 13 Pro Max 128 GB - the most anticipated super product in the second half of 2021 from Apple. The machine has a design that is not very groundbreaking when compared to its predecessor, inside this is still a product with a super beautiful screen, the refresh rate is upgraded to 120 Hz smoothly, the camera sensor has a larger size, With powerful performance and power from Apple A15 Bionic, ready to conquer any challenge with you. Top class design  The new iPhone inherits the distinctive design from the iPhone 12 Pro Max when it has a square frame, a glass back, and an overflowing notch at the front.  Super smooth entertainment screen with 120 Hz . refresh rate iPhone 13 Pro Max is equipped with a 6.7-inch screen with a resolution of 1284 x 2778 Pixels, using an OLED panel with Super Retina XDR technology for outstanding energy savings but still ensuring a sharp display. realistically alive.  This year's iPhone Pro Max has been upgraded to a 120 Hz refresh rate, all transitions when swiping fingers on the screen become smoother and the visual effects when we play games or watch videos are also extremely good. eye-catching. With intelligent ProMotion technology that can change the scan frequency from 10 to 120 times per second depending on the application and operation you are using, to optimize battery life and your experience. If you use iPhone 13 Pro Max to play games, the 120 Hz refresh rate combined with the excellent graphics performance of the GPU will make the device extremely perfect when experiencing. In addition, the device also supports True Tone technology, the wide color range of the P3 cinema standard will make your experience on the device more impressive than ever. The camera cluster has been completely upgraded. iPhone 13 Pro Max will still have a set of 3-lens cameras arranged alternately in a square cluster, located in the upper left corner of the back, including a telephoto camera, super wide-angle camera and wide-angle main camera with a super large f/1.5 aperture. . The super wide-angle camera has also been upgraded with an f/1.8 aperture, a faster sensor, delivering natural and realistic super wide-angle photos and also enhanced the ability to focus at a distance of only 2 cm, bringing to interesting macro photography and movie shooting features. The telephoto camera on iPhone 13 Pro Max can zoom 3x optically, magnify images and videos 3 times but still maintain the same quality, support portrait photography to remove fonts, built-in a lot of natural color filters to help Get the picture you want. In addition, the LiDAR sensor will still be present on the iPhone 13 Pro Max to provide the best augmented reality (AR) experience for all users as well as support for the camera cluster to focus in low light environments.  Apple also enhanced the photography capabilities on the iPhone 13 Pro Max with Cinematic movie recording mode that makes it easy for users to focus on subjects both during and after recording, while blurring the background and other characters. other objects in the frame. It's also the first smartphone to offer an \"end-to-end\" professional workflow that allows you to record and edit video in ProRes or Dolby Vision compressed formats with a variety of in-depth settings that make it easy to significantly shorten the post-production process to create amazing quality footage right on your own phone. Promising performance with Apple A15 Bionic iPhone 13 Pro Max will be equipped with the company's latest Apple A15 Bionic processor, manufactured on the 5 nm process, ensuring impressive performance while still saving power with the ability to support Super high speed 5G network support. According to Apple, the A15 Bionic is the fastest chipset in the smartphone world (September 2021, 50% faster than other chips on the market, able to perform 15.8 trillion operations per second, helping CPU performance is faster than ever. The machine owns 128 GB of internal memory, just enough for the needs of a basic user, without the need to record too much video, in addition this year there is also an internal memory version up to 1TB, you can can be considered if there is a high storage need. In addition, the device is also integrated with Wi-Fi 6 technology, a new wireless connection standard with equipped with multiple 5G bands, compatible with many carriers in different countries, iPhone 13 Pro Max always gives fast speed. maximum network speed, for a smooth 4K movie experience, download files in the blink of an eye, play online games without delay anywhere. Leap in battery life iPhone Pro Max marks a new turning point in battery life. With a large battery capacity combined with a new screen and power-saving Apple A15 Bionic processor, the iPhone 13 Pro Max becomes the iPhone with the best battery life ever, 2.5 hours longer than with its predecessor. Unfortunately, the battery capacity of the new iPhone models has improved, but their fast charging speed still only stops at 20 W over a wired connection and charges via MagSafe at up to 15 W or can be via a charger. Qi-based wire with 7.5 W output. Apple has constantly improved to give users the best product, iPhone 13 Pro Max 128GB retains the highlights of its predecessor, featuring improvements in configuration, battery life as well as camera and many more. What awaits you to discover.",
           Price = 1000,
           CreatedDate = new DateTime(2022,11,19),
           ImageProduct = "1.jfif"
       });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("C72C25A6-805C-4085-98E9-8A4DFFD8A7FF"),
                UserName = "PhamQuang",
                NormalizedUserName = "PhamQuang",
                Email = "quangpham2kst@gmail.com",
                NormalizedEmail = "quangpham2kst@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "quangpham2k",
                SecurityStamp = string.Empty,
                Name = "PhamQuang",
                Dob = new DateTime(2000, 06, 02),
                Address = "Phuong Liet - Thanh Xuan - Ha Noi",
                PhoneNumber = "0395523926"    
            });


            modelBuilder.Entity<ProductCategory>().HasData(
        new ProductCategory { CategoryId = 1, ProductId = 1 },
        new ProductCategory { CategoryId= 2, ProductId = 2 },
        new ProductCategory { CategoryId = 3, ProductId = 3 },
        new ProductCategory {CategoryId = 4, ProductId = 4 },
        new ProductCategory {CategoryId = 5, ProductId = 5 });


            modelBuilder.Entity<OriginalProduct>().HasData(
        new OriginalProduct { OriginalId = 1, ProductId = 1 },
        new OriginalProduct { OriginalId = 2, ProductId = 2 },
        new OriginalProduct { OriginalId = 3, ProductId = 3 },
        new OriginalProduct { OriginalId = 4, ProductId = 4 },
        new OriginalProduct { OriginalId = 5, ProductId = 5 });


            modelBuilder.Entity<Cart>().HasData(
        new Cart {Id = 1, ProductId = 1, Uid = new Guid("C72C25A6-805C-4085-98E9-8A4DFFD8A7FF"),ProductName= "Iphone 13 pro max", ProductImage= "1.jfif", ProductPrice=1000, DateAddCart = new DateTime(2022,11,19),
            NumberProduct = 1, CounterInCart = 1, FeeShipping = 2, Total = 1000, StatusPayment = 0 });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

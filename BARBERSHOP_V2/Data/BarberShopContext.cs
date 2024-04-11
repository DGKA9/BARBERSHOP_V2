using BARBERSHOP_V2.Entity;
using Microsoft.EntityFrameworkCore;


namespace BARBERSHOP_V2.Data
{
    public class BarberShopContext : DbContext
    {
        public BarberShopContext(DbContextOptions<BarberShopContext> opt) : base(opt)
        {

        }

        #region DbSet

        public DbSet<Store>? Stores { get; set; }
        public DbSet<Address>? Address { get; set; }
        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<BookingService>? BookingsService { get; set; }
        public DbSet<BookingStateDescription>? BookingStateDescription { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Customer>? Customer { get; set; }
        public DbSet<City>? City { get; set; }
        public DbSet<Country>? Country { get; set; }
        public DbSet<CustomerNotification>? CustomerNotification { get; set; }
        public DbSet<CustomerAddress>? CustomerAddress { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Evaluate>? Evaluate { get; set; }
        public DbSet<LocationStore>? LocationStore { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Payment>? Payment { get; set; }
        public DbSet<Producer>? Producers { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductOrder>? ProductOrder { get; set; }
        public DbSet<Role>? Role { get; set; }
        public DbSet<Services>? Service { get; set; }
        public DbSet<ServiceCategory>? ServiceCategories { get; set; }
        public DbSet<ServiceManagement>? ServiceManagement { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Warehouse>? Warehouse { get; set; }
        public DbSet<WorkingHour>? WorkingHours { get; set; }
        public DbSet<ServiceEmployee>? ServicesEmployee { get; set; }
        //public DbSet<ServiceStore>? ServicesStore { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Address
            
            modelBuilder.Entity<Address>()
                .HasIndex(a => new { a.currentAddress, a.subDistrict, a.district })
                .IsUnique();

            #endregion

            #region Booking

            modelBuilder.Entity<Booking>()
                .Property(b => b.startDate)
                .IsRequired();
            modelBuilder.Entity<Booking>()
                .HasCheckConstraint("CK_Booking_StartDate_DateFounded", "startDate >= dateFounded");

            modelBuilder.Entity<Booking>()
                .Property(b => b.startTime)
                .IsRequired();
            //modelBuilder.Entity<Booking>()
            //    .HasCheckConstraint("CK_Booking_StartTime_EndTime", "startTime <= endTime");
            

            #endregion

            #region BookingSer

            modelBuilder.Entity<BookingService>()
                .HasKey(se => new { se.bookingID, se.serID });

            modelBuilder.Entity<BookingService>()
                .HasOne(se => se.Booking)
                .WithMany(e => e.BookingServices)
                .HasForeignKey(u => u.bookingID);

            modelBuilder.Entity<BookingService>()
                .HasOne(se => se.Service)
                .WithMany(s => s.BookingServices)
                .HasForeignKey(u => u.serID);

            #endregion



            // BookingStateDes

            #region Category

            modelBuilder.Entity<Category>()
                .HasIndex(u => u.cateName)
                .IsUnique();

            #endregion

            #region City

            modelBuilder.Entity<City>()
                .HasIndex(u => u.cityName)
                .IsUnique();

            #endregion

            #region Country

            modelBuilder.Entity<Country>()
                .HasIndex(u => u.countryName)
                .IsUnique();

            #endregion 

            #region CustomerAddress

            modelBuilder.Entity<CustomerAddress>()
                .HasIndex(c => new { c.customerID, c.addressID })
                .IsUnique();

            #endregion 

            #region Customer

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.numberphone)
                .IsUnique();

            #endregion

            // CustomerNotification

            #region Employee

            modelBuilder.Entity<Employee>()
               .HasIndex(c => c.email)
               .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(c => c.numberphone)
                .IsUnique();

            #endregion

            // Evaluate
            // LocationStore
            // Notification
            // Order

            #region Payment

            modelBuilder.Entity<Payment>()
                .HasIndex(c => c.payMethod)
                .IsUnique();

            #endregion

            // Producer
            // Product
            // ProductOrder

            #region Role

            modelBuilder.Entity<Role>()
                .HasIndex(c => c.roleName)
                .IsUnique();

            #endregion 

            // ServicesCategory

            #region ServicesEmployee

            modelBuilder.Entity<ServiceEmployee>()
                .HasKey(se => new { se.employeeID, se.serID });

            modelBuilder.Entity<ServiceEmployee>()
                .HasOne(se => se.Employee)
                .WithMany(e => e.ServiceEmployee)
                .HasForeignKey(u => u.employeeID);

            modelBuilder.Entity<ServiceEmployee>()
                .HasOne(se => se.Services)
                .WithMany(s => s.ServiceEmployee)
                .HasForeignKey(u => u.serID);

            #endregion 

            #region ServiceStore

            //modelBuilder.Entity<ServiceStore>()
            //    .HasKey(ss => new { ss.storeID, ss.serID });

            //modelBuilder.Entity<ServiceStore>()
            //    .HasOne(ss => ss.Store)
            //    .WithMany(e => e.ServiceStore)
            //    .HasForeignKey(u => u.storeID);

            //modelBuilder.Entity<ServiceStore>()
            //    .HasOne(se => se.Services)
            //    .WithMany(s => s.ServiceStore)
            //    .HasForeignKey(u => u.serID);

            #endregion

            // ServicesManagement
            // Services
            // Store

            #region User

            modelBuilder.Entity<User>()
            .HasIndex(u => u.userName)
            .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.userName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .HasCheckConstraint("CK_userName_no_whitespace", "CHARINDEX(' ', userName) = 0");

            #endregion 

            // Warehouse
            // WorkingHour
        }
    }
}

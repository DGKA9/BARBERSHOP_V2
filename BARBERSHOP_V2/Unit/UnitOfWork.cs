using AutoMapper;
using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Repository.CRUDRepo;

namespace BARBERSHOP_V2.Unit
{
    public class UnitOfWork : IDisposable
    {
        private readonly BarberShopContext _context;
        public IMapper Mapper { get; }

        public IRepository<Address> AddressRepository { get; set; } = null!;
        public IRepository<Booking> BookingRepository { get; set; } = null!;
        public IRepository<BookingService> BookingServiceRepository { get; set; } = null!;
        public IRepository<City> CityRepository { get; set; } = null!;
        public IRepository<Country> CountryRepository { get; set; } = null!;
        public IRepository<BookingStateDescription> BookingStateDescriptionRepository { get; set; } = null!;
        public IRepository<Category> CategoryRepository { get; set; } = null!;
        public IRepository<Customer> CustomerRepository { get; set; } = null!;
        public IRepository<CustomerAddress> CustomerAddressRepository { get; set; } = null!; 
        public IRepository<Employee> EmployeeRepository { get; set; } = null!;
        public IRepository<Evaluate> EvaluateRepository { get; set; } = null!;
        public IRepository<CustomerNotification> CustomerNotificationRepository { get; set; } = null!;
        public IRepository<LocationStore> LocationStoreRepository { get; set; } = null!;
        public IRepository<Notification> NotificationRepository { get; set; } = null!;
        public IRepository<Order> OrderRepository { get; set; } = null!;
        public IRepository<Payment> PaymentRepository { get; set; } = null!;
        public IRepository<Producer> ProducerRepository { get; set; } = null!;
        public IRepository<Product> ProductRepository { get; set; } = null!;
        public IRepository<ProductOrder> ProductOrderRepository { get; set; } = null!;
        public IRepository<Role> RoleRepository { get; set; } = null!;
        public IRepository<ServiceCategory> ServiceCategoryRepository { get; set; } = null!;
        public IRepository<ServiceManagement> ServiceManagementRepository { get; set; } = null!;
        public IRepository<Services> ServiceRepository { get; set; } = null!;
        public IRepository<ServiceEmployee> ServiceEmployeeRepository { get; set; } = null!; 
        public IRepository<Store> StoreRepository { get; set; } = null!;
        public IRepository<User> UserRepository { get; set; } = null!;
        public IRepository<Warehouse> WarehouseRepository { get; set; } = null!;
        public IRepository<WorkingHour> WorkingHourRepository { get; set; } = null!;


        public UnitOfWork(BarberShopContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            InitializeRepositories(mapper);
        }

        private void InitializeRepositories(IMapper mapper)
        {
            AddressRepository = new GenericRepository<Address>(_context, mapper);
            BookingRepository = new GenericRepository<Booking>(_context, mapper);
            BookingServiceRepository = new GenericRepository<BookingService>(_context, mapper);
            CityRepository = new GenericRepository<City>(_context, mapper);
            CountryRepository = new GenericRepository<Country>(_context, mapper);
            BookingStateDescriptionRepository = new GenericRepository<BookingStateDescription>(_context, mapper);
            CategoryRepository = new GenericRepository<Category>(_context, mapper);
            CustomerRepository = new GenericRepository<Customer>(_context, mapper);
            CustomerAddressRepository = new GenericRepository<CustomerAddress>(_context, mapper);
            OrderRepository = new GenericRepository<Order>(_context, mapper);
            NotificationRepository = new GenericRepository<Notification>(_context, mapper);
            LocationStoreRepository = new GenericRepository<LocationStore>(_context, mapper);
            CustomerNotificationRepository = new GenericRepository<CustomerNotification>(_context, mapper);
            EvaluateRepository = new GenericRepository<Evaluate>(_context, mapper);
            EmployeeRepository = new GenericRepository<Employee>(_context, mapper);
            PaymentRepository = new GenericRepository<Payment>(_context, mapper);
            ProducerRepository = new GenericRepository<Producer>(_context, mapper);
            ProductRepository = new GenericRepository<Product>(_context, mapper);
            ProductOrderRepository = new GenericRepository<ProductOrder>(_context, mapper);
            RoleRepository = new GenericRepository<Role>(_context, mapper);
            ServiceCategoryRepository = new GenericRepository<ServiceCategory>(_context, mapper);
            ServiceManagementRepository = new GenericRepository<ServiceManagement>(_context, mapper);
            ServiceRepository = new GenericRepository<Services>(_context, mapper);
            ServiceEmployeeRepository = new GenericRepository<ServiceEmployee>(_context, mapper);
            StoreRepository = new GenericRepository<Store>(_context, mapper);
            UserRepository = new GenericRepository<User>(_context, mapper);
            WarehouseRepository = new GenericRepository<Warehouse>(_context, mapper);
            WorkingHourRepository = new GenericRepository<WorkingHour>(_context, mapper);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}

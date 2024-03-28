using AutoMapper;
using BARBERSHOP_V2.DTO;
using BARBERSHOP_V2.Entity;

namespace BARBERSHOP_V2.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<BookingService, BookingServiceDto>().ReverseMap();
            CreateMap<BookingStateDescription, BookingStateDescriptionDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap();
            CreateMap<CustomerNotification, CustomerNotificationDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Evaluate, EvaluateDto>().ReverseMap();
            CreateMap<LocationStore, LocationStoreDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Producer, ProducerDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductOrder, ProductOrderDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryDto>().ReverseMap();
            //CreateMap<ServiceStore, ServiceStoreDto>().ReverseMap();
            CreateMap<ServiceEmployee, ServiceEmployeeDto>().ReverseMap();
            CreateMap<ServiceManagement, ServiceManagementDto>().ReverseMap();
            CreateMap<Services, ServicesDto>().ReverseMap();
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<WorkingHour, WorkingHourDto>().ReverseMap();
        }
    }
}

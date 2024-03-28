using BARBERSHOP_V2.Data;
using BARBERSHOP_V2.Entity;
using BARBERSHOP_V2.Unit;
using Microsoft.EntityFrameworkCore;

namespace BARBERSHOP_V2.Service
{
    public  class EndTimeForBookingService
    {
        private readonly BarberShopContext _dbContext;

        public EndTimeForBookingService(BarberShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void UpdateBookingEndTime(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.bookingID == bookingId);

            if (booking != null)
            {
                var totalSerTime = _dbContext.BookingsService
                    .Where(bs => bs.bookingID == bookingId)
                    .Select(bs => bs.Service.serTime)
                    .ToList();

                var totalSerTimeSpan = totalSerTime.Aggregate(TimeSpan.Zero, (acc, serTime) => acc.Add(serTime));

                booking.endTime = booking.startTime.Add(totalSerTimeSpan);

                _dbContext.SaveChanges();
            }
        }
    }
}


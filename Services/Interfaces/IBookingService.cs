using EventEase.Models;

namespace EventEase.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);
        IEnumerable<Booking> Search(string keyword);
        bool Create(Booking booking);
        void Delete(int id);
    }
}
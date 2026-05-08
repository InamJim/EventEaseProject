using EventEase.Models;

namespace EventEase.Services.Interfaces
{
    public interface IVenueService
    {
        IEnumerable<Venue> GetAll();
        Venue GetById(int id);
        IEnumerable<Venue> Search(string keyword);
        void Create(Venue venue);
        void Update(Venue venue);
        void Delete(int id);
    }
}
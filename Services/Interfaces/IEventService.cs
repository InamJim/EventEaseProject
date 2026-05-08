using EventEase.Models;

namespace EventEase.Services.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        void Create(Event ev);
        void Update(Event ev);
        void Delete(int id);
    }
}
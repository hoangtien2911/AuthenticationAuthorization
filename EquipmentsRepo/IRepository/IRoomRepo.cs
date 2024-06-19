using EquipmentsBO.Models;

namespace EquipmentsRepo.IRepository;

public interface IRoomRepo
{
    IQueryable<Room> Get();
    Room? GetById(int id);
    List<Room> GetAll();
    bool Create(Room entity);
    Room? Update(Room entity);
    bool Remove(Room entity);
}

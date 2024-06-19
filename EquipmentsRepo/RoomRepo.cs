using EquipmentsBO.Models;
using EquipmentsDAO;
using EquipmentsRepo.IRepository;

namespace EquipmentsRepo;

public class RoomRepo : IRoomRepo
{
    private readonly RoomDAO roomDAO = RoomDAO.Instance;
    public RoomRepo()
    {
    }

    public bool Create(Room entity)
    {
        return roomDAO.Create(entity);
    }

    public IQueryable<Room> Get()
    {
        return roomDAO.Get();
    }

    public List<Room> GetAll()
    {
        return roomDAO.GetAll();
    }

    public Room? GetById(int id)
    {
        return roomDAO.GetById(id);
    }

    public bool Remove(Room entity)
    {
        return roomDAO.Remove(entity);
    }

    public Room? Update(Room entity)
    {
        return roomDAO.Update(entity);
    }
}

using EquipmentsBO.Models;
using EquipmentsDAO;
using EquipmentsRepo.IRepository;

namespace EquipmentsRepo;

public class EquipmentRepo : IEquipmentRepo
{
    private readonly EquipmentDAO equipmentDAO = EquipmentDAO.Instance;
    public EquipmentRepo()
    {
    }

    public bool Create(Equipment entity)
    {
        return equipmentDAO.Create(entity);
    }

    public IQueryable<Equipment> Get()
    {
        return equipmentDAO.Get();
    }

    public List<Equipment> GetAll()
    {
        return equipmentDAO.GetAll();
    }

    public Equipment? GetById(int id)
    {
        return equipmentDAO.GetById(id);
    }

    public bool Remove(Equipment entity)
    {
        return equipmentDAO.Remove(entity);
    }

    public Equipment? Update(Equipment entity)
    {
        return equipmentDAO.Update(entity);
    }
}

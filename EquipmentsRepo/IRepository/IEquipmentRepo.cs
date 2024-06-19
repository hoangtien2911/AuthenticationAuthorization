using EquipmentsBO.Models;

namespace EquipmentsRepo.IRepository;

public interface IEquipmentRepo
{
    IQueryable<Equipment> Get();
    Equipment? GetById(int id);
    List<Equipment> GetAll();
    bool Create(Equipment entity);
    Equipment? Update(Equipment entity);
    bool Remove(Equipment entity);
}

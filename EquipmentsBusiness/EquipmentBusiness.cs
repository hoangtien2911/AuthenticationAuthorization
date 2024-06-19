using EquipmentsBO.Models;
using EquipmentsBusiness.Base;
using EquipmentsBusiness.Request;
using EquipmentsBusiness.Response;
using EquipmentsRepo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EquipmentsBusiness;

public class EquipmentBusiness
{
    private readonly IEquipmentRepo _equipmentsDAO;

    public EquipmentBusiness(IEquipmentRepo equipmentsDAO)
    {
        _equipmentsDAO = equipmentsDAO;
    }

    public IBusinessResult GetAll()
    {
        var equipments = _equipmentsDAO.Get().Include(e => e.Room).ToList();
        List<EquipmentResponse> roomResponses = new List<EquipmentResponse>();
        if (equipments.Count > 0)
        {
            equipments.ForEach(equipment =>
            {
                var roomResponse = new EquipmentResponse()
                {
                    Id = equipment.EqId,
                    Code = equipment.EqCode,
                    Name = equipment.EqName,
                    Description = equipment.Description,
                    Model = equipment.Model,
                    SupplierName = equipment.SupplierName,
                    Quantity = equipment.Quantity,
                    RoomName = equipment.Room.RoomName,
                    Location = equipment.Room.Location
                };
                roomResponses.Add(roomResponse);
            });
        }

        return new BusinessResult(true, roomResponses);
    }

    public IBusinessResult Create(CreateEquipmentRequest request)
    {
        var equipment = new Equipment()
        {
            EqCode = request.Code,
            EqName = request.Name,
            Description = request.Description,
            Model = request.Model,
            SupplierName = request.SupplierName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Quantity = request.Quantity,
            Status = 1,
            RoomId = request.RoomId
        };
        _equipmentsDAO.Create(equipment);
        return new BusinessResult(true, null, "Create new equipment successfully!");
    }

    public IBusinessResult Update(UpdateEquipmentRequest request)
    {
        var equipment = _equipmentsDAO.GetById(request.Id);
        if (equipment == null)
        {
            return new BusinessResult(false, "Not found this equipment id");
        }
        else
        {
            equipment.EqCode = request.Code;
            equipment.EqName = request.Name;
            equipment.Description = request.Description;
            equipment.Model = request.Model;
            equipment.SupplierName = request.SupplierName;
            equipment.CreatedAt = DateTime.Now;
            equipment.UpdatedAt = DateTime.Now;
            equipment.Quantity = request.Quantity;
            equipment.Status = 1;
            equipment.RoomId = request.RoomId;
        }
        _equipmentsDAO.Update(equipment);
        return new BusinessResult(true, null, "Update equipment successfully!");
    }

    public IBusinessResult Delete(DeleteEquipmentRequest request)
    {
        var equipment = _equipmentsDAO.GetById(request.Id);
        if (equipment == null)
        {
            return new BusinessResult(false, "Not found this equipment id");
        }
        _equipmentsDAO.Remove(equipment);
        return new BusinessResult(true, null, "Delete equipment successfully!");
    }
}

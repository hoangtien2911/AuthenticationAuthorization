using EquipmentsBusiness.Base;
using EquipmentsBusiness.Response;
using EquipmentsRepo;
using EquipmentsRepo.IRepository;

namespace EquipmentsBusiness;

public class RoomBusiness
{
    private readonly IRoomRepo _roomRepo;

    public RoomBusiness(IRoomRepo roomRepo)
    {
        _roomRepo = roomRepo;
    }

    public IBusinessResult GetAll()
    {
        var rooms = _roomRepo.GetAll();
        List<RoomResponse> roomResponses = new List<RoomResponse>();
        if (rooms.Count > 0)
        {
            rooms.ForEach(room =>
            {
                var roomResponse = new RoomResponse
                {
                    Id = room.RoomId,
                    Location = room.Location,
                    Name = room.RoomName,
                    Status = room.Status
                };
                roomResponses.Add(roomResponse);
            });
        }

        return new BusinessResult(true, roomResponses);
    }
}

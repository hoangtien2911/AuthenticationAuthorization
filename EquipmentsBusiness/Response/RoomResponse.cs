namespace EquipmentsBusiness.Response;

public class RoomResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Status { get; set; } = null!;
}
namespace AuthenticationAuthorizationFE.Request;

public class CreateEquipmentRequest
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
    public int Quantity { get; set; }
    public int RoomId { get; set; }
}
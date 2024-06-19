namespace AuthenticationAuthorizationFE.Response;

public class EquipmentResponse
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
    public int Quantity { get; set; }
    public string RoomName { get; set; } = null!;
    public string Location { get; set; } = null!;
}
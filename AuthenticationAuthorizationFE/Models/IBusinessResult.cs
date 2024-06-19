using System.Text.Json.Serialization;

namespace EquipmentsBusiness.Base;

public interface IBusinessResult
{
    bool IsSuccess { get; set; }
    string? Message { get; set; }
    object? Data { get; set; }
}
public class BusinessResult : IBusinessResult
{
    public bool IsSuccess { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }

    public BusinessResult()
    {
    }

    public BusinessResult(bool isSuccess, string? message, object? data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public BusinessResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public BusinessResult(bool isSuccess, object data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }
}
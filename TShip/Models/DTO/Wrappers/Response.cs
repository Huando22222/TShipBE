namespace TShip.Models.DTO.Wrappers
{
    //Trường hợp này vẫn trả HTTP 200 (vì request hợp lệ),
    //nhưng success = false để báo lỗi nghiệp vụ (business logic).
    public class Response<T>
    {
        public MetaData? Meta { get; set; } = default!;
        public bool Success { get; set; }//business logic
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}

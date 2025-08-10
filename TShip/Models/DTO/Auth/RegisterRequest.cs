//namespace TShip.Models.DTO.Auth
//{
//    public class RegisterRequest
//    {
//        public required string Username { get; set; }
//        public required string Password { get; set; }
//    }

//}


using System.ComponentModel.DataAnnotations;

namespace TShip.Models.DTO.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(0|\+84)(\d{9})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(9, ErrorMessage = "Mật khẩu phải dài hơn 8 ký tự")]
        public string Password { get; set; } = default!;
    }
}

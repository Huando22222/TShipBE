using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
namespace TShip.Models.DTO.Wrappers
{
    public class Request<T>
    {
        [Required]
        public required MetaData Meta { get; set; }
        [Required]
        [ValidateObjectMembers]
        public required T Data { get; set; }
    }
}

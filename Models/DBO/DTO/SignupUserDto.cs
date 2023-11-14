using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.DTO
{
    public class SignupUserDto : UserDto
    {
        [Required(ErrorMessage = "The 'Name' parameter is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid name format")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The 'LastName' parameter is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid name format")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The 'Type' parameter is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid name format")]
        public string Type { get; set; }
        [Required(ErrorMessage = "The 'Type' parameter is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid name format")]
        public string Role { get; set; }
    }
}

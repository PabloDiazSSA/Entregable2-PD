
using Entregable2_PD.Models.DBO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(20)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])([A-Za-z\d$@$!%*?&]|[^ ]){8,20}$", ErrorMessage = "Password must meet requirements (Al menos ocho cáracteres con: Mayuscula(s), minuscula(s), numero(s), al menos un cáracter especial)")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
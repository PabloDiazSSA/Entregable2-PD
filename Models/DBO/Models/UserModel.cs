using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserModel : Adicionales
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public byte[]? Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string? Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string? Salt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string? Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string? Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RegDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }
    }
}

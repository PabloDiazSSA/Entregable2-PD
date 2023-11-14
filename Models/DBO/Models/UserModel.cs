using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.Models
{
    public class UserModel : Adicionales
    {
        public int Id { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public DateTime RegDate { get; set; }
        public bool Status { get; set; }
    }
}

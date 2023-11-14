using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Cvv { get; set; }
    }
}

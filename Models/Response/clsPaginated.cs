using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Model.Response
{
    public class clsPaginated<T>
    {
        public int? CounPage { get; set; }
        public int Size { get; set; }
        public int CountRegister { get; set; }
        public T Entity { get; set; }
    }
}
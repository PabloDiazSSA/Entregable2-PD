using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Model.Response
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClsPaginated<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int? CounPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CountRegister { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T? Entity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.Response
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClsResponse<T> : ClsResponseAditional
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T? Data { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Entregable2_PD.Tools.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class Param
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object? Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Output { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Param(string name, object value)
        {
            Name = name;
            Value = value;
            Output = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Param(string name)
        {
            Name = name;
            Output = true;
        }

    }
}
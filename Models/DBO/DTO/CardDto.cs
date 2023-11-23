using Entregable2_PD.Tools.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class CardDto : CardNum
    {
        /// <summary>
        /// 
        /// </summary>
        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression(@"^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$", ErrorMessage = "Invalid Name")]
        public string? Fullname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(2)]
        [RegularExpression(@"^(0[1-9]|1[012])$", ErrorMessage = "Invalid month format")]
        public string? Month { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid year format")]
        public string? Year { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(3)]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid cvv format")]
        public string? Cvv { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string? Comment { get; set; }
    }
}

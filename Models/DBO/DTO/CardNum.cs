using System.ComponentModel.DataAnnotations;

namespace Entregable2_PD.Models.DBO.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class CardNum
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'CardNumber' parameter is required.")]
        [MinLength(13)]
        [MaxLength(19)]
        [RegularExpression(@"^(4[0-9]{12}(?:[0-9]{3})?$)|((?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(?:2131|1800|35\d{3})\d{11}$", ErrorMessage = "Invalid card number format")]
        [DataType(DataType.CreditCard)]
       public string? CardNumber { get; set; }
    }
}
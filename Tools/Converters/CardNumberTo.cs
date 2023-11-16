using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public static class CardNumberTo
    {
        /// <summary>
        /// Enmascarar con X el numero de tarjetas exeptuando los ultimos 4 digitos
        /// </summary>
        /// <param name="cardNumber">string value</param>
        /// <returns>string masked</returns>
        public static string MaskCreditCard(string cardNumber)
        {
            return string.Concat(new string('X', cardNumber.Length - 4), cardNumber.AsSpan(cardNumber.Length - 4, 4));
        }
    }
}

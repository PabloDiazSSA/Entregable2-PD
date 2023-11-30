using System.Text;

namespace Entregable2_PD.Tools.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class ArrayBytesTo
    {
        /// <summary>
        /// 
        /// </summary>
        protected ArrayBytesTo()
        {
            
        }
        /// <summary>
        /// Funcion par apasar de array de bytes a string hexadecimal
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertByteArrToStringHex(byte[] bytes)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2")); // Convierte el byte a su representación en hexadecimal
            }

            return stringBuilder.ToString();
        }
    }
}

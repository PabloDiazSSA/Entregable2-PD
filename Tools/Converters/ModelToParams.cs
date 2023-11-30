using System.Reflection;

namespace Entregable2_PD.Tools.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelToParams
    {
        /// <summary>
        /// 
        /// </summary>
        protected ModelToParams()
        {
                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Param> GetParams<T>(T model) where T : class
        {
            PropertyInfo[] lst = typeof(T).GetProperties();
            List<Param> parametros = new();
            foreach (PropertyInfo oProperty in lst)
            {
                var value = oProperty.GetValue(model);
                value ??= DBNull.Value;
                if (value.ToString() == string.Empty)
                {
                    value = value.ToString();
                }
                if (value is not null)
                {
                    parametros.Add(new Param($"@{oProperty.Name}", value));
                }
                //string Tipo = oProperty.GetType().ToString(); //Traer el tipo de dato de a propiedad ej; decimal int, string

            }
            return parametros;
        }
    }
}

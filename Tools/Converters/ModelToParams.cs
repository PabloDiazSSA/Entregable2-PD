using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelToParams
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<Param> GetParams<T>(T model) where T : class
        {
            try
            {
                PropertyInfo[] lst = typeof(T).GetProperties();
                List<Param> parametros = new List<Param>();
                foreach (PropertyInfo oProperty in lst)
                {
                    var value = oProperty.GetValue(model);
                    if (value is null)
                    {
                        value = DBNull.Value;
                    }
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
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}

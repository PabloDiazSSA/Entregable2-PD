using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Converters
{
    public class ModelToParams
    {
        public static List<Param> GetParams<T>(T model) where T : class
        {
            try
            {
                PropertyInfo[] lst = typeof(T).GetProperties();
                List<Param> parametros = new List<Param>();
                foreach (PropertyInfo oProperty in lst)
                {
                    //string Tipo = oProperty.GetType().ToString(); //Traer el tipo de dato de a propiedad ej; decimal int, string
                    parametros.Add(new Param($"@{oProperty.Name}", oProperty.GetValue(model) == null || oProperty.GetValue(model).ToString() == string.Empty ? DBNull.Value : oProperty.GetValue(model).ToString()));
                }
                return parametros;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }
    }
}

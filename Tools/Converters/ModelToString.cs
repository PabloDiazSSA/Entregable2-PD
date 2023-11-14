using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Converters
{
    public class ModelToString
    {
        public static string GetString<T>(T model) where T : class
        {
            string obj = string.Empty;
            try
            {
                PropertyInfo[] lst = typeof(T).GetProperties();
                foreach (PropertyInfo oProperty in lst)
                {
                    string NombreAtributo = oProperty.Name;
                    // string Tipo = oProperty.GetType().ToString();
                    string? Valor = (oProperty.GetValue(model) == null) ? "NULL" : oProperty.GetValue(model).ToString();
                    obj += $" {NombreAtributo}:{Valor},";
                }
                return obj;
            }
            catch (Exception ex)
            {
                obj = ex.Message;
                return obj;
                throw;
            }
        }
       
    }
}

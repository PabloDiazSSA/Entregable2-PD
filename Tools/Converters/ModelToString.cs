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
    public static class ModelToString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetString<T>(T model) where T : class
        {
            try
            {
                PropertyInfo[] lst = typeof(T).GetProperties();
                StringBuilder bld = new StringBuilder();
                foreach (PropertyInfo oProperty in lst)
                {
                    var value = oProperty.GetValue(model);
                    string? Valor=string.Empty;
                    if (value is null)
                    {
                        Valor = "NULL";
                    }
                    if (value is not null && value.ToString() == string.Empty)
                    {
                        Valor = value.ToString();
                    }
                    bld.Append(@$" {oProperty.Name}:{Valor},");
                }
                return bld.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
       
    }
}

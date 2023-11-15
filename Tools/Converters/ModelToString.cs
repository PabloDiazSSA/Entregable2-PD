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
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString<T>(T model, string obj = "") where T : class
        {
            try
            {
                PropertyInfo[] lst = typeof(T).GetProperties();
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
                    
                    obj += @$" {oProperty.Name}:{Valor},";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SanitizeString
    {
        /// <summary>
        /// 
        /// </summary>
        protected SanitizeString()
        {
                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveHtml(string text)
        {
            var removeHTMLtagsRegex = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            return removeHTMLtagsRegex.Replace(text, string.Empty);

        }
    }
}

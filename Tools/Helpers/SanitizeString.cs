using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Helpers
{
    public static class SanitizeString
    {
        public static string RemoveHtml(string text)
        {
            Regex removeHTMLtagsRegex = new Regex("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
            return removeHTMLtagsRegex.Replace(text, string.Empty);

        }
    }
}

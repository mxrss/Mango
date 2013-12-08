namespace System.Web.Mvc.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public static class MarkdownHelper
    {
        public static MvcHtmlString FromMarkdown(this HtmlHelper helper, string text)
        {
            var instance = new MarkdownDeep.Markdown();

            instance.SafeMode = false;
            instance.ExtraMode = true;
            text = MakeCompatiableString(text);

            text = text.Replace("[[", "<").Replace("]]", ">");

            return MvcHtmlString.Create(instance.Transform(text.Trim()));
        }

        private static string MakeCompatiableString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var lines = text.Split('\n');
            return string.Concat(lines.Select(x => x.TrimStart() + "\n").ToList());
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace michaeltroth.blog
{
    public static class SlugifyExtension
    {
        public static string ToSlug(this string nonSlug)
        {
            return nonSlug.Replace(' ', '-');
        }

        public static string FromSlug(this string slug)
        {
            return slug.Replace('-', ' ');
        }
    }
}
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.BadWords
{
    public static class BadWordsFilter
    {
        public static Post FilterPosts(Post unFiltered)
        {
            var text = unFiltered.Text;
            string[] unAllowedWords = {
                "jävla",
                "helvete",
                "fitta",
                "kuk",
                "neger",
                "hora",
                "luder",
                "fitt",
                "jävlar",
                "kuken",
                "fittan",
                "fan"
            };
            foreach (var item in unAllowedWords)
            {
                if (unFiltered.Text.Contains(item))
                {
                    unFiltered.Text = unFiltered.Text.Replace(item, "***olämpligt innehåll***");
                }
            }

            return unFiltered;
        }
    }
}

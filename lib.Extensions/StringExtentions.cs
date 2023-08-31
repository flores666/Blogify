using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace lib.Extensions
{
    public static class StringExtentions
    {
        public static string GenerateSlug(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            // Remove all accents and make the string lower case.  
            string normalizedText = text.RemoveAccent().ToLower();

            var output = Transliteration.ConvertToLatin(normalizedText);

            // Remove all special characters from the string.  
            output = Regex.Replace(output, @"[^A-Za-z0-9\s-]", "");

            // Remove all additional spaces in favour of just one.  
            output = Regex.Replace(output, @"\s+", " ").Trim();

            // Replace all spaces with the hyphen.  
            output = Regex.Replace(output, @"\s", "-");

            // Return the slug.  
            return output;
        }

        private static string RemoveAccent(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            char[] chars = text
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

		public static string CutDescription(this string text, int length)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var result = text.Substring(0, Math.Min(text.Length, length));
            return text.Length > length ? result + "..." : result;
        }

        public static string DeleteFirstSpace(this string text)
        {
			if (string.IsNullOrEmpty(text)) return string.Empty;
            var index = text.IndexOf(' ');
            if (index < 1) return text.Substring(index + 1);
            return text;
		}
	}

}

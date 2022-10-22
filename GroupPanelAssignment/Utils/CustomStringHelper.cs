using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{
    public static class CustomStringHelper
    {
        public static string ConvertToKhebab(string value)
        {
            return value.Replace(" ", "-");
        }

        public static string ConvertToSpaced(string value)
        {
            return value.Replace("-", " ");
        }
        public static string Capitalize(string value)
        {
            string completeString = string.Empty;
            var stringList = value.Split(' ');
            foreach (var item in stringList)
            {
                string word = $"{item.Substring(0, 1).ToUpper()}{item.Substring(1, item.Length - 1).ToLower()}";
                completeString = $"{completeString} {word}";
            }
            return completeString;
        }
    }
}

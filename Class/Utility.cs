using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace SVMember
{
    public static class Utility
    {
        public static String PassphraseBase64
        {
            get { return "1627384950"; }
        }

        public static String KeyserialAscii
        {
            get { return "135797531135797531135797531135797531135797531135797531135797531135797531135797531135797531"; }
        }

        public static String Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static String Right(string param, int length)
        {
            int temp = param.Length - length;
            string result = param.Substring(temp, length);
            return result;
        }

        public static String Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        public static String Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }

        public static string[] Explode(string value, int size)
        {
            // Number of segments exploded to except last.
            int count = value.Length / size;

            // Determine if we need to store a final segment.
            // ... Sometimes we have a partial segment.
            bool final = false;
            if ((size * count) < value.Length)
            {
                final = true;
            }

            // Allocate the array to return.
            // ... The size varies depending on if there is a final fragment.
            string[] result;
            if (final)
            {
                result = new string[count + 1];
            }
            else
            {
                result = new string[count];
            }

            // Loop through each index and take a substring.
            // ... The starting index is computed with multiplication.
            for (int i = 0; i < count; i++)
            {
                result[i] = value.Substring((i * size), size);
            }

            // Sometimes we need to set the final string fragment.
            if (final)
            {
                result[result.Length - 1] = value.Substring(count * size);
            }
            return result;
        }
    }
}

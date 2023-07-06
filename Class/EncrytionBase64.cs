using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SVMember
{
    public class EncrytionBase64
    {
       

        private String is_raw, is_encrypted, is_key = "CGI", retVal, tStr;
        private int sourcePtr, keyPtr, keyLen, sourceLen, tempVal, tempKey;
        

        public string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        public string EncodeASCii1(string value)
        {
            System.Text.Encoding ascii = System.Text.Encoding.ASCII;
            return "";
        }

        public String EncryptAscii(String thestr)
        {

            retVal = is_raw;
            is_raw = thestr;

            keyPtr = 0;
            keyLen = is_key.Length;
            sourceLen = is_raw.Length;
            string is_encrypted = "";
            tempVal = 0;

            String s_tempVal, s_tempKey;
            for (sourcePtr = 1; sourcePtr <= sourceLen; sourcePtr++)
            {

                System.Text.Encoding ascii = System.Text.Encoding.ASCII;

               
                s_tempVal = Utility.Right(is_raw, sourceLen - sourcePtr + 1);
                Byte[] s_tempVal_encodedBytes = ascii.GetBytes(s_tempVal);
                s_tempVal = s_tempVal_encodedBytes.GetValue(0).ToString();
                tempVal = Int32.Parse(s_tempVal);

                s_tempKey = Utility.Mid(Utility.KeyserialAscii, keyPtr, 1);
                Byte[] s_tempKey_encodedBytes = ascii.GetBytes(s_tempKey);
                s_tempKey = s_tempKey_encodedBytes.GetValue(0).ToString();
                tempKey = Int32.Parse(s_tempKey);

                tempVal += tempKey;
                // Added this section to ensure that ASCII Values stay within 0 to 255 range
                do
                {
                    if (tempVal > 255)
                    {
                        tempVal = tempVal - 255;
                    }
                } while (tempVal > 255);

                tStr = tempVal.ToString("000");
                is_encrypted += tStr;
                keyPtr++;
                if (keyPtr > Utility.KeyserialAscii.Length) { keyPtr = 1; };

            }
            return is_encrypted;
        }
    }
}

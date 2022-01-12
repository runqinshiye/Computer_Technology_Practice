
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RunQinBusiness.DataInterface
{
    public class SerializeStringEscapes
    {
        private static Dictionary<char, string> _dictCustomEscapedXml = new Dictionary<char, string>()
    {
      {
        '<',
        "&lt;"
      },
      {
        '>',
        "&gt;"
      },
      {
        '"',
        "&quot;"
      },
      {
        '\'',
        "&#039;"
      },
      {
        '&',
        "&amp;"
      }
    };

        public static string EscapeForXml(string myString)
        {
            if (string.IsNullOrEmpty(myString))
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            int length = myString.Length;
            for (int startIndex = 0; startIndex < length; ++startIndex)
            {
                string str = myString.Substring(startIndex, 1);
                if (str.Equals("<"))
                    stringBuilder.Append("&lt;");
                else if (str.Equals(">"))
                    stringBuilder.Append("&gt;");
                else if (str.Equals("\""))
                    stringBuilder.Append("&quot;");
                else if (str.Equals("'"))
                    stringBuilder.Append("&#039;");
                else if (str.Equals("&"))
                    stringBuilder.Append("&amp;");
                else
                    stringBuilder.Append(str);
            }
            return stringBuilder.ToString();
        }
        public static string EscapeCustomXml(string myString) => string.IsNullOrEmpty(myString) ? "" : new Regex(SerializeStringEscapes.CustomWrapper("(\\b(?:" + string.Join("|", SerializeStringEscapes._dictCustomEscapedXml.Keys.Select<char, string>((Func<char, string>)(x => string.Format("{0}", (object)(int)x)))) + ")\\b)+", true)).Replace(myString, (MatchEvaluator)(match =>
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Capture capture in match.Captures)
            {
                foreach (char character in capture.Value)
                    stringBuilder.Append(SerializeStringEscapes.ToCustomXml(character));
            }
            return stringBuilder.ToString();
        }));
        private static string ToCustomXml(char character) => SerializeStringEscapes.CustomWrapper(string.Format("{0}", (object)(int)character));
        private static string CustomWrapper(string value, bool isRegex = false) => isRegex ? "\\[\\[" + value + "\\]\\]" : "[[" + value + "]]";

        public static string EscapeForXmlCustom(string myString)
        {
            if (string.IsNullOrEmpty(myString))
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            int length = myString.Length;
            for (int startIndex = 0; startIndex < length; ++startIndex)
            {
                string str = myString.Substring(startIndex, 1);
                if (str == "<")
                    stringBuilder.Append("[[60]]");
                else if (str == ">")
                    stringBuilder.Append("[[62]]");
                else if (str == "\"")
                    stringBuilder.Append("[[34]]");
                else if (str == "'")
                    stringBuilder.Append("[[39]]");
                else if (str == "&")
                    stringBuilder.Append("[[38]]");
                else
                    stringBuilder.Append(str);
            }
            return stringBuilder.ToString();
        }

        public static string EscapeForURL(string myString)
        {
            if (string.IsNullOrEmpty(myString))
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            int length = myString.Length;
            for (int startIndex = 0; startIndex < length; ++startIndex)
            {
                string str = myString.Substring(startIndex, startIndex + 1);
                if (str.Equals("<"))
                    stringBuilder.Append("%3C");
                else if (str.Equals(">"))
                    stringBuilder.Append("%3E");
                else if (str.Equals("&"))
                    stringBuilder.Append("%26");
                else
                    stringBuilder.Append(str);
            }
            return stringBuilder.ToString();
        }

        public static string ReplaceEscapes(string myString)
        {
            if (string.IsNullOrEmpty(myString))
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            for (int startIndex = 0; startIndex < myString.Length; ++startIndex)
            {
                string str1 = myString.Substring(startIndex, 1);
                if (str1 != "[")
                    stringBuilder.Append(str1);
                else if (myString.Substring(startIndex + 1, 1) != "[")
                {
                    stringBuilder.Append(str1);
                }
                else
                {
                    string str2 = myString.Substring(startIndex, myString.Length - startIndex);
                    int num = str2.IndexOf("]]");
                    if (num < 0)
                    {
                        stringBuilder.Append(str1);
                    }
                    else
                    {
                        int result;
                        if (!int.TryParse(str2.Substring(2, str2.IndexOf("]]") - 2), out result))
                        {
                            stringBuilder.Append(str1);
                        }
                        else
                        {
                            stringBuilder.Append(char.ConvertFromUtf32(result));
                            startIndex += num + 1;
                        }
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}

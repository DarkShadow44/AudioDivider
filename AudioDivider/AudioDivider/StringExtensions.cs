using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioDivider
{
    static class StringExtensions
    {
        public static string Escape(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                switch (c)
                {
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\0':
                        sb.Append("\\0");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case ';':
                    case '=':
                    case ':':
                    case '#':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string Unescape(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\\')
                {
                    i++;
                    if (i >= str.Length)
                        throw new InvalidOperationException();
                    switch (str[i])
                    {
                        case 'n':
                            sb.Append('\n');
                            break;
                        case '0':
                            sb.Append('\0');
                            break;
                        case 'r':
                            sb.Append('\r');
                            break;
                        case 't':
                            sb.Append('\t');
                            break;
                        case '\\':
                            sb.Append('\\');
                            break;
                        case ';':
                        case '=':
                        case ':':
                        case '#':
                            sb.Append(str[i]);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }
                else
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
    }
}

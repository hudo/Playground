using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace foos
{
    class Program
    {
        static string _allowedChars = "qwertyuiopasdfghjklzxcvbnm1234567890_QWERTYUIOPASDFGHJKLZXCVBNM";

        enum Token { Empty, Equals, Dash, Command, Value }

        static void Main(string[] args)
        {
            var line = "--arg1=a2 --arg2 2 --arg3=\" 3 4 5\" -a a --arg4 \"abc e\"";

            Token token = Token.Empty;
            var result = new Dictionary<string, string>();
            var key = "";
            var value = "";
            var hasQuote = false;
            

            for(var i = 0; i<line.Length; i++)
            {
                var c = line[i];

                if (c == '-')
                {
                    if (token == Token.Dash) 
                        continue;   
                    
                    if (token == Token.Value && hasQuote) {
                        value += c;
                        continue;
                    }

                    if (token == Token.Empty)
                    {
                        token = Token.Dash;
                        continue;
                    }
                }

                if (c == ' ')
                {
                    if (token == Token.Empty) 
                        continue;

                    if (token == Token.Value && hasQuote)
                    {
                        value += c;
                        continue;
                    }

                    if (token == Token.Value && !hasQuote)
                    {
                        if (!IsEmpty(value) && !IsEmpty(key))
                        {
                            result.Add(key, value);
                            key = "";
                            value = "";
                            token = Token.Empty;
                            continue;
                        }
                    }

                    if (token == Token.Command && !IsEmpty(key))
                    {
                        token = Token.Value;
                        continue;
                    }
                }

                if (c == '=')
                {
                    if (token == Token.Command && !IsEmpty(key))
                    {
                        token = Token.Value;
                        continue;
                    }

                    if (token == Token.Value && hasQuote)
                    {
                        value += c;
                        continue;
                    }
                }

                if (c == '"')
                {
                    if (token == Token.Value && !IsEmpty(key))
                    {
                        if (!hasQuote)
                        {
                            hasQuote = true;
                            continue;
                        }
                        else if (hasQuote && !IsEmpty(key) && !IsEmpty(value))
                        {
                            result.Add(key, value);
                            key = "";
                            value = "";
                            token = Token.Empty;
                            hasQuote = false;
                            continue;
                        }
                    }

                    if (token == Token.Empty && !IsEmpty(key))
                    {
                        hasQuote = true;
                        token = Token.Value;
                        continue;
                    }
                }

                if (_allowedChars.Contains(c))
                {
                    if (token == Token.Dash && IsEmpty(key) && IsEmpty(value))
                    {
                        token = Token.Command;
                        key += c;
                        continue;
                    }

                    if (token == Token.Command)
                    {
                        key += c;
                        continue;
                    }
                    
                    if (token == Token.Value)
                    {
                        value += c;
                        continue;
                    }
                }

                W($"Invalid char {c} on pos {i}:");
                W(line);
                W(new string('_', i) + "▲"); // ▲↑
                return;
            }

            W($"Done. Args count is {result.Count}");

            foreach(var item in result)
                W($"{item.Key} = {item.Value}");
        }

        static void W(string value) => System.Console.WriteLine(value);

        static bool IsEmpty(string value) => string.IsNullOrEmpty(value);

    }

    


}

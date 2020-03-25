using System;

namespace TextHello
{
    /// <summary>
    /// The obligatory Hello World in C#
    /// </summary>
    /// <remarks>
    /// This program writes out Hello World 
    /// using the WriteLine method of the
    /// System.Console class.
    /// </remarks>
    class Greeting
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input the arithmetic expression:");
            string s = "";
            // To check the length of  
            // Command line arguments   
            if (args.Length > 0)
            {

                // To print the command line  
                // arguments using foreach loop 
                foreach (Object obj in args)
                {
                    s += obj;
                }
            }

            else
            {
                Console.WriteLine("No command line arguments found.");
            }
            Console.WriteLine(CalculateExpression(s));
        }
        public static List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "," };
        public static List<string> operations = new List<string>() { "^", "/", "*", "+", "-" };
        public static List<string> skobki = new List<string>() { "(", ")" };



        public static double CalculateExpression(string priklad)
        {
            priklad = priklad.Replace(" ", "");
            List<string> symbols = new List<string>();

            string lastSymbol = "";
            string lastFunction = "";

            for (int i = 0; i < priklad.Length; i++)
            {
                if (skobki.Contains(priklad[i].ToString())) //есть ли скобки и числа в них
                {
                    if (lastSymbol != "")         //если не конец строки
                    {
                        symbols.Add(lastSymbol);  //записываем символ
                        lastSymbol = "";
                    }
                    symbols.Add(priklad[i].ToString());
                }
                else if (numbers.Contains(priklad[i].ToString()) || (priklad[i] == ',' && lastSymbol.IndexOf(",") == -1))
                {
                    lastSymbol += priklad[i];
                }
                else if (operations.Contains(priklad[i].ToString()))
                {
                    if (lastSymbol != "")
                    {
                        symbols.Add(lastSymbol);
                        lastSymbol = "";
                    }

                    if (symbols.Count >= 1 && operations.Contains(symbols[symbols.Count - 1]) || symbols.Count == 0)
                    {
                        string number = "";

                        switch (priklad[i].ToString())
                        {
                            case "-":
                                number += "-";
                                break;
                            case "+":
                                number += "+";
                                break;
                        }

                        i++;
                        while (i < priklad.Length && numbers.Contains(priklad[i].ToString()))
                        {
                            number += priklad[i];
                            i++;
                        }

                        symbols.Add(number);
                        i--;
                    }
                    else symbols.Add(priklad[i].ToString());
                }
                else
                {
                    lastFunction += priklad[i].ToString().ToLower();
                }
            }

            if (lastSymbol != "")
            {
                symbols.Add(lastSymbol);
                lastSymbol = "";
            }
            symbols = Skobki_dop_zadanie(symbols);
            symbols = Easier_to_calculate(symbols);

            List<string> result = new List<string>();

            string[] temp = symbols[0].Split(' ');
            for (int i = 0; i < temp.Length; i++)
            {
                if (operations.Contains(temp[i]))
                {
                    if (temp[i] == "^")
                    {
                        result[result.Count - 2] = Math.Pow(double.Parse(result[result.Count - 2]), double.Parse(result[result.Count - 1])).ToString();
                        result.RemoveRange(result.Count - 1, 1);
                    }
                    if (temp[i] == "+")
                    {
                        result[result.Count - 2] = (double.Parse(result[result.Count - 2]) + double.Parse(result[result.Count - 1])).ToString();
                        result.RemoveRange(result.Count - 1, 1);
                    }
                    if (temp[i] == "-")
                    {
                        result[result.Count - 2] = (double.Parse(result[result.Count - 2]) - double.Parse(result[result.Count - 1])).ToString();
                        result.RemoveRange(result.Count - 1, 1);
                    }
                    if (temp[i] == "*")
                    {
                        result[result.Count - 2] = (double.Parse(result[result.Count - 2]) * double.Parse(result[result.Count - 1])).ToString();
                        result.RemoveRange(result.Count - 1, 1);
                    }
                    if (temp[i] == "/")
                    {
                        result[result.Count - 2] = (double.Parse(result[result.Count - 2]) / double.Parse(result[result.Count - 1])).ToString();
                        result.RemoveRange(result.Count - 1, 1);
                    }
                }

                else result.Add(temp[i]);
            }

            return double.Parse(result[0]);

        }

        static List<string> Skobki_dop_zadanie(List<string> symbols)
        {
            while (symbols.Contains("("))
            {
                int start_skobki = 0;
                int end_skobki = 0;
                int how_many_in_skob = 0;

                for (int i = 0; i < symbols.Count; i++)
                {
                    if (symbols[i] == "(")
                    {
                        start_skobki = i;
                        how_many_in_skob = 1;
                        break;
                    }
                }

                for (int i = start_skobki + 1; i < symbols.Count; i++)
                {
                    if (symbols[i] == "(") how_many_in_skob++;
                    if (symbols[i] == ")") how_many_in_skob--;
                    if (how_many_in_skob == 0)
                    {
                        end_skobki = i;
                        break;
                    }
                }

                string what_in_skobkah = "";
                for (int i = start_skobki + 1; i < end_skobki; i++) what_in_skobkah += symbols[i];

                symbols[start_skobki] = CalculateExpression(what_in_skobkah).ToString();
                symbols.RemoveRange(start_skobki + 1, end_skobki - start_skobki);
            }
            return symbols;
        }
        static List<string> Easier_to_calculate(List<string> symbols)
        {

            foreach (var j in operations)
            {
                var flagO = true;
                while (flagO)
                {
                    flagO = false;
                    for (int i = 0; i < symbols.Count; i++)
                    {
                        if (symbols[i] == j)
                        {
                            symbols[i - 1] = symbols[i - 1] + " " + symbols[i + 1] + " " + j;
                            symbols.RemoveRange(i, 2);

                            flagO = true;
                            break;
                        }
                    }
                }
            }
            return symbols;
        }




    }
}



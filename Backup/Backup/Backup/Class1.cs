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
                if (skobki.Contains(priklad[i].ToString())) //���� �� ������ � ����� � ���
                {
                    if (lastSymbol != "")         //���� �� ����� ������
                    {
                        symbols.Add(lastSymbol);  //���������� ������
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
            //////////////////////////////////////////////�����/////////////////////////////////////////
            return 0;
        }
    }
}

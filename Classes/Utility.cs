using Lr1.Core.Classes;

using System.Text;

namespace Lr1.Classes
{
    internal static class Utility
    {
        public static int ReadInt()
        {
            var builder = new StringBuilder(2);
            bool numberEntered = false;
            bool numberExit = false;
            bool isNegative = false;
            bool isPositive = false;
            while (!numberExit)
            {
                var c = (char)Console.Read();
                if (!isNegative && !isPositive && c == '-')
                {
                    isNegative = true;
                }
                else if (!isNegative && !isPositive && c == '+')
                {
                    isPositive = true;
                }
                else if (char.IsNumber(c))
                {
                    numberEntered = true;
                    builder.Append(c);
                }
                else if (numberEntered && char.IsWhiteSpace(c))
                {
                    numberExit = true;
                }
                else if (!char.IsWhiteSpace(c))
                {
                    throw new Exception($"Wrong input. \'{c}\' is not a digit or white-space.");
                }
            }

            int integer = int.Parse(builder.ToString().AsSpan());
            if (isNegative)
                integer = -integer;

            return integer;
        }

        public static char ReadLetter()
        {
            while (true)
            {
                var c = (char)Console.Read();
                if (char.IsLetter(c))
                    return c;
                else if (!char.IsWhiteSpace(c))
                    throw new Exception($"Wrong input. \'{c}\' is not a letter or white-space.");
            }
        }

        public static string ReadString()
        {
            string? str;
            do
            {
                str = Console.ReadLine();
            }
            while (str == null || str.Trim().Length == 0);

            return str.Trim();
        }

        public static void WriteError(string mesage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mesage);
            Console.ResetColor();
        }
    }
}

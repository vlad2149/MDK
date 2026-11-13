using System.Text;

namespace NumSysLib
{
    /// <summary>
    /// Содержит общий алгоритм чтения и записи числа в заданной системе счисления.
    /// </summary>
    internal abstract class ConverterBase
    {
        protected int NumberBase { get; }

        protected ConverterBase(int numberBase)
        {
            NumberBase = numberBase;
        }

        internal bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            foreach (char symbol in value)
            {
                int digit = DigitToNumber(symbol);
                if (digit < 0 || digit >= NumberBase)
                {
                    return false;
                }
            }

            return true;
        }

        internal string ConvertTo(string value, ConverterBase target)
        {
            long number = ReadNumber(value);
            return target.WriteNumber(number);
        }

        private long ReadNumber(string value)
        {
            long result = 0;

            checked
            {
                foreach (char symbol in value)
                {
                    int digit = DigitToNumber(symbol);
                    result = result * NumberBase + digit;
                }
            }

            return result;
        }

        private string WriteNumber(long number)
        {
            if (number == 0)
            {
                return "0";
            }

            StringBuilder result = new StringBuilder();
            while (number > 0)
            {
                int digit = (int)(number % NumberBase);
                result.Insert(0, NumberToDigit(digit));
                number /= NumberBase;
            }

            return result.ToString();
        }

        private int DigitToNumber(char symbol)
        {
            if (symbol >= '0' && symbol <= '9')
            {
                return symbol - '0';
            }
            if (symbol >= 'A' && symbol <= 'F')
            {
                return symbol - 'A' + 10;
            }

            return -1;
        }

        private char NumberToDigit(int number)
        {
            if (number < 10)
            {
                return (char)('0' + number);
            }

            return (char)('A' + number - 10);
        }
    }
}

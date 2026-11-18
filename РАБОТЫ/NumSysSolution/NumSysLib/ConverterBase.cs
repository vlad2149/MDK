namespace NumSysLib;

internal abstract class ConverterBase
{
    internal abstract string ConvertTo10(string value);
    internal abstract string ConvertFrom10(string value);

    protected string ToBase10(string digitTo, int fromBase)
    {
        const string hexChars = "0123456789ABCDEF";
        long decimalValue = 0;

        checked
        {
            foreach (char symbol in digitTo)
            {
                int digit = hexChars.IndexOf(char.ToUpper(symbol));
                decimalValue = decimalValue * fromBase + digit;
            }
        }

        return decimalValue.ToString();
    }

    protected string FromBase10(string digitFrom, int toBase)
    {
        long currentDigit = long.Parse(digitFrom);

        if (currentDigit < 0)
        {
            throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
        }
        if (currentDigit == 0)
        {
            return "0";
        }

        const string hexChars = "0123456789ABCDEF";
        List<char> digits = new List<char>();

        while (currentDigit > 0)
        {
            int digit = (int)(currentDigit % toBase);
            digits.Add(hexChars[digit]);
            currentDigit /= toBase;
        }

        digits.Reverse();
        return new string(digits.ToArray());
    }
}

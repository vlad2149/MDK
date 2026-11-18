namespace NumSysLib;

internal class Hex : ConverterBase
{
    private string digitTo = string.Empty;
    private const int BaseSystem = 16;

    internal string DigitTo
    {
        get
        {
            return digitTo;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
            }

            foreach (char symbol in value.ToUpper())
            {
                bool isNumber = symbol >= '0' && symbol <= '9';
                bool isLetter = symbol >= 'A' && symbol <= 'F';

                if (!isNumber && !isLetter)
                {
                    throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
                }
            }

            digitTo = value.ToUpper();
        }
    }

    internal override string ConvertTo10(string value)
    {
        DigitTo = value;
        return ToBase10(DigitTo, BaseSystem);
    }

    internal override string ConvertFrom10(string value)
    {
        return FromBase10(value, BaseSystem);
    }
}

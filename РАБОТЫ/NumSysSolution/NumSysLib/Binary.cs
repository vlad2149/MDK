namespace NumSysLib;

internal class Binary : ConverterBase
{
    private string digitTo = string.Empty;
    private const int BaseSystem = 2;

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

            foreach (char symbol in value)
            {
                if (symbol != '0' && symbol != '1')
                {
                    throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
                }
            }

            digitTo = value;
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

namespace NumSysLib;

public class Adapter
{
    public string Convert(string value, int fromBase, int toBase)
    {
        ValidateBase(fromBase);
        ValidateBase(toBase);

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
        }

        string preparedValue = value.Trim().ToUpper();

        if (fromBase == 10 && toBase == 10)
        {
            return ReadDecimal(preparedValue).ToString();
        }
        if (fromBase == 10)
        {
            ConverterBase target = CreateConverter(toBase);
            return target.ConvertFrom10(preparedValue);
        }
        if (toBase == 10)
        {
            ConverterBase source = CreateConverter(fromBase);
            return source.ConvertTo10(preparedValue);
        }

        ConverterBase fromConverter = CreateConverter(fromBase);
        ConverterBase toConverter = CreateConverter(toBase);
        string decimalValue = fromConverter.ConvertTo10(preparedValue);
        return toConverter.ConvertFrom10(decimalValue);
    }

    private ConverterBase CreateConverter(int numberBase)
    {
        if (numberBase == 2)
        {
            return new Binary();
        }
        if (numberBase == 8)
        {
            return new Oct();
        }
        if (numberBase == 16)
        {
            return new Hex();
        }

        throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
    }

    private void ValidateBase(int numberBase)
    {
        if (numberBase != 2 && numberBase != 8 && numberBase != 10 && numberBase != 16)
        {
            throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
        }
    }

    private long ReadDecimal(string value)
    {
        long number = long.Parse(value);

        if (number < 0)
        {
            throw new ArgumentException("Ошибка ввода данных, проверьте вводимые данные.");
        }

        return number;
    }
}

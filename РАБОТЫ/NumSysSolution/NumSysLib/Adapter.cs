using System;

namespace NumSysLib
{
    /// <summary>
    /// Предоставляет единый доступ к переводу чисел между системами счисления.
    /// </summary>
    public class Adapter
    {
        private string numberValue = "0";
        private int fromBase = 10;
        private int toBase = 2;

        /// <summary>
        /// Получает или задаёт число для перевода.
        /// </summary>
        public string Value
        {
            get
            {
                return numberValue;
            }
            set
            {
                string newValue = value?.Trim().ToUpperInvariant() ?? "";
                ConverterBase converter = CreateConverter(FromBase);

                if (!converter.IsValid(newValue))
                {
                    throw new ArgumentException($"Число не соответствует системе счисления с основанием {FromBase}.");
                }

                numberValue = newValue;
            }
        }

        /// <summary>
        /// Получает или задаёт основание исходной системы счисления.
        /// </summary>
        public int FromBase
        {
            get
            {
                return fromBase;
            }
            set
            {
                ConverterBase converter = CreateConverter(value);

                if (!converter.IsValid(Value))
                {
                    throw new ArgumentException($"Число не соответствует системе счисления с основанием {value}.");
                }

                fromBase = value;
            }
        }

        /// <summary>
        /// Получает или задаёт основание системы счисления результата.
        /// </summary>
        public int ToBase
        {
            get
            {
                return toBase;
            }
            set
            {
                CreateConverter(value);
                toBase = value;
            }
        }

        /// <summary>
        /// Переводит число между системами счисления 2, 8, 10 и 16.
        /// </summary>
        /// <param name="value">Число для перевода.</param>
        /// <param name="fromBase">Основание исходной системы.</param>
        /// <param name="toBase">Основание системы результата.</param>
        /// <returns>Строковое представление результата.</returns>
        public string Convert(string value, int fromBase, int toBase)
        {
            Value = "0";
            FromBase = fromBase;
            ToBase = toBase;
            Value = value;

            ConverterBase source = CreateConverter(FromBase);
            ConverterBase target = CreateConverter(ToBase);
            return source.ConvertTo(Value, target);
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
            if (numberBase == 10)
            {
                return new DecimalNumber();
            }
            if (numberBase == 16)
            {
                return new Hex();
            }

            throw new ArgumentException("Поддерживаются только системы счисления 2, 8, 10 и 16.");
        }
    }
}

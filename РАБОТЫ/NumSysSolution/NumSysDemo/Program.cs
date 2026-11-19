using NumSysLib;

Adapter converter = new Adapter();

Console.WriteLine(converter.Convert("255", 10, 16));
Console.WriteLine(converter.Convert("11111111", 2, 16));

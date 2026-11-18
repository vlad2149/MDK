using NumSysLib;

namespace NumSysLib.Tests;

public class AdapterTests
{
    [Fact]
    public void BinaryToDecimalReturns13()
    {
        Adapter adapter = new Adapter();
        string value = "1101";
        string expected = "13";

        string actual = adapter.Convert(value, 2, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OctToDecimalReturns63()
    {
        Adapter adapter = new Adapter();
        string value = "77";
        string expected = "63";

        string actual = adapter.Convert(value, 8, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvalidSymbolThrowsException()
    {
        Adapter adapter = new Adapter();

        Action action = () => adapter.Convert("102", 2, 10);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void EmptyValueThrowsException()
    {
        Adapter adapter = new Adapter();

        Action action = () => adapter.Convert("", 2, 10);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void InvalidBaseThrowsException()
    {
        Adapter adapter = new Adapter();

        Action action = () => adapter.Convert("10", 3, 10);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void MaximumDecimalValueConvertsToHex()
    {
        Adapter adapter = new Adapter();
        string value = long.MaxValue.ToString();
        string expected = "7FFFFFFFFFFFFFFF";

        string actual = adapter.Convert(value, 10, 16);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BinaryValueWith63BitsConvertsToDecimal()
    {
        Adapter adapter = new Adapter();
        string value = new string('1', 63);
        string expected = long.MaxValue.ToString();

        string actual = adapter.Convert(value, 2, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BinaryValueWith64BitsThrowsOverflowException()
    {
        Adapter adapter = new Adapter();
        string value = new string('1', 64);

        Action action = () => adapter.Convert(value, 2, 10);

        Assert.Throws<OverflowException>(action);
    }

    [Fact]
    public void BinaryValueWith70BitsThrowsOverflowException()
    {
        Adapter adapter = new Adapter();
        string value = new string('1', 70);

        Action action = () => adapter.Convert(value, 2, 10);

        Assert.Throws<OverflowException>(action);
    }

    [Fact]
    public void DecimalValueLargerThanLongThrowsOverflowException()
    {
        Adapter adapter = new Adapter();
        string value = "9223372036854775808";

        Action action = () => adapter.Convert(value, 10, 2);

        Assert.Throws<OverflowException>(action);
    }
}

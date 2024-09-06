using System.Numerics;
using FluentAssertions;

namespace UnitTests;

public class IntToWordsTest
{
    [Theory]
    [InlineData(0, "ноль")]
    [InlineData(1, "один")]
    [InlineData(-1, "минус один")]
    [InlineData(10, "десять")]
    [InlineData(11, "одиннадцать")]
    [InlineData(20, "двадцать")]
    [InlineData(57, "пятьдесят семь")]
    [InlineData(100, "сто")]
    [InlineData(222, "двести двадцать два")]
    [InlineData(505, "пятьсот пять")]
    [InlineData(1001, "одна тысяча один")]
    [InlineData(56789, "пятьдесят шесть тысяч семьсот восемьдесят девять")]
    [InlineData(1200300, "один миллион двести тысяч триста")]
    [InlineData(5000000000, "пять миллиардов")]
    [InlineData(14342523000283101999, "четырнадцать квинтиллионов триста сорок два квадриллиона пятьсот двадцать три триллиона двести восемьдесят три миллиона сто одна тысяча девятьсот девяносто девять")]
    public void ToWords_Success(BigInteger number, string expected)
    {
        var actual = IntToWords.IntToWords.ToWords(number);
        actual.Should().Be(expected);
    }
}
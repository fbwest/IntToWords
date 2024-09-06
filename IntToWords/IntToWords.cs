using System.Numerics;

namespace IntToWords;

public static class IntToWords
{
    private static readonly string[] FirstDigitMale =
    {
        "ноль",
        "один",
        "два",
        "три",
        "четыре",
        "пять",
        "шесть",
        "семь",
        "восемь",
        "девять"
    };

    private static readonly string[] FirstDigitFemale =
    {
        "",
        "одна",
        "две"
    };

    private static readonly string[] TeensDigit = 
    {
        "десять",
        "одиннадцать",
        "двенадцать",
        "тринадцать",
        "четырнадцать",
        "пятнадцать",
        "шестнадцать",
        "семнадцать",
        "восемнадцать",
        "девятнадцать"
    };

    private static readonly string[] SecondDigit = 
    {
        "",
        "",
        "двадцать",
        "тридцать",
        "сорок",
        "пятьдесят",
        "шестьдесят",
        "семьдесят",
        "восемьдесят",
        "девяносто"
    };

    private static readonly string[] ThirdDigit = 
    {
        "",
        "сто",
        "двести",
        "триста",
        "четыреста",
        "пятьсот",
        "шестьсот",
        "семьсот",
        "восемьсот",
        "девятьсот"
    };

    private static readonly string[] BigNumbers = 
    {
        "",
        "тысяч",
        "миллион",
        "миллиард",
        "триллион",
        "квадриллион",
        "квинтиллион",
        "секстиллион",
        "септиллион",
        "октиллион",
        "нониллион",
        "дециллион"
    };
    
    public static string ToWords(BigInteger number)
    {
        // 0
        if (number == 0) return FirstDigitMale[0];
        
        // minus prefix
        var prefix = string.Empty;
        if (number < 0)
        {
            prefix = "минус ";
            number = -number;
        }
        
        // calculating number's length
        var numberStr = number.ToString();
        var numberWords = string.Empty;
        var numberStrLength = numberStr.Length;
        var bigLength = (int)Math.Ceiling((decimal)numberStrLength / 3); // number of thousands
        
        // go
        for (var i = 0; i < bigLength; i++)
        {
            string partNumberStr;
            if (numberStr.Length > 3)
            {
                partNumberStr = numberStr[^3..];
                numberStr = numberStr.Remove(numberStr.Length - 3);
            }
            else partNumberStr = numberStr;

            if (partNumberStr == "000") continue;
            
            var lastDigits = byte.Parse(partNumberStr.Length > 2
                ? partNumberStr[^2..]
                : partNumberStr);

            numberWords = $"{SmallDigits(partNumberStr, i)} {GetBigNumberName(i, lastDigits)} {numberWords}";
        }
     
        return $"{prefix}{numberWords}".TrimEnd();
    }

    private static void AddDigit(ref string input, string digit)
    {
        if (!string.IsNullOrEmpty(input)) input += " ";

        input += digit;
    }

    private static string GetBigNumberName(int exponent, byte lastDigits)
    {
        int digit;
        if ((lastDigits >= 11) & (lastDigits <= 14))
        {
            digit = 0;
        }
        else
        {
            digit = lastDigits % 10;
        }

        if (exponent == 0) return "";

        string end;
        
        if (exponent == 1)
        {
            end = digit switch
            {
                1 => "а",
                >= 2 and <= 4 => "и",
                _ => ""
            };
        }
        else
        {
            end = digit switch
            {
                1 => "",
                >= 2 and <= 4 => "а",
                _ => "ов"
            };
        }
        return BigNumbers[exponent] + end;
    }

    private static string SmallDigits(string numberStr, int exponent)
    {
        var result = string.Empty;
        int num;
        
        // *** digits
        if (numberStr.Length == 3)
        {
            num = int.Parse(numberStr[0].ToString());
            result = ThirdDigit[num];
            numberStr = numberStr.Remove(0, 1);
        }
        
        // ** digits
        if (numberStr.Length == 2)
        {
            // 10..19
            num = int.Parse(numberStr);
            if ((num >= 10) & (num <= 19))
            {
                var digit = num % 10;
                AddDigit(ref result, TeensDigit[digit]);
                numberStr = "0";
            }
            else
            {
                num = int.Parse(numberStr[0].ToString());
                if (num > 0)
                {
                    AddDigit(ref result, SecondDigit[num]);
                }
                numberStr = numberStr.Remove(0, 1);
            }
        }
        
        // * digit
        if (numberStr != "0")
        {
            if (exponent == 1 && numberStr is "1" or "2")
            {
                AddDigit(ref result, FirstDigitFemale[int.Parse(numberStr)]);
            }
            else
            {
                AddDigit(ref result, FirstDigitMale[int.Parse(numberStr)]);
            }
        }

        return result;
    }
}
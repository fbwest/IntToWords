using System.Numerics;
using System.Text;

namespace IntToWords;

internal static class Program
{
    private enum MessageType
    {
        WrongInput,
        TooBig,
        Success
    }

    private static string _arg;
    
    private static void ShowMessage(MessageType messageType, string message = "")
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Magenta;
        
        switch (messageType)
        {
            case MessageType.WrongInput:
                Console.Write("Copy (Ctrl + c) an integer to the clipboard and run again");
                break;
            case MessageType.TooBig:
                Console.Write("The number is too big. Maximum is 99 decillions (99E+33)");
                break;
            case MessageType.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);
                break;
            default:
                Console.Write("Unknown error");
                break;
        }

        Console.WriteLine("\n");
        Console.ResetColor();
        
        if (_arg == "-m")
        {
            Console.Write("Press any key to exit...");
            Console.ReadLine();
        }
        else
        {
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
        }
    }
    
    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Length > 0) _arg = args[0];
        
        // copy data from clipboard
        var clipData = Clipboard.GetDataObject();
        if (!clipData?.GetDataPresent(DataFormats.Text) ?? false)
        {
            ShowMessage(MessageType.WrongInput);
            return;
        }

        var buffer = clipData?.GetData(DataFormats.Text);
        var bufferStr = buffer?.ToString()?.Trim();
        if (BigInteger.TryParse(bufferStr, out var number))
        {
            // check number length
            if (bufferStr.Length > 35)
            {
                ShowMessage(MessageType.TooBig);
                return;
            }
            
            var intToWords = IntToWords.ToWords(number);

            // copy data to clipboard
            Clipboard.SetData("Text", intToWords);
            
            // show results
            ShowMessage(MessageType.Success,
                $"Input: {number:N0}\n\nResult: {intToWords}\n\nThe result has been copied to the clipboard (use 'Ctrl + v')");
        }
        else
        {
            ShowMessage(MessageType.WrongInput);
        }
    }
}
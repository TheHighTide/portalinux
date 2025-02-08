using System.Text;

namespace PortaLinuxVM.Modules.HighTide
{
    public static class AdvancedConsole
    {
        public static string ReadLine()
        {
            StringBuilder inputBuffer = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.C && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^C");
                    return "\u0003";
                }
                else if (keyInfo.Key == ConsoleKey.A && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^A");
                    return "\u0001";
                }
                else if (keyInfo.Key == ConsoleKey.B && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^B");
                    return "\u0002";
                }
                else if (keyInfo.Key == ConsoleKey.D && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^D");
                    return "\u0004";
                }
                else if (keyInfo.Key == ConsoleKey.E && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^E");
                    return "\u0005";
                }
                else if (keyInfo.Key == ConsoleKey.F && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^F");
                    return "\u0006";
                }
                else if (keyInfo.Key == ConsoleKey.G && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^G");
                    return "\u0007";
                }
                else if (keyInfo.Key == ConsoleKey.H && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^H");
                    return "\u0008";
                }
                else if (keyInfo.Key == ConsoleKey.I && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^I");
                    return "\u0009";
                }
                else if (keyInfo.Key == ConsoleKey.J && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^J");
                    return "\u000A";
                }
                else if (keyInfo.Key == ConsoleKey.K && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^K");
                    return "\u000B";
                }
                else if (keyInfo.Key == ConsoleKey.L && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    return "\u000C";
                }
                else if (keyInfo.Key == ConsoleKey.M && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^M");
                    return "\u000D";
                }
                else if (keyInfo.Key == ConsoleKey.N && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^N");
                    return "\u000E";
                }
                else if (keyInfo.Key == ConsoleKey.O && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^O");
                    return "\u000F";
                }
                else if (keyInfo.Key == ConsoleKey.P && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    Console.WriteLine("^P");
                    return "\u0010";
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return inputBuffer.ToString();
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && inputBuffer.Length > 0)
                {
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    inputBuffer.Append(keyInfo.KeyChar);
                    Console.Write(keyInfo.KeyChar);
                }
            }
        }
    }
}

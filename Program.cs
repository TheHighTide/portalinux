using PortaLinuxVM.Modules.HighTide;
using PortaLinuxVM.Modules.System;
using System.Reflection;
using System.Text;

namespace PortaLinuxVM
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "PortaLinux Virtual Machine";
            Console.TreatControlCAsInput = true;

            string userInfo_name = "user";
            VFS vfs = new VFS();
            string commandLine = GetPrompt(userInfo_name, vfs.CurrentDirectory.Name);
            DateTime startTime = DateTime.Now;

            Console.WriteLine("PortaLinuxVM successfully loaded!");
            Console.Write("Press any key to start...");
            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                Console.Write(commandLine);
                string userInput = AdvancedConsole.ReadLine() ?? string.Empty;
                string[] args = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (args.Length == 0) continue;

                string command = args[0].ToLower();
                string argument = args.Length > 1 ? args[1] : "";

                switch (command)
                {
                    case "mkdir":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("mkdir: missing operand\nTry 'mkdir --help' for more information.");
                        }
                        else if (argument == "--help")
                        {
                            string helpContent = ReadEmbeddedResource("PortaLinuxVM.Properties.Resources.MkdirHelp.txt");
                            Console.WriteLine(helpContent);
                        }
                        else if (vfs.CreateDirectory(argument)) { }
                        else
                        {
                            Console.WriteLine($"mkdir: cannot create directory '{argument}': File exists");
                        }
                        break;

                    case "echo":
                        if (args.Length > 1)
                        {
                            string outputText = string.Join(" ", args.Skip(1));

                            if (outputText.Contains(">"))
                            {
                                string[] parts = outputText.Split('>');
                                string textToWrite = parts[0].Trim();
                                string fileName = parts[1].Trim();

                                bool appendMode = fileName.StartsWith(">>");
                                if (appendMode)
                                {
                                    fileName = fileName.Substring(2).Trim();
                                }

                                if (string.IsNullOrEmpty(fileName))
                                {
                                    Console.WriteLine("echo: No file specified.");
                                    break;
                                }

                                vfs.WriteToFile(fileName, textToWrite, appendMode);
                            }
                            else
                            {
                                Console.WriteLine(outputText);
                            }
                        }
                        break;

                    case "ls":
                        if (argument == "--help")
                        {
                            string helpContent = ReadEmbeddedResource("PortaLinuxVM.Properties.Resources.LsHelp.txt");
                            Console.WriteLine(helpContent);
                        }
                        else
                        {
                            Console.WriteLine(vfs.ListDirectory());
                        }
                        break;

                    case "cd":
                        if (argument == "--help")
                        {
                            string helpContent = ReadEmbeddedResource("PortaLinuxVM.Properties.Resources.CdHelp.txt");
                            Console.WriteLine(helpContent);
                        }
                        else if (args.Length == 1)
                        {
                            vfs.ChangeDirectory(vfs.Root.FullPath);
                            commandLine = GetPrompt(userInfo_name, vfs.CurrentDirectory.FullPath);
                        }
                        else if (vfs.ChangeDirectory(argument))
                            commandLine = GetPrompt(userInfo_name, vfs.CurrentDirectory.FullPath);
                        else
                            Console.WriteLine($"cd: {argument}: No such directory");
                        break;

                    case "cat":
                        if (argument == "--help")
                        {
                            string helpContent = ReadEmbeddedResource("PortaLinuxVM.Properties.Resources.CatHelp.txt");
                            Console.WriteLine(helpContent);
                        }
                        else if (args.Length == 1)
                        {
                            while (true)
                            {
                                bool exitedWithControlC = false;
                                StringBuilder inputBuffer = new StringBuilder();

                                while (true)
                                {
                                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                                    if (keyInfo.Key == ConsoleKey.C && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                                    {
                                        Console.WriteLine("^C");
                                        exitedWithControlC = true;
                                        break;
                                    }
                                    else if (keyInfo.Key == ConsoleKey.Enter)
                                    {
                                        Console.WriteLine();
                                        break;
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

                                if (exitedWithControlC) break;

                                string input = inputBuffer.ToString();
                                Console.WriteLine(input);
                            }
                        }
                        else
                        {
                            Console.WriteLine(vfs.ReadFile(argument));
                        }
                        break;

                    case "neofetch":
                        if (argument == "--help")
                        {
                            string helpContent = ReadEmbeddedResource("PortaLinuxVM.Properties.Resources.NeofetchHelp.txt");
                            Console.WriteLine(helpContent);
                        }
                        else
                        {
                            int userPlusComLen = userInfo_name.Length + "PortaLinuxVM".Length;
                            string nameDivider = String.Empty;
                            for (int i = -1; i < userPlusComLen; i++)
                            {
                                nameDivider += '-';
                            }

                            TimeSpan apptime = DateTime.Now - startTime;

                            string uptime = String.Empty;
                            string secondsText = "secs";
                            string minutesText = "mins";
                            string hoursText = "hours";
                            string daysText = "days";
                            if (apptime.Seconds == 1) secondsText = "sec";
                            if (apptime.Minutes == 1) secondsText = "min";
                            if (apptime.Hours == 1) hoursText = "hour";
                            if (apptime.Days == 1) daysText = "day";

                            if (apptime.Seconds < 60)
                            {
                                uptime = $"{apptime.Seconds} {secondsText}";
                            }
                            else if (apptime.Minutes < 60)
                            {
                                uptime = $"{apptime.Minutes} {minutesText}, {apptime.Seconds}, {secondsText}";
                            }
                            else if (apptime.Hours < 24)
                            {
                                uptime = $"{apptime.Hours} {hoursText}, {apptime.Minutes} {minutesText}";
                            }
                            else
                            {
                                uptime = $"{apptime.Days} {daysText}, {apptime.Hours} {hoursText}, {apptime.Minutes} {minutesText}";
                            }

                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}         {RED}A{CYAN}           " + userInfo_name + "{RESET}@{CYAN}PortaLinuxVM"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}        {RED}/{MAGENTA}|{RED}\\{CYAN}          {RESET}" + nameDivider));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}       {RED}/{CYAN}*{MAGENTA}|{CYAN}*{RED}\\{CYAN}         OS{RESET}: Totally a real OS and not just a simulator"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}      {RED}/{CYAN}##{MAGENTA}|{CYAN}##{RED}\\{CYAN}        Kernel{RESET}: PortaLinux Kernel 0.2.0"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}     {RED}/{CYAN}###{MAGENTA}|{CYAN}###{RED}\\{CYAN}       Uptime{RESET}: " + uptime));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}    {RED}/{CYAN}####{MAGENTA}|\\{CYAN}###{RED}\\{CYAN}      Packages{RESET}: Irrelivant because no pkg manager YET"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}   {RED}/{CYAN}#####{MAGENTA}|\\\\{CYAN}###{RED}\\{CYAN}     Shell{RESET}: PortaBash 1.2.0"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}  {RED}/{CYAN}######{MAGENTA}|{CYAN}%{MAGENTA}\\\\{CYAN}###{RED}\\{CYAN}    Theme{RESET}: Default [TheHighTide]"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN} {RED}/{CYAN}*#####*{MAGENTA}|{CYAN}%%{MAGENTA}\\\\{CYAN}##*{RED}\\{CYAN}   Icons{RESET}: Default [TheHighTide]"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}{RED}{{MAGENTA}>~-----~+~-----~<{RED}}{CYAN}  Terminal{RESET}: PortaBash GT (General Terminal)"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN} {RED}\\{CYAN}*##{MAGENTA}\\\\{CYAN}%%{MAGENTA}|{CYAN}*#####*{RED}/{CYAN}   CPU{RESET}: Unknown"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}  {RED}\\{CYAN}###{MAGENTA}\\\\{CYAN}%{MAGENTA}|{CYAN}######{RED}/{CYAN}    GPU{RESET}: Unknown"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}   {RED}\\{CYAN}###{MAGENTA}\\\\|{CYAN}#####{RED}/{CYAN}     Memory{RESET}: Unknown"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}    {RED}\\{CYAN}###{MAGENTA}\\|{CYAN}####{RED}/{CYAN}"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}     {RED}\\{CYAN}###{MAGENTA}|{CYAN}###{RED}/{CYAN}       {BLACK}███{RED}███{GREEN}███{YELLOW}███{BLUE}███{MAGENTA}███{CYAN}███{WHITE}███{DEFAULT}"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}      {RED}\\{CYAN}##{MAGENTA}|{CYAN}##{RED}/{CYAN}        {LBLACK}███{LRED}███{LGREEN}███{LYELLOW}███{LBLUE}███{LMAGENTA}███{LCYAN}███{LWHITE}███{DEFAULT}"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}       {RED}\\{CYAN}*{MAGENTA}|{CYAN}*{RED}/{CYAN}"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}        {RED}\\{MAGENTA}|{RED}/{CYAN}"));
                            Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}         {RED}V{DEFAULT}"));
                        }
                        
                        break;

                    case "shutdown":
                        Environment.Exit(0);
                        break;

                    case "\u000C":
                        Console.Clear();
                        break;

                    case "\u0003": case "\\u0003":
                        break;

                    default:
                        Console.WriteLine($"{command}: command not found");
                        break;
                }
            }
        }

        static string GetPrompt(string user, string path)
        {
            return ConsoleFormatting.FormatString($"{{GREEN}}{user}@PortaLinuxVM{{DEFAULT}}:{{LBLUE}}{path}{{RESET}}$ ");
        }

        static string ReadEmbeddedResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return $"Resource not found: {resourceName}";
            }

            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
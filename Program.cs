using PortaLinuxVM.Modules.HighTide;
using System.Text;

namespace PortaLinuxVM
{
    internal class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Loading PortaLinuxVM...");
            Console.ResetColor();

            Console.Title = "PortaLinux Virtual Machine"; // Set the title of the window the application is running in
            Console.TreatControlCAsInput = true; // Make the CTRL+C keys not terminate the console application when they're pressed together
            string userInput = String.Empty; // Create an empty variable for storing user input
            string currentPath = "~"; // Create a variable that will store the current file location of the user
            string userInfo_name = "user"; // Create a variable for storing the user's name
            string commandLine = String.Empty; // Create a variable for storing the command line text thats displayed before user input
            commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ "); // Setup the commandLine variable defined before
            DateTime startTime = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PortaLinuxVM has been successfully loaded!");
            Console.ResetColor();

            Console.Write("Press any key to start the VM...");
            Console.ReadKey();
            Console.Clear(); // Clears all content from the current console

            while (true) // Starts the main command loop
            {
                Console.Write(commandLine);
                userInput = AdvancedConsole.ReadLine();
                string lowercaseInput = userInput.ToLower(); // Convert the userInput variable to lowercase for easier use
                string[] lowerCommand = lowercaseInput.Split(' ');
                string[] upperCommand = userInput.Split(' ');

                if (lowerCommand.Length > 0 && lowerCommand[0].Length > 0)
                {
                    if (lowerCommand[0] == "echo")
                    {
                        for (int i = 1; i < upperCommand.Length; i++)
                        {
                            Console.Write(upperCommand[i]);
                        }
                        Console.Write('\n');
                    }
                    else if (lowerCommand[0] == "ls")
                    {
                        if (currentPath == "~")
                        {
                            Console.WriteLine(ConsoleFormatting.FormatString("{LBLUE}ExampleDirectory  Data{RESET}  cool_file.txt  file.txt"));
                        }
                        else if (currentPath == "~/ExampleDirectory")
                        {
                            Console.WriteLine();
                        }
                        else if (currentPath == "~/Data")
                        {
                            Console.WriteLine(ConsoleFormatting.FormatString("{LBLUE}VMStuffs{RESET}  credits.txt"));
                        }
                        else if (currentPath == "~/Data/VMStuffs")
                        {
                            Console.WriteLine("changelog.txt  neofetch.txt");
                        }
                    }
                    else if (lowerCommand[0] == "cd")
                    {
                        if (lowerCommand.Length > 1)
                        {
                            bool madeSuccessfullChange = true;
                            if (upperCommand[1].Contains('/'))
                            {
                                if (upperCommand[1] == "~/Data/VMStuffs")
                                {
                                    currentPath = "~/Data/VMStuffs";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "~/Data")
                                {
                                    currentPath = "~/Data";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "~/ExampleDirectory")
                                {
                                    currentPath = "~/ExampleDirectory";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "Data/VMStuffs" && currentPath == "~")
                                {
                                    currentPath = "~/Data/VMStuffs";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                            }
                            else if (currentPath == "~")
                            {
                                if (upperCommand[1] == "ExampleDirectory")
                                {
                                    currentPath = "~/ExampleDirectory";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "cool_file.txt" || upperCommand[1] == "file.txt")
                                {
                                    Console.WriteLine("cd: " + upperCommand[1] + ": Not a directory");
                                }
                                else if (upperCommand[1] == "Data")
                                {
                                    currentPath = "~/Data";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else
                                {
                                    madeSuccessfullChange = false;
                                }
                            }
                            else if (currentPath == "~/ExampleDirectory")
                            {
                                if (upperCommand[1] == "..")
                                {
                                    currentPath = "~";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else
                                {
                                    madeSuccessfullChange = false;
                                }
                            }
                            else if (currentPath == "~/Data")
                            {
                                if (upperCommand[1] == "..")
                                {
                                    currentPath = "~";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "credits.txt")
                                {
                                    Console.WriteLine("cd: " + upperCommand[1] + ": Not a directory");
                                }
                                else if (upperCommand[1] == "VMStuffs")
                                {
                                    currentPath = "~/Data/VMStuffs";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else
                                {
                                    madeSuccessfullChange = false;
                                }
                            }
                            else if (currentPath == "~/Data/VMStuffs")
                            {
                                if (upperCommand[1] == "..")
                                {
                                    currentPath = "~/Data";
                                    commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                                }
                                else if (upperCommand[1] == "changelog.txt" || upperCommand[1] == "neofetch.txt")
                                {
                                    Console.WriteLine("cd: " + upperCommand[1] + ": Not a directory");
                                }
                                else
                                {
                                    madeSuccessfullChange = false;
                                }
                            }

                            if (!madeSuccessfullChange)
                            {
                                Console.WriteLine("cd: " + upperCommand[1] + ": No such file or directory");
                            }
                        }
                        else
                        {
                            currentPath = "~";
                            commandLine = ConsoleFormatting.FormatString("{GREEN}" + userInfo_name + "@ProtaLinuxVM{RESET}:{LBLUE}" + currentPath + "{RESET}$ ");
                        }
                    }
                    else if (lowerCommand[0] == "cat")
                    {
                        if (upperCommand.Length > 1)
                        {
                            if (lowerCommand[1] == "--help")
                            {
                                Console.WriteLine("Usage: cat [OPTION]... [FILE]...");
                                Console.WriteLine("Concatenate FILE(s) to standard output.");
                                Console.WriteLine("");
                                Console.WriteLine("With no FILE, or when FILE is -, read standard input.");
                                Console.WriteLine("");
                                Console.WriteLine("  -A, --show-all             equivalent to -vET");
                                Console.WriteLine("  -b, --number-nonblank      number nonempty output lines, overrides -n");
                                Console.WriteLine("  -e                         equivalent to -vE");
                                Console.WriteLine("  -E, --show-ends            display $ at end of each line");
                                Console.WriteLine("  -n, --number               number all output lines");
                                Console.WriteLine("  -s, --squeeze-blank        suppress repeated empty output lines");
                                Console.WriteLine("  -t                         equivalent to -vT");
                                Console.WriteLine("  -T, --show-tabs            display TAB characters as ^I");
                                Console.WriteLine("  -u                         (ignored)");
                                Console.WriteLine("  -v, --show-nonprinting     use ^ and M- notation, except for LFD and TAB");
                                Console.WriteLine("      --help         display this help and exit");
                                Console.WriteLine("      --version      output version information and exit");
                                Console.WriteLine("");
                                Console.WriteLine("Examples:");
                                Console.WriteLine("  cat f - g  Output f's contents, then standard input, the g's contents.");
                                Console.WriteLine("  cat        Copy standard input to standard output.");
                                Console.WriteLine("");
                                Console.WriteLine("GNU coreutils online help: <https://www.gnu.org/software/coreutils/>");
                                Console.WriteLine("Report and translation bugs to <https://translationproject.org/team/>");
                                Console.WriteLine("Full documentation <https://www.gnu.org/software/coreutils/cat>");
                                Console.WriteLine("or available locally via: info '(coreutils cat invocation'");
                                Console.WriteLine("");
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("THIS APPLICATION IS NOT FULLY SUPPORTED YET!");
                                Console.ResetColor();
                            }
                            else if (upperCommand[1] == "cool_file.txt" && currentPath == "~")
                            {
                                Console.WriteLine("This is some awesome text that will show up in a cool file!");
                            }
                            else if (upperCommand[1] == "file.txt" && currentPath == "~")
                            {
                                Console.WriteLine("This is a text file that will appear on a computer.");
                                Console.WriteLine("This text file was written on Linux and is mainly here to test my new portable linux VM.");
                                Console.WriteLine("This linux VM isn't actually linux but aims to replicate many features linux did in a portable manner.");
                                Console.WriteLine("This means that you won't need those pesky administrator permissions to run this virtual machine.");
                                Console.WriteLine("I'm hopefully going to get this machine working with Linux formatted executables because that would be really cool :D");
                            }
                            else if (upperCommand[1] == "credits.txt" && currentPath == "~/Data")
                            {
                                Console.WriteLine("This app was mainly developed by a single person 'TheHighTide'.");
                                Console.WriteLine("Although this is true, heres who else put work into this program.");
                                Console.WriteLine("Dipee_       : Emotional and mental support");
                                Console.WriteLine("DPye         : The person who got this project started");
                                Console.WriteLine("The GNU Team : The people behind linux (my reference)");
                                Console.WriteLine("ChatGPT      : Fixing my stupid grammar issues");
                                Console.WriteLine("YurZipper    : Helping me with the code");
                            }
                            else if (upperCommand[1] == "changelog.txt" && currentPath == "~/Data/VMStuffs")
                            {
                                Console.WriteLine("====------------------------------------====");
                                Console.WriteLine("Changelogs for the PortaLinux VM Application");
                                Console.WriteLine("====------------------------------------====");
                                Console.WriteLine();
                                Console.WriteLine("_____________________________Version 0.1.0__");
                                Console.WriteLine("Added:");
                                Console.WriteLine("  - Added the 'echo` command");
                                Console.WriteLine("  - Added the 'ls' command");
                                Console.WriteLine("  - Added the 'cd' command");
                                Console.WriteLine("  - Added the 'cat' command");
                                Console.WriteLine("  - Added a help menu for the 'cat' command");
                                Console.WriteLine();
                                Console.WriteLine("Extra Notes: ");
                                Console.WriteLine("  - Yes, I know how much stuff there still is");
                                Console.WriteLine("    to add in order to make this application");
                                Console.WriteLine("    perfectly replicate linux, but, theres");
                                Console.WriteLine("    also a lot more commands and features I");
                                Console.WriteLine("    have to add as well. The biggest feature");
                                Console.WriteLine("    I still have to add is a 'virtual file");
                                Console.WriteLine("    system'. It might not seem or sound like");
                                Console.WriteLine("    much but if you want to create, delete");
                                Console.WriteLine("    and edit files, you're going to need one.");
                            }
                            else if (upperCommand[1] == "neofetch.txt" && currentPath == "~/Data/VMStuffs")
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
                                Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}      {RED}/{CYAN}##{MAGENTA}|{CYAN}##{RED}\\{CYAN}        Kernel{RESET}: PortaLinux Kernel 0.1.1"));
                                Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}     {RED}/{CYAN}###{MAGENTA}|{CYAN}###{RED}\\{CYAN}       Uptime{RESET}: " + uptime));
                                Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}    {RED}/{CYAN}####{MAGENTA}|\\{CYAN}###{RED}\\{CYAN}      Packages{RESET}: Irrelivant because no pkg manager YET"));
                                Console.WriteLine(ConsoleFormatting.FormatString("{CYAN}   {RED}/{CYAN}#####{MAGENTA}|\\\\{CYAN}###{RED}\\{CYAN}     Shell{RESET}: PortaBash 1.0.0"));
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
                            else if (upperCommand[1] == "ExampleDirectory" && currentPath == "~")
                            {
                                Console.WriteLine("cat: ExampleDirectory: Is a directory");
                            }
                            else
                            {
                                Console.WriteLine("cat: " + upperCommand[1] + ": No such file or directory");
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.CursorTop);
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
                    }
                    else if (lowerCommand[0] == "shutdown")
                    {
                        Environment.Exit(0);
                    }
                    else if (!userInput.Contains("\u0003"))
                    {
                        Console.WriteLine(upperCommand[0] + ": command not found");
                    }
                }
            }
        }
    }
}

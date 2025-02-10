using PortaLinuxVM.Modules.System;
using PortaLinuxVM.Modules.HighTide;
using System.Text;

namespace PortaLinuxVM.Software
{
    public class Vim
    {
        private static bool hasDrawnThisTurn = false;

        public static void StartVim(VFS vfs, string fileName)
        {
            bool fileExists = vfs.CurrentDirectory.Files.ContainsKey(fileName);
            string fileContent = fileExists ? vfs.ReadFile(fileName) : "";

            StringBuilder buffer = new StringBuilder(fileContent);
            bool inInsertMode = false;
            int cursorLine = 1, cursorColumn = 1;

            bool wasJustInCommand = true;

            Console.Clear();
            RedrawLines(buffer.ToString());

            while (true)
            {
                if (cursorLine > Console.WindowHeight)
                {
                    cursorLine = Console.WindowHeight - 1;
                    Console.SetCursorPosition(cursorColumn - 1, Console.WindowHeight - 2);
                }
                else
                {
                    Console.SetCursorPosition(cursorColumn - 1, cursorLine - 1);
                }

                if (inInsertMode)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    string[] lines = buffer.ToString().Split('\n');

                    if (wasJustInCommand) wasJustInCommand = false;

                    if (key.Key == ConsoleKey.Escape)
                    {
                        inInsertMode = false;
                        DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                        FlushInputBuffer();
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (cursorColumn > 1)
                        {
                            buffer.Remove(buffer.Length - 1, 1);
                            cursorColumn--;
                            Console.SetCursorPosition(cursorColumn - 1, cursorLine - 1);
                            Console.Write(" ");
                            Console.SetCursorPosition(cursorColumn - 1, cursorLine - 1);
                            DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                        }
                        else if (cursorLine > 1 && lines.Length > 1)
                        {
                            cursorColumn = lines[cursorLine - 2].Length + 1;
                            cursorLine--;
                            buffer = new StringBuilder(string.Join("\n", lines[..^1]));
                            RedrawLines(buffer.ToString());
                            DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                        }
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (cursorLine != Console.WindowHeight - 1)
                        {
                            buffer.Append("\n");
                            cursorLine++;
                            cursorColumn = 1;
                            RedrawLines(buffer.ToString());
                            DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                        }
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        int position = GetBufferIndex(buffer.ToString(), cursorLine, cursorColumn);
                        if (position >= buffer.Length)
                        {
                            buffer.Append(key.KeyChar);
                        }
                        else
                        {
                            buffer.Insert(position, key.KeyChar);
                        }

                        Console.SetCursorPosition(cursorColumn - 1, cursorLine - 1);
                        Console.Write(key.KeyChar);
                        cursorColumn++;
                        DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.Write(":" + new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(1, Console.WindowHeight - 1);
                    char command = Console.ReadKey().KeyChar;
                    wasJustInCommand = true;

                    switch (command)
                    {
                        case 'q':
                            Console.Clear();
                            return;

                        case 'x':
                            Console.Clear();
                            return;

                        case 'w':
                            vfs.WriteToFile(fileName, buffer.ToString());
                            break;

                        case 's':
                            vfs.WriteToFile(fileName, buffer.ToString());
                            Console.Clear();
                            return;

                        case 'i':
                            inInsertMode = true;
                            DrawStatusBar(inInsertMode, cursorLine, cursorColumn);
                            FlushInputBuffer();
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static int GetBufferIndex(string buffer, int line, int column)
        {
            string[] lines = buffer.Split('\n');
            int index = 0;

            for (int i = 0; i < line - 1; i++)
            {
                index += lines[i].Length + 1;
            }

            return index + column - 1;
        }

        private static void RedrawLines(string content)
        {
            Console.SetCursorPosition(0, 0);
            string[] lines = content.Split('\n');
            int maxLines = Console.WindowHeight - 1;

            for (int i = 0; i < maxLines; i++)
            {
                if (i < lines.Length)
                {
                    Console.Write(lines[i].PadRight(Console.WindowWidth));
                }
                else
                {
                    Console.Write(ConsoleFormatting.FormatString("{LBLUE}~{RESET}").PadRight(Console.WindowWidth + 10));
                }
            }

            DrawStatusBar(false, 1, 1);
        }

        private static void DrawStatusBar(bool inInsertMode, int cursorLine, int cursorColumn, bool wasJustConsole = false)
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight - 1;

            Console.SetCursorPosition(0, consoleHeight);
            Console.Write(new string(' ', consoleWidth));
            Console.SetCursorPosition(0, consoleHeight);

            Console.Write(inInsertMode ? "-- INSERT --" : "");
            Console.SetCursorPosition(consoleWidth - 15, consoleHeight);
            Console.Write($"{cursorLine},{cursorColumn}  All");
        }

        private static void FlushInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
        }
    }
}

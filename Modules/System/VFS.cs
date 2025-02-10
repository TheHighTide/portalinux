using PortaLinuxVM.Modules.HighTide;

namespace PortaLinuxVM.Modules.System
{
    public class VFS
    {
        public class VFile
        {
            public string Name { get; set; }
            public string Content { get; set; }

            public VFile(string name, string content = "")
            {
                Name = name;
                Content = content;
            }
        }

        public class VDirectory
        {
            public string Name { get; }
            public Dictionary<string, VDirectory> Subdirectories { get; } = new();
            public Dictionary<string, VFile> Files { get; } = new();
            public VDirectory Parent { get; }

            public string FullPath
            {
                get
                {
                    if (Parent == null) return "~";
                    return Parent.FullPath + "/" + Name;
                }
            }

            public VDirectory(string name, VDirectory parent = null)
            {
                Name = name;
                Parent = parent;
            }
        }

        public VDirectory Root { get; }
        public VDirectory CurrentDirectory { get; private set; }

        public VFS()
        {
            Root = new VDirectory("~");
            CurrentDirectory = Root;

            CreateFile(Root, "changelog.txt", "====------------------------------------====\r\nChangelogs for the PortaLinux VM Application\r\n====------------------------------------====\r\n\r\n_____________________________Version 0.1.0__\r\nAdded:\r\n  - Added the 'echo` command\r\n  - Added the 'ls' command\r\n  - Added the 'cd' command\r\n  - Added the 'cat' command\r\n  - Added a help menu for the 'cat' command\r\n\r\nExtra Notes: \r\n  - Yes, I know how much stuff there still is\r\n    to add in order to make this application\r\n    perfectly replicate linux, but, theres\r\n    also a lot more commands and features I\r\n    have to add as well. The biggest feature\r\n    I still have to add is a 'virtual file\r\n    system'. It might not seem or sound like\r\n    much but if you want to create, delete\r\n    and edit files, you're going to need one.\r\n\r\n_____________________________Version 0.2.0__\r\nAdded:\r\n  - Added the 'neofetch' command\r\n  - Added the 'mkdir' command\r\n  - Added the --help argument for the\r\n    'mkdir' command\r\n  - Added the --help argument for the 'ls'\r\n    command\r\n  - Added the --help argument for the 'cd'\r\n    command\r\n  - Added the ability to create custom\r\n    directories and files\r\n  - Added the ability for the echo command\r\n    to create files\r\n  - Added the --help argument for the\r\n    'neofetch' command\r\n  - Added the --help argument for the\r\n    'shutdown' command\r\n\r\nChanged:\r\n  - Changed the way text is formatted to be\r\n    easier on RAM\r\n  - Changed the way commands and command\r\n    arguments are handled as it was\r\n    extremely inefficient\r\n\r\nFixed:\r\n  - Fixed a bug that caused '\\u0003' to be\r\n    displayed when pressing CTRL+V keys\r\n    together");
        }

        public bool CreateDirectory(string name)
        {
            if (CurrentDirectory.Subdirectories.ContainsKey(name))
                return false;
            CurrentDirectory.Subdirectories[name] = new VDirectory(name, CurrentDirectory);
            return true;
        }

        public VFile CreateFile(VDirectory directory, string name, string content = "")
        {
            var newFile = new VFile(name, content);
            directory.Files[name] = newFile;
            return newFile;
        }

        public bool ChangeDirectory(string path)
        {
            if (path == "~")
            {
                CurrentDirectory = Root;
                return true;
            }
            if (path == ".." && CurrentDirectory.Parent != null)
            {
                CurrentDirectory = CurrentDirectory.Parent;
                return true;
            }
            if (path == String.Empty || path == "")
            {
                CurrentDirectory = Root;
                return true;
            }
            if (CurrentDirectory.Subdirectories.ContainsKey(path))
            {
                CurrentDirectory = CurrentDirectory.Subdirectories[path];
                return true;
            }
            return false;
        }

        public string ListDirectory()
        {
            var dirs = CurrentDirectory.Subdirectories.Keys.Select(d => ConsoleFormatting.FormatString($"{{LBLUE}}{d}{{RESET}}"));
            var files = CurrentDirectory.Files.Keys;
            return string.Join("  ", dirs.Concat(files));
        }

        public string ReadFile(string fileName)
        {
            return CurrentDirectory.Files.ContainsKey(fileName)
                ? CurrentDirectory.Files[fileName].Content
                : $"cat: {fileName}: No such file or directory";
        }

        public void WriteToFile(string fileName, string content, bool append = false)
        {
            if (CurrentDirectory.Files.ContainsKey(fileName) && append)
            {
                CurrentDirectory.Files[fileName].Content += "\n" + content;
            }
            else
            {
                CurrentDirectory.Files[fileName] = new VFile(fileName, content);
            }
        }
    }
}

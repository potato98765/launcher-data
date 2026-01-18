using System;
using System.IO;
using IWshRuntimeLibrary;

namespace ShortcutCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataPath = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo rootDir = Directory.GetParent(dataPath.TrimEnd(Path.DirectorySeparatorChar));
            if (rootDir == null) return;

            string rootPath = rootDir.FullName;
            string prismPath = Path.Combine(rootPath, "launcher", "prismlauncher.exe");
            string iconPath = Path.Combine(rootPath, "data", "icon.ico");
            string instanceName = "Winter Server Modpack";

            string desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Winter Server Modpack.lnk");

            try
            {
                if (!System.IO.File.Exists(prismPath))
                {
                    Console.WriteLine("[X] PrismLauncher.exe not found!");
                    return;
                }

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(desktopPath);

                shortcut.TargetPath = prismPath;

                shortcut.Arguments = $"--launch \"{instanceName}\"";

                shortcut.WorkingDirectory = Path.GetDirectoryName(prismPath);

                if (System.IO.File.Exists(iconPath))
                {
                    shortcut.IconLocation = iconPath;
                }

                shortcut.Save();
                Console.WriteLine("[!] Shortcut created");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[X] Failure: " + ex.Message);
            }
        }
    }
}
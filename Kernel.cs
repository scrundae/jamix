using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace rootrain
{
    public class Kernel : Sys.Kernel
    {
        public string dir = @"None";

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("Welcome to Jamix");
            Console.WriteLine("To use the filesystem, type startfs");
            Console.WriteLine("For help, type help");
            Console.WriteLine("\n");
            
        }
        int syslt;
        protected override void Run()
        {
            while (true)
            {
                Console.Write(dir + "> ");
                string ans = Console.ReadLine();
                if (ans == "dir") {
                    string[] filePaths = Directory.GetFiles(dir);
                    var drive = new DriveInfo("0");
                    Console.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
                    Console.WriteLine("Directory of " + dir);
                    Console.WriteLine("\n");
                    for (int i = 0; i < filePaths.Length; ++i)
                    {
                        string path = filePaths[i];
                        Console.WriteLine(System.IO.Path.GetFileName(path));
                    }
                    foreach (var d in System.IO.Directory.GetDirectories(dir))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Console.WriteLine(dirName + " <DIR>");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                    Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                }
                else if (ans.Contains("del ")) {
                    string nans = ans.Replace("del ", "");
                    try {
                        File.Delete(dir + nans);
                    }
                    catch {
                        Console.WriteLine("Failed to erase file! (Make sure that you aren't deleting a directory!)");
                    }
                }
                else if (ans.Contains("rmdir ")) {
                    string nans = ans.Replace("rmdir ", "");
                    try {
                        Directory.Delete(dir + nans);
                    }
                    catch {
                        Console.WriteLine("Failed to erase directory! (Make sure that you aren't deleting a file!)");
                    }
                }
                else if (ans == "startfs") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("At the moment, the file system framework used for this operating system is HIGHLY unstable and can cause irreversable damage to drives. Proceed? (Y for Yes, N for No)");
                    Console.ForegroundColor = ConsoleColor.White;
                    char nans = Console.ReadKey(true).KeyChar;
                    if (nans == 'y') {
                        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
                        Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                        dir = @"0:\";
                    }
                    else {
                        
                    }
                }
                else if (ans == "cls") {
                    Console.Clear();
                }
                else if (ans.Contains("make ")) {
                    string nans = ans.Replace("make ", "");
                    File.Create(dir + nans);
                }
                else if (ans == "ed") {
                    Console.Clear();
                    Console.WriteLine("JAMIX EDITOR");
                    Console.WriteLine("Current directory: " + dir);
                    Console.Write("File name> ");
                    string filen = Console.ReadLine();
                    Console.WriteLine("Start Editing:");
                    string tex = Console.ReadLine();
                    File.WriteAllText(dir + filen, tex);
                }
                else if (ans.Contains("lockup ")) {
                    string nans = ans.Replace("lockup ", "");
                    Console.Clear();
                    Console.WriteLine("THIS SYSTEM HAS BEEN LOCKED");
                    Console.Write("Enter the unlock code> ");
                    while (true) {
                        string nnans = Console.ReadLine();
                        if (nnans == nans) {
                            syslt = 0;
                            break;
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            syslt += 60;
                            Console.WriteLine("Wrong code! Pausing system for " + syslt + "s");
                            System.Threading.Thread.Sleep(syslt * 1000);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
                else if (ans.Contains("cd ")) {
                    string nans = ans.Replace("cd ", "");
                    string path = dir + nans;
                    if (Directory.Exists(path)) {
                        dir = path;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown directory!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command error!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

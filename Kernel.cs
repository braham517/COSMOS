using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace minios
{
    public class Kernel : Sys.Kernel
    {
        string version = "2019-03-19";
        string pass = "";
        string error = "Unknown command. For a complete list of commands use help."; //default case of switch case-unknown command
        bool useruno = true;
        public bool FSinit = false;
        string current_path = @"0:\"; //root 

        public bool SudoY = false;  
        public string username = "User";
        public static string file;
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        filesystem: //initialize filesystem
            Console.Write("Do you want to enable the file system  (Y/N)?");
            var filesys = Console.ReadLine();
            if (filesys == "Y" || filesys == "y")
            {
                FSinit = true;
                Console.WriteLine("File System Will Be Initialized!");
                var fs = new Sys.FileSystem.CosmosVFS(); //in built function to make a new file system
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            }
            else if (filesys == "N" || filesys == "n")
            {
                Console.WriteLine("File System Will NOT Be Initialized!");
            }
            else
            {
                goto filesystem;
            }
            try
            {
                file_system.createDir("0:\\System86");  //initial directory creation
                file_system.createDir("0:\\User");
                file_system.createDir("0:\\User\\Documents");
                Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.US_Standard()); //setting keyboard input layout to US standard
            }
            catch (Exception exc)
            {
                goto fatto;
            }
        fatto:
            Console.Clear();
            logo.Logo(version);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                                            ");
            Console.WriteLine("                             Successfully booted                            ");
            Console.WriteLine("                                                                            ");
        inizia:
            Console.Write(username + " do you want to start?(Y/N)");
            var sino = Console.ReadLine();
            if (sino == "Y" || sino == "y")
            {
                Console.Clear();
                Console.Write("Welcome to IOTA OS!!!!\n");
            }
            else if (sino == "N" || sino == "n")
            {
                Stop();
            }
            else
            {
                goto inizia;
            }
        }


        protected override void Run()
        {
           
            Console.Write(current_path + "@root: ");
            var input = Console.ReadLine();
            var co = input;
            var vars = "";
            if (input.ToLower().IndexOf('/') != -1)
            {

                string[] parts = input.Split('/');
                co = parts[0];
                vars = parts[1];
            }
            try
            {
                switch (co)
                {

                    case "reboot":    //Reboots the machine
                        Cosmos.System.Power.Reboot();
                        break;

                    case "shutdown":   //Shuts down the machine
                        if (useruno)
                        {
                            Console.WriteLine("now you can power off your system");
                            Stop();
                        }
                        else
                        {
                            Console.WriteLine("now you can power off your system");
                            Stop();
                        }
                        break;

                    case "clear":   //Clears the screen
                        Console.Clear();
                        break;

                    case "help":  //All the commands
                        Console.WriteLine("Help 1: Normal Commands");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Reboot = reboot");
                        Console.WriteLine("Shutdown = shutdown");
                        Console.WriteLine("Clear = clear");
                        Console.WriteLine("About IOTA OS = about");
                        Console.WriteLine("Lock = lock");
                        Console.WriteLine("Print something on screen = print/things to print");
                        Console.WriteLine("Become user with sudo privilges = sudo");
                        Console.WriteLine("Help page 2 (FileSystem) = help2");
                        Console.WriteLine("Help page 3 (Calculator) = help3");
                        break;
                    case "help2":
                        Console.WriteLine("Help 2: FileSystem");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Go to specified directory = cd/directory");
                        Console.WriteLine("Create directory = md/new directory's name");
                        Console.WriteLine("Show current directories = dir");
                        Console.WriteLine("Use basic text editor/ASII art = text_editor");
                        Console.WriteLine("Deletes the specified directory[sudo] = dd/directory*");
                        Console.WriteLine("                                ");
                        Console.WriteLine("*type helpdir to know what directories not to delete");
                        break;
                    case "help3":
                        Console.WriteLine("Help 3: Calculator*");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Add two numbers together = add/num1#num2");
                        Console.WriteLine("Subtract a number to an other = subtract/num1#num2");
                        Console.WriteLine("Muliply two numbers together = multiply/num1#num2");
                        Console.WriteLine("Divide one number with another number = divide/num1#num2");
                        Console.WriteLine("One nuber to the power of another = power/num1#num2");
                        Console.WriteLine("Least Common Number of two numbers = lcm/num1#num2");
                        Console.WriteLine("Greatest Common Factor of two numbers = gcf/num1#num2");
                        Console.WriteLine("                                ");
                        Console.WriteLine("*it not works with decimals(0.1 for example)");
                        break;
                    case "help4":
                        Console.WriteLine("Help 4 : Miscellaneous");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("                                ");
                        Console.WriteLine("Play the game snake=run snake");
                        break;
                    case "helpdir":
                        Console.WriteLine("Do not delete the directories TEST, Testing and 0 because");
                        Console.WriteLine("they are system's directoryes and deleting them will cause");
                        Console.WriteLine("the Blue Screen of Error");
                        break; //HAIL BSOD .. guide to initiate BSOD ^_^ ^_^

                    case "lock":
                        Console.Write("Set Passcode: "); //user authentication
                        pass = Console.ReadLine();
                        sys_lock.lockpass(pass);
                        break;

                    case "print":   //Prints something
                        Console.WriteLine(vars);
                        break;

                    case "about":  //Some information
                        Console.WriteLine("IOTA OS Build:1.1.0");
                        break;
                        // ver command
                    case "cd":  //Changes current directory 
                        if (FSinit)
                        {
                            current_path = current_path + vars;
                        }
                        else
                        {
                            Console.WriteLine("File System Not Enabled!");
                        }
                        break;

                    case "md":  // Makes new directory
                        if (FSinit)

                        {
                            file_system.createDir(current_path + vars);
                        }
                        else
                        {
                            Console.WriteLine("File System Not Enabled!");
                        }
                        break;

                    case "dir": // Displays current location
                        if (FSinit)
                        {
                            string[] back = file_system.readFiles(current_path);
                            string[] front = file_system.readDirectories(current_path);
                            string[] combined = new string[front.Length + back.Length];
                            Array.Copy(front, combined, front.Length);
                            Array.Copy(back, 0, combined, front.Length, back.Length);
                            foreach (var item in combined)
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("File System Not Enabled!");
                        }
                        break;


                    case "add": // Adds given numbers
                        string[] inputvarsa = vars.Split('#');
                        Console.WriteLine(Calci.Add(inputvarsa[0], inputvarsa[1]));
                        break;

                    case "subtract": // Subtracts given numbers
                        string[] inputvarsb = vars.Split('#');
                        Console.WriteLine(Calci.Subtract(inputvarsb[0], inputvarsb[1]));
                        break;

                    case "multiply": // Multiplys given numbers
                        string[] inputvarsc = vars.Split('#');
                        Console.WriteLine(Calci.Multiply(inputvarsc[0], inputvarsc[1]));
                        break;

                    case "divide": // Divides given numbers
                        string[] inputvarsd = vars.Split('#');
                        Console.WriteLine(Calci.Divide(inputvarsd[0], inputvarsd[1]));
                        break;

                    case "power": // Raises given number to other given number
                        string[] inputvarse = vars.Split('#');
                        Console.WriteLine(Calci.ToPower(inputvarse[0], inputvarse[1]));
                        break;

                    case "gcd": // Gives gcd conversion of given numbers
                        string[] inputvarsf = vars.Split('#');
                        Console.WriteLine(Calci.GcdCon(inputvarsf[0], inputvarsf[1]));
                        break;

                    case "lcm": // Gives lcm conversion of given numbers
                        string[] inputvarsg = vars.Split('#');
                        Console.WriteLine(Calci.LcmCon(inputvarsg[0], inputvarsg[1]));
                        break;

                    case "text_editor": //text_editor
                        Console.Clear();
                        text_editor.init();
                        break;

                    case "open file": //open a file
                        Console.Clear();
                        Kernel.file = Console.ReadLine();
                        if (File.Exists(@"0:\" + Kernel.file))
                        {
                            string[] contents;
                            contents = file_system.readFile(file);
                            Console.Write(contents);
                            Console.ReadKey();
                        }
                        else if (!File.Exists(@"0:\" + Kernel.file))
                        {
                            Console.Write("No such file exists!");
                        }
                        
                        break;


                    //case "BASIC": working on basic-style programming
                    //Console.Clear();
                    //Basic.init();
                    //break;

                    case "sudo": //Become sudo user
                        Console.Write("Are you sure to become a sudo user?(Y/N)");
                        var sicuro = Console.ReadLine();
                        if (sicuro == "Y" || sicuro == "y")
                        {
                            SudoY = true;
                        }
                        else
                        {
                            SudoY = false;
                        }
                        break;

                    case "dd": //delete directory
                        if (SudoY)
                        {
                            file_system.deleteDir(current_path + vars);
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, you aren't a sudo user");
                        }
                        break;

                    case "run snake":  //game implementation
                        Snake snk = new Snake();
                        snk.Run();
                        break;

                    default:
                        Console.WriteLine(error);
                        break;
                }
            }
            catch (Exception e) //BlueScreenOfDeath-like thing I wanted to make noerror false but it bugs
            {

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                spegni:
                Console.Write("   Do you want to reboot or shutdown?(R/S)");
                var risp = Console.ReadLine();
                if (risp == "R" || risp == "r")
                {
                    Sys.Power.Reboot();
                }
                else if (risp == "S" || risp == "s")
                {
                    Stop();
                }
                else
                {
                    goto spegni;
                }
            }
        }
    }
}

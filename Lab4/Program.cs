using System;
using System.IO;
using Lab4ClassLibrary;
using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;

namespace Lab4App
{
    [Command(Name = "Lab4App", Description = "Консольний додаток для виконання лабораторних завдань")]
    [Subcommand(typeof(InfoCommand), typeof(ExecuteCommand), typeof(ConfigurePathCommand))]
    class EntryPoint
    {
        public static int Main(string[] args)
        {
            var application = new CommandLineApplication<EntryPoint>();
            application.Conventions.UseDefaultConventions();

            try
            {
                return application.Execute(args);
            }
            catch (CommandParsingException)
            {
                ShowHelp();
                return 0;
            }
        }

        private int OnExecute(CommandLineApplication app)
        {
            ShowHelp();
            return 0;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Доступні команди:");
            Console.WriteLine("  info        - Показує інформацію про додаток");
            Console.WriteLine("  run         - Запускає обране лабораторне завдання");
            Console.WriteLine("                Приклади використання:");
            Console.WriteLine("                  Lab4App run lab1 -i input.txt -o output.txt");
            Console.WriteLine("                  Lab4App run lab2 --input=input.txt --output=output.txt");
            Console.WriteLine("  configure   - Встановлює шлях до папки для вхідних та вихідних файлів");
            Console.WriteLine("                Приклад використання:");
            Console.WriteLine("                  Lab4App configure -p /path/to/folder");
            Console.WriteLine("  help        - Виводить цю інформацію");
        }

        [Command("info", Description = "Показує інформацію про додаток")]
        class InfoCommand
        {
            private int OnExecute()
            {
                Console.WriteLine("Developer: Pishta Ihor");
                Console.WriteLine("Version: 1.0.0");
                return 0;
            }
        }

        [Command("run", Description = "Запускає обране лабораторне завдання")]
        class ExecuteCommand
        {
            [Argument(0, "task", "Лабораторна задача для виконання (lab1, lab2, lab3)")]
            public string Task { get; set; }

            [Option("-i|--input", "Вхідний файл", CommandOptionType.SingleValue)]
            public string InputFilePath { get; set; }

            [Option("-o|--output", "Вихідний файл", CommandOptionType.SingleValue)]
            public string OutputFilePath { get; set; }

            private int OnExecute()
            {
                string inputFilePath = InputFilePath ?? Path.Combine(Environment.GetEnvironmentVariable("TASK_PATH") ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "INPUT.txt");
                string outputFilePath = OutputFilePath ?? Path.Combine(Environment.GetEnvironmentVariable("TASK_PATH") ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OUTPUT.txt");

                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine($"Файл {inputFilePath} не знайдено.");
                    return 1;
                }

                switch (Task?.ToLower())
                {
                    case "lab1":
                        Lab1Lib.RunLab1(inputFilePath, outputFilePath);
                        break;
                    case "lab2":
                        Lab2Lib.RunLab2(inputFilePath, outputFilePath);
                        break;
                    case "lab3":
                        Lab3Lib.RunLab3(inputFilePath, outputFilePath);
                        break;
                    default:
                        Console.WriteLine("Невірне завдання. Вкажіть lab1, lab2 або lab3.");
                        return 1;
                }

                Console.WriteLine($"Завдання виконано. Результат записано у {outputFilePath}");
                return 0;
            }
        }

        [Command("configure", Description = "Встановлює шлях до папки для вхідних та вихідних файлів")]
        class ConfigurePathCommand
        {
            [Option("-p|--path", "Шлях до папки", CommandOptionType.SingleValue)]
            public string DirectoryPath { get; set; }

            private int OnExecute()
            {
                if (string.IsNullOrEmpty(DirectoryPath))
                {
                    Console.WriteLine("Шлях не вказано.");
                    return 1;
                }

                try
                {
                    SetEnvironmentVariable("TASK_PATH", DirectoryPath);
                    Console.WriteLine($"Змінна TASK_PATH встановлена на: {DirectoryPath}");
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка встановлення змінної середовища: {ex.Message}");
                    return 1;
                }
            }

            private void SetEnvironmentVariable(string variable, string value)
            {
                if (OperatingSystem.IsWindows())
                {
                    Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.User);
                }
                else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    string configFile = OperatingSystem.IsLinux() ? "/etc/environment" : "/etc/paths";

                    if (File.Exists(configFile))
                    {
                        File.AppendAllText(configFile, $"{variable}={value}\n");
                    }
                    else
                    {
                        Console.WriteLine("Системний файл для змінних середовища не знайдено.");
                        throw new InvalidOperationException("Неможливо встановити змінну середовища.");
                    }
                }
            }
        }
    }

}

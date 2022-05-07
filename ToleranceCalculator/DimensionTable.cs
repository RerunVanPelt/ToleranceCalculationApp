using System;
using System.Collections.Generic;
using System.IO;

namespace ToleranceCalculator
{
    public static class DimensionTable
    {
        public static List<DimensionModel> DimensionModels { get; set; } = new List<DimensionModel>();
        public static double LowerLimitClosing { get; set; }
        public static double MaxDimensionClosing { get; set; }
        public static double MinDimensionClosing { get; set; }
        public static double NominalClosing { get; set; }
        public static double UpperLimitClosing { get; set; }

        public static void AddDimensionToTable()
        {
            Console.WriteLine($"\nDimension [{DimensionModels.Count + 1}]:");

            DimensionModels.Add(new DimensionModel()
            {
                Nominal = AddCheckedDimension("Nominal"),
                UpperLimit = AddCheckedDimension("Upper Limit"),
                LowerLimit = AddCheckedDimension("Lower Limit"),
            });
        }

        public static void FixTable()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nFix Dimension");
            Console.ForegroundColor = ConsoleColor.White;

            if (DimensionModels.Count == 0)
            {
                Console.WriteLine("No dimension to fix");
                Console.ReadKey(true);
            }
            else
            {
                do
                {
                    Console.Write($"Dimension to fix [1 - {DimensionModels.Count}]: ");

                    if (int.TryParse(Console.ReadLine(), out int dimPos))
                    {
                        if (dimPos >= 1 && dimPos <= DimensionModels.Count)
                        {
                            DimensionModels[dimPos - 1] = new DimensionModel()
                            {
                                Nominal = AddCheckedDimension("Nominal"),
                                UpperLimit = AddCheckedDimension("Upper Limit"),
                                LowerLimit = AddCheckedDimension("Lower Limit"),
                            };
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Wrong position. Press key to continue.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                        continue;
                    }
                } while (true);
            }
        }

        public static void LoadToleranceTable()
        {
            string filePath = AppContext.BaseDirectory;
            string[] tableFiles = Directory.GetFiles(filePath, "*.ttb");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nLoad  Table");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nSaved tables:");
            for (int i = 0; i < tableFiles.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {Path.GetFileNameWithoutExtension(tableFiles[i])}");
            }

            Console.WriteLine("Enter file name: ");
            string fileName = Console.ReadLine();
            filePath += fileName + ".ttb";

            if (File.Exists(filePath))
            {
                DimensionModels = StorageTable.LoadTable(filePath, typeof(List<DimensionModel>)) as List<DimensionModel>;
            }
        }

        public static void PrintTable()
        {
            string firstRow = $"{"[+/-]",-15}{"Nominal",-20}{"Upper Limit",-20}{"Lower Limit",-20}{"Max Dimension",-20}{"Min Dimension",-20}";
            string tableLine = new string('-', firstRow.Length);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(tableLine);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{"[+/-]",-15}{"Nominal",-20}{"Upper Limit",-20}{"Lower Limit",-20}{"Max Dimension",-20}{"Min Dimension",-20}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(tableLine);

            Console.ForegroundColor = ConsoleColor.White;

            foreach (DimensionModel dimension in DimensionModels)
            {
                Console.WriteLine($"{(char)dimension.Direction,-15}{dimension.Nominal,-20:F2}{dimension.UpperLimit,-20:F2}{dimension.LowerLimit,-20:F2}" +
                    $"{dimension.MaxDimension,-20:F2}{dimension.MinDimension,-20:F2}");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(tableLine);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CalculateTable());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SaveToleranceTable()
        {
            string filePath = AppContext.BaseDirectory;
            string[] tableFiles = Directory.GetFiles(filePath, "*.ttb");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSave Table");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nSaved tables:");
            for (int i = 0; i < tableFiles.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {Path.GetFileNameWithoutExtension(tableFiles[i])}");
            }

            Console.Write("Enter file name: ");
            string fileName = Console.ReadLine();

            StorageTable.SaveTable(fileName);
        }

        private static double AddCheckedDimension(string propertyName)
        {
            do
            {
                Console.Write($"{propertyName}: ");

                if (double.TryParse(Console.ReadLine(), out double input))
                {
                    return input;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            } while (true);
        }

        private static string CalculateTable()
        {
            double nominalClosing = 0;
            double upperLimitClosing = 0;
            double lowerLimitClosing = 0;

            foreach (DimensionModel dimension in DimensionModels)
            {
                switch (dimension.Direction)
                {
                    case StackDirection.plus:
                        nominalClosing += dimension.Nominal;
                        upperLimitClosing += dimension.UpperLimit;
                        lowerLimitClosing += dimension.LowerLimit;
                        break;

                    case StackDirection.minus:
                        nominalClosing -= dimension.Nominal;
                        upperLimitClosing += -1 * (dimension.LowerLimit);
                        lowerLimitClosing += -1 * (dimension.UpperLimit);
                        break;
                }
            }

            NominalClosing = nominalClosing;
            UpperLimitClosing = upperLimitClosing;
            LowerLimitClosing = lowerLimitClosing;
            MaxDimensionClosing = NominalClosing + UpperLimitClosing;
            MinDimensionClosing = NominalClosing + LowerLimitClosing;

            return $"{"",-15}{NominalClosing,-20:F2}{UpperLimitClosing,-20:F2}{LowerLimitClosing,-20:F2}" +
                    $"{MaxDimensionClosing,-20:F2}{MinDimensionClosing,-20:F2}";
        }
    }
}

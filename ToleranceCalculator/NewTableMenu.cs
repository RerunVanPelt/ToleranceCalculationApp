using System;

namespace ToleranceCalculator
{
    public class NewTableMenu : Menu
    {
        public override void Choice()

        {
            do
            {
                char choice = Console.ReadKey(true).KeyChar;

                switch (choice)
                {
                    case '1':
                        // TODO: Code for add dimension
                        DimensionTable.AddDimensionToTable();
                        _ = new NewTableMenu();
                        break;

                    case '2':
                        DimensionTable.FixTable();
                        _ = new NewTableMenu();
                        break;

                    case '3':
                        // TODO: Code for save table
                        DimensionTable.SaveToleranceTable();
                        _ = new NewTableMenu();
                        break;

                    case '4':
                        _ = new StartMenu();
                        break;
                }
            } while (true);
        }

        public override void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("New table");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            DimensionTable.PrintTable();
            Console.WriteLine();

            Console.WriteLine("1.Ad dimension");
            Console.WriteLine("2.Fix dimension");
            Console.WriteLine("3.Save table");
            Console.WriteLine("4.Back to start menu");

            Choice();
        }
    }
}

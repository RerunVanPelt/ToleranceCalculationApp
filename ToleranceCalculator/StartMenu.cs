using System;

namespace ToleranceCalculator
{
    public class StartMenu : Menu
    {
        public override void Choice()
        {
            do
            {
                char choice = Console.ReadKey(true).KeyChar;

                switch (choice)
                {
                    case '1':
                        DimensionTable.DimensionModels.Clear();
                        _ = new NewTableMenu();
                        break;

                    case '2':
                        DimensionTable.LoadToleranceTable();
                        _ = new NewTableMenu();
                        break;

                    case '3':
                        // TODO: Terminate application
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }

        public override void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tolerance calculator");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            Console.WriteLine("1.Create new table");
            Console.WriteLine("2.Load table");
            Console.WriteLine("3.Exit application");

            Choice();
        }
    }
}

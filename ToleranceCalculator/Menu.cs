namespace ToleranceCalculator
{
    public abstract class Menu
    {
        public abstract void ShowMenu();

        public abstract void Choice();

        public Menu()
        {
            ShowMenu();
        }
    }
}

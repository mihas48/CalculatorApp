using Microsoft.Extensions.DependencyInjection;

namespace CalculatorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell())
            {
                Width = 500,
                Height = 700,
                MinimumHeight = 550,
                MinimumWidth = 330
            };
        }
    }
}
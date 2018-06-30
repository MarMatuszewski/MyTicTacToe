using MyTicTacToe.StartUp;
using MyTicTacToe.ViewModels;
using MyTicTacToe.Views;
using Ninject;
using System.Windows;

namespace MyTicTacToe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _kernel;

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            _kernel = new StandardKernel();
            
            _kernel.Load( new Bootstrapper() );

            var mainWindow = new MainWindow
            {
                DataContext = _kernel.Get<MainWindowViewModel>()
            };

            mainWindow.Show();
        }
    }
}

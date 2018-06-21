using MyTicTacToe.Interfaces;
using MyTicTacToe.Models;
using MyTicTacToe.ViewModels;
using MyTicTacToe.Views;
using Ninject.Modules;

namespace MyTicTacToe.StartUp
{
    public class Bootstrapper : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow>()
                .ToSelf().InTransientScope();

            Bind<MainWindowViewModel>()
                .ToSelf().InTransientScope();

            Bind<IGame>()
                .To<Game>().InSingletonScope();
        }
    }
}

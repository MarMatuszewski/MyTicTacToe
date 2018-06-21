using MyTicTacToe.Models;

namespace MyTicTacToe.Interfaces
{
    public interface IGame
    {
        bool IsGameInProgress { get; set; }

        int Draws { get; set; }

        void ExecuteDrawSign( object parameter );

        bool CanExecuteDrawSign( object parameter );

        void StartGame( Player playerOne, Player playerTwo );
    }
}

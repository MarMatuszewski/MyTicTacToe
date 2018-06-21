using MyTicTacToe.Models;
using MyTicTacToe.Commands;
using Prism.Mvvm;
using MyTicTacToe.Interfaces;

namespace MyTicTacToe.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private const string Nought = "o";
        private const string Cross = "x";

        private Player _playerOne;
        private Player _playerTwo;
        private IGame _game;

        public Player PlayerOne
        {
            get => _playerOne;
            set => _playerOne = value;
        }

        public Player PlayerTwo {
            get => _playerTwo;
            set => _playerTwo = value;
        }

        public IGame Game
        {
            get => _game;
            set => SetProperty( ref _game, value );
        }

        public bool IsMultiplayerSelected { get; set; }

        public RelayCommand StartGameCommand { get; private set; }
        public RelayCommand DrawSignCommand { get; private set; }

        public MainWindowViewModel(
            IGame game )
        {
            _game = game;

            PlayerOne = new Player { Id = 1, PlayersSign = Cross };
            PlayerTwo = new Player { Id = 2, PlayersSign = Nought };

            StartGameCommand = new RelayCommand( ExecuteStartGame, CanExecuteStartGame );
            DrawSignCommand = new RelayCommand( ExecuteDrawSign, CanExecuteDrawSign );
        }


        private void ExecuteStartGame( object parameter )
        {
            if( !IsMultiplayerSelected ) 
            {
                PlayerTwo.Name = "Computer";
            }

            _game.StartGame(
                PlayerOne,
                PlayerTwo );
        }

        private bool CanExecuteStartGame( object parameter )
        {
            if( _game.IsGameInProgress ) 
            {
                return false;
            }
            else if( !IsMultiplayerSelected )
            {
                if( string.IsNullOrWhiteSpace( PlayerOne.Name ) )
                {
                    return false;
                }
            }
            else
            {
                if( string.IsNullOrWhiteSpace( PlayerOne.Name ) || string.IsNullOrWhiteSpace( PlayerTwo.Name ) ) 
                {
                    return false;
                }
            }
            return true;
        }

        private void ExecuteDrawSign( object parameter )
        {
            _game.ExecuteDrawSign( parameter );
        }

        private bool CanExecuteDrawSign( object parameter )
        {
            return _game.CanExecuteDrawSign( parameter );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicTacToe.Shared;
using MyTicTacToe.Models;
using System.Windows.Input;
using MyTicTacToe.Commands;
using System.Runtime.CompilerServices;

namespace MyTicTacToe.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private Player _playerOne;
        private Player _playerTwo;
        private Player _currentPlayer;
        private Game _currentGame;
        private string _grid00Sign;
        private string _grid01Sign;
        private string _grid02Sign;
        private string _grid10Sign;
        private string _grid11Sign;
        private string _grid12Sign;
        private string _grid20Sign;
        private string _grid21Sign;
        private string _grid22Sign;

        public Player PlayerOne
        {
            get => _playerOne;
            set => _playerOne = value;
        }

        public Player PlayerTwo {
            get => _playerTwo;
            set => _playerTwo = value;
        }

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set =>  _currentPlayer = value;
        }

        public Game CurrentGame
        {
            get => _currentGame;
            set => _currentGame = value;
        }

        public bool IsMultiplayerSelected { get; set; }

        public string Grid00Sign
        {
            get => _grid00Sign;
            set => SetProperty( ref _grid00Sign, value );
        }

        public string Grid01Sign
        {
            get => _grid01Sign;
            set => SetProperty( ref _grid01Sign, value );
        }

        public string Grid02Sign
        {
            get => _grid02Sign;
            set => SetProperty( ref _grid02Sign, value );
        }

        public string Grid10Sign
        {
            get => _grid10Sign;
            set => SetProperty( ref _grid10Sign, value );
        }

        public string Grid11Sign
        {
            get => _grid11Sign;
            set => SetProperty( ref _grid11Sign, value );
        }

        public string Grid12Sign
        {
            get => _grid12Sign;
            set => SetProperty( ref _grid12Sign, value );
        }

        public string Grid20Sign
        {
            get => _grid20Sign;
            set => SetProperty( ref _grid20Sign, value );
        }

        public string Grid21Sign
        {
            get => _grid21Sign;
            set => SetProperty( ref _grid21Sign, value );
        }

        public string Grid22Sign
        {
            get => _grid22Sign;
            set => SetProperty( ref _grid22Sign, value );
        }

        public RelayCommand StartGameCommand { get; private set; }
        public RelayCommand DrawSignCommand { get; private set; }

        public MainWindowViewModel()
        {
            PlayerOne = new Player { Id = 1, PlayersSign = Sign.Crosses };
            PlayerTwo = new Player { Id = 2, PlayersSign = Sign.Noughts };
            StartGameCommand = new RelayCommand( ExecuteStartGame, CanExecuteStartGame );
            DrawSignCommand = new RelayCommand( ExecuteDrawSign, CanExecuteDrawSign );
            _currentPlayer = PlayerOne;
            _currentGame = new Game();
        }


        private void ExecuteStartGame( object parameter )
        {
            if( !IsMultiplayerSelected ) 
            {
                PlayerTwo.Name = "Computer";
            }

            _currentGame = new Game(
                1,
                PlayerOne,
                PlayerTwo );
        }

        private bool CanExecuteStartGame( object parameter )
        {
            if( !IsMultiplayerSelected )
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
            var parameterName = parameter.ToString();
            var propertyName = $"{parameterName}Sign";


            if( CurrentPlayer.PlayersSign == Sign.Noughts )
            {
                var propertyInfo = this.GetType().GetProperty( propertyName );
                propertyInfo.SetValue( this, "o" );
            }
            else
            {
                var propertyInfo = this.GetType().GetProperty( propertyName );
                propertyInfo.SetValue( this, "x" );
            }

            changePlayer();
        }

        private void changePlayer()
        {
            if( CurrentPlayer.Id == 1 )
            {
                CurrentPlayer = PlayerTwo;
            }
            else
            {
                CurrentPlayer = PlayerOne;
            }

            RaisePropertyChangedEvent( "CurrentPlayer" );
        }

        private bool CanExecuteDrawSign( object parameter )
        {
            var parameterName = parameter.ToString();
            var propertyName = $"{parameterName}Sign";
            var propertyInfo = this.GetType().GetProperty( propertyName );
            if( propertyInfo.GetValue( this ) == null && CurrentGame.Id != 0 )
            {
                return true;
            }
            return false;
        }
    }
}

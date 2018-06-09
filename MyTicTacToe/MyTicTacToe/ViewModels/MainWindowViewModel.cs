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
using System.Reflection;

namespace MyTicTacToe.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private Player _playerOne;
        private Player _playerTwo;
        private Game _currentGame;
        private int _currentGameNumber = 0;
        
        private string _topLeftCorner;
        private string _leftEdge;
        private string _bottomLeftCorner;
        private string _topEdge;
        private string _center;
        private string _bottomEdge;
        private string _topRightCorner;
        private string _rightEdge;
        private string _bottomRightCorner;

        public Player PlayerOne
        {
            get => _playerOne;
            set => _playerOne = value;
        }

        public Player PlayerTwo {
            get => _playerTwo;
            set => _playerTwo = value;
        }

        public Game CurrentGame
        {
            get => _currentGame;
            set => SetProperty( ref _currentGame, value );
        }

        public bool IsMultiplayerSelected { get; set; }

        public string TopLeftCorner
        {
            get => _topLeftCorner;
            set => SetProperty( ref _topLeftCorner, value );
        }

        public string LeftEdge
        {
            get => _leftEdge;
            set => SetProperty( ref _leftEdge, value );
        }

        public string BottomLeftCorner
        {
            get => _bottomLeftCorner;
            set => SetProperty( ref _bottomLeftCorner, value );
        }

        public string TopEdge
        {
            get => _topEdge;
            set => SetProperty( ref _topEdge, value );
        }

        public string Center
        {
            get => _center;
            set => SetProperty( ref _center, value );
        }

        public string BottomEdge
        {
            get => _bottomEdge;
            set => SetProperty( ref _bottomEdge, value );
        }

        public string TopRightCorner
        {
            get => _topRightCorner;
            set => SetProperty( ref _topRightCorner, value );
        }

        public string RightEdge
        {
            get => _rightEdge;
            set => SetProperty( ref _rightEdge, value );
        }

        public string BottomRightCorner
        {
            get => _bottomRightCorner;
            set => SetProperty( ref _bottomRightCorner, value );
        }

        public RelayCommand StartGameCommand { get; private set; }
        public RelayCommand DrawSignCommand { get; private set; }

        public MainWindowViewModel()
        {
            PlayerOne = new Player { Id = 1, PlayersSign = Sign.Crosses };
            PlayerTwo = new Player { Id = 2, PlayersSign = Sign.Noughts };

            StartGameCommand = new RelayCommand( ExecuteStartGame, CanExecuteStartGame );
            DrawSignCommand = new RelayCommand( ExecuteDrawSign, CanExecuteDrawSign );
            
            CurrentGame = new Game();
        }


        private void ExecuteStartGame( object parameter )
        {
            if( !IsMultiplayerSelected ) 
            {
                PlayerTwo.Name = "Computer";
            }

            _currentGameNumber++;

            CurrentGame = new Game(
                _currentGameNumber,
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
            PropertyInfo propertyInfo;

            var parameterName = parameter.ToString();
            var propertyName = $"{parameterName}";


            if( CurrentGame.CurrentPlayer.PlayersSign == Sign.Noughts )
            {
                propertyInfo = this.GetType().GetProperty( propertyName );
                propertyInfo.SetValue( this, "o" );
            }
            else
            {
                propertyInfo = this.GetType().GetProperty( propertyName );
                propertyInfo.SetValue( this, "x" );
            }

            CurrentGame.ChangePlayer();
        }



        private bool CanExecuteDrawSign( object parameter )
        {
            var parameterName = parameter.ToString();
            var propertyName = $"{parameterName}";
            var propertyInfo = this.GetType().GetProperty( propertyName );
            if( propertyInfo.GetValue( this ) == null && CurrentGame.Id != 0 )
            {
                return true;
            }
            return false;
        }
    }
}

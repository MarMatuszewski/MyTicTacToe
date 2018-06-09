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
            CurrentGame.ExecuteDrawSign( parameter );
        }

        private bool CanExecuteDrawSign( object parameter )
        {
            return CurrentGame.CanExecuteDrawSign( parameter );
        }
    }
}

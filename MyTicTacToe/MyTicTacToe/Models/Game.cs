using MyTicTacToe.Shared;
using MyTicTacToe.Views;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTicTacToe.Models
{
    public class Game : BindableBase
    {
        private Player _playerOne;
        private Player _playerTwo;
        private Player _currentPlayer;

        private int _id;
        private int _numberOfMoves;

        private bool _gameInProgress;

        private string _topLeftCorner = string.Empty;
        private string _leftEdge = string.Empty;
        private string _bottomLeftCorner = string.Empty;
        private string _topEdge = string.Empty;
        private string _center = string.Empty;
        private string _bottomEdge = string.Empty;
        private string _topRightCorner = string.Empty;
        private string _rightEdge = string.Empty;
        private string _bottomRightCorner = string.Empty;

        private Dictionary<string, List<string>> possibleWinningLines;

        public int Id
        {
            get => _id;
            set => _id = value;
        }


        public int NumberOfMoves
        {
            get => _numberOfMoves;
            set =>  _numberOfMoves = value;
        }

        public bool IsGameInProgress
        {
            get => _gameInProgress;
            set => SetProperty( ref _gameInProgress, value );
        }

        public Player GamePlayerOne
        {
            get => _playerOne;
            set => _playerOne = value;
        }

        public Player GamePlayerTwo
        {
            get => _playerTwo;
            set => _playerTwo = value;
        }

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set => SetProperty( ref _currentPlayer, value );
        }

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

        public Game()
        {
        }

        public Game(
            int id,
            Player playerOne,
            Player playerTwo )
        {
            Id = id;
            GamePlayerOne = playerOne;
            GamePlayerTwo = playerTwo;
            CurrentPlayer = ChooseFirstPlayer();
            IsGameInProgress = true;

            possibleWinningLines = new Dictionary<string, List<string>>
            {
                { "LeftColumn", new List<string> { "TopLeftCorner", "LeftEdge", "BottomLeftCorner" } },
                { "CenterColumn", new List<string> { "TopEdge", "Center", "BottomEdge" } },
                { "RightColumn", new List<string> { "TopRightCorner", "RightEdge", "BottomRightCorner" } },
                { "TopRow", new List<string> { "TopLeftCorner", "TopEdge", "TopRightCorner" } },
                { "CenterRow", new List<string> { "LeftEdge", "Center", "RightEdge" } },
                { "BottomRow", new List<string> { "BottomLeftCorner", "BottomEdge", "BottomRightCorner" } },
                { "LeftToRightDiagonal", new List<string> { "TopLeftCorner", "Center", "BottomRightCorner" } },
                { "RightToLeftDiagonal", new List<string> { "TopRightCorner", "Center", "BottomLeftCorner" } }
            };
        }

        public Player ChooseFirstPlayer()
        {
            if( Id % 2 == 0)
            {
                return GamePlayerTwo;
            }

            return GamePlayerOne;
        }

        public void ChangePlayer()
        {
            if( CurrentPlayer.Id == 1 )
            {
                CurrentPlayer = GamePlayerTwo;
            }
            else
            {
                CurrentPlayer = GamePlayerOne;
            }
        }

        public void ExecuteDrawSign( object parameter )
        {
            NumberOfMoves++;

            var property = GetType().GetProperty( parameter.ToString() );
            property.SetValue( this, CurrentPlayer.PlayersSign );

            CheckForWinner();
        }

        public bool CanExecuteDrawSign( object parameter )
        {
            var property = GetType().GetProperty( parameter.ToString() );
            if( property.GetValue( this ).Equals( string.Empty ) && Id != 0 && IsGameInProgress )
            {
                return true;
            }
            return false;
        }

        public void CheckForWinner()
        {
            if( NumberOfMoves < 5 )
            {
                ChangePlayer();
                return;
            }

            foreach( var line in possibleWinningLines )
            {
                if( line.Value.All( 
                    prop => GetType().GetProperty( prop ).GetValue( this ).Equals( CurrentPlayer.PlayersSign ) ) )
                {
                    displayMessageAndEndGame( CurrentPlayer );
                    return;
                }
            }
          
            if( NumberOfMoves == 9 )
            {
                displayMessageAndEndGame();
                return;
            }
            ChangePlayer();
        }

        private void clearAllGridFields()
        {
            var properties = GetType().GetProperties( BindingFlags.Public | BindingFlags.Instance )
                                      .Where( prop => prop.PropertyType == typeof( string ) )
                                      .Select( prop => prop.Name )
                                      .ToArray();

            foreach( var prop in properties )
            {
                var property = GetType().GetProperty( prop );
                property.SetValue( this, string.Empty );
            }
        }

        private void displayMessageAndEndGame( Player winner = null )
        {
            var message = winner != null ? 
                $"Congratulation {winner.Name} won!!" 
                : "Draw!!";

            MessageBox.Show( message, "End of game", MessageBoxButton.OK );

            var increase = winner != null ? 
                CurrentPlayer.NumberOfWins++ 
                : GamePlayerOne.Draws++;
            
            IsGameInProgress = false;
            clearAllGridFields();
            CurrentPlayer = null;
        }
    }
}

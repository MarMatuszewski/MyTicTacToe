using MyTicTacToe.Interfaces;
using MyTicTacToe.Shared;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System;

namespace MyTicTacToe.Models
{
    public class Game : BindableBase, IGame
    {
        private Player _playerOne;
        private Player _playerTwo;
        private Player _currentPlayer;

        private int _id;
        private int _numberOfMoves;
        private int _draws;

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

        private Dictionary<string, List<string>> _possibleWinningLines;
        private List<string> _gameFields;
        private Stack<string> _takenFields;
        private List<string> _gameFieldsWithTheSameSign;
        private readonly IDisplayService _displayService;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int Draws
        {
            get => _draws;
            set => SetProperty( ref _draws, value );
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

        public Game( IDisplayService displayService )
        {
            _displayService = displayService;

            _gameFieldsWithTheSameSign = new List<string>();

            _possibleWinningLines = new Dictionary<string, List<string>>
            {
                { "LeftColumn", new List<string> { "TopLeftCorner", "LeftEdge", "BottomLeftCorner" } },
                { "CenterColumn", new List<string> { "TopEdge", "Center", "BottomEdge" } },
                { "RightColumn", new List<string> { "TopRightCorner", "RightEdge", "BottomRightCorner" } },
                { "TopRow", new List<string> { "TopLeftCorner", "TopEdge", "TopRightCorner" } },
                { "CenterRow", new List<string> { "LeftEdge", "Center", "RightEdge" } },
                { "BottomRow", new List<string> { "BottomLeftCorner", "BottomEdge", "BottomRightCorner" } },
                { "TopLeftToBottomRightDiagonal", new List<string> { "TopLeftCorner", "Center", "BottomRightCorner" } },
                { "TopRightToBottomLeftDiagonal", new List<string> { "TopRightCorner", "Center", "BottomLeftCorner" } }
            };
        }

        public void StartGame( Player playerOne, Player playerTwo )
        {
            _takenFields = new Stack<string>();

            _gameFields = new List<string>
            {
                "TopLeftCorner",
                "LeftEdge",
                "BottomLeftCorner",
                "TopEdge",
                "Center",
                "BottomEdge",
                "TopRightCorner",
                "RightEdge",
                "BottomRightCorner"
            };

            _id++;
            GamePlayerOne = playerOne;
            GamePlayerTwo = playerTwo;
            CurrentPlayer = chooseFirstPlayer();
            IsGameInProgress = true;
            if( CurrentPlayer.IsComputer )
            {
                computerMove();
            }
        }

        public void ExecuteDrawSign( object parameter )
        {
            NumberOfMoves++;

            setGameField( parameter.ToString() );

            checkForWinner();
        }

        public bool CanExecuteDrawSign( object parameter )
        {
            var property = GetType().GetProperty( parameter.ToString() );
            if( property.GetValue( this ).Equals( string.Empty ) && IsGameInProgress )
            {
                return true;
            }
            return false;
        }

        private Player chooseFirstPlayer()
        {
            if( Id % 2 == 0 )
            {
                return GamePlayerTwo;
            }

            return GamePlayerOne;
        }

        private void changePlayer()
        {
            if( CurrentPlayer.Id == 1 )
            {
                CurrentPlayer = GamePlayerTwo;
                if ( GamePlayerTwo.IsComputer )
                {
                    computerMove();
                }
            }
            else
            {
                CurrentPlayer = GamePlayerOne;
            }
        }

        private void computerMove()
        {
            var nextMove = string.Empty;

            NumberOfMoves++;
            switch ( NumberOfMoves )
            {
                case 1:
                    setGameField( "Center" );
                    break;
                case 2:
                    if( _takenFields.Peek().Contains( "Center" ) )
                    {
                        nextMove = _gameFields.FirstOrDefault( f => f.Contains( "Corner" ) );
                    }
                    else
                    {
                        nextMove = "Center";
                    }

                    setGameField( nextMove );
                    break;
                case 3:
                    if ( _takenFields.Peek().Contains( "Edge" ) )
                    {
                        nextMove = placeSignInCornerFurthestFromEdge();
                    }
                    else
                    {
                        nextMove = placeSignInDiagonalOppositeCorner();
                    }

                    setGameField( nextMove );
                    break;
                case 4:
                    nextMove = blockOponent();

                    if( nextMove.Equals( string.Empty ) )
                    {
                        getAllFieldsContainingPlayersSign( GamePlayerOne );

                        if( _gameFieldsWithTheSameSign.All( f => f.Contains( "Corner" ) ) )
                        {
                            nextMove = placeSignInEdge();
                        }
                        else if( _gameFieldsWithTheSameSign.All( f => f.Contains( "Edge")))
                        {
                            nextMove = placeSignInCorner();
                        }
                        else
                        {
                            var freeLine = _possibleWinningLines.Values.FirstOrDefault( l => l.All( f => isFieldEmpty( f ) ) );
                            nextMove = freeLine.FirstOrDefault( f => f.Contains( "Edge" ) );
                        }
                    }

                    _gameFieldsWithTheSameSign.Clear();

                    setGameField( nextMove );
                    break;
                case 5:
                    nextMove = winOrBlock();

                    if( nextMove.Equals( string.Empty ) )
                    {
                        nextMove = placeSignInCornerToSetUpTrap();
                    }

                    setGameField( nextMove );
                    break;
                case 6:
                    setGameField( winBlockOrRandomMove() );
                    break;
                case 7:
                    nextMove = winOrBlock();

                    if( nextMove.Equals( string.Empty ) )
                    {
                        nextMove = placeSignInEdge();
                    }

                    setGameField( nextMove );
                    break;
                case 8:
                    setGameField( winBlockOrRandomMove() );
                    break;
                default:
                    setGameField( placeSignInLastFreeField() );
                    break;
            }

            checkForWinner();
        }

        private string winBlockOrRandomMove()
        {
            string nextMove = winOrBlock();
            if( nextMove.Equals( string.Empty ) )
            {
                nextMove = randomMove();
            }

            return nextMove;
        }

        private string winOrBlock()
        {
            var nextMove = winTheGameIfPossible();

            if( nextMove.Equals( string.Empty ) )
            {
                nextMove = blockOponent();
            }

            return nextMove;
        }

        private string placeSignInCorner()
        {
            return _gameFields.FirstOrDefault( f => f.Contains( "Corner" ) );
        }

        private string placeSignInLastFreeField()
        {
            return _gameFields.FirstOrDefault();
        }

        private string placeSignInEdge()
        {
            return _gameFields.FirstOrDefault( f => f.Contains( "Edge" ) );
        }

        private string placeSignInCornerToSetUpTrap()
        {
            var nextField = string.Empty;
            var possibleLinesToFill = new List<List<string>>();
            var counter = 0;

            foreach( var fieldsList in _possibleWinningLines.Values )
            {
                foreach( var field in fieldsList )
                {
                    if( isFieldEmpty( field ) )
                    {
                        counter++;
                    }
                }

                if( counter == 2 )
                {
                    possibleLinesToFill.Add( fieldsList );
                }

                counter = 0;
            }

            var linesToRemove = possibleLinesToFill.Where( l => l.Any( f => f.Equals( "Center" ) 
            || GetType().GetProperty( f ).GetValue( this ).Equals( GamePlayerOne.PlayersSign ) ) ).ToList();

            foreach( var line in linesToRemove )
            {
                possibleLinesToFill.Remove( line );
            }

            foreach( var field in possibleLinesToFill.FirstOrDefault() )
            {
                if( isFieldEmpty( field )
                    && field.Contains( "Corner" ) )
                {
                    nextField = field;
                    break;
                }
            }

            return nextField;
        }

        private string placeSignInDiagonalOppositeCorner()
        {
            var counter = 0;
            var returnList = new List<string>();

            foreach( var fieldsList in _possibleWinningLines.Values )
            {
                foreach( var field in fieldsList )
                {
                    if( GetType().GetProperty( field ).GetValue( this ).Equals( GamePlayerOne.PlayersSign )
                        || GetType().GetProperty( field ).GetValue( this ).Equals( GamePlayerTwo.PlayersSign ) )
                    {
                        counter++;
                    }
                }

                if( counter == 2 )
                {
                    returnList = fieldsList;
                }

                counter = 0;
            }

            return returnList.FirstOrDefault( f => isFieldEmpty( f ) );
        }

        private string placeSignInCornerFurthestFromEdge()
        {
            var lines = _possibleWinningLines.Values
                         .Where( l => l.All( f => _gameFields.Contains( f ) ) );

            var freeLines = new List<string>();
            var rowCounter = 0;
            var columnCounter = 0;

            foreach( var line in lines )
            {
                var freeLine = _possibleWinningLines.FindKeyByValue( line );

                if( freeLine.Contains( "Row" ) )
                {
                    rowCounter++;
                }
                else
                {
                    columnCounter++;
                }

                freeLines.Add( freeLine );
            }

            var isRow = rowCounter < columnCounter;

            var nextLine = isRow ?
                freeLines.FirstOrDefault( f => f.Contains( "Row" ) )
                : freeLines.FirstOrDefault( f => f.Contains( "Column" ) );

            return _possibleWinningLines[ nextLine ].FirstOrDefault( f => f.Contains( "Corner" ) );
        }

        private string blockOponent()
        {
            var nextField = string.Empty;

            getAllFieldsContainingPlayersSign( GamePlayerOne );

            var linesWithTwoOponentsFields = _possibleWinningLines.FindLineWithTwoSameSigns( _gameFieldsWithTheSameSign );

            foreach( var line in linesWithTwoOponentsFields )
            {
                if( _possibleWinningLines[ line ].Any( f => isFieldEmpty( f ) ) )
                {
                    nextField = _possibleWinningLines[ line ].FirstOrDefault(
                       f => isFieldEmpty( f ) );
                    break;
                }
            }

            _gameFieldsWithTheSameSign.Clear();

            return nextField;
        }

        private bool isFieldEmpty( string fieldName )
        {
            return GetType().GetProperty( fieldName ).GetValue( this ).Equals( string.Empty );
        }

        private string winTheGameIfPossible()
        {
            getAllFieldsContainingPlayersSign( GamePlayerTwo );

            var listOfLinesWithWinningPossibility = _possibleWinningLines.FindLineWithTwoSameSigns( _gameFieldsWithTheSameSign );

            var nextMove = string.Empty;

            foreach( var list in listOfLinesWithWinningPossibility )
            {
                var emptyLineField = _possibleWinningLines[ list ]
                    .FirstOrDefault( f => isFieldEmpty( f ) );

                _gameFieldsWithTheSameSign.Clear();

                if( emptyLineField != null )
                {
                    return emptyLineField;
                }
            }

            return nextMove;
        }

        private void getAllFieldsContainingPlayersSign( Player player )
        {
            foreach( var field in _takenFields )
            {
                if( GetType().GetProperty( field ).GetValue( this ).Equals( player.PlayersSign ) )
                {
                    if( !_gameFieldsWithTheSameSign.Contains( field ) )
                    {
                        _gameFieldsWithTheSameSign.Add( field );
                    }
                }
            }
        }


        private string randomMove()
        {
            var randomNumber = new Random();

            var field = randomNumber.Next( 0, _gameFields.Count - 1 );

            return _gameFields[ field ];
        }

        private void setGameField( string fieldName )
        {
            var property = GetType().GetProperty( fieldName );
            property.SetValue( this, CurrentPlayer.PlayersSign );

            _gameFields.Remove( fieldName );
            _takenFields.Push( fieldName );

            _gameFieldsWithTheSameSign.Clear();
        }

        private void checkForWinner()
        {
            if( NumberOfMoves < 5 )
            {
                changePlayer();
                return;
            }

            foreach( var line in _possibleWinningLines )
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
            changePlayer();
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

            _displayService.DisplayMessage( message );

            var increase = winner != null ? 
                CurrentPlayer.NumberOfWins++ 
                : Draws++;
            
            IsGameInProgress = false;
            clearAllGridFields();
            CurrentPlayer = null;
            _numberOfMoves = 0;
        }
    }
}

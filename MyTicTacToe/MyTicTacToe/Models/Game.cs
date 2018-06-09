using MyTicTacToe.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Models
{
    public class Game : ObservableObject
    {
        private Player _playerOne;
        private Player _playerTwo;
        private Player _currentPlayer;

        private int _id;

        private string _topLeftCorner;
        private string _leftEdge;
        private string _bottomLeftCorner;
        private string _topEdge;
        private string _center;
        private string _bottomEdge;
        private string _topRightCorner;
        private string _rightEdge;
        private string _bottomRightCorner;

        public int Id
        {
            get => _id;
            set => _id = value;
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


        public Game(
            int id,
            Player playerOne,
            Player playerTwo )
        {
            Id = id;
            GamePlayerOne = playerOne;
            GamePlayerTwo = playerTwo;
            CurrentPlayer = ChooseFirstPlayer();
        }

        public Game()
        {
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
            PropertyInfo propertyInfo;

            if( CurrentPlayer.PlayersSign == Sign.Noughts )
            {
                propertyInfo = this.GetType().GetProperty( parameter.ToString() );
                propertyInfo.SetValue( this, "o" );
            }
            else
            {
                propertyInfo = this.GetType().GetProperty( parameter.ToString() );
                propertyInfo.SetValue( this, "x" );
            }

            CheckForWinner();
        }

        public bool CanExecuteDrawSign( object parameter )
        {
            var propertyInfo = this.GetType().GetProperty( parameter.ToString() );
            if( propertyInfo.GetValue( this ) == null && Id != 0 )
            {
                return true;
            }
            return false;
        }

        public void CheckForWinner()
        {
            ChangePlayer();
        }

    }
}

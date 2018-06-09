using MyTicTacToe.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

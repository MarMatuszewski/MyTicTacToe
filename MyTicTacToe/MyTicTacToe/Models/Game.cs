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

    }
}

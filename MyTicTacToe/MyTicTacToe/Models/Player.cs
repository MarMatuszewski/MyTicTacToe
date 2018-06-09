using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicTacToe.Shared;

namespace MyTicTacToe.Models
{
    public class Player : ObservableObject
    {
        private string _name;
        private int _id;
        private int _numberOfWins;
        private Sign _playersSign;

        public string Name
        {
            get => _name;
            set => SetProperty( ref _name, value);
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int NumberOfWins
        {
            get => _numberOfWins;
            set => _numberOfWins = value;
        }

        public Sign PlayersSign
        {
            get => _playersSign;
            set => _playersSign = value;
        }
    }
}

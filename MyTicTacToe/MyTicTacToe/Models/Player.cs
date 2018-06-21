using Prism.Mvvm;

namespace MyTicTacToe.Models
{
    public class Player : BindableBase
    {
        private string _name;
        private int _id;
        private int _numberOfWins;
        private string _playersSign;
        private int _draws;

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

        public int Draws
        {
            get => _draws;
            set => SetProperty( ref _draws, value );
        }

        public int NumberOfWins
        {
            get => _numberOfWins;
            set => SetProperty( ref _numberOfWins, value);
        }

        public string PlayersSign
        {
            get => _playersSign;
            set => _playersSign = value;
        }
    }
}

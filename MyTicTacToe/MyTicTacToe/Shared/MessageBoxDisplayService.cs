using MyTicTacToe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicTacToe.Models;
using System.Windows;

namespace MyTicTacToe.Shared
{
    public class MessageBoxDisplayService : IDisplayService
    {
        public void DisplayMessage( string message )
        {
            MessageBox.Show( message, "End of game", MessageBoxButton.OK );
        }
    }
}

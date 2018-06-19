using MyTicTacToe.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Tests.MainWindowViewModelTests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel _viewModel;
        private const string CrossSign = "x";
        private const string NoughtSign = "o";


        public MainWindowViewModelTests()
        {
            _viewModel = new MainWindowViewModel();
        }

        [Test]
        public void Player_One_Should_Have_Id_Of_1()
        {
            Assert.That( _viewModel.PlayerOne.Id, Is.EqualTo( 1 ) );
        }

        [Test]
        public void Player_One_Should_Have_Cross_Sign()
        {
            Assert.That( _viewModel.PlayerOne.PlayersSign, Is.EqualTo( CrossSign ) );
        }

        [Test]
        public void Player_Two_Should_Have_Id_Of_2()
        {
            Assert.That( _viewModel.PlayerTwo.Id, Is.EqualTo( 2 ) );
        }

        [Test]
        public void Player_Two_Should_Have_Nought_Sign()
        {
            Assert.That( _viewModel.PlayerTwo.PlayersSign, Is.EqualTo( NoughtSign ) );
        }
    }
}

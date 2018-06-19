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

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_Game_Is_In_Porgress()
        {
            _viewModel.CurrentGame.IsGameInProgress = true;

            Assert.IsFalse( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_SinglePlayer_Mode_And_Player_Name_Is_Blank()
        {
            _viewModel.IsMultiplayerSelected = false;
            _viewModel.PlayerOne.Name = string.Empty;

            Assert.IsFalse( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_MultiPlayer_Mode_And_Player_One_Name_Is_Blank()
        {
            _viewModel.CurrentGame.IsGameInProgress = false;
            _viewModel.IsMultiplayerSelected = true;
            _viewModel.PlayerOne.Name = string.Empty;
            _viewModel.PlayerTwo.Name = "PlayerTwo";

            Assert.IsFalse( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_MultiPlayer_Mode_And_Player_Two_Name_Is_Blank()
        {
            _viewModel.CurrentGame.IsGameInProgress = false;
            _viewModel.IsMultiplayerSelected = true;
            _viewModel.PlayerOne.Name = "PlayerOne";
            _viewModel.PlayerTwo.Name = string.Empty;

            Assert.IsFalse( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Enabled_When_In_SinglePlayer_Mode_And_Player_One_Name_Is_Set()
        {
            _viewModel.CurrentGame.IsGameInProgress = false;
            _viewModel.IsMultiplayerSelected = false;
            _viewModel.PlayerOne.Name = "PlayerOne";

            Assert.IsTrue( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Enabled_When_In_MultiPlayer_Mode_And_Player_One_And_Two_Names_Are_Set()
        {
            _viewModel.CurrentGame.IsGameInProgress = false;
            _viewModel.IsMultiplayerSelected = true;
            _viewModel.PlayerOne.Name = "PlayerOne";
            _viewModel.PlayerTwo.Name = "PlayerTwo";

            Assert.IsTrue( _viewModel.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void PlayerTwo_Name_Should_Be_Set_To_Computer_When_Game_Is_Started_In_MultiPlayer_Mode()
        {
            _viewModel.IsMultiplayerSelected = false;
            _viewModel.StartGameCommand.Execute( null );

            Assert.That( _viewModel.PlayerTwo.Name, Is.EqualTo( "Computer" ) );
        }
    }
}

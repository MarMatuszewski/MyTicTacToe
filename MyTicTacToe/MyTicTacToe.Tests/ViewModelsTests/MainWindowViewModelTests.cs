using MyTicTacToe.Interfaces;
using MyTicTacToe.Models;
using MyTicTacToe.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MyTicTacToe.Tests.ViewModelTests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel SUT;
        private const string CrossSign = "x";
        private const string NoughtSign = "o";

        private IGame MockGame;


        public MainWindowViewModelTests()
        {
            MockGame = Substitute.For<IGame>();

            SUT = new MainWindowViewModel( MockGame );
        }

        [Test]
        public void Player_One_Should_Have_Id_Of_1()
        {
            Assert.That( SUT.PlayerOne.Id, Is.EqualTo( 1 ) );
        }

        [Test]
        public void Player_One_Should_Have_Cross_Sign()
        {
            Assert.That( SUT.PlayerOne.PlayersSign, Is.EqualTo( CrossSign ) );
        }

        [Test]
        public void Player_Two_Should_Have_Id_Of_2()
        {
            Assert.That( SUT.PlayerTwo.Id, Is.EqualTo( 2 ) );
        }

        [Test]
        public void Player_Two_Should_Have_Nought_Sign()
        {
            Assert.That( SUT.PlayerTwo.PlayersSign, Is.EqualTo( NoughtSign ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_Game_Is_In_Porgress()
        {
            MockGame.IsGameInProgress.Returns( true );

            Assert.IsFalse( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_SinglePlayer_Mode_And_Player_Name_Is_Blank()
        {
            SUT.IsMultiplayerSelected = false;
            SUT.PlayerOne.Name = string.Empty;

            Assert.IsFalse( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_MultiPlayer_Mode_And_Player_One_Name_Is_Blank()
        {
            MockGame.IsGameInProgress.Returns( false );
            SUT.IsMultiplayerSelected = true;
            SUT.PlayerOne.Name = string.Empty;
            SUT.PlayerTwo.Name = "PlayerTwo";

            Assert.IsFalse( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Disabled_When_In_MultiPlayer_Mode_And_Player_Two_Name_Is_Blank()
        {
            MockGame.IsGameInProgress.Returns( false );
            SUT.IsMultiplayerSelected = true;
            SUT.PlayerOne.Name = "PlayerOne";
            SUT.PlayerTwo.Name = string.Empty;

            Assert.IsFalse( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Enabled_When_In_SinglePlayer_Mode_And_Player_One_Name_Is_Set()
        {
            MockGame.IsGameInProgress.Returns( false );
            SUT.IsMultiplayerSelected = false;
            SUT.PlayerOne.Name = "PlayerOne";

            Assert.IsTrue( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void StartGame_Button_Should_Be_Enabled_When_In_MultiPlayer_Mode_And_Player_One_And_Two_Names_Are_Set()
        {
            MockGame.IsGameInProgress.Returns( false );
            SUT.IsMultiplayerSelected = true;
            SUT.PlayerOne.Name = "PlayerOne";
            SUT.PlayerTwo.Name = "PlayerTwo";

            Assert.IsTrue( SUT.StartGameCommand.CanExecute( null ) );
        }

        [Test]
        public void PlayerTwo_Name_Should_Be_Set_To_Computer_When_Game_Is_Started_In_SinglePlayer_Mode()
        {
            SUT.IsMultiplayerSelected = false;
            SUT.StartGameCommand.Execute( null );

            Assert.That( SUT.PlayerTwo.Name, Is.EqualTo( "Computer" ) );
        }

        [Test]
        public void StartGame_Method_Should_Be_Called_Once_When_Game_Is_Started()
        {
            MockGame.ClearReceivedCalls();
            SUT.StartGameCommand.Execute( null );

            MockGame.Received( 1 ).StartGame( Arg.Any<Player>(), Arg.Any<Player>() );
        }

        [Test]
        public void StartGame_Method_Should_Be_Passed_Correct_Players_When_Game_Is_Started()
        {
            MockGame.ClearReceivedCalls();
            SUT.StartGameCommand.Execute( null );

            MockGame.Received().StartGame( Arg.Is<Player>( p => p.Equals( SUT.PlayerOne ) ),
                Arg.Is<Player>( p => p.Equals( SUT.PlayerTwo ) ) );
        }

        [Test]
        public void Game_ExecuteDrawSign_Method_Should_Be_Called_Once_When_Sign_Is_Drawn()
        {
            MockGame.ClearReceivedCalls();
            SUT.DrawSignCommand.Execute( null );

            MockGame.Received( 1 ).ExecuteDrawSign( Arg.Any<object>() );
        }

        [Test]
        public void Game_ExecuteDrawSign_Method_Should_Be_Called_With_Correct_Parameter_When_Sign_Is_Drawn()
        {
            MockGame.ClearReceivedCalls();
            var obj = "object";
            SUT.DrawSignCommand.Execute( obj );

            MockGame.Received().ExecuteDrawSign( Arg.Is<object>( o => o.Equals( obj ) ) );
        }

        [Test]
        public void Game_CanExecuteDrawSign_Method_Should_Be_Called_Once_When_DrawSignButton_Condition_Is_Checked()
        {
            MockGame.ClearReceivedCalls();
            SUT.DrawSignCommand.CanExecute( null );

            MockGame.Received( 1 ).CanExecuteDrawSign( Arg.Any<object>() );
        }

        [Test]
        public void Game_CanExecuteDrawSign_Method_Should_Be_Called_With_Correct_Parameter_When_DrawSignButton_Condition_Is_Checked()
        {
            MockGame.ClearReceivedCalls();
            var obj = "object";
            SUT.DrawSignCommand.CanExecute( obj );

            MockGame.Received().CanExecuteDrawSign( Arg.Is<object>( o => o.Equals( obj ) ) );
        }
    }
}

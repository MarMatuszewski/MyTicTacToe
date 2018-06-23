using MyTicTacToe.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Tests.ModelsTests
{
    [TestFixture]
    public class GameTests
    {
        private Game SUT;
        private Player _testPlayerOne;
        private Player _testPlayerTwo;

        public GameTests()
        {
            _testPlayerOne = new Player() { Id = 1, Name = "PlayerOne", PlayersSign = "x" };
            _testPlayerTwo = new Player() { Id = 2, Name = "PlayerTwo", PlayersSign = "o" };

            SUT = new Game();
        }

        [Test]
        public void Game_Id_Should_Be_One_When_Game_Is_First_Started()
        {
            SUT.Id = 0;
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreEqual( 1, SUT.Id );
        }

        [Test]
        public void PlayerOne_Should_Be_Assigned_To_GamePlayerOne_When_Game_Is_Started()
        {
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreEqual( _testPlayerOne, SUT.GamePlayerOne );
        }

        [Test]
        public void PlayerTwo_Should_Be_Assigned_To_GamePlayerTwo_When_Game_Is_Started()
        {
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreEqual( _testPlayerTwo, SUT.GamePlayerTwo );
        }

        [TestCase( 0 )]
        [TestCase( 6 )]
        [TestCase( 16 )]
        [TestCase( 60 )]
        public void GamePlayerOne_Should_Be_First_Player_When_Game_Is_Started_And_Game_Number_Is_Odd( int gameNumber )
        {
            SUT.Id = gameNumber;
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreSame( _testPlayerOne, SUT.CurrentPlayer );
        }

        [TestCase( 1 )]
        [TestCase( 7 )]
        [TestCase( 19 )]
        [TestCase( 53 )]
        public void GamePlayerTwo_Should_Be_First_Player_When_Game_Is_Started_And_Game_Number_Is_Even( int gameNumber )
        {
            SUT.Id = gameNumber;
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreSame( _testPlayerTwo, SUT.CurrentPlayer );
        }

        [Test]
        public void Game_Should_Be_In_Progress_When_Game_Is_Started()
        {
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );

            Assert.AreEqual( true, SUT.IsGameInProgress );
        }

        [Test]
        public void DrawSign_Command_Should_Be_Disabled_On_Given_Field_When_It_Is_Not_Empty_And_Game_Is_In_Progress()
        {
            var obj = "Center";
            SUT.IsGameInProgress = true;
            SUT.Center = "x";

            Assert.IsFalse( SUT.CanExecuteDrawSign( obj ) );
        }

        [Test]
        public void DrawSign_Command_Should_Be_Disabled_When_Game_Is_Not_In_Progress()
        {
            var obj = "Center";
            SUT.IsGameInProgress = false;

            Assert.IsFalse( SUT.CanExecuteDrawSign( obj ) );
        }

        [Test]
        public void DrawSign_Command_Should_Be_Enabled_On_Given_Field_When_It_Is_Empty_And_Game_Is_In_Progress()
        {
            var obj = "Center";
            SUT.Center = "";
            SUT.IsGameInProgress = true;

            Assert.IsTrue( SUT.CanExecuteDrawSign( obj ) );
        }

        [Test]
        public void NumberOfMoves_Should_Be_One_After_First_Sign_Was_Drawn()
        {
            var obj = "Center";
            SUT.NumberOfMoves = 0;
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );
            SUT.ExecuteDrawSign( obj );

            Assert.AreEqual( 1, SUT.NumberOfMoves );
        }

        [Test]
        public void Game_Field_Should_Have_The_Same_Value_As_Current_Payer_Sign_After_His_Move()
        {
            var obj = "Center";
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );
            var sign = SUT.CurrentPlayer.PlayersSign;
            SUT.ExecuteDrawSign( obj );

            Assert.AreEqual( sign, SUT.Center );
        }

        [TestCase( 1 )]
        [TestCase( 2 )]
        [TestCase( 3 )]
        public void Current_Player_Should_Be_Changed_When_Sign_Will_Be_Drawn_And_NumberOfMove_Will_Be_Lower_Than_5( int numberOfMoves )
        {
            var obj = "Center";
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );
            SUT.CurrentPlayer = _testPlayerOne;
            SUT.NumberOfMoves = numberOfMoves;
            SUT.ExecuteDrawSign( obj );

            Assert.AreSame( _testPlayerTwo, SUT.CurrentPlayer );
        }

        [Test]
        public void Current_Player_Should_Be_Changed_To_PlayerTwo_After_Players_One_Move()
        {
            var obj = "Center";
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );
            SUT.CurrentPlayer = _testPlayerOne;
            SUT.NumberOfMoves = 1;
            SUT.ExecuteDrawSign( obj );

            Assert.AreSame( _testPlayerTwo, SUT.CurrentPlayer );
        }

        [Test]
        public void Current_Player_Should_Be_Changed_To_PlayerOne_After_Players_Two_Move()
        {
            var obj = "Center";
            SUT.StartGame( _testPlayerOne, _testPlayerTwo );
            SUT.CurrentPlayer = _testPlayerTwo;
            SUT.NumberOfMoves = 1;
            SUT.ExecuteDrawSign( obj );

            Assert.AreSame( _testPlayerOne, SUT.CurrentPlayer );
        }
    }
}

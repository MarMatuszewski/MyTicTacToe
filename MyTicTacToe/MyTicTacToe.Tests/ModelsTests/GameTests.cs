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
        private Player TestPlayerOne;
        private Player TestPlayerTwo;


        public GameTests()
        {
            TestPlayerOne = new Player() { Id = 1, Name = "PlayerOne", PlayersSign = "x" };
            TestPlayerTwo = new Player() { Id = 2, Name = "PlayerTwo", PlayersSign = "o" };


            SUT = new Game();
        }

        [Test]
        public void Game_Id_Should_Be_One_When_Game_Is_First_Started()
        {
            SUT.Id = 0;
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreEqual( 1, SUT.Id );
        }

        [Test]
        public void PlayerOne_Should_Be_Assigned_To_GamePlayerOne_When_Game_Is_Started()
        {
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreEqual( TestPlayerOne, SUT.GamePlayerOne );
        }

        [Test]
        public void PlayerTwo_Should_Be_Assigned_To_GamePlayerTwo_When_Game_Is_Started()
        {
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreEqual( TestPlayerTwo, SUT.GamePlayerTwo );
        }

        [TestCase( 0 )]
        [TestCase( 6 )]
        [TestCase( 16 )]
        [TestCase( 60 )]
        public void GamePlayerOne_Should_Be_First_Player_When_Game_Is_Started_And_Game_Number_Is_Odd( int gameNumber )
        {
            SUT.Id = gameNumber;
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreSame( TestPlayerOne, SUT.CurrentPlayer );
        }

        [TestCase( 1 )]
        [TestCase( 7 )]
        [TestCase( 19 )]
        [TestCase( 53 )]
        public void GamePlayerTwo_Should_Be_First_Player_When_Game_Is_Started_And_Game_Number_Is_Even( int gameNumber )
        {
            SUT.Id = gameNumber;
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreSame( TestPlayerTwo, SUT.CurrentPlayer );
        }

        [Test]
        public void Game_Should_Be_In_Progress_When_Game_Is_Started()
        {
            SUT.StartGame( TestPlayerOne, TestPlayerTwo );
            Assert.AreEqual( true, SUT.IsGameInProgress );
        }
    }
}

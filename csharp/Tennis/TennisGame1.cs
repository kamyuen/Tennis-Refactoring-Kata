using System;
using System.Collections.Generic;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Dictionary<string, Player> _playerLookup = new Dictionary<string, Player>();

        /// <summary>
        ///     Creates games with two players and adds players
        ///     into a look up table.
        /// </summary>
        /// <param name="player1Name"></param>
        /// <param name="player2Name"></param>
        public TennisGame1(string player1Name, string player2Name)
        {
            if (player1Name == player2Name)
            {
                throw new ArgumentException("Ambiguous player names, can't distinguish");
            }

            _player1 = new Player(player1Name);
            _playerLookup.Add(_player1.Name, _player1);
            _player2 = new Player(player2Name);
            _playerLookup.Add(_player2.Name, _player2);
        }

        /// <summary>
        ///     Looks up player by name and increments player's points.
        /// </summary>
        /// <param name="playerName"></param>
        public void WonPoint(string playerName)
        {
            if (_playerLookup.TryGetValue(playerName, out var pointWinner))
            {
                pointWinner.AddPoint();
            }
            else
            {
                throw new ArgumentException("Invalid player");
            }
        }

        /// <summary>
        ///     Get the score in tennis terminology.
        /// </summary>
        /// <returns></returns>
        public string GetScore()
        {
            return ScoreConverter.GetScore(_player1, _player2);
        }
    }
}
using System;

namespace Tennis
{
    // Done in the spirit of refactoring existing code as opposed to
    // rewriting everything
    class TennisGame1 : ITennisGame
    {
        private int _player1Score = 0;
        private int _player2Score = 0;
        private readonly string _player1Name;
        private readonly string _player2Name;
        private readonly string[] _scoreTerms = new string[] { "Love", "Fifteen", "Thirty", "Forty" };

        /// <summary>
        ///     Initialise a Tennis game with two players
        /// </summary>
        /// <param name="player1Name"></param>
        /// <param name="player2Name"></param>
        public TennisGame1(string player1Name, string player2Name)
        {
            if (string.IsNullOrEmpty(player1Name))
            {
                throw new ArgumentNullException("Player 1 Name not specified");
            }

            if (string.IsNullOrEmpty(player2Name))
            {
                throw new ArgumentNullException("Player 2 Name not specified");
            }

            if (player1Name == player2Name)
            {
                throw new ArgumentException("Ambiguous player names, can't distinguish");
            }

            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        /// <summary>
        ///     Adds a point to named player
        /// </summary>
        /// <param name="playerName"></param>
        public void WonPoint(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new ArgumentNullException("Player name not specified");
            }

            if (playerName != _player1Name & playerName != _player2Name)
            {
                throw new ArgumentException("Unrecognised player");
            }

            if (playerName == _player1Name)
            {
                _player1Score += 1;
            }
            else
            {
                _player2Score += 1;
            }
        }

        /// <summary>
        ///     Return the game result
        /// </summary>
        /// <returns></returns>
        public string GetScore()
        {
            if (_player1Score == _player2Score)
            {
                return ProcessEqualScores();
            }
            else if (_player1Score >= 4 || _player2Score >= 4)
            {
                return ProcessEndGame();
            }
            else
            {
                // Return normal game score
                return $"{_scoreTerms[_player1Score]}-{_scoreTerms[_player2Score]}";
            }
        }

        /// <summary>
        ///     Return equal scores in Tennis terminology
        /// </summary>
        /// <returns></returns>
        private string ProcessEqualScores()
        {
            if (_player1Score > 2)
            {
                return "Deuce";
            }

            return $"{_scoreTerms[_player1Score]}-All";
        }

        /// <summary>
        ///     Return score when there is a difference of one or
        ///     more points past three points
        /// </summary>
        /// <returns></returns>
        private string ProcessEndGame()
        {
            string score;
            var scoreDiff = _player1Score - _player2Score;

            switch (scoreDiff)
            {
                case 1:
                    score = "Advantage player1";
                    break;
                case -1:
                    score = "Advantage player2";
                    break;
                case >= 2:
                    score = "Win for player1";
                    break;
                default:
                    score = "Win for player2";
                    break;
            }

            return score;
        }
    }
}


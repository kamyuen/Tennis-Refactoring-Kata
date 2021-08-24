using System;

namespace Tennis
{
    // Done in the spirit of refactoring existing code as opposed to
    // rewriting everything
    public class TennisGame2 : ITennisGame
    {
        private int _player1Score;
        private int _player2Score;
        private readonly string _player1Name;
        private readonly string _player2Name;
        private readonly string[] _scoreTerms = new string[] { "Love", "Fifteen", "Thirty", "Forty" };

        /// <summary>
        ///     Initialise a Tennis game with two players
        /// </summary>
        /// <param name="player1Name"></param>
        /// <param name="player2Name"></param>
        public TennisGame2(string player1Name, string player2Name)
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
            _player1Score = 0;
            _player2Name = player2Name;
            _player2Score = 0;
        }

        public string GetScore()
        {
            var score = string.Empty;

            // Process equal scores and not Forty all
            if (_player1Score == _player2Score && _player1Score < 3)
            {
                score = _scoreTerms[_player1Score] + "-All";
            }

            // Forty-All
            if (_player1Score == _player2Score && _player1Score > 2)
            {
                score = "Deuce";
            }

            if ((_player1Score > 0 && _player1Score < 4 && _player2Score == 0) // Player 2 Love
                || (_player2Score > 0 && _player2Score < 4 && _player1Score == 0)) // Player 1 Love
            {
                score = LoveScore(_player1Score, _player2Score);
            }

            if ((_player1Score > _player2Score && _player1Score < 4 && _player2Score > 0)     // Player 1 leading but hasn't won
                || (_player2Score > _player1Score && _player2Score < 4 && _player1Score > 0)) // Player 2 leading but hasn't won 
            {
                score = NormalScoring(_player1Score, _player2Score);
            }

            score = Advantage(score);
            score = Win(score);

            return score;
        }

        /// <summary>
        ///     Return Love points
        /// </summary>
        /// <param name="player1Score"></param>
        /// <param name="player2Score"></param>
        /// <returns></returns>
        private string LoveScore(int player1Score, int player2Score)
        {
            var scoreTerm = _scoreTerms[Math.Max(player1Score, player2Score)];
            var score = player1Score > player2Score ? $"{scoreTerm}-Love" : $"Love-{scoreTerm}";
            return score;
        }

        /// <summary>
        ///     Return normal scoring
        /// </summary>
        /// <param name="player1Score"></param>
        /// <param name="player2Score"></param>
        /// <returns></returns>
        private string NormalScoring(int player1Score, int player2Score)
        {
            var score1Term = _scoreTerms[player1Score];
            var score2Term = _scoreTerms[player2Score];
            return $"{score1Term}-{score2Term}";
        }

        /// <summary>
        ///     Process and advantage point
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        private string Advantage(string score)
        {
            if (_player1Score > _player2Score && _player2Score >= 3)
            {
                return "Advantage player1";
            }

            if (_player2Score > _player1Score && _player1Score >= 3)
            {
                return "Advantage player2";
            }

            return score;
        }

        /// <summary>
        ///     Process a win
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        private string Win(string score)
        {
            if (_player1Score >= 4 && _player2Score >= 0 && (_player1Score - _player2Score) >= 2)
            {
                return "Win for player1";
            }

            if (_player2Score >= 4 && _player1Score >= 0 && (_player2Score - _player1Score) >= 2)
            {
                return "Win for player2";
            }

            return score;
        }

        /// <summary>
        ///     Adds a point to named player
        /// </summary>
        /// <param name="playerName"></param>
        public void WonPoint(string player)
        {
            if (string.IsNullOrEmpty(player))
            {
                throw new ArgumentNullException("Player name not specified");
            }

            if (player != _player1Name & player != _player2Name)
            {
                throw new ArgumentException("Unrecognised player");
            }

            if (player == _player1Name)
            {
                _player1Score += 1;
            }
            else
            {
                _player2Score += 1;
            }
        }
    }
}
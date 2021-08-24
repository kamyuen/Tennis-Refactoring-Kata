using System;

namespace Tennis
{
    // Done in the spirit of refactoring existing code as opposed to
    // rewriting everything
    public class TennisGame3 : ITennisGame
    {
        private int _player1Score;
        private int _player2Score;
        private readonly string _player1Name;
        private readonly string _player2Name;

        /// <summary>
        ///     Initialise a Tennis game with two players
        /// </summary>
        /// <param name="player1Name"></param>
        /// <param name="player2Name"></param>
        public TennisGame3(string player1Name, string player2Name)
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
        ///     Return the game result
        /// </summary>
        /// <returns></returns>
        public string GetScore()
        {
            if ((_player1Score < 4 && _player2Score < 4)
                && (_player1Score + _player2Score < 6)) // All conditions less than a Deuce, i.e. normal play
            {
                return NormalPlayScore();
            }
            else
            {
                if (_player1Score == _player2Score)
                {
                    return "Deuce";
                }

                return AdvantageWin();
            }
        }

        /// <summary>
        ///     Handle Special Points conditions
        /// </summary>
        /// <returns></returns>
        private string AdvantageWin()
        {
            var leadingPlayer = _player1Score > _player2Score ? _player1Name : _player2Name;

            if (Math.Pow((_player1Score - _player2Score), 2) == 1)
            {
                return $"Advantage {leadingPlayer}";
            }
            else
            {
                return $"Win for {leadingPlayer}";
            }
        }

        /// <summary>
        ///     Return score for normal play
        /// </summary>
        /// <returns></returns>
        private string NormalPlayScore()
        {
            string[] _scoreTerms = { "Love", "Fifteen", "Thirty", "Forty" };
            var scoreResult = _scoreTerms[_player1Score];

            if (_player1Score == _player2Score)
            {
                return $"{scoreResult}-All";
            }
            else
            {
                return $"{scoreResult}-{_scoreTerms[_player2Score]}";
            }
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


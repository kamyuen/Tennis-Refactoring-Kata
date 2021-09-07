using System;

namespace Tennis
{
    public static class ScoreConverter
    {
        /// <summary>
        ///     Look up of tennis score terminology.
        /// </summary>
        private static string[] _scoreTerms = new string[] { "Love", "Fifteen", "Thirty", "Forty" };

        /// <summary>
        ///     Returns the overall game score in tennis terminology.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        public static string GetScore(Player player1, Player player2)
        {
            if (player1.Score == player2.Score)
            {
                // Equal scores with special condition of Deuce
                return player1.Score >= 3 ? "Deuce" : $"{_scoreTerms[player1.Score]}-All";
            }

            if (player1.Score <= 3 && player2.Score <= 3)
            {
                // Normal play score
                return $"{_scoreTerms[player1.Score]}-{_scoreTerms[player2.Score]}";
            }

            // One score must be greater than the other as the same
            // score condition is caught higher up
            var player = player1.Score > player2.Score ? player1 : player2;
            var scoreDiff = Math.Abs(player1.Score - player2.Score);

            if((player1.Score >=4 || player2.Score >= 4) && scoreDiff == 1)
            {
                return $"Advantage {player.Name}";
            }

            return $"Win for {player.Name}";
        }
    }
}

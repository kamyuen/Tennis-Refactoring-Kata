using System;

namespace Tennis
{
    public class Player
    {
        /// <summary>
        ///     Constructs a player
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            // Check that a name is specified.
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name not specified");
            }

            Name = name;
            Score = 0;
        }

        public string Name { get; private set; }

        public int Score { get; private set; }

        /// <summary>
        ///     Adds a single point.
        /// </summary>
        public void AddPoint()
        {
            Score += 1;
        }
    }
}

using System.IO;

namespace Othello
{
    public class Player
    {
        public Player(PawnColor _color)
        {
            if ((int)_color > -1)
            {
                Color = _color;
            }
            else
            {
                throw new InvalidDataException($"{_color} error");
            }
        }

        public PawnColor Color { get; set; }
    }
}

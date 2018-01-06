using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Othello.Annotations;

namespace Othello
{
    public class Board : INotifyPropertyChanged
    {
        public static int SquareSize = 8;

        public Board()
        {
            Initialize();
        }

        private void Initialize()
        {
            Pawns = new ObservableCollection<Pawn>();
            Reset();
        }

        public void Reset()
        {
            Pawns.Clear();
            CurrentColorPlayer = PawnColor.Black;
            int size = SquareSize * SquareSize;
            int indexMiddle = (size - 1 - SquareSize) / 2;
            for (int i = 0; i < size; i++)
            {
                var color = PawnColor.Empty;
                if (i == indexMiddle || i == indexMiddle + (SquareSize + 1))
                {
                    color = PawnColor.Black;
                }
                else if (i == indexMiddle + 1 || i == indexMiddle + SquareSize)
                {
                    color = PawnColor.White;
                }
                Pawns.Add(new Pawn(color, i));
            }
            FindPlayable(PawnColor.Black);
        }

        private void FindPlayable(PawnColor _color)
        {
            for (int i = 0; i < Pawns.Count; i++)
            {
                var pawn = Pawns.ElementAt(i);
                pawn.IsPlayable = pawn.Color == PawnColor.Empty && IsPlayable(i, _color);
            }
        }

        private bool IsPlayable(int _index, PawnColor _color)
        {
            //Start with diagonal on top left
            int direction = -SquareSize - 1;
            for (int numberDirection = 0; numberDirection < 8; numberDirection++)
            {
                bool findOtherColor = false, refindColor = false;
                int newIndex = _index + direction;
                while (true)
                {
                    if (newIndex < 0)
                    {
                        break;
                    }
                    if (newIndex > (SquareSize * SquareSize) - 1)
                    {
                        break;
                    }
                    var findPawnColor = Pawns.ElementAt(newIndex).Color;
                    if (findPawnColor == _color && !findOtherColor)
                    {
                        break;
                    }
                    if (findPawnColor == _color && findOtherColor)
                    {
                        refindColor = true;
                    }
                    if (findPawnColor == PawnColor.Empty && !findOtherColor)
                    {
                        break;
                    }
                    if (findPawnColor == PawnColor.Empty && findOtherColor && refindColor)
                    {
                        return true;
                    }
                    if (findPawnColor != _color)
                    {
                        findOtherColor = true;
                    }
                    if (newIndex % SquareSize == 0)
                    {
                        break;
                    }
                    if ((newIndex + 1) % SquareSize == 0)
                    {
                        break;
                    }
                    newIndex += direction;
                }
                direction++;
                if (numberDirection == 2)
                {
                    direction = -1;
                }
                else if (numberDirection == 3)
                {
                    direction = 1;
                }
                else if (numberDirection == 4)
                {
                    direction = SquareSize - 1;
                }
            }
            return false;
        }

        public PawnColor CurrentColorPlayer { get; set; }

        public ObservableCollection<Pawn> Pawns { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

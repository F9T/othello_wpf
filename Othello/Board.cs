using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Othello.Annotations;

namespace Othello
{
    public class Board : INotifyPropertyChanged
    {
        public static int SquareSize = 8;
        private Brush backgroundColor;

        public Board()
        {
            Initialize();
        }

        private void Initialize()
        {
            BackgroundColor = new SolidColorBrush(Colors.DarkGreen);
            Pawns = new ObservableCollection<Pawn>();
            Reset();
        }

        public void Reset()
        {
            Pawns.Clear();
            for (int i = 0; i < SquareSize; i++)
            {
                for (int j = 0; j < SquareSize; j++)
                {
                    var color = PawnColor.Empty;
                    if (j == 3 && i == 3 || j == 4 && i == 4)
                    {
                        color = PawnColor.Black;
                    }
                    else if (j == 4 && i == 3 || j == 3 && i == 4)
                    {
                        color = PawnColor.White;
                    }
                    Pawns.Add(new Pawn(color, new PawnPosition { Row = j, Column = i }));
                }
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

        public string ImageWhiteSource { get; set; }

        public Brush BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public ObservableCollection<Pawn> Pawns { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System.Collections.Generic;
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
        private Player whitePlayer, blackPlayer;

        public Board()
        {
            Initialize();
        }

        private void Initialize()
        {
            Pawns = new ObservableCollection<Pawn>();
            whitePlayer = new Player(PawnColor.White);
            blackPlayer = new Player(PawnColor.Black);
            Reset();
        }

        public void Reset()
        {
            Pawns.Clear();
            CurrentPlayer = blackPlayer;
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
            GetLegalMove(CurrentPlayer.Color);
        }

        private List<int> GetLegalMove(PawnColor _color)
        {
            var listIndex = new List<int>();
            for (int i = 0; i < Pawns.Count; i++)
            {
                var pawn = Pawns.ElementAt(i);
                if (IsPlayable(i, _color))
                {
                    pawn.IsPlayable = true;
                    listIndex.Add(i);
                }
                else
                {
                    pawn.IsPlayable = false;
                }
            }
            return listIndex;
        }

        private bool IsPlayable(int _index, PawnColor _color, bool _turn = false)
        {
            //Start with diagonal on top left
            int direction = -SquareSize - 1;
            var turnPawn = new List<int>();
            if (Pawns.ElementAt(_index).Color != PawnColor.Empty)
            {
                return false;
            }
            for (int numberDirection = 0; numberDirection < 8; numberDirection++)
            {
                bool findOtherColor = false;
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
                        if (_turn)
                        {
                            foreach (int i in turnPawn)
                            {
                                Pawns.ElementAt(i).Color = _color;
                                Pawns.ElementAt(i).IsPlayable = false;
                            }
                            break;
                        }
                        return true;
                    }
                    if (findPawnColor == PawnColor.Empty)
                    {
                        break;
                    }
                    if (findPawnColor != _color)
                    {
                        findOtherColor = true;
                        if (_turn)
                        {
                            turnPawn.Add(newIndex);
                        }
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
                turnPawn.Clear();
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

        public void Turn(int _index)
        {
            IsPlayable(_index, CurrentPlayer.Color, true);
            Pawns.ElementAt(_index).Color = CurrentPlayer.Color;
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == whitePlayer ? blackPlayer : whitePlayer;
            if (GetLegalMove(CurrentPlayer.Color).Count == 0)
            {
                CurrentPlayer = CurrentPlayer == whitePlayer ? blackPlayer : whitePlayer;
            } 
        }

        public Player CurrentPlayer { get; set; }

        public ObservableCollection<Pawn> Pawns { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

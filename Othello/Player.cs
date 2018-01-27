using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Othello.Pawns;
using Othello.Properties;

namespace Othello
{
    public class Player : INotifyPropertyChanged
    {
        private string name, imageSource;
        private TimeSpan time;
        private int actualScore;

        public Player(PawnColor _color, string _name)
        {
            Name = _name;
            if ((int)_color > -1)
            {
                Color = _color;
                if (Color == PawnColor.Black)
                {
                    ImageSource = new FileInfo("../Images/black_pawn.png").FullName;
                }
                else if (Color == PawnColor.White)
                {
                    ImageSource = new FileInfo("../Images/white_pawn.png").FullName;
                }
            }
            else
            {
                throw new InvalidDataException($"{_color} error");
            }
        }

        public void Reset()
        {
            Time = new TimeSpan(0, 0, 5, 0);
            ActualScore = 0;
        }

        public int ActualScore
        {
            get => actualScore;
            set
            {
                actualScore = value;
                OnPropertyChanged(nameof(ActualScore));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public TimeSpan Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public PawnColor Color { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

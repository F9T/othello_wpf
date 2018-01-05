using System.ComponentModel;
using System.Runtime.CompilerServices;
using Othello.Annotations;

namespace Othello
{
    public class Pawn : INotifyPropertyChanged
    {
        private bool isPlayable;
        private string imageSource;
        private PawnColor color;

        public Pawn(PawnColor _color, PawnPosition _position)
        {
            Color = _color;
            Position = _position;
            IsPlayable = false;
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

        public PawnColor Color
        {
            get => color;
            set
            {
                color = value;
                if (value == PawnColor.Black)
                {
                    ImageSource = "Images/black_pawn.png";
                }
                else if (value == PawnColor.White)
                {
                    ImageSource = "Images/white_pawn.png";
                }
                OnPropertyChanged(nameof(Color));
            }
        }

        public PawnPosition Position { get; set; }

        public bool IsPlayable
        {
            get => isPlayable;
            set
            {
                isPlayable = value;
                OnPropertyChanged(nameof(IsPlayable));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Othello.Properties;

namespace Othello.Pawns
{
    [Serializable]
    public class Pawn : INotifyPropertyChanged
    {
        private bool isPlayable;
        private Player owner;

        public Pawn(Player _owner, int _number)
        {
            Owner = _owner;
            Number = _number;
            IsPlayable = false;
        }

        public Player Owner
        {
            get => owner;
            set
            {
                owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        public int Number { get; set; }

        public bool IsPlayable
        {
            get => isPlayable;
            set
            {
                isPlayable = value;
                OnPropertyChanged(nameof(IsPlayable));
            }
        }

        public PawnColor GetColor()
        {
            return Owner?.Color ?? PawnColor.Empty;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Othello.Annotations;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour GameBoard.xaml
    /// </summary>
    public partial class GameBoard : INotifyPropertyChanged
    {
        private Board board;
        private string backgroundImage;
        private Brush backgroundColor;
        private bool useBackgroundImage;

        public GameBoard()
        {
            InitializeComponent();
            DataContext = this;
            BackgroundColor = new SolidColorBrush(Colors.DarkGreen);
            UseBackgroundImage = false;
            BackgroundImage = "/Images/background.png";
            Board = new Board();
        }

        public bool UseBackgroundImage
        {
            get => useBackgroundImage;
            set
            {
                useBackgroundImage = value;
                OnPropertyChanged(nameof(UseBackgroundImage));
            }
        }

        public string BackgroundImage
        {
            get => backgroundImage;
            set
            {
                backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        public Board Board
        {
            get => board;
            set
            {
                board = value;
                OnPropertyChanged(nameof(Board));
            }
        }

        public Brush BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        private void CasePlayable_OnMouseLeftButtonUp(object _sender, MouseButtonEventArgs _e)
        {
            if (_sender is Label label)
            {
                int index = Convert.ToInt32(label.Tag);
                Board.Turn(index);
                Board.ChangePlayer();
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

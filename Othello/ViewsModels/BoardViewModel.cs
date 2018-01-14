using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using Othello.Models;

namespace Othello.ViewsModels
{
    public class BoardViewModel : IViewModel
    {
        public BoardViewModel()
        {
            Board = new Board();
            PlayCommand = new RelayCommand(_param => Board.PlayMove((int)_param), _param => true);
        }

        public Board Board { get; set; }

        public bool UseBackgroundImage
        {
            get => Board.UseBackgroundImage;
            set
            {
                if (Board.UseBackgroundImage != value)
                {
                    Board.UseBackgroundImage = value;
                    OnPropertyChanged(nameof(UseBackgroundImage));
                }
            }
        }

        public ObservableCollection<RibbonItem> RibbonItems => Board.RibbonItems;

        public int SquareSize { get; set; }

        public string BackgroundImage
        {
            get => Board.BackgroundImage;
            set
            {
                if (Board.BackgroundImage != value)
                {
                    Board.BackgroundImage = value;
                    OnPropertyChanged(nameof(BackgroundImage));
                }
            }
        }

        public bool IsStarted
        {
            get => Board.IsStarted;
            set
            {
                if (Board.IsStarted != value)
                {
                    Board.IsStarted = value;
                    OnPropertyChanged(nameof(IsStarted));
                }
            }
        }

        public ICommand PlayCommand { get; set; }

        public Brush BackgroundColor
        {
            get => Board.BackgroundColor;
            set
            {
                if (!Equals(Board.BackgroundColor, value))
                {
                    Board.BackgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public Player CurrentPlayer
        {
            get => Board.CurrentPlayer;
            set
            {
                if (Board.CurrentPlayer != value)
                {
                    Board.CurrentPlayer = value;
                    OnPropertyChanged(nameof(CurrentPlayer));
                }
            }
        }

        public void Dispose()
        {
            Board?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

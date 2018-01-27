using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello.ViewsModels
{
    public class BoardViewModel : IViewModel
    {
        private Visibility winGifVisibility;
        private string winnerName;
        private Visibility winNameVisibility;
        private bool gameFinished;

        public BoardViewModel()
        {
            Board = new Board();
            PlayCommand = new RelayCommand(_param => PlayMove((int)_param), _param => IsStarted && IsCreated);
            RibbonItems = new ObservableCollection<AbstractRibbonItem>
            {
                new RibbonButtonItem("NEW", "../Images/game.png", new RelayCommand(_param => NewGame(), _param => true)),
                new RibbonButtonItem("PLAY", "../Images/start.png", new RelayCommand(_param => StartGame(), _param => IsCreated && !IsStarted && !gameFinished)),
                new RibbonButtonItem("STOP", "../Images/pause.png", new RelayCommand(_param => StopGame(),  _param => IsCreated && IsStarted)),
                new RibbonSplitItem(),
                new RibbonButtonItem("SAVE", "../Images/save.png", new RelayCommand(_param => Save(), _param => IsCreated)),
                new RibbonButtonItem("OPEN", "../Images/load.png", new RelayCommand(_param => Load()))
            };
            WinGifVisibility = WinNameVisibility = Visibility.Collapsed;
            WinnerName = "";
            gameFinished = false;
            PlayerPassName = "";
        }

        private void PlayMove(int _index)
        {
            Board.PlayMove(_index);
            if (Board.PlayerPassName != null && !Board.PlayerPassName.Equals(""))
            {
                PlayerPassName = Board.PlayerPassName;
            }
            CurrentPlayer = Board.CurrentPlayer;
            if (Board.IsEndGame())
            {
                gameFinished = true;
                Board.StopGame();
                string winner = Board.GetWinner();
                switch (winner)
                {
                    case "white":
                    case "black":
                        WinGifVisibility = Visibility.Visible;
                        WinnerName = winner;
                        break;
                }
                WinNameVisibility = Visibility.Visible;
            }
        }

        private void NewGame()
        {
            PlayerPassName = "";
            gameFinished = false;
            WinnerName = "";
            WinGifVisibility = WinNameVisibility = Visibility.Collapsed;
            Board.NewGame();
            IsCreated = true;
            CurrentPlayer = Board.CurrentPlayer;
            StartGame();
        }

        private void StartGame()
        {
            IsStarted = true;
            Board.StartGame();
        }

        private void ResumeGame()
        {
            IsStarted = true;
            Board.ResumeGame();
        }

        public void StopGame()
        {
            IsStarted = false;
            Board.StopGame();
        }

        private void EndGame()
        {
            Board.EndGame();
            IsCreated = false;
        }

        private void Save()
        {
            IsStarted = false;
            Board.Save();
        }

        private void Load()
        {
            IsStarted = false;
            IsCreated = true;
            Board.Load();
            BlackPlayer = Board.BlackPlayer;
            WhitePlayer = Board.WhitePlayer;
            CurrentPlayer = Board.CurrentPlayer;
        }

        public void UpdateBackgroundColor(Color _color)
        {
            BackgroundColor = new SolidColorBrush(_color);
        }

        public Board Board { get; set; }

        public string WinnerName
        {
            get => winnerName;
            set
            {
                winnerName = value;
                OnPropertyChanged(nameof(WinnerName));
            }
        }

        public string PlayerPassName
        {
            get => Board.PlayerPassName;
            set
            {
                Board.PlayerPassName = value;
                OnPropertyChanged(nameof(PlayerPassName));
            }
        }

        public Visibility WinNameVisibility
        {
            get => winNameVisibility;
            set
            {
                winNameVisibility = value;
                OnPropertyChanged(nameof(WinNameVisibility));
            }
        }

        public Visibility WinGifVisibility
        {
            get => winGifVisibility;
            set
            {
                winGifVisibility = value;
                OnPropertyChanged(nameof(WinGifVisibility));
            }
        }

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

        public ObservableCollection<AbstractRibbonItem> RibbonItems
        {
            get => Board.RibbonItems;
            set => Board.RibbonItems = value;
        }

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
                Board.IsStarted = value;
                OnPropertyChanged(nameof(IsStarted));
            }
        }

        public bool IsCreated
        {
            get => Board.IsCreated;
            set
            {
                Board.IsCreated = value;
                OnPropertyChanged(nameof(IsCreated));
            }
        }

        public Player BlackPlayer
        {
            get => Board.BlackPlayer;
            set => Board.BlackPlayer = value;
        }

        public Player WhitePlayer
        {
            get => Board.WhitePlayer;
            set => Board.WhitePlayer = value;
        }

        public ICommand PlayCommand { get; set; }

        public SolidColorBrush BackgroundColor
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
                Board.CurrentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
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

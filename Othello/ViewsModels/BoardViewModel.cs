using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello.ViewsModels
{
    public class BoardViewModel : IViewModel
    {
        public BoardViewModel()
        {
            Board = new Board();
            PlayCommand = new RelayCommand(_param => Board.PlayMove((int)_param), _param => IsStarted && IsCreated);
            RibbonItems = new ObservableCollection<AbstractRibbonItem>
            {
                new RibbonButtonItem("NEW", "../Images/game.png", new RelayCommand(_param => NewGame(), _param => true)),
                new RibbonButtonItem("PLAY", "../Images/start.png", new RelayCommand(_param => StartGame(), _param => IsCreated && !IsStarted)),
                new RibbonButtonItem("STOP", "../Images/pause.png", new RelayCommand(_param => StopGame(),  _param => IsCreated && IsStarted)),
                new RibbonSplitItem(),
                new RibbonButtonItem("SAVE", "../Images/save.png", new RelayCommand(_param => Save(), _param => IsCreated)),
                new RibbonButtonItem("OPEN", "../Images/load.png", new RelayCommand(_param => Load()))
            };
        }
        private void NewGame()
        {
            Board.NewGame();
            IsCreated = true;
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

        private void StopGame()
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

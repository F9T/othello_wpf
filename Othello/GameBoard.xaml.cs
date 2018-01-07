using System;
using System.ComponentModel;
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
        private Player currentPlayer;

        public GameBoard()
        {
            InitializeComponent();
            DataContext = this;
            BackgroundColor = new SolidColorBrush(Colors.DarkGreen);
            UseBackgroundImage = false;
            BackgroundImage = "/Images/background.png";
            BlackPlayer = new Player(PawnColor.Black, "Black");
            WhitePlayer = new Player(PawnColor.White, "White");
            InitializeGame();
        }

        public void InitializeGame()
        {
            Board = new Board();
            Board.Reset(BlackPlayer, WhitePlayer);
            StartGame();//Remove after
        }

        public void StartGame()
        {
            BlackPlayer.Reset();
            WhitePlayer.Reset();
            CurrentPlayer = BlackPlayer;
            Board.GetLegalMove(CurrentPlayer);
            UpdateScore();
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            if (Board.GetLegalMove(CurrentPlayer).Count == 0)
            {
                CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            }
        }

        private void UpdateScore()
        {
            Board.UpdateScore(BlackPlayer);
            Board.UpdateScore(WhitePlayer);
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
                bool isPlayable = Board.IsPlayable(index, CurrentPlayer, true);
                if (isPlayable)
                {
                    Board.AddPawn(index, CurrentPlayer);
                    ChangePlayer();
                    UpdateScore();
                }
            }
        }

        public Player CurrentPlayer
        {
            get => currentPlayer;
            set
            {
                currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        public Player BlackPlayer { get; set; }

        public Player WhitePlayer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

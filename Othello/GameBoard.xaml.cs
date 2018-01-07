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

        public GameBoard()
        {
            InitializeComponent();
            DataContext = this;
            BackgroundColor = new SolidColorBrush(Colors.DarkGreen);
            UseBackgroundImage = false;
            BackgroundImage = "/Images/background.png";
            BlackPlayer = new Player(PawnColor.Black, "Black player");
            WhitePlayer = new Player(PawnColor.White, "White player");
            InitializeGame();
        }

        public void InitializeGame()
        {
            BlackPlayer.Reset();
            WhitePlayer.Reset();
            CurrentPlayer = BlackPlayer;
            Board = new Board();
            Board.Reset(BlackPlayer, WhitePlayer);
            Board.GetLegalMove(CurrentPlayer);
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            if (Board.GetLegalMove(CurrentPlayer).Count == 0)
            {
                CurrentPlayer = CurrentPlayer == WhitePlayer ? BlackPlayer : WhitePlayer;
            }
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
                }
            }
        }

        public Player CurrentPlayer { get; set; }

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

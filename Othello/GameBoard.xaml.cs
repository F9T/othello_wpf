using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Othello.Annotations;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour GameBoard.xaml
    /// </summary>
    public partial class GameBoard : INotifyPropertyChanged
    {
        private Board board;

        public GameBoard()
        {
            InitializeComponent();
            DataContext = this;
            Board = new Board();
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

        private void GameBoard_OnMouseMove(object _sender, MouseEventArgs _e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

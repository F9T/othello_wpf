using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Othello.Properties;
using Othello.ViewsModels;
using WpfAnimatedGif;

namespace Othello.Views
{
    /// <summary>
    /// Logique d'interaction pour GameBoardView.xaml
    /// </summary>
    public partial class GameBoardView : INotifyPropertyChanged
    {

        public GameBoardView()
        {
            InitializeComponent();
        }

        private void PassAnimation_OnCompleted(object _sender, EventArgs _e)
        {
            if (DataContext is BoardViewModel viewModel)
            {
                viewModel.PlayerPassName = "";
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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Othello.Properties;
using Othello.ViewsModels;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

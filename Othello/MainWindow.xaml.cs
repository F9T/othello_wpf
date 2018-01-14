using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Othello.Properties;
using Othello.ViewsModels;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        public MainViewModel MainViewModel { get; set; }

        private void View_OnSelectionChanged(object _sender, SelectionChangedEventArgs _e)
        {
            if (_sender is ListView view)
            {
                if (view.SelectedItem is ItemView)
                {
                    MainViewModel.ChangeView(((ItemView)view.SelectedItem).Name);
                }
            }
        }

        public void Dispose()
        {
            MainViewModel?.Dispose();
        }

        private void MainWindow_OnClosed(object _sender, EventArgs _e)
        {
            Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

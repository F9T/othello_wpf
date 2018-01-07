using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MahApps.Metro.Controls;
using Othello.Annotations;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private ItemView selectItemView;

        public MainWindow()
        {
            InitializeComponent();
            Views = new ObservableCollection<ItemView>
            {
                new ItemView("Images/game.png", new GameBoard()),
                new ItemView("Images/settings.png", new SettingUsercontrol())
            };
            DataContext = this;
        }

        public ItemView SelectItemView
        {
            get => selectItemView;
            set
            {
                selectItemView = value;
                OnPropertyChanged(nameof(SelectItemView));
            }
        }

        public ObservableCollection<ItemView> Views { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Othello.Views;

namespace Othello.ViewsModels
{
    public class MainViewModel : IViewModel
    {
        private IViewModel currentViewModel;

        public MainViewModel()
        {
            Views = new ObservableCollection<ItemView>
            {
                new ItemView("Game", "../Images/game.png"),
                new ItemView("Settings", "../Images/settings.png")
            };
            BoardViewModel = new BoardViewModel();
            CurrentViewModel = BoardViewModel;
        }

        public void ChangeView(string _name)
        {
            switch (_name.ToLower())
            {
                case "game":
                    CurrentViewModel = BoardViewModel;
                break;
                case "settings":
                    CurrentViewModel = new SettingsViewModel();
                break;
            }
        }

        public BoardViewModel BoardViewModel { get; set; }

        public ObservableCollection<ItemView> Views { get; set; }

        public ObservableCollection<RibbonItem> RibbonItems { get; }

        public IViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Othello.Models;

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
                new ItemView("Settings", "../Images/settings.png"),
                new ItemView("Quit", "../Images/quit.png")
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
                case "quit":
                    Application.Current.Shutdown(0);
                    break;
            }
        }

        public BoardViewModel BoardViewModel { get; set; }

        public ObservableCollection<ItemView> Views { get; set; }

        public ObservableCollection<RibbonItem> RibbonItems { get; set; }

        public IViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public void Dispose()
        {
            BoardViewModel?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
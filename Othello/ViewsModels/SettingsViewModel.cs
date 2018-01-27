using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Microsoft.Win32;
using Othello.Models.Ribbons;
using Othello.Pawns;

namespace Othello.ViewsModels
{
    public class SettingsViewModel : IViewModel
    {
        private Player whitePlayer;
        private Player blackPlayer;
        private Color backgroundColor;

        public SettingsViewModel(Player _whitePlayer, Player _blackPlayer, SolidColorBrush _backgroundColor)
        {
            WhitePlayer = _whitePlayer;
            BlackPlayer = _blackPlayer;
            BackgroundColor = _backgroundColor.Color;
            BrowseCommand = new RelayCommand(_param => Browse((PawnColor) _param), _param => true);
        }

        public RelayCommand BrowseCommand { get; set; }

        public void Browse(PawnColor _color)
        {
            string initDirectory = WhitePlayer.ImageSource;
            if (_color == PawnColor.Black)
            {
                initDirectory = BlackPlayer.ImageSource;
            }
            var file = new FileInfo(initDirectory);
            if (!file.Exists)
            {
                initDirectory = "";
            }
            var ofd = new OpenFileDialog
            {
                Filter = "PNG|*.png",
                InitialDirectory = initDirectory
            };
            ofd.ShowDialog();
            if (ofd.FileName.Length > 1)
            {
                if (_color == PawnColor.Black)
                {
                    BlackPlayer.ImageSource = ofd.FileName;
                }
                else
                {
                    WhitePlayer.ImageSource = ofd.FileName;
                }
            }
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public Player WhitePlayer
        {
            get => whitePlayer;
            set
            {
                whitePlayer = value;
                OnPropertyChanged(nameof(WhitePlayer));
            }
        }

        public Player BlackPlayer
        {
            get => blackPlayer;
            set
            {
                blackPlayer = value;
                OnPropertyChanged(nameof(BlackPlayer));
            }
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<AbstractRibbonItem> RibbonItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

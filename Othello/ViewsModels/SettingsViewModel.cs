﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello.ViewsModels
{
    public class SettingsViewModel : IViewModel
    {
        public ObservableCollection<AbstractRibbonItem> RibbonItems { get; set; }

        public void Dispose()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

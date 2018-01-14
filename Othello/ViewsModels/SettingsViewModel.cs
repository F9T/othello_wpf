using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Othello.ViewsModels
{
    public class SettingsViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        public ObservableCollection<RibbonItem> RibbonItems { get; }
    }
}

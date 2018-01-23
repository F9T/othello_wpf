using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Othello.Models.Ribbons;
using Othello.Ribbons;

namespace Othello.ViewsModels
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        ObservableCollection<AbstractRibbonItem> RibbonItems { get; set; }
    }
}

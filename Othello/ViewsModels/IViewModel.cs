using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Othello.ViewsModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<RibbonItem> RibbonItems { get; }
    }
}

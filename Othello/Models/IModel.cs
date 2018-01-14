using System.Collections.ObjectModel;

namespace Othello.Models
{
    public interface IModel
    {
        ObservableCollection<RibbonItem> RibbonItems { get; set; }

        void Initialize();
    }
}

using System.Windows.Input;
using Othello.Models.Ribbons;

namespace Othello.Ribbons
{
    public class RibbonButtonItem : AbstractRibbonItem
    {
        public RibbonButtonItem(string _name, string _imageSource, ICommand _command)
        {
            Name = _name;
            ImageSource = _imageSource;
            Action = _command;
        }

        public string ImageSource { get; set; }

        public string Name { get; set; }

        public ICommand Action { get; set; }
    }
}

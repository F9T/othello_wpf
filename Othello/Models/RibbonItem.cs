using System.Windows.Input;

namespace Othello.Models
{
    public class RibbonItem
    {
        public RibbonItem(string _name, string _imageSource, ICommand _command)
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

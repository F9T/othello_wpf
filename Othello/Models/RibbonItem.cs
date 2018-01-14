using System.Windows.Input;

namespace Othello
{
    public class RibbonItem
    {
        public RibbonItem(string _imageSource, ICommand _command)
        {
            ImageSource = _imageSource;
            Action = _command;
        }

        public string ImageSource { get; set; }

        public ICommand Action { get; set; }
    }
}

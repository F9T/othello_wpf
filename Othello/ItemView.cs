using System.Windows.Controls;

namespace Othello
{
    public class ItemView
    {
        public ItemView(string _imageSource, UserControl _view)
        {
            ImageSource = _imageSource;
            View = _view;
        }

        public string ImageSource { get; set; }

        public UserControl View { get; set; }
    }
}

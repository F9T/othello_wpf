namespace Othello
{
    public class ItemView
    {
        public ItemView(string _name, string _imageSource)
        {
            Name = _name;
            ImageSource = _imageSource;
        }

        public string Name { get; set; }

        public string ImageSource { get; set; }
    }
}

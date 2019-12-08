namespace prj3beer.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Brand // Added to Navigation Menu
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

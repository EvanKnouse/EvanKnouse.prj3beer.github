using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Brand
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

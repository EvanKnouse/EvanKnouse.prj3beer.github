using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Models
{
    public enum MenuItemType
    {
        Status,
        Beverage
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

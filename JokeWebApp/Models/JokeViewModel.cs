using System;
using System.Collections.Generic;

namespace JokesWebApp.Models
{
    public class SideMenuViewModel
    {
        public ICollection<string> Categories { get; set; } = new List<string>();
    }
}

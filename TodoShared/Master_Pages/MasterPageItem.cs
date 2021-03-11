using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Master_Pages
{
    public class MasterPageItem
    {
        public MasterPageItem()
        {
        }
        public string IconSource { get; set; }

        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Utilities.Dtos
{
    public class GroupedData
    {
        public GroupedData()
        {
            children = new List<ChildItem>();
        }

        public string text { get; set; }

        public List<ChildItem> children { get; set; }
    }

    public class ChildItem
    {
        public string id { get; set; }

        public string text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Model
{
    public class MenuModel
    {

        public class MenuDisVision
        {
            public List<MenuItemData> MenuItems { get; set; }
            public bool Overlap { get; set; }//是否能叠带、复选
        }

        public class MenuItemData
        {
            public string ItemName { get; set; }//节点名称
            public string ItemValue { get; set; }//节点内容
        }
    }
}

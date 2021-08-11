using BigPig.Metro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Metro.Model
{
    public class MenuModel
    {
        public class MenuData: UserViewModel
        {
            public string ItemName { get; set; }//节点名称
            public string ItemValue { get; set; }//节点内容

            //UI效果
            private int _PanelWidth;
            private string _PanelColor;
            public int PanelWidth { get => _PanelWidth; set => SetProperty(ref _PanelWidth, value); }
            public string PanelColor { get => _PanelColor; set => SetProperty(ref _PanelColor, value); }
        }
    }
}

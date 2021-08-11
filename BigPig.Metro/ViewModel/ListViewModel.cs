using BigPig.Metro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Metro.ViewModel
{
    public class ListViewModel: UserViewModel
    {
        private List<ListModel> _ListData = new List<ListModel>() { new ListModel() { ImagePath = "https://img.hltongchen.com/upload/pic/2020-01-30/15803200920.jpg", Name = "霸道总裁轻点爱", NewAnthology = "第168话大病初愈", Update = "2021-08-11 " } };
        public List<ListModel> ListData
        {
            get => _ListData;
            set
            {
                _ListData = value;
                RaisePropertyChanged("ListData");
            }
        }
    }
}

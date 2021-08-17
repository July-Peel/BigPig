using BigPig.Metro.ItemControl;
using BigPig.Metro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BigPig.Metro.ViewModel
{
    public class ListViewModel: UserViewModel
    {
        public static MainViewModel main = null;

        public ListViewModel(MainViewModel Ma)
        {
            main = Ma;
        }




        private List<ListModel> _ListData = new List<ListModel>();
        public List<ListModel> ListData
        {
            get => _ListData;
            set
            {
                _ListData = value;
                RaisePropertyChanged("ListData");
            }
        }

        private List<string> _PageList = new List<string>();
        public List<string> PageList
        {
            get => _PageList;
            set
            {
                _PageList = value;
                RaisePropertyChanged("PageList");
            }
        }

        private string _SelectPage = string.Empty;
        public string SelectPage { get => _SelectPage; set { _SelectPage = value; RaisePropertyChanged("SelectPage"); } }

        private int _PageNum = 66;
        public int PageNum { get => _PageNum; set { _PageNum = value; RaisePropertyChanged("PageNum"); } }

        private string _PageTxt= "共6523条数据";
        public string PageTxt { get => _PageTxt; set { _PageTxt = value; RaisePropertyChanged("PageTxt"); } }


        public ICommand WindowsLoad => new AnotherCommand(_WindowsLoad);
        private void _WindowsLoad(object obj)
        {
            try
            {
                Task.Factory.StartNew(delegate {

                    Thread.Sleep(500);
                    Assembly assm = Assembly.GetExecutingAssembly();
                    Stream istr = assm.GetManifestResourceStream("BigPig.Metro.Static.bainianlist.json");
                    StreamReader sr = new System.IO.StreamReader(istr);
                    string str = sr.ReadToEnd();
                    sr.Dispose();
                    sr.Close();
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ListData = JsonConvert.DeserializeObject<List<ListModel>>(str);

                        for (int i = 1; i <= PageNum; i++)
                        {
                            PageList.Add($"第{i}页");
                        }

                        SelectPage = "第1页";
                    });
                });
            }
            catch
            {
            }
        }


        public ICommand WatchClcik => new AnotherCommand(_WatchClcik);
        private void _WatchClcik(object obj)
        {
            try
            {
                ListModel m = (ListModel)obj;
                main.UserContentWatch = new AnthologyControl() { DataContext = new AnthologyViewModel() };
                main.OpenWatch = true;
            }
            catch { }
        }
    }
}

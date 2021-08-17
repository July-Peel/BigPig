using BigPig.Metro.ItemControl;
using BigPig.Metro.Unit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static BigPig.Metro.Model.MenuModel;

namespace BigPig.Metro.ViewModel
{
    public class MainViewModel : UserViewModel
    {

        #region 界面交互

        private UserControl _UserContentMenu;//菜单
        public UserControl UserContentMenu
        {
            get => _UserContentMenu;
            set
            {
                _UserContentMenu = value;
                RaisePropertyChanged("UserContentMenu");
            }
        }

        private UserControl _UserContentList;//内容列表
        public UserControl UserContentList
        {
            get => _UserContentList;
            set
            {
                _UserContentList = value;
                RaisePropertyChanged("UserContentList");
            }
        }

        private UserControl _UserContentAnthology;//集数列表
        public UserControl UserContentAnthology
        {
            get => _UserContentAnthology;
            set
            {
                _UserContentAnthology = value;
                RaisePropertyChanged("UserContentAnthology");
            }
        }

        private UserControl _UserContentWatch;//请观赏
        public UserControl UserContentWatch
        {
            get => _UserContentWatch;
            set
            {
                _UserContentWatch = value;
                RaisePropertyChanged("UserContentWatch");
            }
        }

        private bool _OpenMenu = false;
        public bool OpenMenu
        {
            get => _OpenMenu;
            set
            {
                _OpenMenu = value;
                if (!value) UserContentMenu = null;
                RaisePropertyChanged("OpenMenu");
            }
        }

        private bool _OpenList = false;
        public bool OpenList
        {
            get => _OpenList;
            set
            {
                _OpenList = value;
                if (!value) UserContentList = null;
                RaisePropertyChanged("OpenList");
            }
        }

        private bool _OpenAnthology = false;
        public bool OpenAnthology
        {
            get => _OpenAnthology;
            set
            {
                _OpenAnthology = value;
                if (!value) UserContentAnthology = null;
                RaisePropertyChanged("OpenAnthology");
            }
        }

        private bool _OpenWatch = false;
        public bool OpenWatch
        {
            get => _OpenWatch;
            set
            {
                _OpenWatch = value;
                if (!value)
                {
                    UserContentWatch = null;
                }
                RaisePropertyChanged("OpenWatch");
            }
        }
        #endregion



        public ICommand WindowsLoad => new AnotherCommand(_WindowsLoad);
        private void _WindowsLoad(object obj)
        {
            try
            {
                //Task.Factory.StartNew(delegate { GetinitializeControl(); });
            }
            catch
            {
            }
        }

        public ICommand openMenuClick => new AnotherCommand(_openMenuClick);
        private void _openMenuClick(object obj)
        {
            try
            {

                BainianComics.GetSearchAnthology("/comic/16085.html");

                //UserContentMenu = new MenuControl() { DataContext = new MenuViewModel(this) };
                //OpenMenu = true;
            }
            catch
            {
            }
        }

    }
}

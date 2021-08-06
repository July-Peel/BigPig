using MaterialDesignThemes.Wpf.Transitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static BigPig.Model.MenuModel;

namespace BigPig.ViewModel
{
    public class MenuViewModel : UserViewModel
    {
        private static Transitioner Tran = null;

        private List<MenuDisVision> _Menus;
        public List<MenuDisVision> Menus
        {
            get => _Menus;
            set
            {
                _Menus = value;
                RaisePropertyChanged("Menus");
            }
        }


        public MenuViewModel()
        {
            Task.Factory.StartNew(delegate
            {
                Thread.Sleep(500);
                Assembly assm = Assembly.GetExecutingAssembly();
                Stream istr = assm.GetManifestResourceStream("BigPig.Static.bainian.json");
                StreamReader sr = new System.IO.StreamReader(istr);
                string str = sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                App.Current.Dispatcher.Invoke(delegate {
                    Menus = JsonConvert.DeserializeObject<List<MenuDisVision>>(str);
                });

            });
        }

        public ICommand MenuLefteClick => new AnotherCommand(_MenuLefteClick);
        private void _MenuLefteClick(object obj)
        {
            try
            {





                MessageBox.Show("6666");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public ICommand OpenNext => new AnotherCommand(_OpenNext);
        private void _OpenNext(object obj)
        {
            if (Tran.SelectedIndex == 0) Tran.SelectedIndex = 1;
            else if (Tran.SelectedIndex == 1) Tran.SelectedIndex = 2;
            else Tran.SelectedIndex = 0;

        }
    }
}

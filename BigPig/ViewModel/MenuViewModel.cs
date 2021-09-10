using BigPig.ItemControl;
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
using static BigPig.Model.MenuModel;

namespace BigPig.ViewModel
{
    public class MenuViewModel : UserViewModel
    {
        private List<MenuData> _Menus;
        public List<MenuData> Menus
        {
            get => _Menus;
            set
            {
                _Menus = value;
                RaisePropertyChanged("Menus");
            }
        }


        public static MainViewModel main = null;


        private List<int> PanelWidths = new List<int>() { 100, 150, 200, 250 };
        private List<string> PanelColor = new List<string>() { "#008080", "#298EDD", "#696969", "#D2691E", "#008000", "#d43907", "#009688", "#5FB878", "#393D49", "#1E9FFF", "#FFB800", "#FF5722" };


        public MenuViewModel(MainViewModel Ma)
        {
            main = Ma;
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
                    Menus = JsonConvert.DeserializeObject<List<MenuData>>(str);


                    Menus.ForEach(a =>
                    {
                        a.PanelColor = RandomColor();

                        if (a.ItemName.Length > 3)
                        {
                            a.PanelWidth = 250;
                        }
                        else
                        {
                            a.PanelWidth = RandomWidth();
                        }

                    });

                });

            });
        }

        private int RandomWidth()
        {
            int index = new Random().Next(0, 4);
            return PanelWidths[index];
        }
        private string RandomColor()
        {
            int index = new Random().Next(0, 12);
            return PanelColor[index];
        }

        public ICommand OpenNext => new AnotherCommand(_OpenNext);
        private void _OpenNext(object obj)
        {
            string pageKey = (string)obj;
            main.UserContentList = new ListControl() { DataContext = new ListViewModel(main, pageKey) };
            main.OpenList = true;
        }
    }
}

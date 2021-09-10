using BigPig.ItemControl;
using BigPig.Model;
using BigPig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BigPig.ViewModel
{
    public class AnthologyViewModel : UserViewModel
    {
        private AnthologyModel _Anthology = new AnthologyModel();
        public AnthologyModel Anthology
        {
            get => _Anthology;
            set
            {
                _Anthology = value;
                RaisePropertyChanged("Anthology");
            }
        }


        private string ConPath = string.Empty;
        private static MainViewModel main;
        public AnthologyViewModel(MainViewModel m, string ManhuaPath)
        {
            ConPath = ManhuaPath;
            main = m;
        }


        public ICommand WindowsLoad => new AnotherCommand(_WindowsLoad);
        private void _WindowsLoad(object obj)
        {
            try
            {
                Task.Factory.StartNew(delegate {

                    Thread.Sleep(500);
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        Anthology = BainianComics.GetSearchAnthology(ConPath);
                    });
                });
            }
            catch
            {
            }
        }

        public ICommand OpenWatch => new AnotherCommand(_OpenWatch);
        private void _OpenWatch(object obj)
        {
            try
            {
                string manhuaImgPath = (string)obj;
                main.UserContentWatch = new WatchControl() { DataContext = new WatchViewModel(manhuaImgPath) };
                main.OpenWatch = true;
            }
            catch
            {
            }
        }
    }
}

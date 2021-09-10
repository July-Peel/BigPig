using BigPig.Unit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BigPig.ViewModel
{
    public class WatchViewModel : UserViewModel
    {
        private string ImgPage = string.Empty;
        public WatchViewModel(string iurl)
        {
            ImgPage = iurl;
        }


        ObservableCollection<string> _ImageList = new ObservableCollection<string>();
        public ObservableCollection<string> ImageList
        {
            get => _ImageList;
            set
            {
                _ImageList = value;
                RaisePropertyChanged("ImageList");
            }
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
                        List<string> iamges = BainianComics.GetSearchManhuaImage(ImgPage);
                        iamges.ForEach(a => ImageList.Add(a));
                    });
                });
            }
            catch
            {
            }
        }

    }
}

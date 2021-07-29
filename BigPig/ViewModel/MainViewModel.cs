using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BigPig.ViewModel
{
    public class MainViewModel: UserViewModel
    {
        private UserControl _UserContentOne = new ContentControl();
        public UserControl UserContentOne
        {
            get => _UserContentOne;
            set
            {
                _UserContentOne = value;
                RaisePropertyChanged("UserContentOne");
            }
        }
        private UserControl _UserContentTwo = new MenuControl();
        public UserControl UserContentTwo
        {
            get => _UserContentTwo;
            set
            {
                _UserContentTwo = value;
                RaisePropertyChanged("UserContentTwo");
            }
        }
        public ICommand OpenNext => new AnotherCommand(_OpenNext);
        private void _OpenNext(object obj)
        {
            Transitioner T = (Transitioner)obj;
            if (T.SelectedIndex == 0) T.SelectedIndex = 1;
            else if (T.SelectedIndex == 1) T.SelectedIndex = 2;
            else T.SelectedIndex = 0;

        }
    }
}

using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BigPig.ViewModel
{
    public class ContentViewModel : UserViewModel
    {
        private static Transitioner Tran = null;
        public ContentViewModel(Transitioner T)
        {
            Tran = T;
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

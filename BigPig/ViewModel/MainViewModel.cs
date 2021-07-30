﻿using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BigPig.ViewModel
{
    public class MainViewModel: UserViewModel
    {
        private UserControl _UserContentOne;
        public UserControl UserContentOne
        {
            get => _UserContentOne;
            set
            {
                _UserContentOne = value;
                RaisePropertyChanged("UserContentOne");
            }
        }
        private UserControl _UserContentTwo;
        public UserControl UserContentTwo
        {
            get => _UserContentTwo;
            set
            {
                _UserContentTwo = value;
                RaisePropertyChanged("UserContentTwo");
            }
        }
        private static Transitioner Tran = null;
        public ICommand WindowsLoad => new AnotherCommand(_WindowsLoad);
        private void _WindowsLoad(object obj)
        {
            try
            {
                Tran = (Transitioner)obj;
                Task.Factory.StartNew(delegate { GetinitializeControl(); });
            }
            catch
            {
            }
        }
        private void GetinitializeControl()
        {
            Thread.Sleep(500);
            App.Current.Dispatcher.Invoke(delegate
            {
                UserContentOne= new ContentControl() { DataContext = new ContentViewModel(Tran) };
                UserContentTwo= new MenuControl() { DataContext = new MenuViewModel(Tran) };
            });
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

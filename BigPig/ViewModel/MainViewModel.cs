﻿using BigPig.Unit;
using MaterialDesignThemes.Wpf.Transitions;
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

        //打开漫画主页
        public ICommand OpenComics => new AnotherCommand(_OpenComics);
        private void _OpenComics(object obj)
        {
            new CoCoComics().GetSearchItem();
        }
        //搜索
        public ICommand OpenSearch => new AnotherCommand(_OpenSearch);
        private void _OpenSearch(object obj)
        {



        }
        //打开收藏
        public ICommand OpenCollect => new AnotherCommand(_OpenCollect);
        private void _OpenCollect(object obj)
        {

        }
        //打开历史浏览
        public ICommand OpenHistory => new AnotherCommand(_OpenHistory);
        private void _OpenHistory(object obj)
        {

        }
    }
}

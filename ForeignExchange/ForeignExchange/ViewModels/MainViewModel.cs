﻿using ForeignExchange.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ForeignExchange.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public string  Amount { get; set; }
        public ObservableCollection<Rate> Rates { get; set; }
        public Rate SourceRate { get; set; }
        public Rate TargetRate { get; set; }
        public bool IsRunning { get; set; }
        public bool IsEnabled { get; set; }
        public string Result { get; set; }
        #endregion

        public MainViewModel()
        {
        }
        #region Commands
        public ICommand ConvertCommand
        {
            get
            {
                return new RelayCommand(Convert);
            }
        }
        private void Convert()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
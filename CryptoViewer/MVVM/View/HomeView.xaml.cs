﻿using CryptoViewer.MVVM.Model;
using CryptoViewer.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoViewer.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {          
           InitializeComponent();

    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            mainWindow.rbInfo.IsChecked = true;
            mainWindow.rbInfo.Command.Execute(null);
            mainWindow.rbInfo.IsEnabled = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(searchBox.Text))
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow.rbInfo.IsChecked = true;
                mainWindow.rbInfo.Command.Execute(null);
                mainWindow.rbInfo.IsEnabled = true;
            }
        }
    }
}

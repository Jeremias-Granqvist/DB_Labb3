﻿using DB_Labb3.Viewmodel;
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
using System.Windows.Shapes;

namespace DB_Labb3.DialogWindows
{
    /// <summary>
    /// Interaction logic for RemoveCategoryDialogWindow.xaml
    /// </summary>
    public partial class RemoveCategoryDialogWindow : Window
    {
        public RemoveCategoryDialogWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            this.DataContext = mainWindowViewModel;

        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication.Demonstrators
{
    /// <summary>
    /// Interaction logic for Experimenter.xaml
    /// </summary>
    public partial class Experimenter : UserControl
    {
        public Experimenter()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void _buttonClangeFaceTextColor_Click(object sender, RoutedEventArgs e)
        {
            BoundObject bo = DataContext as BoundObject;
            bo.FaceTextColor = Colors.Blue;
        }
    }
}
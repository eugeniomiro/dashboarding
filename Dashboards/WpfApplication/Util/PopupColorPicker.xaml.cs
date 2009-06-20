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

namespace WpfApplication.Util
{
    /// <summary>
    /// Interaction logic for PopupColorPicker.xaml
    /// </summary>
    public partial class PopupColorPicker : UserControl
    {

        public static PopupColorPicker Instance { get; set; }

        public PopupColorPicker()
        {
            InitializeComponent();
            _picker.ColorChanged += PickerColorChanged;
        }

        private Color _selectedColor;

        public Color SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                _selectedColor = value;
                _picker.BaseHue = new SolidColorBrush(value);
            }
        }

        void PickerColorChanged(object sender, ColorPickerControl.ColorChangedEventArgs e)
        {
            _selectedColor = e.newColor.Color;
        }

        public event EventHandler<EventArgs> Closed;

        private void OnClose()
        {
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            OnColorSelected(_picker.SelectedColor.Color);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            if (ColorChangedEvent != null)
            {
                ColorChangedEvent -= _lastMethod;
            }
        }


        public delegate void ColorChangedEventHandler(object sender, ColorSelectedEventargs e);

        private event ColorChangedEventHandler ColorChangedEvent;


        public event ColorChangedEventHandler ColorChanged
        {
            add { ColorChangedEvent += value; _lastMethod = value; }
            remove { ColorChangedEvent -= value; }
        }

        private ColorChangedEventHandler _lastMethod;
        protected virtual void OnColorSelected(Color c)
        {
            if (ColorChangedEvent != null)
            {
                ColorChangedEvent(this, new ColorSelectedEventargs { Color = c });
                ColorChangedEvent -= _lastMethod;
            }
        }


        internal void Show()
        {
            this.Visibility = Visibility.Visible;
        }
    }
}

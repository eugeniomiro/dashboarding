using System.Windows;
using System.Windows.Controls;
using Demos.Common.Util;

namespace Demos.Common.Demonstrators
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


        private void _buttonClangeFaceTextColor_Click(object sender, RoutedEventArgs e)
        {
            BoundObject bo = DataContext as BoundObject;
            PopupColorPicker.Instance.SelectedColor = bo.FaceTextColor;
            PopupColorPicker.Instance.ColorChanged += new PopupColorPicker.ColorChangedEventHandler(FaceColorChanged);
            PopupColorPicker.Instance.Show();
        }

        void FaceColorChanged(object sender, ColorSelectedEventargs e)
        {
            BoundObject bo = DataContext as BoundObject;
            bo.FaceTextColor = e.Color;
        }


        private void _buttonClangeValueTextColor_Click(object sender, RoutedEventArgs e)
        {
            BoundObject bo = DataContext as BoundObject;
            PopupColorPicker.Instance.SelectedColor = bo.ValueTextColor;
            PopupColorPicker.Instance.ColorChanged += new PopupColorPicker.ColorChangedEventHandler(ValueColorChanged);
            PopupColorPicker.Instance.Show();
        }

        void ValueColorChanged(object sender, ColorSelectedEventargs e)
        {
            BoundObject bo = DataContext as BoundObject;
            bo.ValueTextColor = e.Color;
        }
    }
}

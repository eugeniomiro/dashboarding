﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication.SingleGuages
{
    public partial class TopStrip : UserControl
    {
        int pos = 0;
        private Random _random = new Random();
        private Storyboard _timer = new Storyboard();

        public TopStrip(IDictionary<string, string> parameters)
        {
            InitializeComponent();
            _timer.Duration = new Duration(new TimeSpan(0, 0, 1));
            _timer.Completed += new EventHandler(timer_Completed);
            Loaded += new RoutedEventHandler(IAmLoaded);
        }

        /// <summary>
        /// On load show value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IAmLoaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        /// <summary>
        /// Timer has ticked
        /// </summary>
        void timer_Completed(object sender, EventArgs e)
        {
            SetValues();
        }

        /// <summary>
        /// Set the value and display
        /// </summary>
        private void SetValues()
        {
            Animate();
            _timer.Begin();

        }



        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected void Animate()
        {
            if (pos == 0)
            {
                _c1.Value = _random.Next(100);
                _c3.Value = _random.Next(100);
                _c4.Value = _random.Next(100);


                _prog1.Value = _random.Next(100);

                _slider.Value = _random.Next(100);
             

            }
            _c2.Value = _random.Next(100);
            _c12.Value = _random.Next(100);
            _c13.Value = _random.Next(100);


            _perf.Value = _random.Next(100);


            _o1.Value = _random.Next(100);


            string  t = DateTime.Now.ToString("hhmmsstt");

            _sH1.DisplayCharacter = "" + t[0];
            _sH2.DisplayCharacter = "" + t[1];
            _sM1.DisplayCharacter = "" + t[2];
            _sM2.DisplayCharacter = "" + t[3];
            _sS1.DisplayCharacter = "" + t[4];
            _sS2.DisplayCharacter = "" + t[5];
            _sD1.DisplayCharacter = "" + t[6];
            _sD2.DisplayCharacter = "" + t[7];

            _ther2.Value = _random.Next(100);
            _wall1.Value = _random.Next(100);
            _odometer.Increment();
            

            pos++;
            if (pos == 4)
                pos = 0;
        }

       
    }
}

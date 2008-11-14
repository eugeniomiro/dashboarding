using System;
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
    public partial class TopStrip : SingleGauge
    {
        int pos = 0;
        public TopStrip(IDictionary<string, string> parameters): base(parameters)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the control according the the current value
        /// </summary>
        protected override void Animate()
        {
            if (pos == 0)
            {
                _c1.Value = Random.Next(100);
                _c3.Value = Random.Next(100);
                _c4.Value = Random.Next(100);


                _prog1.Value = Random.Next(100);
                _prog2.Value = Random.Next(100);
             

            }
            _c2.Value = Random.Next(100);
            _c12.Value = Random.Next(100);
            _c13.Value = Random.Next(100);
           
            
            _perf.Value = Random.Next(100);


            _o1.Value = Random.Next(100);
            _o2.Value = Random.Next(100);


            string  t = DateTime.Now.ToString("hhmmsstt");

            _sH1.DisplayCharacter = "" + t[0];
            _sH2.DisplayCharacter = "" + t[1];
            _sM1.DisplayCharacter = "" + t[2];
            _sM2.DisplayCharacter = "" + t[3];
            _sS1.DisplayCharacter = "" + t[4];
            _sS2.DisplayCharacter = "" + t[5];
            _sD1.DisplayCharacter = "" + t[6];
            _sD2.DisplayCharacter = "" + t[7];

            _ther2.Value = Random.Next(100);
            _wall1.Value = Random.Next(100);
            _odometer.Increment();
            

            pos++;
            if (pos == 4)
                pos = 0;
        }

       
    }
}

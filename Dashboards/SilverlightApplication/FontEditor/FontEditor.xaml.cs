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
using Codeplex.Dashboarding;
using System.Text;

namespace SilverlightApplication.FontEditor
{
    public partial class FontEditor : UserControl
    {
        public FontEditor()
        {
            InitializeComponent();

            _rhp.LetterSelected += new EventHandler<LetterSelectedEventArgs>(LetterSelected);
        }

        void LetterSelected(object sender, LetterSelectedEventArgs e)
        {
            _giant.SetLedsFromCharacter(e.Letter, e.MatrixLedCharacter);
        }

        private void _save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            List<string> letters = new List<string>();
            letters.AddRange(MatrixLedCharacterDefintions.CharacterDefintions.Keys);
            foreach (string letter in letters)
            {
             

                sb.AppendFormat("{{ \"{0}\", new byte [] {{ {1},{2},{3},{4},{5},0}} }},",
                    (letter != "\"") ? letter : "\\\"",
                    MatrixLedCharacterDefintions.GetDefintion(letter)[0],
                    MatrixLedCharacterDefintions.GetDefintion(letter)[1],
                    MatrixLedCharacterDefintions.GetDefintion(letter)[2],
                    MatrixLedCharacterDefintions.GetDefintion(letter)[3],
                    MatrixLedCharacterDefintions.GetDefintion(letter)[4]
                    );
                sb.AppendLine();
            }
            int t = 0;
            _tb.Text = sb.ToString();
            _codePopup.IsOpen = true;
            
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            _codePopup.IsOpen = false;
        }
    }
}

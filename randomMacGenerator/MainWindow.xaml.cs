using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComboBox = System.Windows.Controls.ComboBox;

namespace randomMacGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly List<Char> _avaiableChars = new List<char>(16);
        private readonly List<string> separatorsList = new List<string>();
        public MainWindow()
        {
            separatorsList.Add("");
            separatorsList.Add("-");
            separatorsList.Add(":");
            for (int i = 0; i < 10; i++)
            {
                _avaiableChars.Add((char) ('0' + i));
            }
            ToUpper();
            InitializeComponent();

            //Initilize
            Button_Click(new object(), new RoutedEventArgs());
            Separators.ItemsSource = separatorsList;
            ItemCases.Items.Add("Upper");
            ItemCases.Items.Add("Lower");
            ItemCases.SelectionChanged += (sender, args) =>
            {
                if((string)ItemCases.SelectedItem == "Upper")
                    ToUpper();
                else if ((string)ItemCases.SelectedItem == "Lower")
                    ToLower();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            macOutput.Text = "";
            do
            {
                for (int i = 0; i < 12; i++)
                {
                    macOutput.Text += _avaiableChars.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    if (i % 2 == 1 && i != 11)
                        macOutput.Text += Separators.Text;
                }
            } while (macOutput.Text.Count(f => f == 'f' || f == 'F') == 12); // ff:ff:ff:ff:ff:ff elimination
        }

        private void ToLower()
        {
            CleanLetters();
            for (int i = 0; i < 6; i++)
            {
                _avaiableChars.Add((char)('a' + i));
            }
        }

        private void CleanLetters()
        {
            if(_avaiableChars.Count == 16)
                for (int i = 0; i < 6; i++)
                {
                    _avaiableChars.RemoveAll(x => x == _avaiableChars.Last());
                }
        }

        private void ToUpper()
        {
            CleanLetters();
            for (int i = 0; i < 6; i++)
            {
                _avaiableChars.Add((char)('A' + i));
            }
        }
    }
}

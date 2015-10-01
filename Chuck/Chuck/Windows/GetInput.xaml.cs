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

namespace Chuck.Windows
{
    /// <summary>
    /// Interaction logic for GetInput.xaml
    /// </summary>
    public partial class GetInput
    {
        public string Input { get; set; }

        public GetInput(string inputName)
        {
            InitializeComponent();

            lblName.Content = inputName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input = txtInput.Text;
            Close();
        }
    }
}

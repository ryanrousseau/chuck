using System.Collections.Generic;
using System.Windows;
using Chuck.Contexts;
using Chuck.Models;

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowContext();

            //: Initial create items, e.g., folder structure?
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var list = new List<string>();
            list.Add("test");
            list.Add("chuck testa");

            var t = new TestDetails(new TestDetailsModel("Super Awesome Test 1","Test", "exec Testarino", list));
            t.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = new TestPlanDetails();
            t.ShowDialog();
        }
    }
}

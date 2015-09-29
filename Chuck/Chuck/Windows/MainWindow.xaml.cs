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
            //var t = new TestPlanDetails();
            //t.ShowDialog();
            //: Will be removed, just using for testing
            var tests = new List<TestDetailsModel>();

            var tags = new List<string>();
            tags.Add("fdasdsag");
            tags.Add("dsaf");

            tests.Add(new TestDetailsModel("Chuck Testa", "Chuck/ChuckTesta.cs", "insert scripty stuff here\n\tblahblah formats blah...",tags));
            tests.Add(new TestDetailsModel("The Throne", "Chuck/Otherwis.cs", "insert scripty RAWR! stuff here\n\tblahblah formats blah...", tags));

            var model = new TestPlanDetailsModel("Focus the rage", tests);
            var dialog = new TestPlanDetails(model);
            dialog.ShowDialog();
        }
    }
}

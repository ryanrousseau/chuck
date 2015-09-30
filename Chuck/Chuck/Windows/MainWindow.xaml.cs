using System.Collections.Generic;
using System.Windows;
using Chuck.Contexts;
using Chuck.Models;
using System.Linq;
using Chuck.Core.Git;
using Chuck.Core.Git.Github;
using Chuck.Helpers;
using LibGit2Sharp;

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
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var list = new List<string>();
            list.Add("test");
            list.Add("chuck testa");

            var t = new TestDetails(new TestDetailsModel("Super Awesome Test 1","Test", @"
    I.Open(""http://knockoutjs.com/examples/cartEditor.html"");
    I.Select(""Motorcycles"").From("".liveExample tr select:eq(0)""); // Select by value/text
    I.Select(2).From("".liveExample tr select:eq(1)""); // Select by index
    I.Enter(6).In("".liveExample td.quantity input:eq(0)"");
    I.Expect.Text(""$197.70"").In("".liveExample tr span:eq(1)"");

    // add second product
    I.Click("".liveExample button:eq(0)"");
    I.Select(1).From("".liveExample tr select:eq(2)"");
    I.Select(4).From("".liveExample tr select:eq(3)"");
    I.Enter(8).In("".liveExample td.quantity input:eq(1)"");
    I.Expect.Text(""$788.64"").In("".liveExample tr span:eq(3)"");

    // validate totals
    I.Expect.Text(""$986.34"").In(""p.grandTotal span"");

    // remove first product
    I.Click("".liveExample a:eq(0)"");

    // validate new total
    I.WaitUntil(() => I.Expect.Text(""$788.64"").In(""p.grandTotal span""));

", list));
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

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            (new Settings()).ShowDialog();
        }

        private void rectSync_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Saf_OnClick(object sender, RoutedEventArgs e)
        {
            var credentials = GitHelper.GetGitCredentials();

            var ri = new RepositoryInfo
            {
                Name = "ChuckTesta",
                ProjectName = "ChuckTestaScripts"
            };

            var gh = new Github(ri);

            //: gh->Add() 
            //: Status: Working!
            foreach (var entry in gh.Status())
            {
                gh.Add(entry.Key);
            }

            //: gh->Commit() 
            if(gh.Status().Any(t => t.Value != FileStatus.Staged))
                gh.Commit();

            //: gh->Pull() 
            //: Status: Working!
            gh.Pull(credentials);

            //: gh->Push() 
            //: Status: Working!
            var result = gh.Push(credentials);
            if (result != string.Empty && result.Contains("401"))
            {
                //:not the best way to handle this.
                GitHelper.NotifyBadCredentials();
                GitHelper.GetGitCredentials(true);
            }
        }
    }
}

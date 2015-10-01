using System.Collections.Generic;
using System.Windows;
using Chuck.Contexts;
using Chuck.Models;
using System.Linq;
using System.Windows.Input;
using Chuck.Core.Git;
using Chuck.Core.Git.Github;
using Chuck.Helpers;
using LibGit2Sharp;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Controls;

//: holy mess of a file batman

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    ///     Icons used on this page: https://www.iconfinder.com/iconsets/technology-and-hardware-2
    /// </summary>
    public partial class MainWindow
    {
        private IList<RepositoryInfo> _Repositories;

        public MainWindow()
        {
            InitializeComponent();
            LoadRepositoryJSON();
            LoadRepositories();
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

", list), (RepositoryInfo)cbProjects.SelectedItem);
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
            var dialog = new TestPlanDetails(model, (RepositoryInfo)cbProjects.SelectedItem);
            dialog.ShowDialog();
        }

        private void LoadRepositoryJSON()
        {
            if(File.Exists("repos.JSON"))
            {
                _Repositories = JsonHelper<IList<RepositoryInfo>>.FromFile("repos.JSON");
            }
            
            //: File doesnt exist, or file was empty
            if(_Repositories == null)
            {
                _Repositories = new List<RepositoryInfo>();
            }
        }

        private void LoadRepositories()
        {
            cbProjects.Items.Clear();
            foreach(var repository in _Repositories)
            {
                cbProjects.Items.Add(repository);
            }
        }

        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
            var settings = new Settings(_Repositories);
            settings.ShowDialog();
            JsonHelper<IList<RepositoryInfo>>.SaveToFile(settings.Repositories, "repos.JSON");

            LoadRepositories();
            dgTestPlans.Items.Clear();
            dgTests.Items.Clear();
        }

        private void Sync_Click(object sender, MouseButtonEventArgs e)
        {
            if (cbProjects.SelectedItem == null)
            {
                MessageBox.Show("Please select a project to sync.");
                return;
            }

            lblSync.Visibility = Visibility.Visible;

            var gh = new Github((RepositoryInfo)cbProjects.SelectedItem);
            if (IOHelper.LocalDirectoryExists(string.Format("Projects\\{0}", ((RepositoryInfo)cbProjects.SelectedItem).Name)))
            {
                var credentials = GitHelper.GetGitCredentials();
                while (credentials == null || string.IsNullOrWhiteSpace(credentials.Password) || string.IsNullOrWhiteSpace(credentials.Username))
                {
                    credentials = GitHelper.GetGitCredentials();
                }

                gh.Pull(credentials);
                GitHelper.StageAll(gh);
                gh.Commit();
                var result = gh.Push(credentials);

                if(result != string.Empty)
                {
                    MessageBox.Show(result);
                }
            }
            else
            {
                IOHelper.CreateLocalDirectoryIfNotExists(string.Format("Projects\\{0}", ((RepositoryInfo)cbProjects.SelectedItem).Name));
                gh.CloneRepository();
            }

            LoadTests();
            lblSync.Visibility = Visibility.Hidden;
        }

        private void Saf_OnClick(object sender, RoutedEventArgs e)
        {
            var credentials = GitHelper.GetGitCredentials();

            var ri = new RepositoryInfo
            {
                Name = "ChuckTesta",
                HttpsLink = new System.Uri("test")
            };

            var gh = new Github(ri);

            //: gh->Add() 
            //: Status: Working!
            foreach (var file in gh.Status())
            {
                gh.Add(file.Key, file.Value);
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

        private void cbProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgTestPlans.Items.Clear();

            LoadTests();

            //: No point faking it if we can make it!
//            var list = new List<string>();
//            list.Add("test");
//            list.Add("chuck testa");


//            dgTests.Items.Add(new TestDetailsModel("Super Awesome Test 1","Test", @"
//    I.Open(""http://knockoutjs.com/examples/cartEditor.html"");
//    I.Select(""Motorcycles"").From("".liveExample tr select:eq(0)""); // Select by value/text
//    I.Select(2).From("".liveExample tr select:eq(1)""); // Select by index
//    I.Enter(6).In("".liveExample td.quantity input:eq(0)"");
//    I.Expect.Text(""$197.70"").In("".liveExample tr span:eq(1)"");
//
//    // add second product
//    I.Click("".liveExample button:eq(0)"");
//    I.Select(1).From("".liveExample tr select:eq(2)"");
//    I.Select(4).From("".liveExample tr select:eq(3)"");
//    I.Enter(8).In("".liveExample td.quantity input:eq(1)"");
//    I.Expect.Text(""$788.64"").In("".liveExample tr span:eq(3)"");
//
//    // validate totals
//    I.Expect.Text(""$986.34"").In(""p.grandTotal span"");
//
//    // remove first product
//    I.Click("".liveExample a:eq(0)"");
//
//    // validate new total
//    I.WaitUntil(() => I.Expect.Text(""$788.64"").In(""p.grandTotal span""));
//
//", list));
        }

        private void LoadTests()
        {
            dgTests.Items.Clear();


            if (cbProjects.SelectedItem != null)
            {
                var projectName = ((RepositoryInfo)cbProjects.SelectedItem).Name;
                var projDirectory =  string.Format("{0}\\Projects\\{1}", Directory.GetCurrentDirectory(), projectName);
                if (!IOHelper.LocalDirectoryExists(string.Format("Projects\\{0}", projectName)))
                {
                    MessageBox.Show("Hey buddy!\r\nI know we're rearin to get to work... But we need to sync first!\r\nIt's that nice button in the top right!", "Whoah!");
                    return;
                }

                foreach (var folder in Directory.GetDirectories(projDirectory).Where(t => t.Substring(0, 1) != "."))
                {
                    foreach (var file in Directory.GetFiles(folder))
                    {
                        if (file.Contains(".Chuck"))
                        {
                            var test = JsonHelper<TestDetailsModel>.FromFile(file);

                            dgTests.Items.Add(test);
                        }
                    }
                }
            }   
        }

        private void dgTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTests.SelectedItem == null) return; //: Misclick?

            var t = new TestDetails((TestDetailsModel)dgTests.SelectedItem, (RepositoryInfo)cbProjects.SelectedItem);
            t.ShowDialog();

            LoadTests();
        }
    }
}

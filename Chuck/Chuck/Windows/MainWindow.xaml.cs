using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chuck.Contexts;
using Chuck.Core.Git;
using Chuck.Core.Git.Github;
using Chuck.Helpers;
using Chuck.Models;
using LibGit2Sharp;

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    ///     Icons used on this page: https://www.iconfinder.com/iconsets/technology-and-hardware-2
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        ///     The projects(repos) this user has.
        /// </summary>
        private IList<RepositoryInfo> _Repositories;

        /// <summary>
        ///     Create a new instance of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadRepositoryJSON();
            LoadRepositories();
            DataContext = new MainWindowContext();
        } 

        /// <summary>
        ///     When creating a test we need a name for the test. After getting the name from GetInput window,
        ///     we show the TestDetails window with a blank TestDetails model, which will save the test if they 
        ///     so choose.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAddTest_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new GetInput("Test Name");
            inputDialog.ShowDialog();
            var input = inputDialog.Input;
            //:todo - add validation input was received

            var tdm = new TestDetailsModel(input, "", "", new List<string>());
            var t = new TestDetails(tdm, (RepositoryInfo)cbProjects.SelectedItem);
            t.ShowDialog();

            LoadTests();
        }

        /// <summary>
        ///     When cbProjects changes, the user just selected a different project(repo) to work with. 
        ///     Need to load associated tests/testplans.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTests();
            LoadTestPlans();
        }

        /// <summary>
        ///     When the user double clicks a test within the datagrid it's fairly obvious they want to edit it.
        ///     Bring up the edit test page, once completed reload tests for any changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTests.SelectedItem == null) return; //: Misclick?

            var t = new TestDetails((TestDetailsModel)dgTests.SelectedItem, (RepositoryInfo)cbProjects.SelectedItem);
            t.ShowDialog();

            LoadTests();
        }

        /// <summary>
        ///     A reload for cbProjects, which contains all the projects(repos) available to someone.
        /// </summary>
        private void LoadRepositories()
        {
            cbProjects.Items.Clear();
            foreach (var repository in _Repositories)
            {
                cbProjects.Items.Add(repository);
            }
        }

        /// <summary>
        ///     Loads the projects(repos) from file.
        /// </summary>
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

        /// <summary>
        ///     Unload testplans from the datagrid and repopulate based on what the local repo contains.
        /// </summary>
        private void LoadTestPlans()
        {
            //: Soon.... ™
        }

        /// <summary>
        ///     Unload tests from the datagrid and repopulate based on what the local repo contains.
        /// </summary>
        private void LoadTests()
        {
            dgTests.Items.Clear();

            if (cbProjects.SelectedItem != null)
            {
                var projectName = ((RepositoryInfo)cbProjects.SelectedItem).Name;
                var projDirectory = string.Format("{0}\\Projects\\{1}", Directory.GetCurrentDirectory(), projectName);
                if (!IOHelper.LocalDirectoryExists(string.Format("Projects\\{0}", projectName)))
                {
                    MessageBox.Show("Hey buddy!\r\nI know we're rearin to get to work... But we need to sync first!\r\nIt's that nice button in the top right!", "Whoah!");
                    return;
                }

                ((MainWindowContext)DataContext).Enabled = true;

                foreach (var folder in Directory.GetDirectories(projDirectory).Where(t => t.Substring(0, 1) != "."))
                {
                    foreach (var file in Directory.GetFiles(folder).Where(f => f.Contains(".Chuck")))
                    {
                        var test = JsonHelper<TestDetailsModel>.FromFile(file);
                        dgTests.Items.Add(test);
                    }
                }
            }
        }

        /// <summary>
        ///     On click for settings, we display a settings window. Once this window is gone, the user could have updated
        ///     the projects(repos) that they work on. We need to reload the cbProjects and remove items from the datagrids.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
            var settings = new Settings(_Repositories);
            settings.ShowDialog();
            JsonHelper<IList<RepositoryInfo>>.SaveToFile(settings.Repositories, "repos.JSON");

            LoadRepositories();
            dgTestPlans.Items.Clear();
            dgTests.Items.Clear();
        }

        /// <summary>
        ///     On sync we have 2 options.
        ///     A) The user doesn't even have a copy of the repo.
        ///         -- In which case we clone it for them.
        ///     B) The user has a local copy.
        ///         -- In which case we get latest changes, then push any changes the user has.
        /// 
        ///     Then a reload of the tests/testplans.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
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
                if (gh.Status().Count != 0)
                {
                    GitHelper.StageAll(gh);
                    if (gh.Status().Any(t => t.Value != FileStatus.Staged))
                        gh.Commit();
                    var result = gh.Push(credentials);

                    if (result != string.Empty)
                    {
                        MessageBox.Show(result);
                    }
                }
            }
            else
            {
                IOHelper.CreateLocalDirectoryIfNotExists(string.Format("Projects\\{0}", ((RepositoryInfo)cbProjects.SelectedItem).Name));
                gh.CloneRepository();
            }

            LoadTests();
            LoadTestPlans();
            lblSync.Visibility = Visibility.Hidden;
        }
    }
}

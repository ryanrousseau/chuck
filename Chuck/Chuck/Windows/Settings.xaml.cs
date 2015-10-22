using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Chuck.Core.Git;

namespace Chuck.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        /// <summary>
        ///     A list of the projects(repos) the user has.
        /// </summary>
        public IList<RepositoryInfo> Repositories { get; set; }

        /// <summary>
        ///     Create a new instance of Settings Window
        /// </summary>
        /// <param name="repositories">A list of the projects(repos) the user has.</param>
        public Settings(IList<RepositoryInfo> repositories)
        {
            InitializeComponent();
            Repositories = repositories;
            LoadRepositories();
        }

        /// <summary>
        ///     Add a project(repo) for the user and stop showing the add section.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAddRepo_Click(object sender, RoutedEventArgs e)
        {
            //: Todo - better validation [e.g., special character removal, repo exists, etc]
            Uri validUri;
            if(!Uri.TryCreate(txtRepoLink.Text, UriKind.Absolute, out validUri))
            {
                txtRepoLink.Background = new SolidColorBrush(Color.FromArgb(120, 250, 10, 19));
                return;
            }

            Repositories.Add(new RepositoryInfo()
            {
                HttpsLink = new Uri(txtRepoLink.Text),
                Name = txtRepoName.Text
            });

           // _Settings.Add(new SettingsContext(Repositories));
            LoadRepositories();
            Height = 167;
        }

        /// <summary>
        ///     Show the add section so the user can Add a repo.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnShowAdd_Click(object sender, RoutedEventArgs e)
        {
            Height = 364;
        }

        /// <summary>
        ///     Remove a project(Repo) for the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRemoveRepo_Click(object sender, RoutedEventArgs e)
        {
            if (cbRepos.SelectedItem == null) return;

            if (MessageBox.Show(string.Format("Really delete this repo?\r\n    {0}", cbRepos.SelectedItem), "Confirmation for delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Repositories.Remove((RepositoryInfo)cbRepos.SelectedItem);
                LoadRepositories();
            }
        }

        /// <summary>
        ///     Load the combobox with projects(repos) the user has.
        /// </summary>
        private void LoadRepositories()
        {
            cbRepos.Items.Clear();

            foreach(var repo in Repositories)
            {
                cbRepos.Items.Add(repo);
            }
        }
    }
}

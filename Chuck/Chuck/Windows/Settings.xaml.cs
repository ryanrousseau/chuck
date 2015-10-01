using Chuck.Contexts;
using Chuck.Core.Git;
using Chuck.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public IList<RepositoryInfo> Repositories { get; set; }

        public Settings(IList<RepositoryInfo> repositories)
        {
            InitializeComponent();
            Repositories = repositories;
            LoadRepositories();
        }

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

        private void LoadRepositories()
        {
            cbRepos.Items.Clear();

            foreach(var repo in Repositories)
            {
                cbRepos.Items.Add(repo);
            }
        }

        private void btnShowAdd_Click(object sender, RoutedEventArgs e)
        {
            Height = 364;
        }

        private void btnRemoveRepo_Click(object sender, RoutedEventArgs e)
        {
            if(cbRepos.SelectedItem == null) return;

            if(MessageBox.Show(string.Format("Really delete this repo?\r\n    {0}", cbRepos.SelectedItem), "Confirmation for delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Repositories.Remove((RepositoryInfo)cbRepos.SelectedItem);
                LoadRepositories();
            }
        }
    }
}

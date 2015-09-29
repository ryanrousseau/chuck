namespace Chuck.Contexts
{
    /// <summary>
    ///     DataContext for MainWindow.xaml
    /// </summary>
    public class MainWindowContext
    {
        public string Flair { get; set; }

        public MainWindowContext()
        {
            Flair = "Chuck Testa";
        }
    }
}

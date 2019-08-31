namespace ZbW.Testing.Dms.Client.Views
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    using ZbW.Testing.Dms.Client.ViewModels;

    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainView : Window
    {
        public MainView(string benutzername)
        {
            InitializeComponent();
            DataContext = new MainViewModel(benutzername);
        }
    }
}
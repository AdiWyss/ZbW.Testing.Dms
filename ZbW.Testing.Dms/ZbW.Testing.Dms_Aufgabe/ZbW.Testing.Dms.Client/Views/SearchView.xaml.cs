namespace ZbW.Testing.Dms.Client.Views
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;

    using ZbW.Testing.Dms.Client.ViewModels;

    /// <summary>
    /// Interaction logic for SearchView.xaml
    [ExcludeFromCodeCoverage]
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();
        }
    }
}
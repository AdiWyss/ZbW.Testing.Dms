namespace ZbW.Testing.Dms.Client.Views
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;

    using ZbW.Testing.Dms.Client.ViewModels;

    /// <summary>
    /// Interaction logic for NewDocumentView.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class DocumentDetailView : UserControl
    {
        public DocumentDetailView(string benutzer, Action navigateBack)
        {
            InitializeComponent();
            DataContext = new DocumentDetailViewModel(benutzer, navigateBack);
        }
    }
}
namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System.Collections.Generic;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Windows;
    using ZbW.Testing.Dms.Client.Model;
    using ZbW.Testing.Dms.Client.Repositories;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SearchViewModel : BindableBase
    {
        //Declaration
        private IEnumerable<MetadataItem> _filteredMetadataItems;
        private MetadataItem _selectedMetadataItem;
        private string _repositoryPath;
        private string _selectedTypItem;
        private string _suchbegriff = "";
        private List<string> _typItems;
        private FileHandler file;

        //Properties
        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;

            RepositoryPath = ConfigurationManager.AppSettings["RepositoryDir"];

            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
        }
        public DelegateCommand CmdOeffnen { get; } 
        public DelegateCommand CmdSuchen { get; }
        public DelegateCommand CmdReset { get; }
        public string Suchbegriff
        {
            get
            {
                return _suchbegriff;
            }

            set
            {
                SetProperty(ref _suchbegriff, value);
            }
        }
        public List<string> TypItems
        {
            get
            {
                return _typItems;
            }

            set
            {
                SetProperty(ref _typItems, value);
            }
        }
        public string SelectedTypItem
        {
            get
            {
                return _selectedTypItem;
            }

            set
            {
                SetProperty(ref _selectedTypItem, value);
            }
        }
        public IEnumerable<MetadataItem> FilteredMetadataItems
        {
            get
            {
                return _filteredMetadataItems;
            }

            set
            {
                SetProperty(ref _filteredMetadataItems, value);
            }
        }
        public MetadataItem SelectedMetadataItem
        {
            get
            {
                return _selectedMetadataItem;
            }

            set
            {
                if (SetProperty(ref _selectedMetadataItem, value))
                {
                    CmdOeffnen.RaiseCanExecuteChanged();
                }
            }
        }
        public string RepositoryPath
        {
            get
            {
                return _repositoryPath;
            }

            set
            {
                SetProperty(ref _repositoryPath, value);
            }
        }
        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }
        private void OnCmdOeffnen()
        {
            if (SelectedMetadataItem != null){
                OpenDocument();
            }
        }
        private void OnCmdSuchen()
        {
            GuidHandler g = new GuidHandler();
            RepositoryPath = ConfigurationManager.AppSettings["RepositoryDir"];
            file = new FileHandler(RepositoryPath, g);

            FilteredMetadataItems = file.GetDocuments(_selectedTypItem, _suchbegriff);
        }
        private void OnCmdReset()
        {
            resetFoundDocuments();
        }

        private void resetFoundDocuments()
        {
            Suchbegriff = "";
            SelectedTypItem = null;
            FilteredMetadataItems = null;
        }

        private void OpenDocument()
        {
            System.Diagnostics.Process.Start(SelectedMetadataItem.FullFilePath);
        }
    }
}
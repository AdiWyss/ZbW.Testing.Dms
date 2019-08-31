namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Xml;
    using System.Configuration;
    using Microsoft.Win32;
    using Prism.Commands;
    using Prism.Mvvm;
    using ZbW.Testing.Dms.Client.Repositories;
    using ZbW.Testing.Dms.Client.Model;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DocumentDetailViewModel : BindableBase
    {
        //Declaration
        private readonly Action _navigateBack;
        private string _benutzer;
        private string _bezeichnung;
        private DateTime _erfassungsdatum;
        private string _filePath;
        private string _fileName;
        private string _fileGUID;
        private string _fullPathDestination;
        private string _fullFilePathDestination;
        private string _repositoryPath;
        private bool _isRemoveFileEnabled;
        private string _selectedTypItem;
        private string _stichwoerter;
        private List<string> _typItems;
        private DateTime? _valutaDatum;
        FileHandler file;
        XmlHandler xml;

        //Properties
        public string Stichwoerter
        {
            get
            {
                return _stichwoerter;
            }

            set
            {
                SetProperty(ref _stichwoerter, value);
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
        public string Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }

            set
            {
                SetProperty(ref _bezeichnung, value);
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                SetProperty(ref _fileName, value);
            }
        }
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                SetProperty(ref _filePath, value);
            }
        }
        public string FileGUID
        {
            get
            {
                return _fileGUID;
            }

            set
            {
                SetProperty(ref _fileGUID, value);
            }
        }
        public string FullPathDestination
        {
            get
            {
                return _fullPathDestination;
            }

            set
            {
                SetProperty(ref _fullPathDestination, value);
            }
        }
        public string FullFilePathDestination
        {
            get
            {
                return _fullFilePathDestination;
            }

            set
            {
                SetProperty(ref _fullFilePathDestination, value);
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
        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetProperty(ref _erfassungsdatum, value);
            }
        }
        public string Benutzer
        {
            get
            {
                return _benutzer;
            }

            set
            {
                SetProperty(ref _benutzer, value);
            }
        }
        public DelegateCommand CmdDurchsuchen { get; }
        public DelegateCommand CmdSpeichern { get; }
        public DateTime? ValutaDatum
        {
            get
            {
                return _valutaDatum;
            }

            set
            {
                SetProperty(ref _valutaDatum, value);
            }
        }
        public bool IsRemoveFileEnabled
        {
            get
            {
                return _isRemoveFileEnabled;
            }

            set
            {
                SetProperty(ref _isRemoveFileEnabled, value);
            }
        }

        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;

            Benutzer = benutzer;
            TypItems = ComboBoxItems.Typ;

            RepositoryPath = ConfigurationManager.AppSettings["RepositoryDir"];

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
        }

        private void OnCmdDurchsuchen()
        {
            GuidHandler g = new GuidHandler();
            file = new FileHandler(RepositoryPath, g);
            
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                FilePath = openFileDialog.FileName;
                FileName = openFileDialog.SafeFileName;
                Bezeichnung = Path.GetFileNameWithoutExtension(FileName);
            }
        }

        private void OnCmdSpeichern()
        {
            if (Bezeichnung == null || ValutaDatum == null || SelectedTypItem == null || FilePath == null)
            {
                MessageBox.Show("Es müssen alle Pflichtfelder ausgefüllt werden");
            } else
            {
                //Fileoptions
                FileGUID = file.CreateGUID();
                FullPathDestination = file.GetDestinationPath(ValutaDatum);
                file.CreateRepository(RepositoryPath);
                file.CreateDir(FullPathDestination);
                FullFilePathDestination = file.GetFileDestinationPath(FullPathDestination, FileGUID, FileName);

                //XMLoptions
                xml = new XmlHandler(FullPathDestination);
                xml.GenerateXmlPath(FullPathDestination, FileGUID);
                xml.CreateXML(FileName, Bezeichnung, ValutaDatum, SelectedTypItem, Stichwoerter, Benutzer);

                file.MoveFile(FilePath, FullFilePathDestination, xml.XmlFilePath, IsRemoveFileEnabled);
                ResetFields();
            }           
        }

        private void ResetFields()
        {
            Bezeichnung = null;
            Erfassungsdatum = DateTime.Now;
            FilePath = null;
            FileName = null;
            FileGUID = null;
            FullPathDestination = null;
            SelectedTypItem = null;
            Stichwoerter = null;
            ValutaDatum = null;
            IsRemoveFileEnabled = false;
        }
    }
}
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

    internal class DocumentDetailViewModel : BindableBase
    {
        private readonly Action _navigateBack;

        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _filePath;

        private string _fileName;

        private string _fileGUID;

        private string _fullPathDestination;

        private string _repositoryPath;

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime? _valutaDatum;

        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            TypItems = ComboBoxItems.Typ;

            //Filestream for XML
            _repositoryPath = ConfigurationManager.AppSettings["RepositoryDir"];
            CreateDir(_repositoryPath);

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
        }

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

        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                _filePath = openFileDialog.FileName;
                _fileName = openFileDialog.SafeFileName;
                Bezeichnung = Path.GetFileNameWithoutExtension(_fileName);
            }
        }

        private void OnCmdSpeichern()
        {
            if (Bezeichnung == null || ValutaDatum == null || SelectedTypItem == null || _filePath == null)
            {
                MessageBox.Show("Es müssen alle Pflichtfelder ausgefüllt werden");
            } else
            {
                _fileGUID = CreateGUID();
                _fullPathDestination = _repositoryPath + "\\" + _valutaDatum.Value.Year.ToString();
                CreateDir(_fullPathDestination);
                CreateXML(_fileGUID);
                MoveFile(_filePath);
                //_bezeichnung = Bezeichnung;
                //_valutaDatum = ValutaDatum;
                //_selectedTypItem = SelectedTypItem;
                //_stichwoerter = Stichwoerter;
                //_erfassungsdatum = Erfassungsdatum;
                //_benutzer = Benutzer;
                //_isRemoveFileEnabled = IsRemoveFileEnabled;
                _navigateBack();
            }   
            
        }

        private void CreateDir(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

        }

        private string CreateGUID()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }

        private void CreateXML(String _fileGUID)
        {
            String _XMLfileName = _fileGUID + "_Metadata.xml";
            String _XMLfullFilePath = _fullPathDestination + "\\" + _XMLfileName;

            XmlWriter xml = XmlWriter.Create(_XMLfullFilePath);

            xml.WriteStartElement("Document");
            xml.WriteElementString("Bezeichnung", Bezeichnung);
            xml.WriteElementString("ValutaDatum", ValutaDatum.ToString());
            xml.WriteElementString("Typ", SelectedTypItem);
            xml.WriteElementString("Stichwoerter", Stichwoerter);
            xml.WriteElementString("ErfassungsDatum", Erfassungsdatum.ToString());
            xml.WriteElementString("Benutzer", Benutzer);
            xml.WriteEndElement();
            xml.Flush();
       
            
        }

        private void MoveFile(String file)
        {
            String _DestinationFileName = _fileGUID + "_Content" + Path.GetExtension(_fileName);
            String _DestinationFullFilePath = _fullPathDestination + "\\" + _DestinationFileName;

            if (_isRemoveFileEnabled)
            {
                System.IO.File.Move(file, _DestinationFullFilePath);
            } else
            {
                System.IO.File.Copy(file, _DestinationFullFilePath);
            }
            
        }

    }
}
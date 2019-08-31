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
    using System.Data;
    using System.Xml.Linq;
    using System;
    using System.ComponentModel;
    using System.Windows.Controls;

    internal class SearchViewModel : BindableBase
    {
        private BindingList<MetadataItem> _filteredMetadataItems;

        private List<MetadataItem> _allMetadataItems;

        private MetadataItem _selectedMetadataItem;

        private string _bezeichnung;

        private DateTime _valutaDatum;

        private string _typ;

        private string _stichwoerter;

        private DateTime _erfassungsDatum;

        private string _benutzer;

        private string _repositoryPath;

        private string _selectedTypItem;

        private string _suchbegriff = "";

        private List<string> _typItems;

        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;

            
            _repositoryPath = ConfigurationManager.AppSettings["RepositoryDir"];

            //_filteredMetadataItems.Add(new MetadataItem("test", new DateTime(2008, 04, 14), "Verträge" , "" , new DateTime(2008, 04, 14), "Adi"));

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

        public BindingList<MetadataItem> FilteredMetadataItems
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

        public List<MetadataItem> AllMetadataItems
        {
            get
            {
                return _allMetadataItems;
            }

            set
            {
                SetProperty(ref _allMetadataItems, value);
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


        public string Typ
        {
            get
            {
                return _typ;
            }

            set
            {
                SetProperty(ref _typ, value);
            }
        }


        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsDatum;
            }

            set
            {
                SetProperty(ref _erfassungsDatum, value);
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

      
        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            // TODO: Add your Code here
        }

        private void OnCmdSuchen()
        {
            if (SelectedTypItem == null)
            {
                MessageBox.Show("Bitte Dokumententyp angeben");
            }
            else
            {
                _filteredMetadataItems = new BindingList<MetadataItem>();
                foreach (string directories in Directory.GetDirectories(_repositoryPath)) { 
                    foreach (string file in Directory.GetFiles(directories, "*.xml"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(file);

                        XmlNodeList elemList = doc.GetElementsByTagName("Typ");
                        _typ = elemList[0].InnerXml;
                        elemList = doc.GetElementsByTagName("Stichwoerter");
                        _stichwoerter = elemList[0].InnerXml;

                        if (_typ == _selectedTypItem && _stichwoerter.Contains(_suchbegriff))
                        {
                            
                            elemList = doc.GetElementsByTagName("Bezeichnung");
                            _bezeichnung = elemList[0].InnerXml;
                            elemList = doc.GetElementsByTagName("ValutaDatum");
                            _valutaDatum = Convert.ToDateTime(elemList[0].InnerXml);
                            elemList = doc.GetElementsByTagName("ErfassungsDatum");
                            _erfassungsDatum = Convert.ToDateTime(elemList[0].InnerXml);
                            elemList = doc.GetElementsByTagName("Benutzer");
                            _benutzer = elemList[0].InnerXml;
                           
                            _filteredMetadataItems.Add(new MetadataItem(_bezeichnung, _valutaDatum, _typ, _stichwoerter, _erfassungsDatum, _benutzer));
                        }

                    }
                }
                
               this.MyDataGrid.
            }
        }

        private void OnCmdReset()
        {
            // TODO: Add your Code here
        }
    }
}
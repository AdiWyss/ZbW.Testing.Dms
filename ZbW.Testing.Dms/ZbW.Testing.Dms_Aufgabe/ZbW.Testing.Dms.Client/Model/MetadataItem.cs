using System;
using System.Diagnostics.CodeAnalysis;
using Prism.Mvvm;

namespace ZbW.Testing.Dms.Client.Model
{
    [ExcludeFromCodeCoverage]
    public class MetadataItem : BindableBase
    {
        
        //Declaration
        public string _bezeichnung;
        public string _fullFilePath; 
        public string _typ;
        public string _stichwoerter;
        public string _benutzer;
        public string _fileName;
        public DateTime _erfassungsdatum;
        public DateTime? _valutaDatum;

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
        public string FullFilePath
        {
            get
            {
                return _fullFilePath;
            }

            set
            {
                SetProperty(ref _fullFilePath, value);
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

        public MetadataItem(String bezeichnung, DateTime valutaDatum, String typ, String stichwoerter, DateTime erfassungsdatum, String benutzer, String fullFilePath, String fileName)
        {
            Bezeichnung = bezeichnung;
            ValutaDatum = valutaDatum;
            Typ = typ;
            Stichwoerter = stichwoerter;
            Erfassungsdatum = erfassungsdatum;
            Benutzer = benutzer;
            FullFilePath = fullFilePath;
            FileName = fileName;
        }
    }
}